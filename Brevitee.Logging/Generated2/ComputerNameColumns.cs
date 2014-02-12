using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class ComputerNameColumns: QueryFilter<ComputerNameColumns>, IFilterToken
    {
        public ComputerNameColumns() { }
        public ComputerNameColumns(string columnName)
            : base(columnName)
        { }
		
		public ComputerNameColumns KeyColumn
		{
			get
			{
				return new ComputerNameColumns("Id");
			}
		}	
				
        public ComputerNameColumns Id
        {
            get
            {
                return new ComputerNameColumns("Id");
            }
        }
        public ComputerNameColumns Value
        {
            get
            {
                return new ComputerNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ComputerName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}