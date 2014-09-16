using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelCollection: DaoCollection<RequiredLevelColumns, RequiredLevel>
    { 
		public RequiredLevelCollection(){}
		public RequiredLevelCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RequiredLevelCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequiredLevelCollection(Query<RequiredLevelColumns, RequiredLevel> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequiredLevelCollection(Database db, Query<RequiredLevelColumns, RequiredLevel> q, bool load) : base(db, q, load) { }
		public RequiredLevelCollection(Query<RequiredLevelColumns, RequiredLevel> q, bool load) : base(q, load) { }
    }
}