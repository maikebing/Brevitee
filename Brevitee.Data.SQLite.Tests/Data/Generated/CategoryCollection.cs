using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class CategoryCollection: DaoCollection<CategoryColumns, Category>
    { 
		public CategoryCollection(){}
		public CategoryCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CategoryCollection(Query<CategoryColumns, Category> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CategoryCollection(Query<CategoryColumns, Category> q, bool load) : base(q, load) { }
    }
}