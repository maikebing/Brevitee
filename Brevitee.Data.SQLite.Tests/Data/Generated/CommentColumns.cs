using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class CommentColumns: QueryFilter<CommentColumns>, IFilterToken
    {
        public CommentColumns() { }
        public CommentColumns(string columnName)
            : base(columnName)
        { }

        public CommentColumns Id
        {
            get
            {
                return new CommentColumns("Id");
            }
        }
        public CommentColumns Text
        {
            get
            {
                return new CommentColumns("Text");
            }
        }
        public CommentColumns Created
        {
            get
            {
                return new CommentColumns("Created");
            }
        }
        public CommentColumns Modified
        {
            get
            {
                return new CommentColumns("Modified");
            }
        }

        public CommentColumns UserId
        {
            get
            {
                return new CommentColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}