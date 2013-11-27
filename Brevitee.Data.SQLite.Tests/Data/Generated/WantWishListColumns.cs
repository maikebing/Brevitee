using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class WantWishListColumns: QueryFilter<WantWishListColumns>, IFilterToken
    {
        public WantWishListColumns() { }
        public WantWishListColumns(string columnName)
            : base(columnName)
        { }

        public WantWishListColumns Id
        {
            get
            {
                return new WantWishListColumns("Id");
            }
        }

        public WantWishListColumns WantId
        {
            get
            {
                return new WantWishListColumns("WantId");
            }
        }
        public WantWishListColumns WishListId
        {
            get
            {
                return new WantWishListColumns("WishListId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}