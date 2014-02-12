using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Logging
{
    /// <summary>
    /// The same values as LogEventType.
    /// Both exist for clarity in specific
    /// contexts.
    /// </summary>
    public enum VerbosityLevel : int
    {
        None = 0,
        Fatal = 1,
        Error = 2,
        Warning = 3,
        Information = 4,
        Custom = 5
    }
}
