using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class PlayerOneWeaponColumns: QueryFilter<PlayerOneWeaponColumns>, IFilterToken
    {
        public PlayerOneWeaponColumns() { }
        public PlayerOneWeaponColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerOneWeaponColumns KeyColumn
		{
			get
			{
				return new PlayerOneWeaponColumns("Id");
			}
		}	
				
        public PlayerOneWeaponColumns Id
        {
            get
            {
                return new PlayerOneWeaponColumns("Id");
            }
        }

        public PlayerOneWeaponColumns PlayerOneId
        {
            get
            {
                return new PlayerOneWeaponColumns("PlayerOneId");
            }
        }
        public PlayerOneWeaponColumns WeaponId
        {
            get
            {
                return new PlayerOneWeaponColumns("WeaponId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerOneWeapon);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}