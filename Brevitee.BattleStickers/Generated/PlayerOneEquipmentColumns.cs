using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerOneEquipmentColumns: QueryFilter<PlayerOneEquipmentColumns>, IFilterToken
    {
        public PlayerOneEquipmentColumns() { }
        public PlayerOneEquipmentColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneEquipmentColumns KeyColumn
		{
			get
			{
				return new PlayerOneEquipmentColumns("Id");
			}
		}	
				
        public PlayerOneEquipmentColumns Id
        {
            get
            {
                return new PlayerOneEquipmentColumns("Id");
            }
        }

        public PlayerOneEquipmentColumns PlayerOneId
        {
            get
            {
                return new PlayerOneEquipmentColumns("PlayerOneId");
            }
        }
        public PlayerOneEquipmentColumns EquipmentId
        {
            get
            {
                return new PlayerOneEquipmentColumns("EquipmentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOneEquipment);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}