using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class EquipmentCollection: DaoCollection<EquipmentColumns, Equipment>
    { 
		public EquipmentCollection(){}
		public EquipmentCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public EquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EquipmentCollection(Query<EquipmentColumns, Equipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EquipmentCollection(Database db, Query<EquipmentColumns, Equipment> q, bool load) : base(db, q, load) { }
		public EquipmentCollection(Query<EquipmentColumns, Equipment> q, bool load) : base(q, load) { }
    }
}