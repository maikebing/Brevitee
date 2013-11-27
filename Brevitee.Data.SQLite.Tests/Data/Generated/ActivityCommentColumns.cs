using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ActivityCommentColumns: QueryFilter<ActivityCommentColumns>, IFilterToken
    {
        public ActivityCommentColumns() { }
        public ActivityCommentColumns(string columnName)
            : base(columnName)
        { }

        public ActivityCommentColumns Id
        {
            get
            {
                return new ActivityCommentColumns("Id");
            }
        }

        public ActivityCommentColumns ActivityId
        {
            get
            {
                return new ActivityCommentColumns("ActivityId");
            }
        }
        public ActivityCommentColumns CommentId
        {
            get
            {
                return new ActivityCommentColumns("CommentId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}