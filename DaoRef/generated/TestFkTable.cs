using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef
{
    [Brevitee.Data.Table("TestFkTable", "DaoRef")]
    public partial class TestFkTable : Dao
    {
        public TestFkTable()
            : base()
        {
            this.KeyColumnName = "Id";
            this.SetChildren();
        }

        public TestFkTable(DataRow data)
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


        // start TestTableId -> TestTableId
        [Brevitee.Data.ForeignKey(
            Table = "TestFkTable",
            Name = "TestTableId",
            ExtractedType = "BigInt",
            MaxLength = "8",
            AllowNull = false,
            ReferencedKey = "Id",
            ReferencedTable = "TestTable",
            Suffix = "1")]
        public long? TestTableId
        {
            get
            {
                return GetLongValue("TestTableId");
            }
            set
            {
                SetValue("TestTableId", value);
            }
        }

        TestTable _testTableOfTestTableId;
        public TestTable TestTableOfTestTableId
        {
            get
            {
                if (_testTableOfTestTableId == null)
                {
                    _testTableOfTestTableId = Brevitee.DaoRef.TestTable.OneWhere(f => f.Id == this.TestTableId);
                }
                return _testTableOfTestTableId;
            }
        }



        public override IQueryFilter GetUniqueFilter()
        {
            var colFilter = new TestFkTableColumns();
            return (colFilter.Id == IdValue);
        }

        public static TestFkTableCollection Where(Func<TestFkTableColumns, QueryFilter<TestFkTableColumns>> where, OrderBy<TestFkTableColumns> orderBy = null)
        {
            return new TestFkTableCollection(new Query<TestFkTableColumns, TestFkTable>(where, orderBy), true);
        }

        public static TestFkTableCollection Where(WhereDelegate<TestFkTableColumns> where, Database db = null)
        {
            return new TestFkTableCollection(new Query<TestFkTableColumns, TestFkTable>(where, db), true);
        }

        public static TestFkTableCollection Where(WhereDelegate<TestFkTableColumns> where, OrderBy<TestFkTableColumns> orderBy = null, Database db = null)
        {
            return new TestFkTableCollection(new Query<TestFkTableColumns, TestFkTable>(where, orderBy, db), true);
        }

        public static TestFkTableCollection Where(QiQuery where, Database db = null)
        {
            return new TestFkTableCollection(Select<TestFkTableColumns>.From<TestFkTable>().Where(where, db));
        }

        public static TestFkTable OneWhere(WhereDelegate<TestFkTableColumns> where, Database db = null)
        {
            var results = new TestFkTableCollection(Select<TestFkTableColumns>.From<TestFkTable>().Where(where, db));
            return OneOrThrow(results);
        }

        public static TestFkTable OneWhere(QiQuery where, Database db = null)
        {
            var results = new TestFkTableCollection(Select<TestFkTableColumns>.From<TestFkTable>().Where(where, db));
            return OneOrThrow(results);
        }

        private static TestFkTable OneOrThrow(TestFkTableCollection c)
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

        public static TestFkTable FirstOneWhere(WhereDelegate<TestFkTableColumns> where, Database db = null)
        {
            var results = new TestFkTableCollection(Select<TestFkTableColumns>.From<TestFkTable>().Where(where, db));
            if (results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public static TestFkTableCollection Top(int count, WhereDelegate<TestFkTableColumns> where, Database db = null)
        {
            return Top(count, where, null, db);
        }

        public static TestFkTableCollection Top(int count, WhereDelegate<TestFkTableColumns> where, OrderBy<TestFkTableColumns> orderBy, Database database = null)
        {
            TestFkTableColumns c = new TestFkTableColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<TestFkTable>() : database;
            QuerySet query = GetQuerySet(db);
            query.Top<TestFkTable>(count);
            query.Where(filter);

            if (orderBy != null)
            {
                query.OrderBy<TestFkTableColumns>(orderBy);
            }

            query.Execute(db);
            return query.Results.As<TestFkTableCollection>(0);
        }

        public static long Count(WhereDelegate<TestFkTableColumns> where, Database database = null)
        {
            TestFkTableColumns c = new TestFkTableColumns();
            IQueryFilter filter = where(c);

            Database db = database == null ? _.Db.For<TestFkTable>() : database;
            QuerySet query = GetQuerySet(db);
            query.Count<TestFkTable>();
            query.Where(filter);
            query.Execute(db);
            return query.Results.As<CountResult>(0).Value;
        }
    }
}
