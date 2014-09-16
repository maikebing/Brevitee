using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class SpellCollection: DaoCollection<SpellColumns, Spell>
    { 
		public SpellCollection(){}
		public SpellCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SpellCollection(Query<SpellColumns, Spell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SpellCollection(Database db, Query<SpellColumns, Spell> q, bool load) : base(db, q, load) { }
		public SpellCollection(Query<SpellColumns, Spell> q, bool load) : base(q, load) { }
    }
}