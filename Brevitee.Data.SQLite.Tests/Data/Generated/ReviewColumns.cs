using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class ReviewColumns: QueryFilter<ReviewColumns>, IFilterToken
    {
        public ReviewColumns() { }
        public ReviewColumns(string columnName)
            : base(columnName)
        { }

        public ReviewColumns Id
        {
            get
            {
                return new ReviewColumns("Id");
            }
        }
        public ReviewColumns Title
        {
            get
            {
                return new ReviewColumns("Title");
            }
        }
        public ReviewColumns Text
        {
            get
            {
                return new ReviewColumns("Text");
            }
        }
        public ReviewColumns Rating
        {
            get
            {
                return new ReviewColumns("Rating");
            }
        }
        public ReviewColumns Created
        {
            get
            {
                return new ReviewColumns("Created");
            }
        }
        public ReviewColumns Modified
        {
            get
            {
                return new ReviewColumns("Modified");
            }
        }


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}