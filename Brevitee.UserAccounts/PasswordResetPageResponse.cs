using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.UserAccounts
{
    public class PasswordResetPageResponse: RequestResponse
    {
        public PasswordResetPageResponse() { }

        public string Token { get; set; }
        public string Layout { get; set; }
    }
}
