using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoCollection: DaoCollection<PlayerTwoColumns, PlayerTwo>
    { 
		public PlayerTwoCollection(){}
		public PlayerTwoCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerTwoCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoCollection(Query<PlayerTwoColumns, PlayerTwo> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoCollection(Database db, Query<PlayerTwoColumns, PlayerTwo> q, bool load) : base(db, q, load) { }
		public PlayerTwoCollection(Query<PlayerTwoColumns, PlayerTwo> q, bool load) : base(q, load) { }
    }
}