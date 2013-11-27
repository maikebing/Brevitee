using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class NeedCollection: DaoCollection<NeedColumns, Need>
    { 
		public NeedCollection(){}
		public NeedCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public NeedCollection(Query<NeedColumns, Need> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public NeedCollection(Query<NeedColumns, Need> q, bool load) : base(q, load) { }
    }
}