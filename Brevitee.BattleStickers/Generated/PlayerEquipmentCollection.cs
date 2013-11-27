using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerEquipmentCollection: DaoCollection<PlayerEquipmentColumns, PlayerEquipment>
    { 
		public PlayerEquipmentCollection(){}
		public PlayerEquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerEquipmentCollection(Query<PlayerEquipmentColumns, PlayerEquipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerEquipmentCollection(Query<PlayerEquipmentColumns, PlayerEquipment> q, bool load) : base(q, load) { }
    }
}