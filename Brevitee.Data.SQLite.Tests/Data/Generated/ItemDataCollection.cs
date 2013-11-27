using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class ItemDataCollection: DaoCollection<ItemDataColumns, ItemData>
    { 
		public ItemDataCollection(){}
		public ItemDataCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ItemDataCollection(Query<ItemDataColumns, ItemData> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ItemDataCollection(Query<ItemDataColumns, ItemData> q, bool load) : base(q, load) { }
    }
}