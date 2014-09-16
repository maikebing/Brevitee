using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using Brevitee.Incubation;
using Brevitee.Logging;

namespace Brevitee.Data
{
    public static class Db
    {
        public static DatabaseContainer DefaultContainer
        {
            get
            {
                return Incubator.Default.Get<DatabaseContainer>();
            }
        }

        public static Database For<T>() where T : Dao
        {
            return DefaultContainer[typeof(T)];
        }

        public static Database For(Type type)
        {
            return DefaultContainer[type];
        }

        public static Database For(string connectionName, Database database = null)
        {
            if (database != null)
            {
                DefaultContainer[connectionName] = database;
            }

            return DefaultContainer[connectionName];
        }

        public static DaoTransaction BeginTransaction<T>() where T : Dao
        {
            return BeginTransaction(typeof(T));
        }

        public static DaoTransaction BeginTransaction(Type type)
        {
            Database original = Db.For(type);//_.Db[type];
            return BeginTransaction(original);
        }

        public static DaoTransaction BeginTransaction(Database db)
        {
            Database original = db;
            DaoTransaction tx = new DaoTransaction(original);
            Db.For(db.ConnectionName, tx.Database);//_.Db[db.ConnectionName] = tx.Database;
            tx.Disposed += (o, a) =>
            {
                Db.For(db.ConnectionName, original);//_.Db[db.ConnectionName] = original;
            };

            return tx;
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>true on success, false if an error was thrown, possibly due to the 
        /// schema already having been written.</returns>
        public static bool TryEnsureSchema<T>(Database db = null) where T : Dao
        {
            return TryEnsureSchema(typeof(T), db);
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <param name="type"></param>
        /// <returns>true on success, false if an error was thrown, possibly due to the 
        /// schema already having been written.</returns>
        public static bool TryEnsureSchema(Type type, Database db = null)
        {
            try
            {
                EnsureSchema(type, db);
                return true;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }

        public static bool TryEnsureSchema(string connectionName, Database db = null)
        {
            Exception ignore;
            return TryEnsureSchema(connectionName, db, out ignore);
        }

        public static bool TryEnsureSchema(string connectionName, out Exception ex)
        {
            return TryEnsureSchema(connectionName, null, out ex);
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <param name="connectionName">The name of the connection in the config file</param>
        public static bool TryEnsureSchema(string connectionName, Database db, out Exception ex)
        {
            ex = null;
            try
            {
                EnsureSchema(connectionName, db);
                return true;
            }
            catch(Exception e)
            {
                ex = e;
                return false;
            }
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void EnsureSchema<T>(Database db = null) where T : Dao
        {
            EnsureSchema(typeof(T), db);
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <param name="connectionName"></param>
        public static void EnsureSchema(string connectionName, Database db = null)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                return;
            }

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] types;
                if (TryGetTypes(assembly, out types))
                {
                    foreach (Type type in types)
                    {
                        if (Dao.ConnectionName(type).Equals(connectionName))
                        {
                            EnsureSchema(type, db);
                        }
                    }
                }
            }
        }

        private static bool TryGetTypes(Assembly assembly, out Type[] types)
        {
            bool result = true;
            types = null;
            try
            {
                types = assembly.GetTypes();
            }
            catch (Exception ex)
            {
                result = false;
                Log.AddEntry("An exception occurred getting types from assembly ({0}): {1}", ex, assembly.FullName, ex.Message);
            }

            return result;
        }

        static List<string> _ensuredSchemas = new List<string>();
        static object _ensureLock = new object();
        /// <summary>
        /// Creates the tables for the specified type
        /// </summary>
        /// <param name="type"></param>
        public static void EnsureSchema(Type type, Database database = null)
        {
            lock (_ensureLock)
            {
                string connectionName = Dao.ConnectionName(type);
                if (_ensuredSchemas.Contains(connectionName))
                {
                    return;
                }

                _ensuredSchemas.Add(connectionName);

                Database db = database ?? Db.For(type);
                SchemaWriter schema = db.ServiceProvider.Get<SchemaWriter>();
                schema.WriteSchemaScript(type);

                db.ExecuteSql(schema, db.ServiceProvider.Get<IParameterBuilder>());
            }
        }

        public static ColumnAttribute[] GetColumns<T>() where T : Dao
        {
            return ColumnAttribute.GetColumns(typeof(T));
        }

        public static ColumnAttribute[] GetColumns(Type type)
        {
            return ColumnAttribute.GetColumns(type);
        }

        public static ColumnAttribute[] GetColumns(object instance)
        {
            return ColumnAttribute.GetColumns(instance);
        }

        public static ForeignKeyAttribute[] GetForeignKeys(object instance)
        {
            return ColumnAttribute.GetForeignKeys(instance);
        }

        public static ForeignKeyAttribute[] GetForeignKeys(Type type)
        {
            return ColumnAttribute.GetForeignKeys(type);
        }
    }

}
