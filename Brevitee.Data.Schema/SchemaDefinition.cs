using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using Brevitee;
using Brevitee.Data;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace Brevitee.Data.Schema
{
    public class SchemaDefinition
    {
        Dictionary<string, Table> _tables = new Dictionary<string, Table>();
        Dictionary<string, ColumnAttribute> _columns = new Dictionary<string, ColumnAttribute>();

        public SchemaDefinition() 
        { 
        }

        /// <summary>
        /// Gets or sets the type of the database that this SchemaDefinition was
        /// extracted from.  May be null.
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// Gets or sets the name of the current SchemaDefinition.
        /// </summary>
        public string Name { get; set; }

        FileInfo file;
        [Exclude]
        public string File
        {
            get
            {
                if (file != null)
                {
                    return file.FullName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                this.file = new FileInfo(value);
            }
        }

        public void RemoveTable(Table table)
        {
            RemoveTable(table.Name);
        }

        public void RemoveTable(string tableName)
        {
            if (this._tables.ContainsKey(tableName))
            {
                this._tables.Remove(tableName);
            }
        }

        public void RenameTable(string tableName, string newName)
        {
            Table table = GetTable(tableName);
            if (table != null)
            {
                table.Name = newName;
                this.Save();
            }
        }
        
        public Table[] Tables
        {
            get
            {
                return this._tables.Values.ToArray();
            }
            set
            {
                this._tables.Clear();
                foreach (Table table in value)
                {
                    if (string.IsNullOrEmpty(table.ConnectionName))
                    {
                        table.ConnectionName = this.Name;
                    }
                    if (!this._tables.ContainsKey(table.Name))
                    {
                        this._tables.Add(table.Name, table);
                    }
                    else
                    {
                        throw Args.Exception<InvalidOperationException>("Table named {0} defined more than once", table.Name);
                    }
                }
            }
        }

        internal Table GetTable(string tableName)
        {
            Table table = null;
            if (this._tables.ContainsKey(tableName))
            {
                table = this._tables[tableName];
            }
            return table;
        }

        List<ForeignKeyColumn> _foreignKeys = new List<ForeignKeyColumn>();
        public ForeignKeyColumn[] ForeignKeys
        {
            get
            {
                return this._foreignKeys.ToArray();
            }
            set
            {
                this._foreignKeys.Clear();
                this._foreignKeys.AddRange(value);
            }
        }

        Dictionary<string, XrefTable> _xrefs = new Dictionary<string, XrefTable>();
        public XrefTable[] Xrefs
        {
            get
            {
                return _xrefs.Values.ToArray();
            }
            set
            {
                this._xrefs = value.ToDictionary<XrefTable, string>(x => x.Name); // to Dictionary key by name
            }
        }

        public XrefInfo[] LeftXrefsFor(string tableName)
        {
            return (from xref in Xrefs
                    where xref.Left.Equals(tableName)
                    select xref).Select(x => new XrefInfo(tableName, x.Name, x.Right)).ToArray();
        }

        public XrefInfo[] RightXrefsFor(string tableName)
        {
            return (from xref in Xrefs
                    where xref.Right.Equals(tableName)
                    select xref).Select(x => new XrefInfo(tableName, x.Name, x.Left)).ToArray();
        }

        internal XrefTable GetXref(string tableName)
        {
            XrefTable result = null;
            if (this._xrefs.ContainsKey(tableName))
            {
                result = this._xrefs[tableName];
            }

            return result;
        }

        public Result AddXref(XrefTable xref)
        {
            Result r = new Result(string.Format("XrefTable {0} was added.", xref.Name));
            try
            {
                xref.ConnectionName = this.Name;
                if (_xrefs.ContainsKey(xref.Name))
                {
                    _xrefs[xref.Name] = xref;
                    r.Message = string.Format("XrefTable {0} was updated.", xref.Name);
                }
                else
                {
                    _xrefs.Add(xref.Name, xref);
                }
            }
            catch (Exception ex)
            {
                SetErrorDetails(r, ex);
            }

            return r;
        }

        public void RemoveXref(string name)
        {
            if (_xrefs.ContainsKey(name))
            {
                _xrefs.Remove(name);
            }
        }

        public void RemoveXref(XrefTable xrefTable)
        {
            if (_xrefs.ContainsKey(xrefTable.Name))
            {
                _xrefs.Remove(xrefTable.Name);
            }
        }

        object _tableLock = new object();
        public Result AddTable(Table table)
        {
            lock (_tableLock)
            {
                Result r = new Result(string.Format("Table {0} was added.", table.Name));
                try
                {
                    table.ConnectionName = this.Name;
                    if (this._tables.ContainsKey(table.Name))
                    {
                        this._tables[table.Name] = table;
                        r.Message = string.Format("Table {0} was updated.", table.Name);
                    }
                    else
                    {
                        this._tables.Add(table.Name, table);
                    }
                }
                catch (Exception ex)
                {
                    SetErrorDetails(r, ex);
                }

                return r;
            }
        }

        public Result AddForeignKey(ForeignKeyColumn fk)
        {
            Result r = new Result(string.Format("ForeignKey {0} was added.", fk.ReferenceName));
            try
            {
                if (!this._foreignKeys.Contains(fk))
                {
                    this._foreignKeys.Add(fk);
                }
                else
                {
                    ForeignKeyColumn existing = (from efk in this._foreignKeys
                                                 where efk.Equals(fk)
                                                 select efk).FirstOrDefault();

                    existing.AllowNull = fk.AllowNull;
                    existing.DbDataType = fk.DbDataType;
                    existing.Key = fk.Key;
                    existing.MaxLength = fk.MaxLength;
                    existing.Name = fk.Name;
                    existing.ReferencedKey = fk.ReferencedKey;
                    existing.ReferencedTable = fk.ReferencedTable;
                    existing.ReferenceName = fk.ReferenceName;
                    existing.TableName = fk.TableName;                    
                }
            }
            catch (Exception ex)
            {
                SetErrorDetails(r, ex);
            }

            return r;
        }

        internal Result SetKeyColumn(string tableName, string columnName)
        {
            Result r = new Result(string.Format("Key for table [{0}] set to [{1}]", tableName, columnName));
            try
            {
                Table table = GetTable(tableName);
                table.SetKeyColumn(columnName);
            }
            catch (Exception ex)
            {
                SetErrorDetails(r, ex);
            }

            this.Save();
            return r;
        }

        private void SetErrorDetails(Result r, Exception ex)
        {
            this.LastException = ex;
            r.Message = ex.Message;
            r.Success = false;
            r.StackTrace = ex.StackTrace;
        }
        
        /// <summary>
        /// The most recent exception that occurred after trying to add a table
        /// or a foreign key
        /// </summary>
        public Exception LastException
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Loads a SchemaDefinition from the specified file, the file
        /// will be created if it doesn't exist.
        /// </summary>
        /// <param name="schemaFile"></param>
        /// <returns></returns>
        public static SchemaDefinition Load(string schemaFile)
        {
            SchemaDefinition schema = new SchemaDefinition();
            schema.File = schemaFile;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (System.IO.File.Exists(schemaFile))
            {
                using (StreamReader sr = new StreamReader(schemaFile))
                {
                    schema = serializer.Deserialize<SchemaDefinition>(sr.ReadToEnd());
                }
            }
            else
            {
                Save(schema);
            }
            return schema;
        }

        /// <summary>
        /// Serializes the current SchemaDefinition as json to the
        /// file specified in the File property
        /// </summary>
        public void Save()
        {
            Save(this);
        }

        /// <summary>
        /// Serializes the current SchemaDefinition as json to the
        /// specified filePath
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            this.File = filePath;
            Save(this);
        }

        private static void Save(SchemaDefinition schema)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StringBuilder builder = new StringBuilder();
            serializer.Serialize(schema, builder);
            builder.ToString().SafeWriteToFile(schema.File, true);
        }
    }
}
