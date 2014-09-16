using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy.Secure
{
    public class ApiKeyNotFoundException: Exception
    {
        public ApiKeyNotFoundException(string clientId)
            : base("The key for the specified clientId ({0}) was not found"._Format(clientId))
        { }
    }
}
