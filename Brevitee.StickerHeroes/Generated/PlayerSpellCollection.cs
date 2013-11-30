using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerSpellCollection: DaoCollection<PlayerSpellColumns, PlayerSpell>
    { 
		public PlayerSpellCollection(){}
		public PlayerSpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerSpellCollection(Query<PlayerSpellColumns, PlayerSpell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerSpellCollection(Query<PlayerSpellColumns, PlayerSpell> q, bool load) : base(q, load) { }
    }
}