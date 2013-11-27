using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef
{
    [Brevitee.Data.Table("DaoReferenceObject", "DaoReferenceObjects")]
    public partial class DaoReferenceObject : Dao
    {
        public DaoReferenceObject()
            : base()
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        public DaoReferenceObject(DataRow data)
            : base(data)
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        private void SetChildren()
        {

            this.ChildCollections.Add("DaoReferenceObjectWithForeignKey_DaoReferenceObjectId", new DaoReferenceObjectWithForeignKeyCollection(new Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>((c) => c.DaoReferenceObjectId == this.Id), this, "DaoReferenceObjectId"));
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

        // property:IntProperty, columnName:IntProperty	
        [Brevitee.Data.Column(Name = "IntProperty", ExtractedType = "Int", MaxLength = "4", AllowNull = true)]
        public int? IntProperty
        {
            get
            {
                return GetIntValue("IntProperty");
            }
            set
            {
                SetValue("IntProperty", value);
            }
        }

        // property:DecimalProperty, columnName:DecimalProperty	
        [Brevitee.Data.Column(Name = "DecimalProperty", ExtractedType = "Decimal", MaxLength = "9", AllowNull = true)]
        public decimal? DecimalProperty
        {
            get
            {
                return GetDecimalValue("DecimalProperty");
            }
            set
            {
                SetValue("DecimalProperty", value);
            }
        }

        // property:LongProperty, columnName:LongProperty	
        [Brevitee.Data.Column(Name = "LongProperty", ExtractedType = "BigInt", MaxLength = "8", AllowNull = true)]
        public long? LongProperty
        {
            get
            {
                return GetLongValue("LongProperty");
            }
            set
            {
                SetValue("LongProperty", value);
            }
        }

        // property:DateTimeProperty, columnName:DateTimeProperty	
        [Brevitee.Data.Column(Name = "DateTimeProperty", ExtractedType = "DateTime", MaxLength = "8", AllowNull = true)]
        public DateTime DateTimeProperty
        {
            get
            {
                return GetDateTimeValue("DateTimeProperty");
            }
            set
            {
                SetValue("DateTimeProperty", value);
            }
        }

        // property:BoolProperty, columnName:BoolProperty	
        [Brevitee.Data.Column(Name = "BoolProperty", ExtractedType = "Bit", MaxLength = "1", AllowNull = true)]
        public bool? BoolProperty
        {
            get
            {
                return GetBooleanValue("BoolProperty");
            }
            set
            {
                SetValue("BoolProperty", value);
            }
        }

        // property:GuidProperty, columnName:GuidProperty	
        [Brevitee.Data.Column(Name = "GuidProperty", ExtractedType = "UniqueIdentifier", MaxLength = "16", AllowNull = true)]
        public string GuidProperty
        {
            get
            {
                return GetStringValue("GuidProperty");
            }
            set
            {
                SetValue("GuidProperty", value);
            }
        }

        // property:DoubleProperty, columnName:DoubleProperty	
        [Brevitee.Data.Column(Name = "DoubleProperty", ExtractedType = "Float", MaxLength = "8", AllowNull = true)]
        public string DoubleProperty
        {
            get
            {
                return GetStringValue("DoubleProperty");
            }
            set
            {
                SetValue("DoubleProperty", value);
            }
        }

        // property:ByteArrayProperty, columnName:ByteArrayProperty	
        [Brevitee.Data.Column(Name = "ByteArrayProperty", ExtractedType = "VarBinary", MaxLength = "MAX", AllowNull = true)]
        public byte[] ByteArrayProperty
        {
            get
            {
                return GetByteValue("ByteArrayProperty");
            }
            set
            {
                SetValue("ByteArrayProperty", value);
            }
        }

        // property:StringProperty, columnName:StringProperty	
        [Brevitee.Data.Column(Name = "StringProperty", ExtractedType = "VarChar", MaxLength = "50", AllowNull = true)]
        public string StringProperty
        {
            get
            {
                return GetStringValue("StringProperty");
            }
            set
            {
                SetValue("StringProperty", value);
            }
        }



        [Exclude]
        public DaoReferenceObjectWithForeignKeyCollection DaoReferenceObjectWithForeignKeyCollectionByDaoReferenceObjectId
        {
            get
            {
                if (!this.ChildCollections.ContainsKey("DaoReferenceObjectWithForeignKey_DaoReferenceObjectId"))
                {
                    SetChildren();
                }

                var c = (DaoReferenceObjectWithForeignKeyCollection)this.ChildCollections["DaoReferenceObjectWithForeignKey_DaoReferenceObjectId"];
                if (!c.Loaded)
                {
                    c.Load();
                }
                return c;
            }
        }

        public override IQueryFilter GetUniqueFilter()
        {
            var colFilter = new DaoReferenceObjectColumns();
            return (colFilter.Id == IdValue);
        }

        public static DaoReferenceObjectCollection Where(Func<DaoReferenceObjectColumns, QueryFilter<DaoReferenceObjectColumns>> where, OrderBy<DaoReferenceObjectColumns> orderBy = null)
        {
            return new DaoReferenceObjectCollection(new Query<DaoReferenceObjectColumns, DaoReferenceObject>(where, orderBy), true);
        }

        public static DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
        {
            return new DaoReferenceObjectCollection(new Query<DaoReferenceObjectColumns, DaoReferenceObject>(where, db), true);
        }

        public static DaoReferenceObjectCollection Where(WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy = null, Database db = null)
        {
            return new DaoReferenceObjectCollection(new Query<DaoReferenceObjectColumns, DaoReferenceObject>(where, orderBy, db), true);
        }

        public static DaoReferenceObjectCollection Where(QiQuery where, Database db = null)
        {
            return new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
        }

        public static DaoReferenceObject OneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
        {
            var results = new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
            return OneOrThrow(results);
        }

        public static DaoReferenceObject OneWhere(QiQuery where, Database db = null)
        {
            var results = new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
            return OneOrThrow(results);
        }

        private static DaoReferenceObject OneOrThrow(DaoReferenceObjectCollection c)
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

        public static DaoReferenceObject FirstOneWhere(WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
        {
            var results = new DaoReferenceObjectCollection(Select<DaoReferenceObjectColumns>.From<DaoReferenceObject>().Where(where, db));
            if (results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public static DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, Database db = null)
        {
            return Top(count, where, null, db);
        }

        public static DaoReferenceObjectCollection Top(int count, WhereDelegate<DaoReferenceObjectColumns> where, OrderBy<DaoReferenceObjectColumns> orderBy, Database database = null)
        {
            DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<DaoReferenceObject>() : database;
            QuerySet query = GetQuerySet(db);
            query.Top<DaoReferenceObject>(count);
            query.Where(filter);

            if (orderBy != null)
            {
                query.OrderBy<DaoReferenceObjectColumns>(orderBy);
            }

            query.Execute(db);
            return query.Results.As<DaoReferenceObjectCollection>(0);
        }

        public static long Count(WhereDelegate<DaoReferenceObjectColumns> where, Database database = null)
        {
            DaoReferenceObjectColumns c = new DaoReferenceObjectColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<DaoReferenceObject>() : database;
            QuerySet query = GetQuerySet(db);
            query.Count<DaoReferenceObject>();
            query.Where(filter);
            query.Execute(db);
            return query.Results.As<CountResult>(0).Value;
        }
    }
}
