using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerTwoWeaponColumns: QueryFilter<PlayerTwoWeaponColumns>, IFilterToken
    {
        public PlayerTwoWeaponColumns() { }
        public PlayerTwoWeaponColumns(string columnName)
            : base(columnName)
        { }
		
		public PlayerTwoWeaponColumns KeyColumn
		{
			get
			{
				return new PlayerTwoWeaponColumns("Id");
			}
		}	
				
        public PlayerTwoWeaponColumns Id
        {
            get
            {
                return new PlayerTwoWeaponColumns("Id");
            }
        }

        public PlayerTwoWeaponColumns PlayerTwoId
        {
            get
            {
                return new PlayerTwoWeaponColumns("PlayerTwoId");
            }
        }
        public PlayerTwoWeaponColumns WeaponId
        {
            get
            {
                return new PlayerTwoWeaponColumns("WeaponId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PlayerTwoWeapon);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}