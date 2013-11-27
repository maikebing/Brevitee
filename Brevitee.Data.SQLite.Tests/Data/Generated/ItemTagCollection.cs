using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ItemTagCollection: DaoCollection<ItemTagColumns, ItemTag>
    { 
		public ItemTagCollection(){}
		public ItemTagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ItemTagCollection(Query<ItemTagColumns, ItemTag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ItemTagCollection(Query<ItemTagColumns, ItemTag> q, bool load) : base(q, load) { }
    }
}