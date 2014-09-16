using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoEquipmentCollection: DaoCollection<PlayerTwoEquipmentColumns, PlayerTwoEquipment>
    { 
		public PlayerTwoEquipmentCollection(){}
		public PlayerTwoEquipmentCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public PlayerTwoEquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoEquipmentCollection(Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoEquipmentCollection(Database db, Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment> q, bool load) : base(db, q, load) { }
		public PlayerTwoEquipmentCollection(Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment> q, bool load) : base(q, load) { }
    }
}