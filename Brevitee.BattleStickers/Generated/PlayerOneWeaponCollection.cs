using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerOneWeaponCollection: DaoCollection<PlayerOneWeaponColumns, PlayerOneWeapon>
    { 
		public PlayerOneWeaponCollection(){}
		public PlayerOneWeaponCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneWeaponCollection(Query<PlayerOneWeaponColumns, PlayerOneWeapon> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneWeaponCollection(Query<PlayerOneWeaponColumns, PlayerOneWeapon> q, bool load) : base(q, load) { }
    }
}