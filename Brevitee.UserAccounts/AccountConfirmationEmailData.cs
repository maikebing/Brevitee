using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.UserAccounts.Data;
using Brevitee.ServiceProxy;

namespace Brevitee.UserAccounts
{
    public class AccountConfirmationEmailData
    {
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string ConfirmationUrl { get; set; }

    }
}
