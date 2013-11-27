using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.CommandLine;

namespace Brevitee.Automation.Build
{
    public class ConsoleBuildLogger: BuildLogger<ConsoleLogger>
    {
        public ConsoleBuildLogger()
        {
            Dictionary<string, object> props = new Dictionary<string, object>();
            props.Add("AddDetails", false);
            props.Add("UseColors", true);
            SetLoggerProperties(props);
        }
    }
}
