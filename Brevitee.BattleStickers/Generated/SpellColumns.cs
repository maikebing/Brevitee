using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers
{
    public class SpellColumns: QueryFilter<SpellColumns>, IFilterToken
    {
        public SpellColumns() { }
        public SpellColumns(string columnName)
            : base(columnName)
        { }
		
		public SpellColumns KeyColumn
		{
			get
			{
				return new SpellColumns("Id");
			}
		}	
				
        public SpellColumns Id
        {
            get
            {
                return new SpellColumns("Id");
            }
        }
        public SpellColumns Name
        {
            get
            {
                return new SpellColumns("Name");
            }
        }
        public SpellColumns Strength
        {
            get
            {
                return new SpellColumns("Strength");
            }
        }
        public SpellColumns Element
        {
            get
            {
                return new SpellColumns("Element");
            }
        }

        public SpellColumns EffectOverTimeId
        {
            get
            {
                return new SpellColumns("EffectOverTimeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Spell);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}