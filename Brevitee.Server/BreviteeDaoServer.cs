using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server
{
    /// <summary>
    /// A specialized server that serves
    /// content and responds to dao requests
    /// </summary>
    public class BreviteeDaoServer: BreviteeServer
    {
        public BreviteeDaoServer(BreviteeConf conf)
            : base(conf)
        {
            this.EnableDao = true;
            this.EnableServiceProxy = false;
        }
    }
}
