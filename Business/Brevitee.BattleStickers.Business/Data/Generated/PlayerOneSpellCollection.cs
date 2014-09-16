using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneSpellCollection: DaoCollection<PlayerOneSpellColumns, PlayerOneSpell>
    { 
		public PlayerOneSpellCollection(){}
		public PlayerOneSpellCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerOneSpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneSpellCollection(Query<PlayerOneSpellColumns, PlayerOneSpell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneSpellCollection(Database db, Query<PlayerOneSpellColumns, PlayerOneSpell> q, bool load) : base(db, q, load) { }
		public PlayerOneSpellCollection(Query<PlayerOneSpellColumns, PlayerOneSpell> q, bool load) : base(q, load) { }
    }
}