using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ItemReviewCollection: DaoCollection<ItemReviewColumns, ItemReview>
    { 
		public ItemReviewCollection(){}
		public ItemReviewCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ItemReviewCollection(Query<ItemReviewColumns, ItemReview> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ItemReviewCollection(Query<ItemReviewColumns, ItemReview> q, bool load) : base(q, load) { }
    }
}