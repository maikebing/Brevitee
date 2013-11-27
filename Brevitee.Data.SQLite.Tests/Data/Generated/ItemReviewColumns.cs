using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ItemReviewColumns: QueryFilter<ItemReviewColumns>, IFilterToken
    {
        public ItemReviewColumns() { }
        public ItemReviewColumns(string columnName)
            : base(columnName)
        { }

        public ItemReviewColumns Id
        {
            get
            {
                return new ItemReviewColumns("Id");
            }
        }

        public ItemReviewColumns ItemId
        {
            get
            {
                return new ItemReviewColumns("ItemId");
            }
        }
        public ItemReviewColumns ReviewId
        {
            get
            {
                return new ItemReviewColumns("ReviewId");
            }
        }
        public ItemReviewColumns UserId
        {
            get
            {
                return new ItemReviewColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}