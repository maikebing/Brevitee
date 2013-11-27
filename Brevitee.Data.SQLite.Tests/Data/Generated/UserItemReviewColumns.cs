using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserItemReviewColumns: QueryFilter<UserItemReviewColumns>, IFilterToken
    {
        public UserItemReviewColumns() { }
        public UserItemReviewColumns(string columnName)
            : base(columnName)
        { }

        public UserItemReviewColumns Id
        {
            get
            {
                return new UserItemReviewColumns("Id");
            }
        }

        public UserItemReviewColumns ReviewerId
        {
            get
            {
                return new UserItemReviewColumns("ReviewerId");
            }
        }
        public UserItemReviewColumns RevieweeId
        {
            get
            {
                return new UserItemReviewColumns("RevieweeId");
            }
        }
        public UserItemReviewColumns ItemId
        {
            get
            {
                return new UserItemReviewColumns("ItemId");
            }
        }
        public UserItemReviewColumns ReviewId
        {
            get
            {
                return new UserItemReviewColumns("ReviewId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}