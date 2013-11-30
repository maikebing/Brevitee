using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class RequiredLevelWeaponCollection: DaoCollection<RequiredLevelWeaponColumns, RequiredLevelWeapon>
    { 
		public RequiredLevelWeaponCollection(){}
		public RequiredLevelWeaponCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public RequiredLevelWeaponCollection(Query<RequiredLevelWeaponColumns, RequiredLevelWeapon> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public RequiredLevelWeaponCollection(Query<RequiredLevelWeaponColumns, RequiredLevelWeapon> q, bool load) : base(q, load) { }
    }
}