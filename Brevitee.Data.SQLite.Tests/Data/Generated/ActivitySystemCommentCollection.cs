using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ActivitySystemCommentCollection: DaoCollection<ActivitySystemCommentColumns, ActivitySystemComment>
    { 
		public ActivitySystemCommentCollection(){}
		public ActivitySystemCommentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ActivitySystemCommentCollection(Query<ActivitySystemCommentColumns, ActivitySystemComment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ActivitySystemCommentCollection(Query<ActivitySystemCommentColumns, ActivitySystemComment> q, bool load) : base(q, load) { }
    }
}