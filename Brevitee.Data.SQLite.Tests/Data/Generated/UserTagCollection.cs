using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserTagCollection: DaoCollection<UserTagColumns, UserTag>
    { 
		public UserTagCollection(){}
		public UserTagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserTagCollection(Query<UserTagColumns, UserTag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserTagCollection(Query<UserTagColumns, UserTag> q, bool load) : base(q, load) { }
    }
}