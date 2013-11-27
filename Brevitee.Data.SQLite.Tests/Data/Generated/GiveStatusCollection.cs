using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class GiveStatusCollection: DaoCollection<GiveStatusColumns, GiveStatus>
    { 
		public GiveStatusCollection(){}
		public GiveStatusCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public GiveStatusCollection(Query<GiveStatusColumns, GiveStatus> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public GiveStatusCollection(Query<GiveStatusColumns, GiveStatus> q, bool load) : base(q, load) { }
    }
}