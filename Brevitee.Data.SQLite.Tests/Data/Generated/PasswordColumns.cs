using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class PasswordColumns: QueryFilter<PasswordColumns>, IFilterToken
    {
        public PasswordColumns() { }
        public PasswordColumns(string columnName)
            : base(columnName)
        { }

        public PasswordColumns Id
        {
            get
            {
                return new PasswordColumns("Id");
            }
        }
        public PasswordColumns SHA256
        {
            get
            {
                return new PasswordColumns("SHA256");
            }
        }
        public PasswordColumns Modified
        {
            get
            {
                return new PasswordColumns("Modified");
            }
        }
        public PasswordColumns Verified
        {
            get
            {
                return new PasswordColumns("Verified");
            }
        }

        public PasswordColumns UserId
        {
            get
            {
                return new PasswordColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}