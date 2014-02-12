using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brevitee;
using Brevitee.Data;
using Brevitee.Dust;
using Brevitee.Logging;
using Brevitee.ServiceProxy;
using Brevitee.Data.Schema;
using LD = Brevitee.Logging.Data;
using Brevitee.Analytics.Crawlers;
using Brevitee.Profiguration;
using Brevitee.Configuration;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof($rootnamespace$.Brevitee), "BeAwesome")]

namespace $rootnamespace$ {
    public static class Brevitee {
        public static void BeAwesome() {
            Profiguration.Initialize();
            SQLiteRegistrar.Register<LD.LogEvent>();
            Register(DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN"));
            Log.Start();
            Dust.Initialize();
            ServiceProxySystem.Register<Echo>();

            ServiceProxySystem.RegisterBinProviders();
        }
		
		public static void Register(string connectionName)
        {
            SQLiteRegistrar.Register(connectionName);
            DaoProxyRegistration.RegisterConnection(connectionName);
            _.TryEnsureSchema(connectionName);
        }
    }
}