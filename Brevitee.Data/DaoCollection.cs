using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Brevitee.Data
{
    public class DaoCollection<C, T> : PagedEnumerator<T>, IEnumerable<T>, ICommittable, IHasDataTable
        where C : IQueryFilter, IFilterToken, new()
        where T : Dao, new()
    {
        Book<T> _book;
        List<T> _values;
        DataTable _table;

        Dao _parent;

        ConstructorInfo _ctor;

        public static implicit operator DaoCollection<C, T>(DataTable table)
        {
            return new DaoCollection<C, T>(table);
        }

        public DaoCollection()
        {
            this._book = new Book<T>();
            this._values = new List<T>();
        }

        public DaoCollection(DataTable table, Dao parent = null, string referencingColumn = null)
            : this()
        {
            this._parent = parent;
            this._table = table;
            this.ReferencingColumn = referencingColumn;

            SetDataTable(table);
        }

        public DaoCollection(Query<C, T> query, Dao parent = null, string referencingColumn = null): this()
        {
            this._parent = parent;
            this.Query = query;
            this.ReferencingColumn = referencingColumn;
        }

        public DaoCollection(Query<C, T> query, bool load = false): this(query, null, null)
        {
            if (load)
            {
                Load();
            }
        }

        public Co Convert<Co>() where Co : DaoCollection<C, T>, IHasDataTable, new()
        {
            Co val = As<Co>();
            val.Parent = this.Parent;
            return val;
        }

        protected string ReferencingColumn
        {
            get;
            set;
        }

        protected Query<C, T> Query
        {
            get;
            set;
        }

        public DataRow DataRow
        {
            get
            {
                return DataTable.Rows[0];
            }
            set { }
        }

        /// <summary>
        /// Instantiates a new instance of T and calls SetDataTable passing
        /// in the DataTable from the current instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T As<T>() where T : IHasDataTable, new()
        {
            T val = new T();
            val.SetDataTable(this.DataTable);
            return val;
        }

        public bool Loaded
        {
            get;
            set;
        }

        public void Load()
        {
            if (Query == null)
            {
                throw new ArgumentNullException("Query is not set");
            }

            SetDataTable(Query.GetDataTable());            
        }

        public void Reload()
        {
            Load();
        }

        public void SetDataTable(DataTable table)
        {
            Initialize(table);
            this.Reset();
            Loaded = true;
        }

        public Dao Parent
        {
            get
            {
                return this._parent;
            }
            protected set
            {
                this._parent = value;
            }
        }
        
        private void Initialize(DataTable table)
        {
            _ctor = typeof(T).GetConstructor(new Type[] { typeof(DataRow) });
            _values = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T dao = (T)_ctor.Invoke(new object[] { row });
                _values.Add(dao);
            }
            this._book = new Book<T>(_values);
        }
        
        public DataTable DataTable
        {
            get { return this._table; }
            set { this._table = value; }
        }

        public T this[int index]
        {
            get
            {
                return this._values[index];
            }
        }

        public T AddNew()
        {
            T dao = new T();

            Add(dao);

            return dao;
        }

        public virtual void Add(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (_parent != null)
            {
                AssociateToParent(instance);
            }

            this._values.Add(instance);
            this._book = new Book<T>(this._values);
        }

        public virtual void AddRange(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            if (_parent != null)
            {
                foreach (T val in values)
                {
                    AssociateToParent(val);
                }
            }

            this._values.AddRange(values);
            this._book = new Book<T>(this._values);
        }

        private void AssociateToParent(T instance)
        {
            Type childType = instance.GetType();

            ValidateParent();

            // from the parent get the ReferencedBy Attribute that matches the referencingClass name
            PropertyInfo[] properties = childType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                ForeignKeyAttribute fk;
                if (property.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                {
                    if (fk.ReferencedTable.Equals(Dao.TableName(_parent)))
                    {
                        property.SetValue(instance, _parent.IdValue, null);
                    }
                }
            }
        }

        protected void ValidateParent()
        {
            if (_parent == null)
            {
                throw new ArgumentNullException(string.Format("{0}.Parent", this.GetType().Name));
            }

            if (_parent.IsNew || _parent.IdValue == null)
            {
                throw new InvalidOperationException("The parent hasn't been committed, unable to associate child by id");
            }            
        }

        public void Save()
        {
            Commit();
        }

        public void Commit()
        {
            Database db = _.Db.For<T>();
            SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
            WriteCommit(sql);

            sql.Execute(db);
        }

        public void WriteCommit(SqlStringBuilder sql)
        {
            foreach (T dao in this._values)
            {
                dao.WriteCommit(sql);
            }
        }

        public void Delete(Database db = null)
        {
            if (db == null)
            {
                db = _.Db.For<T>();
            }
            SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
            WriteDelete(sql);

            sql.Execute(db);
        }

        public virtual void WriteDelete(SqlStringBuilder sql)
        {
            if (this._values.Count > 0)
            {
                bool deleteIndividually = Parent == null;

                if (!deleteIndividually)
                {
                    if (string.IsNullOrEmpty(ReferencingColumn))
                    {
                        throw new ArgumentNullException("{0}.ReferencingColumn not set", this.GetType().Name);
                    }

                    sql.Delete(Dao.TableName(typeof(T)))
                        .Where(new AssignValue(ReferencingColumn, Parent.IdValue))
                        .Go();
                }
                
                foreach (Dao d in this)
                {
                    if (d.AutoDeleteChildren)
                    {
                        d.WriteChildDeletes(sql);
                        sql.Go();
                    }

                    if (deleteIndividually)
                    {
                        d.WriteDelete(sql);
                        sql.Go();
                    }
                }
                
            }
        }

        public List<T> Sorted(Comparison<T> comparison)
        {
            T[] results = new T[this._values.Count];
            _values.CopyTo(results);
            List<T> sorter = new List<T>(results);
            sorter.Sort(comparison);

            return sorter;
        }

        /// <summary>
        /// Gets one value if it exists, creates it if it doesn't.  Throws MultipleEntriesFoundException
        /// if more than one value is in this collection.
        /// </summary>
        /// <returns></returns>
        public T JustOne(bool saveIfNew = false)
        {
            if (this.Count > 1)
            {
                throw new MultipleEntriesFoundException();
            }

            T result = this.FirstOrDefault();
            if (result == null)
            {
                result = AddNew();
                if (saveIfNew)
                {
                    result.Save();
                }
            }

            return result;
        }

        public object[] ToJsonSafe()
        {
            object[] result = new object[this.Count];
            this.Each((o, i) =>
            {
                result[i] = o.ToJsonSafe();
            });
            return result;
        }
        /// <summary>
        /// Get the 1 based page number or an empty list
        /// if the specified page number is not found.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public List<T> GetPage(int pageNum)
        {
            return _book[pageNum - 1];
        }    

        public int PageCount
        {
            get
            {
                return this._book.PageCount;
            }
        }

        public int Count
        {
            get
            {
                return this._book.ItemCount;
            }
        }

        public int PageSize
        {
            get
            {
                return this._book.PageSize;
            }
            set
            {
                this._book.PageSize = value;
            }
        }

        public override bool MoveNextPage()
        {
            CurrentPageIndex++;
            if (CurrentPageIndex >= _book.PageCount)
            {
                return false;
            }

            this.CurrentPage = this._book[CurrentPageIndex];
            return true;
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion
    }
}
