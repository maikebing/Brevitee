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
    class Kisses : ServiceExe
    {
        static void Main(string[] args)
        {
            SetInfo(new ServiceInfo("Kisses", "Kisses", "Brevitee Application Continuous Integration (baci => Italian for 'kisses')"));

            if (!ProcessCommandLineArgs(args))
            {
                RunService<Kisses>();
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

        static BreviteeServiceProxyServer _server;
        static object _serverLock = new object();
        public static BreviteeServiceProxyServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () => new BreviteeServiceProxyServer(BreviteeConf.Load()));
            }
        }

    }
}
