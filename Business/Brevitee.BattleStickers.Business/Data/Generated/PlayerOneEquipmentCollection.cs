using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOneEquipmentCollection: DaoCollection<PlayerOneEquipmentColumns, PlayerOneEquipment>
    { 
		public PlayerOneEquipmentCollection(){}
		public PlayerOneEquipmentCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerOneEquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneEquipmentCollection(Query<PlayerOneEquipmentColumns, PlayerOneEquipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneEquipmentCollection(Database db, Query<PlayerOneEquipmentColumns, PlayerOneEquipment> q, bool load) : base(db, q, load) { }
		public PlayerOneEquipmentCollection(Query<PlayerOneEquipmentColumns, PlayerOneEquipment> q, bool load) : base(q, load) { }
    }
}