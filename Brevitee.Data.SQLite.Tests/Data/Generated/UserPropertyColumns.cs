using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserPropertyColumns: QueryFilter<UserPropertyColumns>, IFilterToken
    {
        public UserPropertyColumns() { }
        public UserPropertyColumns(string columnName)
            : base(columnName)
        { }

        public UserPropertyColumns Id
        {
            get
            {
                return new UserPropertyColumns("Id");
            }
        }
        public UserPropertyColumns Name
        {
            get
            {
                return new UserPropertyColumns("Name");
            }
        }
        public UserPropertyColumns Value
        {
            get
            {
                return new UserPropertyColumns("Value");
            }
        }

        public UserPropertyColumns UserId
        {
            get
            {
                return new UserPropertyColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}