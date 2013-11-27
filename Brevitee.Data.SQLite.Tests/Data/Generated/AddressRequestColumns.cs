using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class AddressRequestColumns: QueryFilter<AddressRequestColumns>, IFilterToken
    {
        public AddressRequestColumns() { }
        public AddressRequestColumns(string columnName)
            : base(columnName)
        { }

        public AddressRequestColumns Id
        {
            get
            {
                return new AddressRequestColumns("Id");
            }
        }
        public AddressRequestColumns Approved
        {
            get
            {
                return new AddressRequestColumns("Approved");
            }
        }

        public AddressRequestColumns RequesterId
        {
            get
            {
                return new AddressRequestColumns("RequesterId");
            }
        }
        public AddressRequestColumns RequesteeId
        {
            get
            {
                return new AddressRequestColumns("RequesteeId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}