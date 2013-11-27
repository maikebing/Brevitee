using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class PinboardPostingCollection: DaoCollection<PinboardPostingColumns, PinboardPosting>
    { 
		public PinboardPostingCollection(){}
		public PinboardPostingCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PinboardPostingCollection(Query<PinboardPostingColumns, PinboardPosting> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PinboardPostingCollection(Query<PinboardPostingColumns, PinboardPosting> q, bool load) : base(q, load) { }
    }
}