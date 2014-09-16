using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy.Secure
{
    public class ServiceProxyInvocationFailedException: Exception 
    {
        public ServiceProxyInvocationFailedException(string message) : base(message) { }
    }
}
