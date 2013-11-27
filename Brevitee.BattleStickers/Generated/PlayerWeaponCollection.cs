using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerWeaponCollection: DaoCollection<PlayerWeaponColumns, PlayerWeapon>
    { 
		public PlayerWeaponCollection(){}
		public PlayerWeaponCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerWeaponCollection(Query<PlayerWeaponColumns, PlayerWeapon> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerWeaponCollection(Query<PlayerWeaponColumns, PlayerWeapon> q, bool load) : base(q, load) { }
    }
}