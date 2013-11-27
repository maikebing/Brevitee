using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = Microsoft.Build.Framework;
using B = Brevitee.Logging;
using System.IO;

namespace Brevitee.Automation.Build
{
    public class CsvBuildLogger: BuildLogger<B.CsvLogger>
    {
        B.CsvLogger _actualLogger;
        public CsvBuildLogger(string folderPath)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("Folder", new DirectoryInfo(folderPath));
            SetLoggerProperties(properties);
        }
    }
}
