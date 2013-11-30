using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public class CharacterColumns: QueryFilter<CharacterColumns>, IFilterToken
    {
        public CharacterColumns() { }
        public CharacterColumns(string columnName)
            : base(columnName)
        { }
		
		public CharacterColumns KeyColumn
		{
			get
			{
				return new CharacterColumns("Id");
			}
		}	
				
        public CharacterColumns Id
        {
            get
            {
                return new CharacterColumns("Id");
            }
        }
        public CharacterColumns Name
        {
            get
            {
                return new CharacterColumns("Name");
            }
        }
        public CharacterColumns Strength
        {
            get
            {
                return new CharacterColumns("Strength");
            }
        }
        public CharacterColumns Defense
        {
            get
            {
                return new CharacterColumns("Defense");
            }
        }
        public CharacterColumns Speed
        {
            get
            {
                return new CharacterColumns("Speed");
            }
        }
        public CharacterColumns Magic
        {
            get
            {
                return new CharacterColumns("Magic");
            }
        }
        public CharacterColumns Acuracy
        {
            get
            {
                return new CharacterColumns("Acuracy");
            }
        }
        public CharacterColumns Element
        {
            get
            {
                return new CharacterColumns("Element");
            }
        }
        public CharacterColumns MaxHealth
        {
            get
            {
                return new CharacterColumns("MaxHealth");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Character);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}