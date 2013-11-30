using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class RequiredLevelCollection: DaoCollection<RequiredLevelColumns, RequiredLevel>
    { 
		public RequiredLevelCollection(){}
		public RequiredLevelCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequiredLevelCollection(Query<RequiredLevelColumns, RequiredLevel> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequiredLevelCollection(Query<RequiredLevelColumns, RequiredLevel> q, bool load) : base(q, load) { }
    }
}