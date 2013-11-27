using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ContentCollection: DaoCollection<ContentColumns, Content>
    { 
		public ContentCollection(){}
		public ContentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ContentCollection(Query<ContentColumns, Content> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ContentCollection(Query<ContentColumns, Content> q, bool load) : base(q, load) { }
    }
}