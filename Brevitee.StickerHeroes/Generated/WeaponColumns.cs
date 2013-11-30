using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class WeaponColumns: QueryFilter<WeaponColumns>, IFilterToken
    {
        public WeaponColumns() { }
        public WeaponColumns(string columnName)
            : base(columnName)
        { }
		
		public WeaponColumns KeyColumn
		{
			get
			{
				return new WeaponColumns("Id");
			}
		}	
				
        public WeaponColumns Id
        {
            get
            {
                return new WeaponColumns("Id");
            }
        }
        public WeaponColumns Name
        {
            get
            {
                return new WeaponColumns("Name");
            }
        }
        public WeaponColumns Strength
        {
            get
            {
                return new WeaponColumns("Strength");
            }
        }
        public WeaponColumns Element
        {
            get
            {
                return new WeaponColumns("Element");
            }
        }

        public WeaponColumns EffectOverTimeId
        {
            get
            {
                return new WeaponColumns("EffectOverTimeId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Weapon);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}