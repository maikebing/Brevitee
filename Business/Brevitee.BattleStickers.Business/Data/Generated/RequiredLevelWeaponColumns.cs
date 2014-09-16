using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class RequiredLevelWeaponColumns: QueryFilter<RequiredLevelWeaponColumns>, IFilterToken
    {
        public RequiredLevelWeaponColumns() { }
        public RequiredLevelWeaponColumns(string columnName)
            : base(columnName)
        { }
		
		public RequiredLevelWeaponColumns KeyColumn
		{
			get
			{
				return new RequiredLevelWeaponColumns("Id");
			}
		}	
				
        public RequiredLevelWeaponColumns Id
        {
            get
            {
                return new RequiredLevelWeaponColumns("Id");
            }
        }

        public RequiredLevelWeaponColumns RequiredLevelId
        {
            get
            {
                return new RequiredLevelWeaponColumns("RequiredLevelId");
            }
        }
        public RequiredLevelWeaponColumns WeaponId
        {
            get
            {
                return new RequiredLevelWeaponColumns("WeaponId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(RequiredLevelWeapon);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}