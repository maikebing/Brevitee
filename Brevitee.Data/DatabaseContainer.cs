using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using Brevitee.Incubation;

namespace Brevitee.Data
{
    public static class _
    {
        public static DatabaseContainer Db
        {
            get
            {
                return Incubator.Default.Get<DatabaseContainer>();
            }
        }
        
        public static DaoTransaction BeginTransaction<T>() where T: Dao
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
                foreach (Type type in assembly.GetTypes())
                {
                    if (Dao.ConnectionName(type).Equals(connectionName))
                    {
                        EnsureSchema(type);
                    }
                }
            }
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

                Database db = _.Db.For(type);
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

    public class DatabaseContainer
    {
        Dictionary<string, Database> _databases;
        public DatabaseContainer()
        {
            this._databases = new Dictionary<string, Database>();
        }

        /// <summary>
        /// Gets the Database for the specified type.
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <returns></returns>
        public Database For<T>() where T : Dao
        {
            return this[typeof(T)];
        }

        /// <summary>
        /// Gets the Databse for the specified entity relationship diagram
        /// name.  This directly correlates to the 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public Database For(string connectionName)
        {
            return this[connectionName];
        }

        public Database For(Type type)
        {
            return this[type];
        }

        public DaoTransaction BeginTransaction<T>() where T : Dao
        {
            return _.BeginTransaction<T>();
        }

        public DaoTransaction BeginTransaction(Type type)
        {
            return _.BeginTransaction(type);
        }
        /// <summary>
        /// Gets the database for the specified type.
        /// </summary>
        /// <param name="daoType"></param>
        /// <returns></returns>
        public Database this[Type daoType]
        {
            get
            {
                return this[Dao.ConnectionName(daoType)];
            }
            internal set
            {
                this[Dao.ConnectionName(daoType)] = value;
            }
        }
        
        public Database this[string connectionName]
        {
            get
            {
                if (!_databases.ContainsKey(connectionName))
                {
                    InitializeDatabase(connectionName, _databases);
                }

                return _databases[connectionName];
            }
            internal set
            {
                if (_databases.ContainsKey(connectionName))
                {
                    _databases[connectionName] = value;
                }
                else
                {
                    _databases.Add(connectionName, value);
                }
            }
        }

        internal void InitializeDatabase(string connectionName, Dictionary<string, Database> databases)
        {
            DatabaseInitializationResult dir = DatabaseInitializers.TryInitialize(connectionName);
            if (dir.Success)
            {
                databases.AddMissing(connectionName, dir.Database);
            }
            else
            {
                throw dir.Exception;
            }
        }

    }
}
