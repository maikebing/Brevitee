using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class HaveDescriptionColumns: QueryFilter<HaveDescriptionColumns>, IFilterToken
    {
        public HaveDescriptionColumns() { }
        public HaveDescriptionColumns(string columnName)
            : base(columnName)
        { }

        public HaveDescriptionColumns Id
        {
            get
            {
                return new HaveDescriptionColumns("Id");
            }
        }
        public HaveDescriptionColumns Text
        {
            get
            {
                return new HaveDescriptionColumns("Text");
            }
        }
        public HaveDescriptionColumns LastModified
        {
            get
            {
                return new HaveDescriptionColumns("LastModified");
            }
        }

        public HaveDescriptionColumns HaveId
        {
            get
            {
                return new HaveDescriptionColumns("HaveId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}