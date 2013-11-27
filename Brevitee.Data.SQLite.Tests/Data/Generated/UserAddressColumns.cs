using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserAddressColumns: QueryFilter<UserAddressColumns>, IFilterToken
    {
        public UserAddressColumns() { }
        public UserAddressColumns(string columnName)
            : base(columnName)
        { }

        public UserAddressColumns Id
        {
            get
            {
                return new UserAddressColumns("Id");
            }
        }

        public UserAddressColumns UserId
        {
            get
            {
                return new UserAddressColumns("UserId");
            }
        }
        public UserAddressColumns AddressId
        {
            get
            {
                return new UserAddressColumns("AddressId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}