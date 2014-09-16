using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.CommandLine
{
    public interface IPreAndPostInvoke
    {
        string Before { get; set; }
        string AfterSuccess { get; set; }
        string AlwaysAfter { get; set; }
    }
}
