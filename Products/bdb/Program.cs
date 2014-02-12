using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Brevitee;
using Brevitee.CommandLine;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Configuration;
using System.IO;
using Brevitee.Yaml;
using Brevitee.Testing;
using System.Reflection;

namespace Brevitee.Server
{
    class Program : ServiceExe
    {
        static void Main(string[] args)
        {
            SetInfo(new ServiceInfo("BreviteeDaoServer", "Brevitee Dao Server", "Brevitee Data Access Object Server"));

            if (!ProcessCommandLineArgs(args))
            {
                RunService<Program>();
            }
        }

        protected override void OnStart(string[] args)
        {
            Server.Start();
        }

        protected override void OnStop()
        {
            Server.Stop();
            Thread.Sleep(1000);
        }

        static BreviteeServer _server;
        static object _serverLock = new object();
        public static BreviteeServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () => new BreviteeDaoServer(BreviteeConf.Load()));
            }
        }


    }
}
