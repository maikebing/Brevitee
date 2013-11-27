using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserPropertyCollection: DaoCollection<UserPropertyColumns, UserProperty>
    { 
		public UserPropertyCollection(){}
		public UserPropertyCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserPropertyCollection(Query<UserPropertyColumns, UserProperty> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserPropertyCollection(Query<UserPropertyColumns, UserProperty> q, bool load) : base(q, load) { }
    }
}