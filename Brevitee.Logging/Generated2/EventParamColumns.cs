using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class EventParamColumns: QueryFilter<EventParamColumns>, IFilterToken
    {
        public EventParamColumns() { }
        public EventParamColumns(string columnName)
            : base(columnName)
        { }
		
		public EventParamColumns KeyColumn
		{
			get
			{
				return new EventParamColumns("Id");
			}
		}	
				
        public EventParamColumns Id
        {
            get
            {
                return new EventParamColumns("Id");
            }
        }

        public EventParamColumns EventId
        {
            get
            {
                return new EventParamColumns("EventId");
            }
        }
        public EventParamColumns ParamId
        {
            get
            {
                return new EventParamColumns("ParamId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(EventParam);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}