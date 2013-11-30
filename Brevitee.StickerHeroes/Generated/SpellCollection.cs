using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class SpellCollection: DaoCollection<SpellColumns, Spell>
    { 
		public SpellCollection(){}
		public SpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SpellCollection(Query<SpellColumns, Spell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SpellCollection(Query<SpellColumns, Spell> q, bool load) : base(q, load) { }
    }
}