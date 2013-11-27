using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef
{
    [Brevitee.Data.Table("TestTable", "DaoRef")]
    public partial class TestTable : Dao
    {
        public TestTable()
            : base()
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        public TestTable(DataRow data)
            : base(data)
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        private void SetChildren()
        {

            this.ChildCollections.Add("TestFkTable_TestTableId", new TestFkTableCollection(new Query<TestFkTableColumns, TestFkTable>((c) => c.TestTableId == this.Id), this, "TestTableId"));
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
        [Brevitee.Data.Column(Name = "Name", ExtractedType = "NVarChar", MaxLength = "255", AllowNull = false)]
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

        // property:Description, columnName:Description	
        [Brevitee.Data.Column(Name = "Description", ExtractedType = "NVarChar", MaxLength = "MAX", AllowNull = true)]
        public string Description
        {
            get
            {
                return GetStringValue("Description");
            }
            set
            {
                SetValue("Description", value);
            }
        }



        [Exclude]
        public TestFkTableCollection TestFkTableCollectionByTestTableId
        {
            get
            {
                if (!this.ChildCollections.ContainsKey("TestFkTable_TestTableId"))
                {
                    SetChildren();
                }

                var c = (TestFkTableCollection)this.ChildCollections["TestFkTable_TestTableId"];
                if (!c.Loaded)
                {
                    c.Load();
                }
                return c;
            }
        }

        public override IQueryFilter GetUniqueFilter()
        {
            var colFilter = new TestTableColumns();
            return (colFilter.Id == IdValue);
        }

        public static TestTableCollection Where(Func<TestTableColumns, QueryFilter<TestTableColumns>> where, OrderBy<TestTableColumns> orderBy = null)
        {
            return new TestTableCollection(new Query<TestTableColumns, TestTable>(where, orderBy), true);
        }

        public static TestTableCollection Where(WhereDelegate<TestTableColumns> where, Database db = null)
        {
            return new TestTableCollection(new Query<TestTableColumns, TestTable>(where, db), true);
        }

        public static TestTableCollection Where(WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy = null, Database db = null)
        {
            return new TestTableCollection(new Query<TestTableColumns, TestTable>(where, orderBy, db), true);
        }

        public static TestTableCollection Where(QiQuery where, Database db = null)
        {
            return new TestTableCollection(Select<TestTableColumns>.From<TestTable>().Where(where, db));
        }

        public static TestTable OneWhere(WhereDelegate<TestTableColumns> where, Database db = null)
        {
            var results = new TestTableCollection(Select<TestTableColumns>.From<TestTable>().Where(where, db));
            return OneOrThrow(results);
        }

        public static TestTable OneWhere(QiQuery where, Database db = null)
        {
            var results = new TestTableCollection(Select<TestTableColumns>.From<TestTable>().Where(where, db));
            return OneOrThrow(results);
        }

        private static TestTable OneOrThrow(TestTableCollection c)
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

        public static TestTable FirstOneWhere(WhereDelegate<TestTableColumns> where, Database db = null)
        {
            var results = new TestTableCollection(Select<TestTableColumns>.From<TestTable>().Where(where, db));
            if (results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public static TestTableCollection Top(int count, WhereDelegate<TestTableColumns> where, Database db = null)
        {
            return Top(count, where, null, db);
        }

        public static TestTableCollection Top(int count, WhereDelegate<TestTableColumns> where, OrderBy<TestTableColumns> orderBy, Database database = null)
        {
            TestTableColumns c = new TestTableColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<TestTable>() : database;
            QuerySet query = GetQuerySet(db);
            query.Top<TestTable>(count);
            query.Where(filter);

            if (orderBy != null)
            {
                query.OrderBy<TestTableColumns>(orderBy);
            }

            query.Execute(db);
            return query.Results.As<TestTableCollection>(0);
        }

        public static long Count(WhereDelegate<TestTableColumns> where, Database database = null)
        {
            TestTableColumns c = new TestTableColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<TestTable>() : database;
            QuerySet query = GetQuerySet(db);
            query.Count<TestTable>();
            query.Where(filter);
            query.Execute(db);
            return query.Results.As<CountResult>(0).Value;
        }
    }
}
