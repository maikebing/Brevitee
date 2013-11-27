using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Brevitee;
//using Brevitee.FileExt;
using Brevitee.Incubation;
using System.Reflection;

namespace Brevitee.Data
{
    /// <summary>
    /// Data Access Object
    /// </summary>
    public abstract class Dao: ICommittable, IHasDataRow
    {
        static Dictionary<string, string> _proxiedConnectionNames;
        Dictionary<string, object> _newValues;
        
        static Dao()
        {
            _proxiedConnectionNames = new Dictionary<string, string>();
        }

        static volatile Incubator _proxyTypeProvider;
        static object _proxyTypeProviderLock = new object();
        public static Incubator ProxyTypeProvider
        {
            get
            {
                if (_proxyTypeProvider == null)
                {
                    lock (_proxyTypeProviderLock)
                    {
                        if (_proxyTypeProvider == null)
                        {
                            _proxyTypeProvider = Incubator.Default;
                        }
                    }
                }

                return _proxyTypeProvider;
            }

            set
            {
                _proxyTypeProvider = value;
            }
        }

        Dictionary<string, ICommittable> _childCollections;

        public Dao()
        {
            this._childCollections = new Dictionary<string, ICommittable>();
            this._newValues = new Dictionary<string, object>();
            this.ServiceProvider = Incubator.Default;
            this.IsNew = true;
            this.DataRow = this.ToDataRow();
            this.AutoDeleteChildren = true;
        }

        public Dao(DataRow row)
            : this()
        {
            this.DataRow = row;
            this.IsNew = false;
        }

        public Dao(DataRow row, Incubator serviceProvider)
            : this(row)
        {
            this.ServiceProvider = serviceProvider;
        }

        public static void RegisterDaoTypes(Type daoSibling)
        {
            RegisterDaoTypes(daoSibling.Assembly, Incubator.Default);
        }

        public static void RegisterDaoTypes(Type daoSibling, Incubator serviceProvider)
        {
            RegisterDaoTypes(daoSibling.Assembly, serviceProvider);
        }

        /// <summary>
        /// Instantiate all Dao types in the specified assembly and place them into the specified
        /// serviceProvider
        /// </summary>
        /// <param name="daoAssembly"></param>
        /// <param name="serviceProvider"></param>
        public static void RegisterDaoTypes(Assembly daoAssembly, Incubator serviceProvider)
        {
            Type[] types = daoAssembly.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type current = types[i];
                if (current.IsSubclassOf(typeof(Dao)))
                {
                    serviceProvider.Construct(current);
                }
            }
        }

        public override int GetHashCode()
        {
            return IdValue.GetValueOrDefault().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Dao dao = obj as Dao;
            if (dao != null)
            {
                return dao.GetType() == this.GetType() && dao.IdValue == this.IdValue;
            }
            else
            {
                return base.Equals(obj);
            }
        }
        
        /// <summary>
        /// If true, any references to the current
        /// record will be deleted prior to deleting
        /// the current record in a call to Delete()
        /// </summary>
        [Exclude]
        public bool AutoDeleteChildren { get; set; }

        public event DaoDelegate BeforeWriteCommit;
        public static event DaoDelegate BeforeWriteCommitAny;
        protected void OnBeforeWriteCommit(Database db)
        {
            if (BeforeWriteCommit != null)
            {
                BeforeWriteCommit(db, this);
            }

            if (BeforeWriteCommitAny != null)
            {
                BeforeWriteCommitAny(db, this);
            }
        }        

        public event DaoDelegate AfterWriteCommit;
        public static event DaoDelegate AfterWriteCommitAny;
        protected void OnAfterWriteCommit(Database db)
        {
            if (AfterCommit != null)
            {
                AfterCommit(db, this);
            }

            if (AfterWriteCommitAny != null)
            {
                AfterWriteCommitAny(db, this);
            }
        }

        public event DaoDelegate BeforeCommit;
        public static event DaoDelegate BeforeCommitAny;
        protected void OnBeforeCommit(Database db)
        {
            if (BeforeCommit != null)
            {
                BeforeCommit(db, this);
            }

            if (BeforeCommitAny != null)
            {
                BeforeCommitAny(db, this);
            }
        }

        public event DaoDelegate AfterCommit;
        public static event DaoDelegate AfterCommitAny;
        protected void OnAfterCommit(Database db)
        {
            if (AfterCommit != null)
            {
                AfterCommit(db, this);
            }

            if (AfterCommitAny != null)
            {
                AfterCommitAny(db, this);
            }
        }

        public event DaoDelegate BeforeWriteDelete;
        public static event DaoDelegate BeforeWriteDeleteAny;
        protected void OnBeforeWriteDelete(Database db)
        {
            if (BeforeWriteDelete != null)
            {
                BeforeWriteDelete(db, this);
            }

            if (BeforeWriteDeleteAny != null)
            {
                BeforeWriteDeleteAny(db, this);
            }
        }

        public event DaoDelegate AfterWriteDelete;
        public static event DaoDelegate AfterWriteDeleteAny;
        protected void OnAfterWriteDelete(Database db)
        {
            if (AfterWriteDelete != null)
            {
                AfterWriteDelete(db, this);
            }

            if (AfterWriteDeleteAny != null)
            {
                AfterWriteDeleteAny(db, this);
            }
        }
        
        public event DaoDelegate BeforeDelete;
        public static event DaoDelegate BeforeDeleteAny;
        protected void OnBeforeDelete(Database db)
        {
            if (BeforeDelete != null)
            {
                BeforeDelete(db, this);
            }

            if (BeforeDeleteAny != null)
            {
                BeforeDeleteAny(db, this);
            }
        }

        public event DaoDelegate AfterDelete;
        public static event DaoDelegate AfterDeleteAny;
        protected void OnAfterDelete(Database db)
        {
            if (AfterDelete != null)
            {
                AfterDelete(db, this);
            }

            if (AfterDeleteAny != null)
            {
                AfterDeleteAny(db, this);
            }

        }

        protected Dictionary<string, ICommittable> ChildCollections
        {
            get
            {
                return _childCollections;
            }
        }

        /// <summary>
        /// Reset the child collections for this instance forcing
        /// them to be reloaded the next time they are referenced.
        /// </summary>
        public void ResetChildren()
        {
            _childCollections.Clear();
        }
        
        public virtual ValidationResult IsValid()
        {
            return new ValidationResult();
        }

        /// <summary>
        /// Checks that every required column has a value.  Untested as of 05/09/2013 :b
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        protected bool ValidateRequiredProperties(out string[] messages)
        {
            Type type = this.GetType();
            PropertyInfo[] props = type.GetPropertiesWithAttributeOfType<ColumnAttribute>();
            List<string> msgs = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                ColumnAttribute ca = prop.GetCustomAttributeOfType<ColumnAttribute>();
                if (!(ca is KeyColumnAttribute) && !ca.AllowNull)
                {
                    string propTypeName = prop.PropertyType.Name;
                    MethodInfo getter = type.GetMethod("Get{0}Value"._Format(propTypeName));
                    object val = getter.Invoke(this, new object[] { ca.Name });
                    if (val == null || (val is string && string.IsNullOrEmpty((string)val)))
                    {
                        msgs.Add("{0} can't be null"._Format(prop.Name));
                    }
                }
            }
            messages = msgs.ToArray();
            return messages.Length == 0;
        }

        public void Save()
        {
            Commit();
        }

        public void Save(Database db)
        {
            Commit(db);
        }
        
        public virtual void Commit()
        {
            Database db = _.Db.For(this.GetType());

            Commit(db);
        }

        public virtual void Commit(DaoTransaction tx)
        {
            Commit(tx.Database);
        }
        
        private void Commit(Database db)
        {
            ValidationResult valid = this.IsValid();
            if (!valid.Success)
            {
                throw new ValidationException(valid.Message, valid.Exception);
            }            

            QuerySet querySet = GetQuerySet(db);

            WriteCommit(querySet, db);
            WriteChildCommits(querySet);

            if (!string.IsNullOrWhiteSpace(querySet.ToString()))
            {
                OnBeforeCommit(db);
                
                querySet.Execute(db);             
                this.IsNew = false;
                this.ResetChildren();
                OnAfterCommit(db);
            }
        }

        protected void WriteChildCommits(SqlStringBuilder sql)
        {
            foreach (string key in this.ChildCollections.Keys)
            {
                ICommittable c = this.ChildCollections[key];
                c.WriteCommit(sql);                
            }

            sql.Executed += (s, d) =>
            {
                this.ChildCollections.Clear();
            };
        }

        public void WriteChildDeletes(SqlStringBuilder sql)
        {
            foreach (string key in this.ChildCollections.Keys)
            {
                this.ChildCollections[key].WriteDelete(sql);
            }
        }

        public virtual void Delete(Database database = null)
        {
            Database db;
            SqlStringBuilder sql = GetSqlStringBuilder(out db);
            if (database != null)
            {
                sql = GetSqlStringBuilder(database);
                db = database;
            }

            if (AutoDeleteChildren)
            {
                WriteChildDeletes(sql);
            }
            WriteDelete(sql);

            OnBeforeDelete(db);
            sql.Execute(db);
            OnAfterDelete(db);
        }

        protected virtual void Delete<C>(Func<C, IQueryFilter<C>> where) where C : IFilterToken, new()
        {
            C columns = new C();
            IQueryFilter<C> filter = where(columns);
            Delete(filter);
        }

        protected virtual void Delete(IQueryFilter filter)
        {
            Database db;
            SqlStringBuilder sql = GetSqlStringBuilder(out db);

            foreach (string key in this.ChildCollections.Keys)
            {
                this.ChildCollections[key].WriteDelete(sql);
            }

            WriteDelete(sql, filter);
            sql.Execute(db);
            sql.Reset();
        }

        protected internal SqlStringBuilder GetSqlStringBuilder()
        {
            Database ignore;
            return GetSqlStringBuilder(out ignore);
        }
        
        protected internal SqlStringBuilder GetSqlStringBuilder(out Database db)
        {
            db = _.Db.For(this.GetType());
            return GetSqlStringBuilder(db);
        }

        protected internal static SqlStringBuilder GetSqlStringBuilder(Database db)
        {
            SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
            return sql;
        }

        protected internal QuerySet GetQuerySet()
        {
            Database ignore;
            return GetQuerySet(out ignore);
        }

        protected internal QuerySet GetQuerySet(out Database db)
        {
            db = _.Db.For(this.GetType());
            return GetQuerySet(db);
        }

        protected internal static QuerySet GetQuerySet(Database db)
        {
            QuerySet sql = db.ServiceProvider.Get<QuerySet>();
            return sql;
        }

        public virtual void WriteDelete(SqlStringBuilder sql)
        {
            Database db = _.Db.For(this.GetType());
            OnBeforeWriteDelete(db);
            sql.Delete(TableName()).Where(new AssignValue(KeyColumnName, IdValue));
            OnAfterWriteDelete(db);
        }

        protected internal virtual void WriteDelete(SqlStringBuilder sqlStringBuilder, IQueryFilter filter)
        {            
            sqlStringBuilder.Delete(TableName()).Where(filter).Go();
        }

        /// <summary>
        /// Write the update or insert statement for the current instance
        /// to the specified SqlStringBuilder.
        /// </summary>
        /// <param name="sqlStringBuilder"></param>
        public virtual void WriteCommit(SqlStringBuilder sqlStringBuilder)
        {
            Database db = _.Db.For(this.GetType());
            WriteCommit(sqlStringBuilder, db);
        }

        public virtual void WriteCommit(SqlStringBuilder sqlStringBuilder, Database db)
        {
            OnBeforeWriteCommit(db);            
            if (this._newValues.Count > 0)
            {
                AssignValue[] valueAssignments = GetNewAssignValues();

                if (this.IsNew)
                {
                    sqlStringBuilder
                        .Insert(this)
                        .Go();
                }
                else
                {
                    sqlStringBuilder
                        .Update(this.TableName(), valueAssignments)
                        .Where(this.GetUniqueFilter())
                        .Go();
                }
            }
            OnAfterWriteCommit(db);
        }

        public virtual void Undo(Database db = null)
        {
            Type thisType = this.GetType();
            if (db == null)
            {
                db = _.Db.For(thisType);
            }
            
            SqlStringBuilder sql = GetSqlStringBuilder(db);
            ColumnAttribute[] columns = _.GetColumns(thisType);
            foreach (ColumnAttribute col in columns)
            {
                this.SetValue(col.Name, this.GetOriginalValue(col.Name));
            }
            AssignValue[] values = GetNewAssignValues();
            sql.Update(this.TableName(), values)
                .Where(this.GetUniqueFilter())
                .Go();

            sql.Execute(db);
        }

        public virtual void Undelete(Database db = null)
        {
            Type thisType = this.GetType();
            if (db == null)
            {
                db = _.Db.For(thisType);
            }

            this.IsNew = true;
            ColumnAttribute[] columns = _.GetColumns(thisType);
            foreach (ColumnAttribute col in columns)
            {
                this.SetValue(col.Name, this.GetCurrentValue(col.Name));
            }

            this.Commit();
        }

        /// <summary>
        /// Creates an in memory dynamic type representing
        /// the current Dao's Columns only.
        /// </summary>
        /// <param name="daoObject"></param>
        /// <returns></returns>
        public object ToJsonSafe()
        {
            Type jsonSafeType = this.CreateDynamicType<ColumnAttribute>(false);
            ConstructorInfo ctor = jsonSafeType.GetConstructor(new Type[] { });
            object jsonSafeInstance = ctor.Invoke(null);
            jsonSafeInstance.CopyProperties(this);
            return jsonSafeInstance;
        }

        /// <summary>
        /// Gets an array of AssignValue instances that represent 
        /// the names and values of columns with new values
        /// </summary>
        /// <returns></returns>
        protected internal AssignValue[] GetNewAssignValues()
        {
            List<AssignValue> valueAssignments = new List<AssignValue>();
            foreach (string columnName in this._newValues.Keys)
            {
                valueAssignments.Add(new AssignValue(columnName, this._newValues[columnName]));
            }
            return valueAssignments.ToArray();
        }

        /// <summary>
        /// Gets an array of AssignValue instances that represent 
        /// the names and values of columns with old values (prior to any new sets)
        /// </summary>
        /// <returns></returns>
        protected internal AssignValue[] GetOldAssignValues()
        {
            List<AssignValue> valueAssignments = new List<AssignValue>();
            foreach (DataColumn column in this.DataRow.Table.Columns)
            {
                valueAssignments.Add(new AssignValue(column.ColumnName, this.DataRow[column]));
            }
            return valueAssignments.ToArray();
        }    

        /// <summary>
        /// When implemented by a derived class returns filters that should uniquely
        /// identify a single record.
        /// </summary>
        /// <returns></returns>
        public abstract IQueryFilter GetUniqueFilter();

        public string ConnectionName()
        {
            return ConnectionName(this);
        }

        public static string ConnectionName(object instance)
        {
            Type type = instance.GetType();
            return ConnectionName(type);
        }

        /// <summary>
        /// Returns the connection name for the specified type or the proxied
        /// name if the connection name for the specified type has been proxied
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ConnectionName(Type type)
        {
            string real = RealConnectionName(type);
            string proxied = ProxiedConnectionName(type);

            if (!real.Equals(proxied))
            {
                return proxied;
            }
            else
            {
                return real;
            }
        }

        protected static string RealConnectionName(Type type)
        {
            TableAttribute tableAttr = type.GetCustomAttributeOfType<TableAttribute>();
            string value = string.Empty;
            if (tableAttr != null)
            {
                value = tableAttr.ConnectionName;
            }

            return value;
        }

        /// <summary>
        /// Returns the proxied connection name for the specified
        /// Type if the connection hasn't been proxied/redirected
        /// then the real connection name will be returned.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected static string ProxiedConnectionName(Type type)
        {
            string realName = RealConnectionName(type);
            if (_proxiedConnectionNames.ContainsKey(realName))
            {
                return _proxiedConnectionNames[realName];
            }
            return realName;
        }

        /// <summary>
        /// Stop redirecting the connection name for the specified
        /// type
        /// </summary>
        /// <param name="type"></param>
        public static void UnproxyConnection(Type type)
        {
            string realName = RealConnectionName(type);
            UnproxyConnection(realName);
        }

        /// <summary>
        /// Stop redirecting the connection name for the specified type
        /// </summary>
        /// <param name="realName"></param>
        public static void UnproxyConnection(string realName)
        {
            if (_proxiedConnectionNames.ContainsKey(realName))
            {
                _proxiedConnectionNames.Remove(realName);
            }
        }

        /// <summary>
        /// Causes calls to ConnectionName for the specified type to 
        /// return the specified newConnectionName.  This method must be
        /// called prior to any XXXRegistrar.Register(Type, "CxName") calls
        /// for example: SqlClientRegistrar.Register(typeof(Employee), "MyConnectionName")
        /// </summary>
        /// <param name="type"></param>
        /// <param name="newConnectionName"></param>
        public static void ProxyConnection(Type type, string newConnectionName)
        {
            string real = RealConnectionName(type);
            ProxyConnection(real, newConnectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalConnectionName"></param>
        /// <param name="newConnectionName"></param>
        public static void ProxyConnection(string originalConnectionName, string newConnectionName)
        {
            _proxiedConnectionNames.AddMissing(originalConnectionName, newConnectionName);
            _proxiedConnectionNames[originalConnectionName] = newConnectionName;
        }

        public string TableName()
        {
            return TableName(this);
        }
        
        public static string TableName(object instance)
        {
            Type type = instance.GetType();
            return TableName(type);
        }

        public static string TableName<T>() where T : Dao, new()
        {
            return TableName(typeof(T));
        }

        public static string TableName(Type type)
        {
            TableAttribute tableAttr = type.GetCustomAttributeOfType<TableAttribute>();
            string value = type.Name;
            if (tableAttr != null)
            {
                value = tableAttr.TableName;
            }

            return value;
        }

        public static string GetKeyColumnName<T>() where T: Dao, new()
        {
            return typeof(T).Construct<T>().KeyColumnName;
        }

        public static string GetKeyColumnName(Type type)
        {
            return ((Dao)type.Construct()).KeyColumnName;
        }

        long? _idValue;
        [Exclude]
        public long? IdValue
        {
            get
            {
                if (DataRow != null && DataRow.Table.Columns.Contains(KeyColumnName))
                {
                    object value = DataRow[KeyColumnName];
                    if (value != null && value != DBNull.Value)
                    {
                        try
                        {
                            _idValue = new long?(Convert.ToInt64(value));
                        }
                        catch
                        {
                            _idValue = null;
                        }
                    }
                }
                return _idValue;
            }
            set
            {
                _idValue = value;
            }
        }

        object _primaryKey;
        protected object PrimaryKey
        {
            get
            {
                if (DataRow != null && DataRow.Table.Columns.Contains(KeyColumnName))
                {
                    _primaryKey = DataRow[KeyColumnName];
                }

                return _primaryKey;
            }
        }

        [Exclude]
        public string KeyColumnName { get; protected set; }

        bool _isNew;
        /// <summary>
        /// Returns true if the current instance hasn't been committed
        /// </summary>
        [Exclude]
        public bool IsNew
        {
            get
            {
                if (IdValue != null && IdValue.HasValue)
                {
                    _isNew = false;
                }

                return _isNew;
            }
            set
            {
                _isNew = value;
            }
        }
        
        [Exclude]
        public Incubator ServiceProvider { get; protected internal set; }

        [Exclude]
        public DataRow DataRow { get; set; }
        
        protected internal DataRow ToDataRow()
        {
            return ToDataRow(this);
        }

        protected internal static DataRow ToDataRow(Dao instance)
        {
            if (instance.DataRow != null)
            {
                return instance.DataRow;
            }

            Type instanceType = instance.GetType();
            PropertyInfo[] properties = instanceType.GetProperties();

            DataTable table = new DataTable(TableName(instance));
            List<object> rowValues = new List<object>();
            foreach (PropertyInfo property in properties)
            {
                ColumnAttribute column;
                if (property.HasCustomAttributeOfType<ColumnAttribute>(true, out column))
                {
                    table.Columns.Add(column.Name);
                    rowValues.Add(property.GetValue(instance, null));
                }
            }

            return table.Rows.Add(rowValues.ToArray());
        }

        internal Dictionary<string, object> NewValues
        {
            get
            {
                return this._newValues;
            }
        }

        protected internal object GetOriginalValue(string columnName)
        {
            if (DataRow != null && DataRow.Table.Columns.Contains(columnName))
            {
                return DataRow[columnName];
            }
            else if (columnName.Equals(KeyColumnName))
            {
                return IdValue;
            }

            return null;
        }
        
        protected internal object GetCurrentValue(string columnName)
        {
            object result = null;
            if (columnName.Equals(KeyColumnName))
            {
                result = IdValue;
                if (result == null)
                {
                    result = PrimaryKey;
                }
            }
            else
            {
                if (_newValues.ContainsKey(columnName))
                {
                    result = _newValues[columnName];
                }
                else
                {
                    result = GetOriginalValue(columnName);
                }
            }
            return result;
        }

        protected string GetStringValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val != null && val != DBNull.Value)
            {
                return (string)val;
            }

            return string.Empty;
        }

        protected int? GetIntValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val != null && val != DBNull.Value)
            {
                return new int?(Convert.ToInt32(val));
            }

            return new int?();
        }

        protected long? GetLongValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val != null && val != DBNull.Value)
            {
                return new long?(Convert.ToInt64(val));
            }

            return new long?();
        }

        protected decimal? GetDecimalValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val != null && val != DBNull.Value)
            {
                return new decimal?((decimal)val);
            }

            return new decimal?();
        }

        protected bool? GetBooleanValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val != null && val != DBNull.Value)
            {
                if (val is long || val is int)
                {
                    return new bool?((long)val > 0);
                }
                else
                {
                    return new bool?((bool)val);
                }                
            }

            return new bool?(false);
        }

        protected byte[] GetByteValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val == null || val == DBNull.Value)
            {
                return new byte[] { };
            }
            else
            {
                return (byte[])val;
            }
        }

        protected DateTime GetDateTimeValue(string columnName)
        {
            object val = GetCurrentValue(columnName);
            if (val != null && val != DBNull.Value)
            {
                if (val is double)
                {
                    return ((double)val).ToDateTime();
                }
                else if (val is string)
                {
                    return DateTime.Parse((string)val);
                }
                else
                {
                    return (DateTime)val;
                }
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        protected internal void SetValue(string columnName, object value)
        {
            if (columnName.Equals(KeyColumnName))
            {
                if (value != null && value != DBNull.Value)
                {
                    IdValue = new long?(Convert.ToInt64(value));
                }
            }
            else if (this._newValues.ContainsKey(columnName))
            {
                this._newValues[columnName] = value;
            }
            else
            {
                this._newValues.Add(columnName, value);
            }
        }
    }
}
