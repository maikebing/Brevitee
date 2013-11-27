using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class AccessRequestCollection: DaoCollection<AccessRequestColumns, AccessRequest>
    { 
		public AccessRequestCollection(){}
		public AccessRequestCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AccessRequestCollection(Query<AccessRequestColumns, AccessRequest> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AccessRequestCollection(Query<AccessRequestColumns, AccessRequest> q, bool load) : base(q, load) { }
    }
}