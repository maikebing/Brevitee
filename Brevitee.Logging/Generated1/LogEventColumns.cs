using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;
using Brevitee.Data;

namespace Brevitee.Logging.Data
{
    public class LogEventColumns: QueryFilter<LogEventColumns>, IFilterToken
    {
        public LogEventColumns() { }
        public LogEventColumns(string columnName)
            : base(columnName)
        { }

        public LogEventColumns Id
        {
            get
            {
                return new LogEventColumns("Id");
            }
        }
        public LogEventColumns Source
        {
            get
            {
                return new LogEventColumns("Source");
            }
        }
        public LogEventColumns Category
        {
            get
            {
                return new LogEventColumns("Category");
            }
        }
        public LogEventColumns EventID
        {
            get
            {
                return new LogEventColumns("EventID");
            }
        }
        public LogEventColumns User
        {
            get
            {
                return new LogEventColumns("User");
            }
        }
        public LogEventColumns Time
        {
            get
            {
                return new LogEventColumns("Time");
            }
        }
        public LogEventColumns MessageSignature
        {
            get
            {
                return new LogEventColumns("MessageSignature");
            }
        }
        public LogEventColumns MessageVariableValues
        {
            get
            {
                return new LogEventColumns("MessageVariableValues");
            }
        }
        public LogEventColumns Message
        {
            get
            {
                return new LogEventColumns("Message");
            }
        }
        public LogEventColumns Computer
        {
            get
            {
                return new LogEventColumns("Computer");
            }
        }
        public LogEventColumns Severity
        {
            get
            {
                return new LogEventColumns("Severity");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}