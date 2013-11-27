using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Server
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleRequired: Attribute
    {
        public RoleRequired() { }

        public RoleRequired(string roleName)
        {
            this.RoleName = roleName;
        }

        public string RoleName
        {
            get;
            set;
        }
    }
}
