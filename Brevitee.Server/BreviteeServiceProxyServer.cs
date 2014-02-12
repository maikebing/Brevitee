using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server
{
    /// <summary>
    /// BreviteeServer where EnableDao is false
    /// and EnableServiceProxy is true
    /// </summary>
    public class BreviteeServiceProxyServer: BreviteeServer
    {
        public BreviteeServiceProxyServer()
            : base()
        {
            this.EnableDao = false;
            this.EnableServiceProxy = true;
        }

        public BreviteeServiceProxyServer(BreviteeConf conf)
            : base(conf)
        {
            this.EnableDao = false;
            this.EnableServiceProxy = true;
        }
    }
}
