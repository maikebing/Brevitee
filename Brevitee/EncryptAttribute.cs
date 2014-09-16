using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee
{
    /// <summary>
    /// Denotes a class that requires clients use
    /// encrypted channels for method invokation calls
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EncryptAttribute: Attribute
    {
    }
}
