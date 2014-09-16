using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Brevitee.UserAccounts.Data;
using System.Web.Security;

namespace Brevitee.UserAccounts
{
    public class DaoPrincipal: IPrincipal
    {
        public DaoPrincipal(User user, bool isAuthenticated)
        {
            this.Identity = new DaoIdentity(user, isAuthenticated);
            this.RoleProvider = DaoRoleProvider.Default;
        }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public RoleProvider RoleProvider
        {
            get;
            set;
        }

        public bool IsInRole(string role)
        {
            return RoleProvider.IsUserInRole(Identity.Name, role);
        }
    }
}
