using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;
//using Brevitee.Data;

namespace SampleData
{
    public partial class Role
    {
        public User[] Members
        {
            get
            {
                List<long> ids = this.UserRoleCollectionByRoleId.Select<UserRole, long>(xref => xref.UserId.GetValueOrDefault()).ToList();

                return User.Where(c => c.Id.In(ids.ToArray())).ToArray();
            }
        }

        public void AddMember(User user)
        {
            UserRole xref = this.UserRoleCollectionByRoleId.AddNew();
            xref.UserId = user.Id;
            xref.Save();
        }

        public static RoleCollection GetRoles(string[] roleNames)
        {
            QuerySet query = new QuerySet();
            foreach (string role in roleNames)
            {
                query.SelectTop<Role>(1).Where<RoleColumns>(c => c.Name == role);
            }

            query.Execute(_.Db.For<Role>());

            RoleCollection roles = new RoleCollection();
            for (int i = 0; i < roleNames.Length; i++)
            {
                Role role = query.Results[i].As<RoleCollection>().FirstOrDefault();
                if (role == null)
                {
                    role = GetRole(roleNames[i], true);
                }

                roles.Add(role);
            }

            return roles;
        }

        /// <summary>
        /// Gets a Role instance representing the specified roleName, if
        /// it doesn't already exist one will be instantiated and optionally
        /// saved if 'save' parameter is true.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public static Role GetRole(string roleName, bool save = false)
        {
            Role role = Role.OneWhere(c => c.Name == roleName);
            if (role == null)
            {
                role = new Role();
                role.Name = roleName;
                if (save)
                {
                    role.Save();
                }
            }
            return role;
        }
    }
}
