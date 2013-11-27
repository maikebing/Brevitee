using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class WantCollection: DaoCollection<WantColumns, Want>
    { 
		public WantCollection(){}
		public WantCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WantCollection(Query<WantColumns, Want> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WantCollection(Query<WantColumns, Want> q, bool load) : base(q, load) { }
    }
}