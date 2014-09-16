using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoSpellCollection: DaoCollection<PlayerTwoSpellColumns, PlayerTwoSpell>
    { 
		public PlayerTwoSpellCollection(){}
		public PlayerTwoSpellCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerTwoSpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoSpellCollection(Query<PlayerTwoSpellColumns, PlayerTwoSpell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoSpellCollection(Database db, Query<PlayerTwoSpellColumns, PlayerTwoSpell> q, bool load) : base(db, q, load) { }
		public PlayerTwoSpellCollection(Query<PlayerTwoSpellColumns, PlayerTwoSpell> q, bool load) : base(q, load) { }
    }
}