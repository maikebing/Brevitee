using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class WishListColumns: QueryFilter<WishListColumns>, IFilterToken
    {
        public WishListColumns() { }
        public WishListColumns(string columnName)
            : base(columnName)
        { }

        public WishListColumns Id
        {
            get
            {
                return new WishListColumns("Id");
            }
        }
        public WishListColumns Name
        {
            get
            {
                return new WishListColumns("Name");
            }
        }
        public WishListColumns Description
        {
            get
            {
                return new WishListColumns("Description");
            }
        }

        public WishListColumns UserId
        {
            get
            {
                return new WishListColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}