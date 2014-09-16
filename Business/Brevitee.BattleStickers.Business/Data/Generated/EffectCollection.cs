using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class EffectCollection: DaoCollection<EffectColumns, Effect>
    { 
		public EffectCollection(){}
		public EffectCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EffectCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EffectCollection(Query<EffectColumns, Effect> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EffectCollection(Database db, Query<EffectColumns, Effect> q, bool load) : base(db, q, load) { }
		public EffectCollection(Query<EffectColumns, Effect> q, bool load) : base(q, load) { }
    }
}