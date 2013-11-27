using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Logging
{
    public delegate void LogEntryAddedListener(string applicationName, LogEvent logEvent);   
}
