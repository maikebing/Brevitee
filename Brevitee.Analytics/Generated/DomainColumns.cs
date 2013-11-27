using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class DomainColumns: QueryFilter<DomainColumns>, IFilterToken
    {
        public DomainColumns() { }
        public DomainColumns(string columnName)
            : base(columnName)
        { }
		
		public DomainColumns KeyColumn
		{
			get
			{
				return new DomainColumns("Id");
			}
		}	
				
        public DomainColumns Id
        {
            get
            {
                return new DomainColumns("Id");
            }
        }
        public DomainColumns Value
        {
            get
            {
                return new DomainColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Domain);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}