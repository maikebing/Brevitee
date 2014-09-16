using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoWeaponCollection: DaoCollection<PlayerTwoWeaponColumns, PlayerTwoWeapon>
    { 
		public PlayerTwoWeaponCollection(){}
		public PlayerTwoWeaponCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerTwoWeaponCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoWeaponCollection(Query<PlayerTwoWeaponColumns, PlayerTwoWeapon> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoWeaponCollection(Database db, Query<PlayerTwoWeaponColumns, PlayerTwoWeapon> q, bool load) : base(db, q, load) { }
		public PlayerTwoWeaponCollection(Query<PlayerTwoWeaponColumns, PlayerTwoWeapon> q, bool load) : base(q, load) { }
    }
}