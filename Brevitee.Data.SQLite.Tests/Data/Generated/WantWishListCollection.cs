using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class WantWishListCollection: DaoCollection<WantWishListColumns, WantWishList>
    { 
		public WantWishListCollection(){}
		public WantWishListCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public WantWishListCollection(Query<WantWishListColumns, WantWishList> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public WantWishListCollection(Query<WantWishListColumns, WantWishList> q, bool load) : base(q, load) { }
    }
}