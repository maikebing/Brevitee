using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserItemReviewCollection: DaoCollection<UserItemReviewColumns, UserItemReview>
    { 
		public UserItemReviewCollection(){}
		public UserItemReviewCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserItemReviewCollection(Query<UserItemReviewColumns, UserItemReview> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserItemReviewCollection(Query<UserItemReviewColumns, UserItemReview> q, bool load) : base(q, load) { }
    }
}