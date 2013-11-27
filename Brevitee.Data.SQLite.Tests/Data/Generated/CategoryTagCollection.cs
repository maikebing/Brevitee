using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class CategoryTagCollection: DaoCollection<CategoryTagColumns, CategoryTag>
    { 
		public CategoryTagCollection(){}
		public CategoryTagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CategoryTagCollection(Query<CategoryTagColumns, CategoryTag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CategoryTagCollection(Query<CategoryTagColumns, CategoryTag> q, bool load) : base(q, load) { }
    }
}