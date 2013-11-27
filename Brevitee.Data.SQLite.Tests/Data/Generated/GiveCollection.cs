using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class GiveCollection: DaoCollection<GiveColumns, Give>
    { 
		public GiveCollection(){}
		public GiveCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public GiveCollection(Query<GiveColumns, Give> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public GiveCollection(Query<GiveColumns, Give> q, bool load) : base(q, load) { }
    }
}