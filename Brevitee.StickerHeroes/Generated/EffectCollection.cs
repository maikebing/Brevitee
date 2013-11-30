using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class EffectCollection: DaoCollection<EffectColumns, Effect>
    { 
		public EffectCollection(){}
		public EffectCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EffectCollection(Query<EffectColumns, Effect> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EffectCollection(Query<EffectColumns, Effect> q, bool load) : base(q, load) { }
    }
}