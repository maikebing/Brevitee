using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoCharacterHealthCollection: DaoCollection<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth>
    { 
		public PlayerTwoCharacterHealthCollection(){}
		public PlayerTwoCharacterHealthCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerTwoCharacterHealthCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoCharacterHealthCollection(Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoCharacterHealthCollection(Database db, Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth> q, bool load) : base(db, q, load) { }
		public PlayerTwoCharacterHealthCollection(Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth> q, bool load) : base(q, load) { }
    }
}