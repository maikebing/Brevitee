using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class EquipmentColumns: QueryFilter<EquipmentColumns>, IFilterToken
    {
        public EquipmentColumns() { }
        public EquipmentColumns(string columnName)
            : base(columnName)
        { }
		
		public EquipmentColumns KeyColumn
		{
			get
			{
				return new EquipmentColumns("Id");
			}
		}	
				
        public EquipmentColumns Id
        {
            get
            {
                return new EquipmentColumns("Id");
            }
        }
        public EquipmentColumns Uuid
        {
            get
            {
                return new EquipmentColumns("Uuid");
            }
        }
        public EquipmentColumns Name
        {
            get
            {
                return new EquipmentColumns("Name");
            }
        }
        public EquipmentColumns Element
        {
            get
            {
                return new EquipmentColumns("Element");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Equipment);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}