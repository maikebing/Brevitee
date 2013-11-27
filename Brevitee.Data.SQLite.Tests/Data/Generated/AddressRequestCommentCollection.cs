using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class AddressRequestCommentCollection: DaoCollection<AddressRequestCommentColumns, AddressRequestComment>
    { 
		public AddressRequestCommentCollection(){}
		public AddressRequestCommentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AddressRequestCommentCollection(Query<AddressRequestCommentColumns, AddressRequestComment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AddressRequestCommentCollection(Query<AddressRequestCommentColumns, AddressRequestComment> q, bool load) : base(q, load) { }
    }
}