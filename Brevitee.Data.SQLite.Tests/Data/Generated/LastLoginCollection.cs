using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class LastLoginCollection: DaoCollection<LastLoginColumns, LastLogin>
    { 
		public LastLoginCollection(){}
		public LastLoginCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LastLoginCollection(Query<LastLoginColumns, LastLogin> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LastLoginCollection(Query<LastLoginColumns, LastLogin> q, bool load) : base(q, load) { }
    }
}