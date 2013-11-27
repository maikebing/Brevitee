using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ReviewCommentCollection: DaoCollection<ReviewCommentColumns, ReviewComment>
    { 
		public ReviewCommentCollection(){}
		public ReviewCommentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ReviewCommentCollection(Query<ReviewCommentColumns, ReviewComment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ReviewCommentCollection(Query<ReviewCommentColumns, ReviewComment> q, bool load) : base(q, load) { }
    }
}