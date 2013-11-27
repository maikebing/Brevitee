using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.CommandLine
{
    public interface IPreAndPostInvoke
    {
        string PreInvokeMethodName { get; set; }
        string PostInvokeMethodName { get; set; }
        string AlwaysPostInvokeMethodName { get; set; }
    }
}
