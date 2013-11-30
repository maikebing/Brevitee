using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerOneEquipmentCollection: DaoCollection<PlayerOneEquipmentColumns, PlayerOneEquipment>
    { 
		public PlayerOneEquipmentCollection(){}
		public PlayerOneEquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public PlayerOneEquipmentCollection(Query<PlayerOneEquipmentColumns, PlayerOneEquipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public PlayerOneEquipmentCollection(Query<PlayerOneEquipmentColumns, PlayerOneEquipment> q, bool load) : base(q, load) { }
    }
}