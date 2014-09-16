using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Data.Tests
{
    public class ItemColumns: QueryFilter<ItemColumns>, IFilterToken
    {
        public ItemColumns() { }
        public ItemColumns(string columnName)
            : base(columnName)
        { }
		
		public ItemColumns KeyColumn
		{
			get
			{
				return new ItemColumns("Id");
			}
		}	
				
        public ItemColumns Id
        {
            get
            {
                return new ItemColumns("Id");
            }
        }
        public ItemColumns Uuid
        {
            get
            {
                return new ItemColumns("Uuid");
            }
        }
        public ItemColumns Name
        {
            get
            {
                return new ItemColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Item);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}