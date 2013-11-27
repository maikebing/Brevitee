using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class AddressColumns: QueryFilter<AddressColumns>, IFilterToken
    {
        public AddressColumns() { }
        public AddressColumns(string columnName)
            : base(columnName)
        { }

        public AddressColumns Id
        {
            get
            {
                return new AddressColumns("Id");
            }
        }
        public AddressColumns Line1
        {
            get
            {
                return new AddressColumns("Line1");
            }
        }
        public AddressColumns Line2
        {
            get
            {
                return new AddressColumns("Line2");
            }
        }
        public AddressColumns City
        {
            get
            {
                return new AddressColumns("City");
            }
        }
        public AddressColumns StateOrProvince
        {
            get
            {
                return new AddressColumns("StateOrProvince");
            }
        }
        public AddressColumns PostalCode
        {
            get
            {
                return new AddressColumns("PostalCode");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}