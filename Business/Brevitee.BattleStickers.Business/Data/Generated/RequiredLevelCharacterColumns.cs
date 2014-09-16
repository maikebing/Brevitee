using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelCharacterColumns: QueryFilter<RequiredLevelCharacterColumns>, IFilterToken
    {
        public RequiredLevelCharacterColumns() { }
        public RequiredLevelCharacterColumns(string columnName)
            : base(columnName)
        { }
		
		public RequiredLevelCharacterColumns KeyColumn
		{
			get
			{
				return new RequiredLevelCharacterColumns("Id");
			}
		}	
				
        public RequiredLevelCharacterColumns Id
        {
            get
            {
                return new RequiredLevelCharacterColumns("Id");
            }
        }

        public RequiredLevelCharacterColumns RequiredLevelId
        {
            get
            {
                return new RequiredLevelCharacterColumns("RequiredLevelId");
            }
        }
        public RequiredLevelCharacterColumns CharacterId
        {
            get
            {
                return new RequiredLevelCharacterColumns("CharacterId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(RequiredLevelCharacter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}