using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public class SessionStateColumns: QueryFilter<SessionStateColumns>, IFilterToken
    {
        public SessionStateColumns() { }
        public SessionStateColumns(string columnName)
            : base(columnName)
        { }
		
		public SessionStateColumns KeyColumn
		{
			get
			{
				return new SessionStateColumns("Id");
			}
		}	
				
        public SessionStateColumns Id
        {
            get
            {
                return new SessionStateColumns("Id");
            }
        }
        public SessionStateColumns Name
        {
            get
            {
                return new SessionStateColumns("Name");
            }
        }
        public SessionStateColumns ValueType
        {
            get
            {
                return new SessionStateColumns("ValueType");
            }
        }
        public SessionStateColumns Value
        {
            get
            {
                return new SessionStateColumns("Value");
            }
        }

        public SessionStateColumns SessionId
        {
            get
            {
                return new SessionStateColumns("SessionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SessionState);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}