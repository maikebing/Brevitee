using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelSpellColumns: QueryFilter<RequiredLevelSpellColumns>, IFilterToken
    {
        public RequiredLevelSpellColumns() { }
        public RequiredLevelSpellColumns(string columnName)
            : base(columnName)
        { }
		
		public RequiredLevelSpellColumns KeyColumn
		{
			get
			{
				return new RequiredLevelSpellColumns("Id");
			}
		}	
				
        public RequiredLevelSpellColumns Id
        {
            get
            {
                return new RequiredLevelSpellColumns("Id");
            }
        }

        public RequiredLevelSpellColumns RequiredLevelId
        {
            get
            {
                return new RequiredLevelSpellColumns("RequiredLevelId");
            }
        }
        public RequiredLevelSpellColumns SpellId
        {
            get
            {
                return new RequiredLevelSpellColumns("SpellId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(RequiredLevelSpell);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}