using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef
{
    [Brevitee.Data.Table("DaoReferenceObjectWithForeignKey", "DaoReferenceObjects")]
    public partial class DaoReferenceObjectWithForeignKey : Dao
    {
        public DaoReferenceObjectWithForeignKey()
            : base()
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        public DaoReferenceObjectWithForeignKey(DataRow data)
            : base(data)
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        private void SetChildren()
        {

        }

        // property:Id, columnName:Id	
        [Exclude]
        [Brevitee.Data.KeyColumn(Name = "Id", ExtractedType = "BigInt", MaxLength = "8")]
        public long? Id
        {
            get
            {
                return GetLongValue("Id");
            }
            set
            {
                SetValue("Id", value);
            }
        }

        // property:Name, columnName:Name	
        [Brevitee.Data.Column(Name = "Name", ExtractedType = "NVarChar", MaxLength = "50", AllowNull = false)]
        public string Name
        {
            get
            {
                return GetStringValue("Name");
            }
            set
            {
                SetValue("Name", value);
            }
        }


        // start DaoReferenceObjectId -> DaoReferenceObjectId
        [Brevitee.Data.ForeignKey(
            Table = "DaoReferenceObjectWithForeignKey",
            Name = "DaoReferenceObjectId",
            ExtractedType = "BigInt",
            MaxLength = "8",
            AllowNull = false,
            ReferencedKey = "Id",
            ReferencedTable = "DaoReferenceObject",
            Suffix = "1")]
        public long? DaoReferenceObjectId
        {
            get
            {
                return GetLongValue("DaoReferenceObjectId");
            }
            set
            {
                SetValue("DaoReferenceObjectId", value);
            }
        }

        DaoReferenceObject _daoReferenceObjectOfDaoReferenceObjectId;
        public DaoReferenceObject DaoReferenceObjectOfDaoReferenceObjectId
        {
            get
            {
                if (_daoReferenceObjectOfDaoReferenceObjectId == null)
                {
                    _daoReferenceObjectOfDaoReferenceObjectId = Brevitee.DaoRef.DaoReferenceObject.OneWhere(f => f.Id == this.DaoReferenceObjectId);
                }
                return _daoReferenceObjectOfDaoReferenceObjectId;
            }
        }



        public override IQueryFilter GetUniqueFilter()
        {
            var colFilter = new DaoReferenceObjectWithForeignKeyColumns();
            return (colFilter.Id == IdValue);
        }

        public static DaoReferenceObjectWithForeignKeyCollection Where(Func<DaoReferenceObjectWithForeignKeyColumns, QueryFilter<DaoReferenceObjectWithForeignKeyColumns>> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy = null)
        {
            return new DaoReferenceObjectWithForeignKeyCollection(new Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>(where, orderBy), true);
        }

        public static DaoReferenceObjectWithForeignKeyCollection Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
        {
            return new DaoReferenceObjectWithForeignKeyCollection(new Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>(where, db), true);
        }

        public static DaoReferenceObjectWithForeignKeyCollection Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy = null, Database db = null)
        {
            return new DaoReferenceObjectWithForeignKeyCollection(new Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>(where, orderBy, db), true);
        }

        public static DaoReferenceObjectWithForeignKeyCollection Where(QiQuery where, Database db = null)
        {
            return new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
        }

        public static DaoReferenceObjectWithForeignKey OneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
        {
            var results = new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
            return OneOrThrow(results);
        }

        public static DaoReferenceObjectWithForeignKey OneWhere(QiQuery where, Database db = null)
        {
            var results = new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
            return OneOrThrow(results);
        }

        private static DaoReferenceObjectWithForeignKey OneOrThrow(DaoReferenceObjectWithForeignKeyCollection c)
        {
            if (c.Count == 1)
            {
                return c[0];
            }
            else if (c.Count > 1)
            {
                throw new MultipleEntriesFoundException();
            }

            return null;
        }

        public static DaoReferenceObjectWithForeignKey FirstOneWhere(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
        {
            var results = new DaoReferenceObjectWithForeignKeyCollection(Select<DaoReferenceObjectWithForeignKeyColumns>.From<DaoReferenceObjectWithForeignKey>().Where(where, db));
            if (results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public static DaoReferenceObjectWithForeignKeyCollection Top(int count, WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database db = null)
        {
            return Top(count, where, null, db);
        }

        public static DaoReferenceObjectWithForeignKeyCollection Top(int count, WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy, Database database = null)
        {
            DaoReferenceObjectWithForeignKeyColumns c = new DaoReferenceObjectWithForeignKeyColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<DaoReferenceObjectWithForeignKey>() : database;
            QuerySet query = GetQuerySet(db);
            query.Top<DaoReferenceObjectWithForeignKey>(count);
            query.Where(filter);

            if (orderBy != null)
            {
                query.OrderBy<DaoReferenceObjectWithForeignKeyColumns>(orderBy);
            }

            query.Execute(db);
            return query.Results.As<DaoReferenceObjectWithForeignKeyCollection>(0);
        }

        public static long Count(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, Database database = null)
        {
            DaoReferenceObjectWithForeignKeyColumns c = new DaoReferenceObjectWithForeignKeyColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<DaoReferenceObjectWithForeignKey>() : database;
            QuerySet query = GetQuerySet(db);
            query.Count<DaoReferenceObjectWithForeignKey>();
            query.Where(filter);
            query.Execute(db);
            return query.Results.As<CountResult>(0).Value;
        }
    }
}
