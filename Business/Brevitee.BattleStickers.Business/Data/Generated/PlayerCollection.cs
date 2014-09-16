using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerCollection: DaoCollection<PlayerColumns, Player>
    { 
		public PlayerCollection(){}
		public PlayerCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerCollection(Query<PlayerColumns, Player> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerCollection(Database db, Query<PlayerColumns, Player> q, bool load) : base(db, q, load) { }
		public PlayerCollection(Query<PlayerColumns, Player> q, bool load) : base(q, load) { }
    }
}