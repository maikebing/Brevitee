using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;
using Brevitee.Configuration;
using Brevitee.Logging;
using Brevitee.Users.Data;
using Brevitee;
using Brevitee.Data;

namespace Brevitee.Users
{
    [Proxy("roles")]
    [RoleRequired("Admin")]
    public class EasyRoleProvider : RoleProvider
    {
        /// <summary>
        /// Initialize roles from the config file.  This will look for the 
        /// appSetting with the key "Roles" and assume that it is a
        /// semi-colon (;) separated list of key value pairs delimited by colons (:)
        /// where the key is the name of a role to initialize and the 
        /// value is a comma separated list of users to create and add to 
        /// the role.
        /// </summary>
        public static void InitializeFromConfig()
        {
            string[] roleNameToUserNames = DefaultConfiguration.GetAppSetting("Roles", "").DelimitSplit(";", true);
            EasyRoleProvider provider = new EasyRoleProvider();
            roleNameToUserNames.Each(keyValues =>
            {
                string[] split = keyValues.DelimitSplit(":", true);
                Expect.IsTrue(split.Length <= 2, "Unrecognized Role config value.");
                string role = split[0];
                string[] userNames = split.Length == 2 ? split[1].DelimitSplit(",", true) : new string[] { };
                provider.CreateRole(role);

                if (userNames.Length > 0)
                {
                    List<string> usersToAddToRole = new List<string>();
                    userNames.Each(userName =>
                    {
                        if (User.Exists(userName))
                        {
                            usersToAddToRole.Add(userName);
                        }
                    });
                    if (usersToAddToRole.Count > 0)
                    {
                        provider.AddUsersToRoles(usersToAddToRole.ToArray(), new string[] { role });
                    }
                }
            });
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            UserCollection users = User.Where(c => c.UserName.In(usernames));
            RoleCollection roles = Role.Where(c => c.Name.In(roleNames));

            SqlStringBuilder sql = _.Db.For<User>().ServiceProvider.Get<SqlStringBuilder>();
            for (int i = 0; i < users.Count; i++)
            {
                User currentUser = users[i];
                for (int ii = 0; ii < roles.Count; ii++)
                {
                    Role currentRole = roles[ii];
                    currentUser.Roles.Add(currentRole);
                }
                currentUser.Roles.WriteCommit(sql);
            }

            sql.Execute(_.Db.For<User>());
        }

        public override string ApplicationName
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ApplicationName", "UNKOWN");
            }
            set
            {
                // must be in the config file
                Log.AddEntry("An attempt was made to set the ApplicationName property of {0}", this.GetType().Name);
            }
        }

        public override void CreateRole(string roleName)
        {
            Role role = Role.OneWhere(c => c.Name == roleName);
            if (role == null)
            {
                role = new Role();
                role.Name = roleName;
                role.Save();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            Role role = Role.OneWhere(c => c.Name == roleName);
            bool result = false;
            if (role != null)
            {
                if (throwOnPopulatedRole && role.Users.Count > 0)
                {
                    throw new InvalidOperationException("({0}) Role is populated:\r\n{1}"._Format(roleName, role.Users.ToArray().ToDelimited(u => u.UserName, ", ")));
                }
                else
                {
                    Database db = _.Db.For<Role>();
                    SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
                   
                    // deleting the role directly will cause the framework to attempt
                    // to delete the users as well since the relationship is an Xref.
                    // Doing it this way will prevent the deletion of the users.
                    UserRoleCollection xrefs = UserRole.Where(c => c.RoleId == role.Id);
                    xrefs.WriteDelete(sql);
                    role.WriteDelete(sql);
                    sql.Execute(db);
                    result = true;
                }
            }
            return result;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            Role role = Role.OneWhere(c => c.Name == roleName);
            string[] results = new string[] { };
            if (role != null)
            {
                results = role.Users
                    .Where(u => u.UserName.ToLowerInvariant().Equals(usernameToMatch.ToLowerInvariant()))
                    .Select(u => u.UserName).ToArray();
            }

            return results;
        }

        public override string[] GetAllRoles()
        {
            return Role.Where(c => c.Id != null).Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            User user = User.GetByUserName(username);
            string[] results = new string[] { };
            if (user != null)
            {
                results = user.Roles.Select(r => r.Name).ToArray();
            }

            return results;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            Role role = Role.OneWhere(c => c.Name == roleName);
            string[] results = new string[] { };
            if (role != null)
            {
                results = role.Users.Select(u => u.UserName).ToArray();
            }

            return results;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            User user = User.GetByUserName(username);
            bool result = false;
            if (user != null)
            {
                Role role = Role.OneWhere(c => c.Name == roleName);
                if (role != null)
                {
                    UserRole xref = UserRole.OneWhere(c => c.UserId == user.Id && c.RoleId == role.Id);
                    result = xref != null;
                }
            }

            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            RoleCollection roles = Role.Where(c => c.Name.In(roleNames));
            UserCollection users = User.Where(c => c.UserName.In(usernames));

            long[] roleIds = roles.Select(r => r.Id.Value).ToArray();
            long[] userIds = users.Select(u => u.Id.Value).ToArray();
            UserRoleCollection xrefs = UserRole.Where(c => c.RoleId.In(roleIds) && c.UserId.In(userIds));
            xrefs.Delete();            
        }

        public override bool RoleExists(string roleName)
        {
            Role role = Role.OneWhere(c => c.Name == roleName);
            return role != null;
        }
    }
}
