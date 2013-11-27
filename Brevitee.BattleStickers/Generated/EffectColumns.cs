using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class EffectColumns: QueryFilter<EffectColumns>, IFilterToken
    {
        public EffectColumns() { }
        public EffectColumns(string columnName)
            : base(columnName)
        { }
		
		public EffectColumns KeyColumn
		{
			get
			{
				return new EffectColumns("Id");
			}
		}	
				
        public EffectColumns Id
        {
            get
            {
                return new EffectColumns("Id");
            }
        }
        public EffectColumns Attribute
        {
            get
            {
                return new EffectColumns("Attribute");
            }
        }
        public EffectColumns Value
        {
            get
            {
                return new EffectColumns("Value");
            }
        }

        public EffectColumns EquipmentId
        {
            get
            {
                return new EffectColumns("EquipmentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Effect);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}