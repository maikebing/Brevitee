using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerTwoEquipmentCollection: DaoCollection<PlayerTwoEquipmentColumns, PlayerTwoEquipment>
    { 
		public PlayerTwoEquipmentCollection(){}
		public PlayerTwoEquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerTwoEquipmentCollection(Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerTwoEquipmentCollection(Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment> q, bool load) : base(q, load) { }
    }
}