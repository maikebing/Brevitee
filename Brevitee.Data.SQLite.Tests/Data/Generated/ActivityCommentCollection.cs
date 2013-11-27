using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ActivityCommentCollection: DaoCollection<ActivityCommentColumns, ActivityComment>
    { 
		public ActivityCommentCollection(){}
		public ActivityCommentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ActivityCommentCollection(Query<ActivityCommentColumns, ActivityComment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ActivityCommentCollection(Query<ActivityCommentColumns, ActivityComment> q, bool load) : base(q, load) { }
    }
}