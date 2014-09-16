using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Configuration;

namespace Brevitee.ServiceProxy
{
    public class DefaultConfigurationApplicationNameProvider: IApplicationNameProvider
    {
        static IApplicationNameProvider _instance;
        static object _lock = new object();
        public static IApplicationNameProvider Instance
        {
            get
            {
                return _lock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationApplicationNameProvider());
            }
        }
        public string GetApplicationName()
        {
            return DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
        }
    }
}
