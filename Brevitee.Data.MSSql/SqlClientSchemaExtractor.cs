using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Common;
using BAS = Brevitee.Data.Schema;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Configuration;
using Brevitee;
using Brevitee.Data;

namespace Brevitee.Data.MsSql
{
    // TODO: update this to allow for specifying a connection string without looking in the config, so it can be specified in the UI  
    // TODO: Allow LaoTzU.exe (note the U) to specify Xref tables when extracting schema definition
    public class SqlClientSchemaExtractor: BAS.ISchemaExtractor
    {
        SqlConnection _connection; 
        ServerConnection _serverConnection;
        Microsoft.SqlServer.Management.Smo.Server _server;
        Microsoft.SqlServer.Management.Smo.Database _database;
        string _connectionName;
        Database _daoDatabase;

        BAS.ForeignKeyColumn[] foreignKeys;

        public SqlClientSchemaExtractor(string connectionName)
        {
            this._connectionName = connectionName;
            this._daoDatabase = Db.For(connectionName);//_.Db[connectionName];
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            Initialize(connectionString);
        }

        private void Initialize(string connectionString)
        {
            this._connection = new SqlConnection(connectionString);
            this._serverConnection = new ServerConnection(this._connection);
            this._server = new Server(this._serverConnection);

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = connectionStringBuilder["Initial Catalog"] as string;
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                databaseName = connectionStringBuilder["Database"] as string;
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new InvalidOperationException(
                    string.Format("Unable to determine database name from the specified connection string: {0}",
                    connectionString));
            }

            this._database = this._server.Databases[databaseName];

            this.foreignKeys = GetForeignKeyColumns();
        }
        
        #region ISchemaExtractor Members

        public BAS.SchemaDefinition Extract()
        {
            BAS.SchemaManager mgr = new BAS.SchemaManager();
            mgr.SetSchema(_database.Name);
            AddTables(mgr);
            SetForeignKeys(mgr);
            return mgr.GetCurrentSchema();
        }

	    public event EventHandler<SchemaExtractorEventArgs> ProcessingTable;
	    public event EventHandler<SchemaExtractorEventArgs> ProcessingColumn;

	    protected void OnProcessingTable(Table table) 
		{
		    if (ProcessingTable != null) 
			{
			    ProcessingTable(this, new SchemaExtractorEventArgs {Table = table});
		    }
	    }

	    protected void OnProcessingColumn(Column column) 
		{
		    if (ProcessingColumn != null) 
			{
			    ProcessingColumn(this, new SchemaExtractorEventArgs {Column = column});
		    }
	    }

        public void AddTables(BAS.SchemaManager mgr)
        {
            foreach (Table table in _database.Tables)
            {
				OnProcessingTable(table);
                mgr.AddTable(table.Name);
                foreach (Column column in table.Columns)
                {
					OnProcessingColumn(column);
                    BAS.Column pocoColumn = new BAS.Column();

                    if (column.IsForeignKey)
                    {
                        BAS.ForeignKeyColumn fkColumn = GetForeignKey(table.Name, column.Name);
                        pocoColumn = fkColumn;
                    }

                    pocoColumn.Name = column.Name;
                    string dbDataType = column.DataType.SqlDataType.ToString();
                    if (column.DataType.SqlDataType == SqlDataType.VarCharMax)
                    {
                        dbDataType = "VarChar";
                    }
                    else if (column.DataType.SqlDataType == SqlDataType.NVarCharMax)
                    {
                        dbDataType = "NVarChar";
                    }
                    else if (column.DataType.SqlDataType == SqlDataType.VarBinaryMax)
                    {
                        dbDataType = "VarBinary";
                    }
                    pocoColumn.DbDataType = dbDataType;
                    pocoColumn.Type = TranslateDataType(column.DataType.SqlDataType);
                    pocoColumn.MaxLength = column.DataType.MaximumLength == -1 ? "MAX": column.DataType.MaximumLength.ToString();
                    pocoColumn.AllowNull = column.Nullable;
                    
                    mgr.AddColumn(table.Name, pocoColumn);
                     
                    if (column.InPrimaryKey)
                    {
                        mgr.SetKeyColumn(table.Name, pocoColumn.Name);
                    }
                }
            }
        }

        #endregion

        private BAS.ForeignKeyColumn GetForeignKey(string owningTable, string columnName)
        {
            return (from fk in this.foreignKeys
                    where fk.TableName.Equals(owningTable) && fk.Name.Equals(columnName)
                    select fk).FirstOrDefault();
        }

        public void SetForeignKeys(BAS.SchemaManager mgr)
        {
            foreach (BAS.ForeignKeyColumn fk in this.foreignKeys)
            {
                mgr.SetForeignKey(fk.ReferencedTable, fk.TableName, fk.Name);
            }
        }

        public BAS.ForeignKeyColumn[] GetForeignKeyColumns()
        {
            DataTable foreignKeyData = GetForeignKeyData(_connectionName);
            List<BAS.ForeignKeyColumn> results = new List<BAS.ForeignKeyColumn>();
            foreach (DataRow row in foreignKeyData.Rows)
            {
                BAS.ForeignKeyColumn fk = new BAS.ForeignKeyColumn();
                fk.TableName = row["ForeignKeyTable"].ToString();
                fk.ReferenceName = row["ForeignKeyName"].ToString();
                fk.Name = row["ForeignKeyColumn"].ToString();
                fk.ReferencedKey = row["PrimaryKeyColumn"].ToString();
                fk.ReferencedTable = row["PrimaryKeyTable"].ToString();
                results.Add(fk);
            }

            return results.ToArray();
        }

        internal static DataTable GetForeignKeyData(string connectionName)
        {
            return GetForeignKeyData(Db.For(connectionName));//_.Db[connectionName]);
        }

        internal static DataTable GetForeignKeyData(Database db)
        {
            #region sql
            string sql = @"SELECT FK.constraint_name as ForeignKeyName, 
                FK.table_name as ForeignKeyTable, 
                FKU.column_name as ForeignKeyColumn,
                UK.constraint_name as PrimaryKeyName, 
                UK.table_name as PrimaryKeyTable, 
                UKU.column_name as PrimaryKeyColumn
                FROM Information_Schema.Table_Constraints AS FK
                INNER JOIN
                Information_Schema.Key_Column_Usage AS FKU
                ON FK.constraint_type = 'FOREIGN KEY' AND
                FKU.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Referential_Constraints AS RC
                ON RC.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Table_Constraints AS UK
                ON UK.constraint_name = RC.unique_constraint_name
                INNER JOIN
                Information_Schema.Key_Column_Usage AS UKU
                ON UKU.constraint_name = UK.constraint_name AND
                UKU.ordinal_position =FKU.ordinal_position";
            #endregion
            List<BAS.ForeignKeyColumn> results = new List<BAS.ForeignKeyColumn>();
            DataTable foreignKeyData = db.GetDataTableFromSql(sql, CommandType.Text);
            return foreignKeyData;
        }

        protected internal BAS.DataTypes TranslateDataType(SqlDataType sqlDataType)
        {
            switch (sqlDataType)
            {
                case SqlDataType.BigInt:
                    return BAS.DataTypes.Long;
                case SqlDataType.Binary:
                    return BAS.DataTypes.Byte;
                case SqlDataType.Bit:
                    return BAS.DataTypes.Boolean;
                case SqlDataType.Char:
                    return BAS.DataTypes.String;
                case SqlDataType.DateTime:
                    return BAS.DataTypes.DateTime;
                case SqlDataType.Decimal:
                    return BAS.DataTypes.Decimal;
                case SqlDataType.Float:
                    return BAS.DataTypes.String;
                case SqlDataType.Image:
                    return BAS.DataTypes.Byte;
                case SqlDataType.Int:
                    return BAS.DataTypes.Int;
                case SqlDataType.Money:
                    return BAS.DataTypes.Decimal;
                case SqlDataType.NChar:
                    return BAS.DataTypes.String;
                case SqlDataType.NText:
                    return BAS.DataTypes.String;
                case SqlDataType.NVarChar:
                    return BAS.DataTypes.String;
                case SqlDataType.NVarCharMax:
                    return BAS.DataTypes.String;
                case SqlDataType.None:
                    return BAS.DataTypes.String;
                case SqlDataType.Numeric:
                    return BAS.DataTypes.Long;
                case SqlDataType.Real:
                    return BAS.DataTypes.String;
                case SqlDataType.SmallDateTime:
                    return BAS.DataTypes.DateTime;
                case SqlDataType.SmallInt:
                    return BAS.DataTypes.Int;
                case SqlDataType.SmallMoney:
                    return BAS.DataTypes.Decimal;
                case SqlDataType.SysName:
                    return BAS.DataTypes.String;
                case SqlDataType.Text:
                    return BAS.DataTypes.String;
                case SqlDataType.Timestamp:
                    return BAS.DataTypes.DateTime;
                case SqlDataType.TinyInt:
                    return BAS.DataTypes.Int;
                case SqlDataType.UniqueIdentifier:
                    return BAS.DataTypes.String;
                case SqlDataType.UserDefinedDataType:
                    return BAS.DataTypes.String;
                case SqlDataType.UserDefinedType:
                    return BAS.DataTypes.String;
                case SqlDataType.VarBinary:
                    return BAS.DataTypes.Byte;
                case SqlDataType.VarBinaryMax:
                    return BAS.DataTypes.Byte;
                case SqlDataType.VarChar:
                    return BAS.DataTypes.String;
                case SqlDataType.VarCharMax:
                    return BAS.DataTypes.String;
                case SqlDataType.Variant:
                    return BAS.DataTypes.String;
                case SqlDataType.Xml:
                    return BAS.DataTypes.String;
                default:
                    return BAS.DataTypes.String;
            }
        }
    }
}
