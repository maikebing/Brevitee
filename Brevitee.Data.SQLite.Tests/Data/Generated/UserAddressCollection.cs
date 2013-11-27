using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserAddressCollection: DaoCollection<UserAddressColumns, UserAddress>
    { 
		public UserAddressCollection(){}
		public UserAddressCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserAddressCollection(Query<UserAddressColumns, UserAddress> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserAddressCollection(Query<UserAddressColumns, UserAddress> q, bool load) : base(q, load) { }
    }
}