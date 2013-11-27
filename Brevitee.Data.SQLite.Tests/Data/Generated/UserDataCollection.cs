using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserDataCollection: DaoCollection<UserDataColumns, UserData>
    { 
		public UserDataCollection(){}
		public UserDataCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserDataCollection(Query<UserDataColumns, UserData> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserDataCollection(Query<UserDataColumns, UserData> q, bool load) : base(q, load) { }
    }
}