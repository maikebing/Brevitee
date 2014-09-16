using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy.Secure
{
    /// <summary>
    /// Attribute used to addorn classes or methods that require
    /// authentication or authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyRequiredAttribute: EncryptAttribute
    {
    }
}
