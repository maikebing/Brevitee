using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.UserAccounts
{
    public abstract class RequestEmailResponse:RequestResponse
    {
        public bool EmailSent { get; set; }
    }
}
