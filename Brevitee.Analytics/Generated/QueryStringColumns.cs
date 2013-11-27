using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class QueryStringColumns: QueryFilter<QueryStringColumns>, IFilterToken
    {
        public QueryStringColumns() { }
        public QueryStringColumns(string columnName)
            : base(columnName)
        { }
		
		public QueryStringColumns KeyColumn
		{
			get
			{
				return new QueryStringColumns("Id");
			}
		}	
				
        public QueryStringColumns Id
        {
            get
            {
                return new QueryStringColumns("Id");
            }
        }
        public QueryStringColumns Value
        {
            get
            {
                return new QueryStringColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(QueryString);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}