using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerOneSpellCollection: DaoCollection<PlayerOneSpellColumns, PlayerOneSpell>
    { 
		public PlayerOneSpellCollection(){}
		public PlayerOneSpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneSpellCollection(Query<PlayerOneSpellColumns, PlayerOneSpell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneSpellCollection(Query<PlayerOneSpellColumns, PlayerOneSpell> q, bool load) : base(q, load) { }
    }
}