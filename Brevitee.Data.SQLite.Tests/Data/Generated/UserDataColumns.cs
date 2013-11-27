using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserDataColumns: QueryFilter<UserDataColumns>, IFilterToken
    {
        public UserDataColumns() { }
        public UserDataColumns(string columnName)
            : base(columnName)
        { }

        public UserDataColumns Id
        {
            get
            {
                return new UserDataColumns("Id");
            }
        }
        public UserDataColumns Name
        {
            get
            {
                return new UserDataColumns("Name");
            }
        }
        public UserDataColumns Value
        {
            get
            {
                return new UserDataColumns("Value");
            }
        }
        public UserDataColumns DataType
        {
            get
            {
                return new UserDataColumns("DataType");
            }
        }

        public UserDataColumns UserId
        {
            get
            {
                return new UserDataColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}