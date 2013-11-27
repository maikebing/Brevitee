using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ProtocolColumns: QueryFilter<ProtocolColumns>, IFilterToken
    {
        public ProtocolColumns() { }
        public ProtocolColumns(string columnName)
            : base(columnName)
        { }
		
		public ProtocolColumns KeyColumn
		{
			get
			{
				return new ProtocolColumns("Id");
			}
		}	
				
        public ProtocolColumns Id
        {
            get
            {
                return new ProtocolColumns("Id");
            }
        }
        public ProtocolColumns Value
        {
            get
            {
                return new ProtocolColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Protocol);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}