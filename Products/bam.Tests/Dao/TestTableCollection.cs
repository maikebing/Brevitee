using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.DaoRef
{
    public class TestTableCollection: DaoCollection<TestTableColumns, TestTable>
    { 
		public TestTableCollection(){}
		public TestTableCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public TestTableCollection(Query<TestTableColumns, TestTable> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public TestTableCollection(Query<TestTableColumns, TestTable> q, bool load) : base(q, load) { }
    }
}