using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class AddressCollection: DaoCollection<AddressColumns, Address>
    { 
		public AddressCollection(){}
		public AddressCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public AddressCollection(Query<AddressColumns, Address> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public AddressCollection(Query<AddressColumns, Address> q, bool load) : base(q, load) { }
    }
}