using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class UserReviewColumns: QueryFilter<UserReviewColumns>, IFilterToken
    {
        public UserReviewColumns() { }
        public UserReviewColumns(string columnName)
            : base(columnName)
        { }

        public UserReviewColumns Id
        {
            get
            {
                return new UserReviewColumns("Id");
            }
        }

        public UserReviewColumns ReviewerId
        {
            get
            {
                return new UserReviewColumns("ReviewerId");
            }
        }
        public UserReviewColumns RevieweeId
        {
            get
            {
                return new UserReviewColumns("RevieweeId");
            }
        }
        public UserReviewColumns ReviewId
        {
            get
            {
                return new UserReviewColumns("ReviewId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}