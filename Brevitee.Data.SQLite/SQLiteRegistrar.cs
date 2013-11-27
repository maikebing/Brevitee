using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Incubation;
using System.Data.SQLite;
using Brevitee.Data;
using Brevitee;
using System.Reflection;
using System.IO;

namespace Brevitee.Data
{
    public static class SQLiteRegistrar
    {
        static object _monitorLock = new object();
        static bool _added;
        public static void MonitorBitness()
        {
            if (!_added)
            {
                lock (_monitorLock)
                {
                    if (!_added)
                    {
                        _added = true;
                        AppDomain.CurrentDomain.AssemblyResolve += (o, a) =>
                        {
                            if (a.Name.StartsWith("System.Data.SQLite"))
                            {
                                string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                                string fileName = Path.Combine(assemblyDir, string.Format("SQLite\\{0}\\{1}.dll", IntPtr.Size == 4 ? "x86" : "x64", a));

                                return Assembly.LoadFrom(fileName);
                            }
                            return null;
                        };
                    }
                }
            }
        }

        public static void OutputAssemblyQualifiedName()
        {
            Console.WriteLine(SQLiteAssemblyQualifiedName());
        }

        public static string SQLiteAssemblyQualifiedName()
        {
            return SQLiteFactory.Instance.GetType().AssemblyQualifiedName;
        }

        /// <summary>
        /// Registers SQLite as the handler for the specified connection name.
        /// This dao handler will register apropriate DatabaseInitializer and
        /// ConnectionStringResolver.  This behavior is different compared to the
        /// SqlClientRegistrar's Register method.
        /// </summary>
        /// <param name="connectionName"></param>
        public static void Register(string connectionName)
        {
            SetInitializerAndConnectionStringResolver(connectionName);
            Register(_.Db.For(connectionName).ServiceProvider);
        }

        public static void Register(Type daoType)
        {
            SetInitializerAndConnectionStringResolver(daoType);
            Register(_.Db.For(daoType).ServiceProvider);
        }

        public static void Register<T>() where T : Dao
        {
            SetInitializerAndConnectionStringResolver(typeof(T));
            Register(_.Db.For<T>().ServiceProvider);
        }

        private static void SetInitializerAndConnectionStringResolver(Type daoType)
        {
            SetInitializerAndConnectionStringResolver(Dao.ConnectionName(daoType));
        }

        private static void SetInitializerAndConnectionStringResolver(string connectionName)
        {
            DatabaseInitializers.Ignore<DefaultDatabaseInitializer>(connectionName);
            DatabaseInitializers.AddInitializer(new SQLiteDatabaseInitializer());
            SQLiteConnectionStringResolver.Register();
        }

        public static void Register(Incubator incubator)
        {
            SQLiteConnectionStringResolver.Register();
            MonitorBitness();

            incubator.Set<IParameterBuilder>(() => new SQLiteParameterBuilder());
            incubator.Set<SqlStringBuilder>(() => new SqlStringBuilder());
            incubator.Set<SchemaWriter>(() => new SQLiteSqlStringBuilder());
            incubator.Set<QuerySet>(() => new SQLiteQuerySet());
        }
    }
}
