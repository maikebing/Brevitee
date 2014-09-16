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
    /// <summary>
    /// The magic underscore convenience static class
    /// to quickly get references to databases for
    /// different generated dao types
    /// </summary>
    [Obsolete("This class is deprecated in favor of the Db class")]
    public static class _
    {
        public static DatabaseContainer Db
        {
            get
            {
                return Incubator.Default.Get<DatabaseContainer>();
            }
        }

        public static DaoTransaction BeginTransaction<T>() where T : Dao
        {
            return BeginTransaction(typeof(T));
        }

        public static DaoTransaction BeginTransaction(Type type)
        {
            Database original = _.Db[type];
            return BeginTransaction(original);
        }

        public static DaoTransaction BeginTransaction(Database db)
        {
            Database original = db;
            DaoTransaction tx = new DaoTransaction(original);
            _.Db[db.ConnectionName] = tx.Database;
            tx.Disposed += (o, a) =>
            {
                _.Db[db.ConnectionName] = original;
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
        public static bool TryEnsureSchema<T>() where T : Dao
        {
            return TryEnsureSchema(typeof(T));
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <param name="type"></param>
        /// <returns>true on success, false if an error was thrown, possibly due to the 
        /// schema already having been written.</returns>
        public static bool TryEnsureSchema(Type type)
        {
            try
            {
                EnsureSchema(type);
                return true;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <param name="connectionName">The name of the connection in the config file</param>
        public static bool TryEnsureSchema(string connectionName)
        {
            try
            {
                EnsureSchema(connectionName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void EnsureSchema<T>() where T : Dao
        {
            EnsureSchema(typeof(T));
        }

        /// <summary>
        /// Creates the tables for the specified type and 
        /// associated sibling tables
        /// </summary>
        /// <param name="connectionName"></param>
        public static void EnsureSchema(string connectionName)
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
                            EnsureSchema(type);
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
        public static void EnsureSchema(Type type)
        {
            lock (_ensureLock)
            {
                string connectionName = Dao.ConnectionName(type);
                if (_ensuredSchemas.Contains(connectionName))
                {
                    return;
                }

                _ensuredSchemas.Add(connectionName);

                Database db = Db.For(type);
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
