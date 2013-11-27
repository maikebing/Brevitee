using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class AddressRequestCommentColumns: QueryFilter<AddressRequestCommentColumns>, IFilterToken
    {
        public AddressRequestCommentColumns() { }
        public AddressRequestCommentColumns(string columnName)
            : base(columnName)
        { }

        public AddressRequestCommentColumns Id
        {
            get
            {
                return new AddressRequestCommentColumns("Id");
            }
        }

        public AddressRequestCommentColumns AddressRequestId
        {
            get
            {
                return new AddressRequestCommentColumns("AddressRequestId");
            }
        }
        public AddressRequestCommentColumns CommentId
        {
            get
            {
                return new AddressRequestCommentColumns("CommentId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}