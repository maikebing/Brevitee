using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bryan.Apellanes;
using Bryan.Apellanes.Data;
using Bryan.Apellanes.Dust;
using Bryan.Apellanes.Logging;
using Bryan.Apellanes.ServiceProxy;
using Bryan.Apellanes.DaoRef;
using Bryan.Apellanes.Data.Schema;
using LD = Bryan.Apellanes.Logging.Data;
using Bryan.Apellanes.Analytics.Crawlers;
using Bryan.Apellanes.Profiguration;
using Bryan.Apellanes.Management;

[assembly:WebActivatorEx.PostApplicationStartMethod(typeof($rootnamespace$.Bryan), "StartBeingAwesome")]

namespace $rootnamespace$
{
        public abstract class Bryan // : abstract, not static,  get it :)  :b
    {
        /// <summary>
        /// Initialize Bryan's Awesome Framework of Frameworks
        /// </summary>
        public static void StartBeingAwesome()
        {
            Profiguration.Initialize();
            SQLiteRegistrar.Register<LD.LogEvent>();

            BeAwesome("BAMvvm");
            Log.Start();
            Dust.Initialize();
            ServiceProxySystem.Register<Echo>();
            ServiceProxySystem.RegisterBinProviders();

            RegisterDaoType<TestTable>();
        }

        public static void BeAwesome(string connectionName)
        {
            SQLiteRegistrar.Register(connectionName);
            DaoProxyRegistration.RegisterConnection(connectionName);
            _.TryEnsureSchema(connectionName);
        }

        public static void BeAwesome<Dao1>()
            where Dao1 : Dao
        {
            StartBeingAwesome();
            RegisterDaoType<Dao1>();
        }

        public static void BeAwesome<Dao1, Dao2>()
            where Dao1 : Dao
            where Dao2 : Dao
        {
            BeAwesome<Dao1>();
            RegisterDaoType<Dao2>();
        }

        public static void BeAwesome<Dao1, Dao2, Dao3>()
            where Dao1 : Dao
            where Dao2 : Dao
            where Dao3 : Dao
        {
            BeAwesome<Dao1, Dao2>();
            RegisterDaoType<Dao3>();
        }

        public static void BeAwesome<Dao1, Dao2, Dao3, Dao4>()
            where Dao1 : Dao
            where Dao2 : Dao
            where Dao3 : Dao
            where Dao4 : Dao
        {
            BeAwesome<Dao1, Dao2, Dao3>();
            RegisterDaoType<Dao4>();
        }

        private static void RegisterDaoType<T>() where T : Dao
        {
            SQLiteRegistrar.Register<T>();
            DaoProxyRegistration.Register<T>();
            _.TryEnsureSchema<T>();
        }

    }
}