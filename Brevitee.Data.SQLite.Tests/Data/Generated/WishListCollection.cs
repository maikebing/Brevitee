using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class WishListCollection: DaoCollection<WishListColumns, WishList>
    { 
		public WishListCollection(){}
		public WishListCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WishListCollection(Query<WishListColumns, WishList> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WishListCollection(Query<WishListColumns, WishList> q, bool load) : base(q, load) { }
    }
}