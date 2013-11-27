using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class EffectOverTimeCollection: DaoCollection<EffectOverTimeColumns, EffectOverTime>
    { 
		public EffectOverTimeCollection(){}
		public EffectOverTimeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EffectOverTimeCollection(Query<EffectOverTimeColumns, EffectOverTime> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EffectOverTimeCollection(Query<EffectOverTimeColumns, EffectOverTime> q, bool load) : base(q, load) { }
    }
}