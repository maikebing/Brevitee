using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class TestFkTableCollection : DaoCollection<TestFkTableColumns, TestFkTable>
    {
        public TestFkTableCollection() { }
        public TestFkTableCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
        public TestFkTableCollection(Query<TestFkTableColumns, TestFkTable> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
        public TestFkTableCollection(Query<TestFkTableColumns, TestFkTable> q, bool load) : base(q, load) { }
    }
}