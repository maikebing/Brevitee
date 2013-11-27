using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerTwoSpellCollection: DaoCollection<PlayerTwoSpellColumns, PlayerTwoSpell>
    { 
		public PlayerTwoSpellCollection(){}
		public PlayerTwoSpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoSpellCollection(Query<PlayerTwoSpellColumns, PlayerTwoSpell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoSpellCollection(Query<PlayerTwoSpellColumns, PlayerTwoSpell> q, bool load) : base(q, load) { }
    }
}