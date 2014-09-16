using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Classification
{
    public class CategoryCollection: DaoCollection<CategoryColumns, Category>
    { 
		public CategoryCollection(){}
		public CategoryCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CategoryCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CategoryCollection(Query<CategoryColumns, Category> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CategoryCollection(Database db, Query<CategoryColumns, Category> q, bool load) : base(db, q, load) { }
		public CategoryCollection(Query<CategoryColumns, Category> q, bool load) : base(q, load) { }
    }
}