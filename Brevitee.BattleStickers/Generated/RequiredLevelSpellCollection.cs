using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class RequiredLevelSpellCollection: DaoCollection<RequiredLevelSpellColumns, RequiredLevelSpell>
    { 
		public RequiredLevelSpellCollection(){}
		public RequiredLevelSpellCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequiredLevelSpellCollection(Query<RequiredLevelSpellColumns, RequiredLevelSpell> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequiredLevelSpellCollection(Query<RequiredLevelSpellColumns, RequiredLevelSpell> q, bool load) : base(q, load) { }
    }
}