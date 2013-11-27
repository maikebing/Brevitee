using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class GiveColumns: QueryFilter<GiveColumns>, IFilterToken
    {
        public GiveColumns() { }
        public GiveColumns(string columnName)
            : base(columnName)
        { }

        public GiveColumns Id
        {
            get
            {
                return new GiveColumns("Id");
            }
        }
        public GiveColumns LastModified
        {
            get
            {
                return new GiveColumns("LastModified");
            }
        }

        public GiveColumns HaveId
        {
            get
            {
                return new GiveColumns("HaveId");
            }
        }
        public GiveColumns WantId
        {
            get
            {
                return new GiveColumns("WantId");
            }
        }
        public GiveColumns GiveStatusId
        {
            get
            {
                return new GiveColumns("GiveStatusId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}