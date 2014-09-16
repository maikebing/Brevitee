using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerWeaponColumns: QueryFilter<PlayerWeaponColumns>, IFilterToken
    {
        public PlayerWeaponColumns() { }
        public PlayerWeaponColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerWeaponColumns KeyColumn
		{
			get
			{
				return new PlayerWeaponColumns("Id");
			}
		}	
				
        public PlayerWeaponColumns Id
        {
            get
            {
                return new PlayerWeaponColumns("Id");
            }
        }

        public PlayerWeaponColumns PlayerId
        {
            get
            {
                return new PlayerWeaponColumns("PlayerId");
            }
        }
        public PlayerWeaponColumns WeaponId
        {
            get
            {
                return new PlayerWeaponColumns("WeaponId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerWeapon);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}