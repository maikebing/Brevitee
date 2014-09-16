using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoEquipmentColumns: QueryFilter<PlayerTwoEquipmentColumns>, IFilterToken
    {
        public PlayerTwoEquipmentColumns() { }
        public PlayerTwoEquipmentColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoEquipmentColumns KeyColumn
		{
			get
			{
				return new PlayerTwoEquipmentColumns("Id");
			}
		}	
				
        public PlayerTwoEquipmentColumns Id
        {
            get
            {
                return new PlayerTwoEquipmentColumns("Id");
            }
        }

        public PlayerTwoEquipmentColumns PlayerTwoId
        {
            get
            {
                return new PlayerTwoEquipmentColumns("PlayerTwoId");
            }
        }
        public PlayerTwoEquipmentColumns EquipmentId
        {
            get
            {
                return new PlayerTwoEquipmentColumns("EquipmentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwoEquipment);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}