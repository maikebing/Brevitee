using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ReviewCommentColumns: QueryFilter<ReviewCommentColumns>, IFilterToken
    {
        public ReviewCommentColumns() { }
        public ReviewCommentColumns(string columnName)
            : base(columnName)
        { }

        public ReviewCommentColumns Id
        {
            get
            {
                return new ReviewCommentColumns("Id");
            }
        }

        public ReviewCommentColumns ReviewId
        {
            get
            {
                return new ReviewCommentColumns("ReviewId");
            }
        }
        public ReviewCommentColumns CommentId
        {
            get
            {
                return new ReviewCommentColumns("CommentId");
            }
        }
        public ReviewCommentColumns UserId
        {
            get
            {
                return new ReviewCommentColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}