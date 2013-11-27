using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class HaveCollection: DaoCollection<HaveColumns, Have>
    { 
		public HaveCollection(){}
		public HaveCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public HaveCollection(Query<HaveColumns, Have> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public HaveCollection(Query<HaveColumns, Have> q, bool load) : base(q, load) { }
    }
}