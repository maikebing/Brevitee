using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class HaveStatusCollection: DaoCollection<HaveStatusColumns, HaveStatus>
    { 
		public HaveStatusCollection(){}
		public HaveStatusCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HaveStatusCollection(Query<HaveStatusColumns, HaveStatus> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HaveStatusCollection(Query<HaveStatusColumns, HaveStatus> q, bool load) : base(q, load) { }
    }
}