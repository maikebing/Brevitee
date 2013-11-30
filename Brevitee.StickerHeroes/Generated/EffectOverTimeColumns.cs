using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class EffectOverTimeColumns: QueryFilter<EffectOverTimeColumns>, IFilterToken
    {
        public EffectOverTimeColumns() { }
        public EffectOverTimeColumns(string columnName)
            : base(columnName)
        { }
		
		public EffectOverTimeColumns KeyColumn
		{
			get
			{
				return new EffectOverTimeColumns("Id");
			}
		}	
				
        public EffectOverTimeColumns Id
        {
            get
            {
                return new EffectOverTimeColumns("Id");
            }
        }
        public EffectOverTimeColumns Name
        {
            get
            {
                return new EffectOverTimeColumns("Name");
            }
        }
        public EffectOverTimeColumns Strength
        {
            get
            {
                return new EffectOverTimeColumns("Strength");
            }
        }
        public EffectOverTimeColumns Speed
        {
            get
            {
                return new EffectOverTimeColumns("Speed");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(EffectOverTime);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}