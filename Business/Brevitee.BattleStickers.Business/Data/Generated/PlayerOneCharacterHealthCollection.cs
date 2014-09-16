using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneCharacterHealthCollection: DaoCollection<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth>
    { 
		public PlayerOneCharacterHealthCollection(){}
		public PlayerOneCharacterHealthCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerOneCharacterHealthCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneCharacterHealthCollection(Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneCharacterHealthCollection(Database db, Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth> q, bool load) : base(db, q, load) { }
		public PlayerOneCharacterHealthCollection(Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth> q, bool load) : base(q, load) { }
    }
}