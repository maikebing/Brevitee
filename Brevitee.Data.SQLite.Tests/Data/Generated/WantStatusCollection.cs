using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class WantStatusCollection: DaoCollection<WantStatusColumns, WantStatus>
    { 
		public WantStatusCollection(){}
		public WantStatusCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WantStatusCollection(Query<WantStatusColumns, WantStatus> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WantStatusCollection(Query<WantStatusColumns, WantStatus> q, bool load) : base(q, load) { }
    }
}