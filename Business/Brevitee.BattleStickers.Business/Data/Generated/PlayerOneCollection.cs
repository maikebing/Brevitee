using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneCollection: DaoCollection<PlayerOneColumns, PlayerOne>
    { 
		public PlayerOneCollection(){}
		public PlayerOneCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerOneCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneCollection(Query<PlayerOneColumns, PlayerOne> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneCollection(Database db, Query<PlayerOneColumns, PlayerOne> q, bool load) : base(db, q, load) { }
		public PlayerOneCollection(Query<PlayerOneColumns, PlayerOne> q, bool load) : base(q, load) { }
    }
}