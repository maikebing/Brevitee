using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class PlayerEquipmentColumns: QueryFilter<PlayerEquipmentColumns>, IFilterToken
    {
        public PlayerEquipmentColumns() { }
        public PlayerEquipmentColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerEquipmentColumns KeyColumn
		{
			get
			{
				return new PlayerEquipmentColumns("Id");
			}
		}	
				
        public PlayerEquipmentColumns Id
        {
            get
            {
                return new PlayerEquipmentColumns("Id");
            }
        }

        public PlayerEquipmentColumns PlayerId
        {
            get
            {
                return new PlayerEquipmentColumns("PlayerId");
            }
        }
        public PlayerEquipmentColumns EquipmentId
        {
            get
            {
                return new PlayerEquipmentColumns("EquipmentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerEquipment);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}