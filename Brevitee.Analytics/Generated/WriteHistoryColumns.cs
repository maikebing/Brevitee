using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class WriteHistoryColumns: QueryFilter<WriteHistoryColumns>, IFilterToken
    {
        public WriteHistoryColumns() { }
        public WriteHistoryColumns(string columnName)
            : base(columnName)
        { }
		
		public WriteHistoryColumns KeyColumn
		{
			get
			{
				return new WriteHistoryColumns("Id");
			}
		}	
				
        public WriteHistoryColumns Id
        {
            get
            {
                return new WriteHistoryColumns("Id");
            }
        }
        public WriteHistoryColumns TableName
        {
            get
            {
                return new WriteHistoryColumns("TableName");
            }
        }
        public WriteHistoryColumns ColumnName
        {
            get
            {
                return new WriteHistoryColumns("ColumnName");
            }
        }
        public WriteHistoryColumns DateTime
        {
            get
            {
                return new WriteHistoryColumns("DateTime");
            }
        }
        public WriteHistoryColumns Value
        {
            get
            {
                return new WriteHistoryColumns("Value");
            }
        }
        public WriteHistoryColumns ValueType
        {
            get
            {
                return new WriteHistoryColumns("ValueType");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(WriteHistory);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}