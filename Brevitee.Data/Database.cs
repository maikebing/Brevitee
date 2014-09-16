using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Incubation;
using Brevitee.Logging;
using System.Threading;
using System.Reflection;
using System.IO;

namespace Brevitee.Data
{
    public class Database
    {
        AutoResetEvent resetEvent;
        List<DbConnection> connections;
        public Database()
        {
            this.ServiceProvider = Incubator.Default;
            this.resetEvent = new AutoResetEvent(false);
            this.connections = new List<DbConnection>();
            this.MaxConnections = 25;
        }

        public Database(Incubator serviceProvider, string connectionString, string connectionName = null)
            : this()
        {
            this.ServiceProvider = serviceProvider;
            this.ConnectionString = connectionString;
            this.ConnectionName = connectionName;
        }

        public DaoTransaction BeginTransaction()
        {
            return Db.BeginTransaction(this);
        }

        public int MaxConnections { get; set; }

        public Incubator ServiceProvider { get; set; }

        public string ConnectionName { get; set; }

        public string Name
        {
            get
            {
                DbConnectionStringBuilder cb = CreateConnectionStringBuilder();                
                cb.ConnectionString = this.ConnectionString;

                string databaseName = cb["Initial Catalog"] as string;

                if (string.IsNullOrEmpty(databaseName))
                {
                    databaseName = cb["Database"] as string;
                }

                if (string.IsNullOrEmpty(databaseName))
                {
                    databaseName = cb["Data Source"] as string;
                }

                if (string.IsNullOrEmpty(databaseName))
                {
                    throw new InvalidOperationException("Unable to determine database name from the connection string");
                }
                return databaseName;
            }
        }

        public Dictionary<EnumType, T> FillEnumDictionary<EnumType, T>(Dictionary<EnumType, T> dictionary, string nameColumn) where T : Dao, new()
        {
            QuerySet query = ExecuteQuery<T>();

            QueryResult result = ((QueryResult)query.Results[0]);
            if (result.DataTable.Rows.Count == 0)
            {
                InitEnumValues<EnumType, T>("Value", nameColumn);
                query = ExecuteQuery<T>();
                result = ((QueryResult)query.Results[0]);
            }

            foreach (DataRow row in result.DataTable.Rows)
            {
                EnumType enumVal = (EnumType)Enum.Parse(typeof(EnumType), (string)row[nameColumn]);
                T inst = new T();
                inst.DataRow = row;
                dictionary.AddMissing(enumVal, inst);
            }

            return dictionary;
        }

        private QuerySet ExecuteQuery<T>() where T : Dao, new()
        {
            QuerySet query = new QuerySet();
            query.Select<T>();
            query.Execute(this);
            return query;
        }

        static object initEnumLock = new object();
        private void InitEnumValues<EnumType, T>(string valueColumn, string nameColumn) where T : Dao, new()
        {
            FieldInfo[] fields = typeof(EnumType).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                T entry = new T();
                entry.SetValue(valueColumn, field.GetRawConstantValue());
                entry.SetValue(nameColumn, field.Name);
                entry.Save();
            }
        }

        /// <summary>
        /// Execute the specified SqlStringBuilder using the 
        /// specified generic type to determine which database
        /// to use.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public virtual void ExecuteSql<T>(SqlStringBuilder builder) where T : Dao
        {
            Database db = Db.For<T>();
            ExecuteSql(builder, db.ServiceProvider.Get<IParameterBuilder>());
        }

        public virtual void ExecuteSql(SqlStringBuilder builder)
        {
            ExecuteSql(builder, ServiceProvider.Get<IParameterBuilder>());
        }

        public virtual void ExecuteSql(SqlStringBuilder builder, IParameterBuilder parameterBuilder)
        {
            ExecuteSql(builder, CommandType.Text, parameterBuilder.GetParameters(builder));
        }

        public virtual void ExecuteSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParameters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();
            DbConnection conn = GetDbConnection();
            try
            {
                conn.Open();
                DbCommand cmd = BuildCommand(sqlStatement, commandType, dbParameters, providerFactory, conn);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                ReleaseConnection(conn);
            }
        }
        
        public virtual DataTable GetDataTableFromSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParamaters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();
            DbConnection conn = GetDbConnection();
            DataTable table = new DataTable();
            try
            {
                DbCommand command = BuildCommand(sqlStatement, commandType, dbParamaters, providerFactory, conn);
                FillTable(table, command);
            }
            finally
            {
                ReleaseConnection(conn);
            }

            return table;
        }

        public virtual DbCommand CreateCommand()
        {
            return ServiceProvider.Get<DbProviderFactory>().CreateCommand();
        }

        public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return ServiceProvider.Get<DbProviderFactory>().CreateConnectionStringBuilder();
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, params DbParameter[] dbParamaters)
        {
            return GetDataSetFromSql(sqlStatement, commandType, true, dbParamaters);
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, bool releaseConnection, params DbParameter[] dbParamaters)
        {
            DbConnection conn = GetDbConnection();
            return GetDataSetFromSql(sqlStatement, commandType, releaseConnection,  conn, dbParamaters);
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, bool releaseConnection, DbConnection conn, params DbParameter[] dbParamaters)
        {
            return GetDataSetFromSql(sqlStatement, commandType, releaseConnection, conn, null, dbParamaters);
        }

        public virtual DataSet GetDataSetFromSql(string sqlStatement, CommandType commandType, bool releaseConnection, DbConnection conn, DbTransaction tx, params DbParameter[] dbParamaters)
        {
            DbProviderFactory providerFactory = ServiceProvider.Get<DbProviderFactory>();
           
            DataSet set = new DataSet();
            try
            {
                DbCommand command = BuildCommand(sqlStatement, commandType, dbParamaters, providerFactory, conn, tx);
                FillDataSet(set, command);
            }
            finally
            {
                if (releaseConnection)
                {
                    ReleaseConnection(conn);
                }
            }

            return set;
        }

        internal static DbCommand BuildCommand(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbProviderFactory providerFactory, DbConnection conn, DbTransaction tx  =null)
        {
            DbCommand command = providerFactory.CreateCommand();
            command.Connection = conn;
            if (tx != null)
            {
                command.Transaction = tx;
            }
            command.CommandText = sqlStatement;
            command.CommandType = commandType;
            command.CommandTimeout = 10000;            
            command.Parameters.AddRange(dbParameters);
            return command;
        }

        private void FillTable(DataTable table, DbCommand command)
        {
            DbDataAdapter adapter = ServiceProvider.Get<DbProviderFactory>().CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
        }

        private void FillDataSet(DataSet dataSet, DbCommand command)
        {
            DbDataAdapter adapter = ServiceProvider.Get<DbProviderFactory>().CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
        }

        public string ConnectionString
        {
            get;
            set;
        }

        public DbConnection GetDbConnection()
        {
            return GetDbConnection(this.MaxConnections);
        }
        
        private DbConnection GetDbConnection(int max)
        {
            if (connections.Count >= max)
            {
                resetEvent.WaitOne();
            }

            DbConnection conn = ServiceProvider.Get<DbProviderFactory>().CreateConnection();
            conn.ConnectionString = this.ConnectionString;
            lock (connectionLock)
            {
                connections.Add(conn);
            }
            return conn;
        }

        object connectionLock = new object();
        private void ReleaseConnection(DbConnection conn)
        {
            try
            {
                lock (connectionLock)
                {
                    if (connections.Contains(conn))
                    {
                        connections.Remove(conn);
                    }

                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
            catch //(Exception ex)
            {
                // do nothing
            }

            resetEvent.Set();
        }
    }
}
