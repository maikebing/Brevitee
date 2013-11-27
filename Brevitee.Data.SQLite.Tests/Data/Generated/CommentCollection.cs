using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class CommentCollection: DaoCollection<CommentColumns, Comment>
    { 
		public CommentCollection(){}
		public CommentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CommentCollection(Query<CommentColumns, Comment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CommentCollection(Query<CommentColumns, Comment> q, bool load) : base(q, load) { }
    }
}