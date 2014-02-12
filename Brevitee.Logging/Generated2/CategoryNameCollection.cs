using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class CategoryNameCollection: DaoCollection<CategoryNameColumns, CategoryName>
    { 
		public CategoryNameCollection(){}
		public CategoryNameCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CategoryNameCollection(Query<CategoryNameColumns, CategoryName> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CategoryNameCollection(Query<CategoryNameColumns, CategoryName> q, bool load) : base(q, load) { }
    }
}