using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Shop
{
    public class ListColumns: QueryFilter<ListColumns>, IFilterToken
    {
        public ListColumns() { }
        public ListColumns(string columnName)
            : base(columnName)
        { }
		
		public ListColumns KeyColumn
		{
			get
			{
				return new ListColumns("Id");
			}
		}	
				
        public ListColumns Id
        {
            get
            {
                return new ListColumns("Id");
            }
        }
        public ListColumns Uuid
        {
            get
            {
                return new ListColumns("Uuid");
            }
        }
        public ListColumns Name
        {
            get
            {
                return new ListColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(List);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}