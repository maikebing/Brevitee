using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public class AppConfApplicationNameProvider: IApplicationNameProvider
    {
        public AppConfApplicationNameProvider(AppConf conf)
        {
            this.Conf = conf;
        }

        protected internal AppConf Conf
        {
            get;
            set;
        }
        public string GetApplicationName()
        {
            return Conf.Name;
        }
    }
}
