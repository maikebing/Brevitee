using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserReviewCollection: DaoCollection<UserReviewColumns, UserReview>
    { 
		public UserReviewCollection(){}
		public UserReviewCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserReviewCollection(Query<UserReviewColumns, UserReview> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserReviewCollection(Query<UserReviewColumns, UserReview> q, bool load) : base(q, load) { }
    }
}