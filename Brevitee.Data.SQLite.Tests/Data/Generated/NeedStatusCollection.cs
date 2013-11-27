using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class NeedStatusCollection: DaoCollection<NeedStatusColumns, NeedStatus>
    { 
		public NeedStatusCollection(){}
		public NeedStatusCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public NeedStatusCollection(Query<NeedStatusColumns, NeedStatus> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public NeedStatusCollection(Query<NeedStatusColumns, NeedStatus> q, bool load) : base(q, load) { }
    }
}