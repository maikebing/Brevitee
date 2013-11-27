using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KLGates.Core;
using KLGates.Core.Data;
using KLGates.Core.Dust;
using KLGates.Core.Logging;
using KLGates.Core.ServiceProxy;
using KLGates.Core.Data.Schema;
using LD = KLGates.Core.Logging.Data;
using KLGates.Core.Analytics.Crawlers;
using KLGates.Core.Profiguration;
using KLGates.Core.Configuration;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof($rootnamespace$.KLGates), "BeAwesome")]

namespace $rootnamespace$ {
    public static class KLGates {
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