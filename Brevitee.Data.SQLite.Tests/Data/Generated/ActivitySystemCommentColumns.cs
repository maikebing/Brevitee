using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ActivitySystemCommentColumns: QueryFilter<ActivitySystemCommentColumns>, IFilterToken
    {
        public ActivitySystemCommentColumns() { }
        public ActivitySystemCommentColumns(string columnName)
            : base(columnName)
        { }

        public ActivitySystemCommentColumns Id
        {
            get
            {
                return new ActivitySystemCommentColumns("Id");
            }
        }

        public ActivitySystemCommentColumns ActivityId
        {
            get
            {
                return new ActivitySystemCommentColumns("ActivityId");
            }
        }
        public ActivitySystemCommentColumns CommentId
        {
            get
            {
                return new ActivitySystemCommentColumns("CommentId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}