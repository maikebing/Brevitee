using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Logging
{
    public class EventColumns: QueryFilter<EventColumns>, IFilterToken
    {
        public EventColumns() { }
        public EventColumns(string columnName)
            : base(columnName)
        { }
		
		public EventColumns KeyColumn
		{
			get
			{
				return new EventColumns("Id");
			}
		}	
				
        public EventColumns Id
        {
            get
            {
                return new EventColumns("Id");
            }
        }
        public EventColumns Occurred
        {
            get
            {
                return new EventColumns("Occurred");
            }
        }
        public EventColumns Severity
        {
            get
            {
                return new EventColumns("Severity");
            }
        }
        public EventColumns EventId
        {
            get
            {
                return new EventColumns("EventId");
            }
        }
        public EventColumns ComputerId
        {
            get
            {
                return new EventColumns("ComputerId");
            }
        }
        public EventColumns CategoryId
        {
            get
            {
                return new EventColumns("CategoryId");
            }
        }
        public EventColumns SourceId
        {
            get
            {
                return new EventColumns("SourceId");
            }
        }
        public EventColumns UserId
        {
            get
            {
                return new EventColumns("UserId");
            }
        }

        public EventColumns SignatureId
        {
            get
            {
                return new EventColumns("SignatureId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Event);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}