using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ReviewCollection: DaoCollection<ReviewColumns, Review>
    { 
		public ReviewCollection(){}
		public ReviewCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ReviewCollection(Query<ReviewColumns, Review> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ReviewCollection(Query<ReviewColumns, Review> q, bool load) : base(q, load) { }
    }
}