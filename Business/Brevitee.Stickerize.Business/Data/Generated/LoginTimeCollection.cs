using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class LoginTimeCollection: DaoCollection<LoginTimeColumns, LoginTime>
    { 
		public LoginTimeCollection(){}
		public LoginTimeCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public LoginTimeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public LoginTimeCollection(Query<LoginTimeColumns, LoginTime> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public LoginTimeCollection(Database db, Query<LoginTimeColumns, LoginTime> q, bool load) : base(db, q, load) { }
		public LoginTimeCollection(Query<LoginTimeColumns, LoginTime> q, bool load) : base(q, load) { }
    }
}