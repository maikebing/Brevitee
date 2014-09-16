using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy.Secure
{
    public class ApplicationCreateResult
    {
        public ApplicationCreateStatus Status
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public Application Application
        {
            get;
            set;
        }
    }
}
