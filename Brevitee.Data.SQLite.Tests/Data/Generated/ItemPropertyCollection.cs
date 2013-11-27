using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ItemPropertyCollection: DaoCollection<ItemPropertyColumns, ItemProperty>
    { 
		public ItemPropertyCollection(){}
		public ItemPropertyCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ItemPropertyCollection(Query<ItemPropertyColumns, ItemProperty> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ItemPropertyCollection(Query<ItemPropertyColumns, ItemProperty> q, bool load) : base(q, load) { }
    }
}