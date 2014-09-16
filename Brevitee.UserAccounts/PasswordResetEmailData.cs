using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.UserAccounts
{
    public class PasswordResetEmailData
    {
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string PasswordResetUrl { get; set; }
    }
}
