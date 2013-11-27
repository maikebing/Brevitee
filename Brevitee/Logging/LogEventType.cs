using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Logging
{
    public enum LogEventType : int
    {
        None = 0,
        Fatal = 1,
        Error = 2,
        Warning = 3,
        Information = 4,
        Custom = 5
    }
}
