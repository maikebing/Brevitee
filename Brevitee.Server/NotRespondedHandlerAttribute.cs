using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NotRespondedHandlerAttribute: Attribute
    {
    }
}
