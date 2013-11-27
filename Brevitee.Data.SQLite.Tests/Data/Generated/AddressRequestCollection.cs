using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class AddressRequestCollection: DaoCollection<AddressRequestColumns, AddressRequest>
    { 
		public AddressRequestCollection(){}
		public AddressRequestCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AddressRequestCollection(Query<AddressRequestColumns, AddressRequest> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AddressRequestCollection(Query<AddressRequestColumns, AddressRequest> q, bool load) : base(q, load) { }
    }
}