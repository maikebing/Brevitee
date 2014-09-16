using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelWeaponCollection: DaoCollection<RequiredLevelWeaponColumns, RequiredLevelWeapon>
    { 
		public RequiredLevelWeaponCollection(){}
		public RequiredLevelWeaponCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public RequiredLevelWeaponCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequiredLevelWeaponCollection(Query<RequiredLevelWeaponColumns, RequiredLevelWeapon> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequiredLevelWeaponCollection(Database db, Query<RequiredLevelWeaponColumns, RequiredLevelWeapon> q, bool load) : base(db, q, load) { }
		public RequiredLevelWeaponCollection(Query<RequiredLevelWeaponColumns, RequiredLevelWeapon> q, bool load) : base(q, load) { }
    }
}