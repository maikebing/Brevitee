using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerWeaponCollection: DaoCollection<PlayerWeaponColumns, PlayerWeapon>
    { 
		public PlayerWeaponCollection(){}
		public PlayerWeaponCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerWeaponCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerWeaponCollection(Query<PlayerWeaponColumns, PlayerWeapon> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerWeaponCollection(Database db, Query<PlayerWeaponColumns, PlayerWeapon> q, bool load) : base(db, q, load) { }
		public PlayerWeaponCollection(Query<PlayerWeaponColumns, PlayerWeapon> q, bool load) : base(q, load) { }
    }
}