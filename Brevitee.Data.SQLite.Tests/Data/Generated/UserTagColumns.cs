using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserTagColumns: QueryFilter<UserTagColumns>, IFilterToken
    {
        public UserTagColumns() { }
        public UserTagColumns(string columnName)
            : base(columnName)
        { }

        public UserTagColumns Id
        {
            get
            {
                return new UserTagColumns("Id");
            }
        }

        public UserTagColumns UserId
        {
            get
            {
                return new UserTagColumns("UserId");
            }
        }
        public UserTagColumns TagId
        {
            get
            {
                return new UserTagColumns("TagId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}