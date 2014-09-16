using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class EffectOverTimeCollection: DaoCollection<EffectOverTimeColumns, EffectOverTime>
    { 
		public EffectOverTimeCollection(){}
		public EffectOverTimeCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EffectOverTimeCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EffectOverTimeCollection(Query<EffectOverTimeColumns, EffectOverTime> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EffectOverTimeCollection(Database db, Query<EffectOverTimeColumns, EffectOverTime> q, bool load) : base(db, q, load) { }
		public EffectOverTimeCollection(Query<EffectOverTimeColumns, EffectOverTime> q, bool load) : base(q, load) { }
    }
}