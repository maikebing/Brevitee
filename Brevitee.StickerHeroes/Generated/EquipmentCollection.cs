using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class EquipmentCollection: DaoCollection<EquipmentColumns, Equipment>
    { 
		public EquipmentCollection(){}
		public EquipmentCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public EquipmentCollection(Query<EquipmentColumns, Equipment> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public EquipmentCollection(Query<EquipmentColumns, Equipment> q, bool load) : base(q, load) { }
    }
}