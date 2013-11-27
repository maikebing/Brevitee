using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ItemCollection: DaoCollection<ItemColumns, Item>
    { 
		public ItemCollection(){}
		public ItemCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ItemCollection(Query<ItemColumns, Item> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ItemCollection(Query<ItemColumns, Item> q, bool load) : base(q, load) { }
    }
}