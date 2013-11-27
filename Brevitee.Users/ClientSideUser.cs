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
    [Proxy("user", MethodCase = MethodCase.Both)]
    public class ClientSideUser
    {
        public dynamic Get()
        {
            User user = User.GetCurrent();            
            bool isAuthenticated = false;
            if (user == User.Anonymous)
            {
                string webUser = UserUtil.GetCurrentWebUserName();
                user = User.GetByUserName(webUser);
                if (user == null)
                {
                    user = User.GetByEmail(webUser);
                }

                if (user == null)
                {
                    user = User.Anonymous;
                }
            }

            if (user != User.Anonymous)
            {
                isAuthenticated = true;
            }

            int loginCount = isAuthenticated ? user.LoginsByUserId.Count: 0;

            dynamic result = new { 
                userName = user.UserName, 
                id = user.Id, 
                isAuthenticated = isAuthenticated,
                loginCount = loginCount
            };

            return result;
        }   

        public bool IsInRole(string roleName)
        {
            User user = User.GetCurrent();
            return Roles.IsUserInRole(user.UserName, roleName);
        }
    }
}
