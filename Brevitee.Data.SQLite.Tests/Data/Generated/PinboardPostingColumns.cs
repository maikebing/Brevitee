using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace SampleData
{
    public class PinboardPostingColumns: QueryFilter<PinboardPostingColumns>, IFilterToken
    {
        public PinboardPostingColumns() { }
        public PinboardPostingColumns(string columnName)
            : base(columnName)
        { }

        public PinboardPostingColumns Id
        {
            get
            {
                return new PinboardPostingColumns("Id");
            }
        }
        public PinboardPostingColumns Content
        {
            get
            {
                return new PinboardPostingColumns("Content");
            }
        }
        public PinboardPostingColumns Dimensions
        {
            get
            {
                return new PinboardPostingColumns("Dimensions");
            }
        }
        public PinboardPostingColumns Coordinates
        {
            get
            {
                return new PinboardPostingColumns("Coordinates");
            }
        }

        public PinboardPostingColumns UserId
        {
            get
            {
                return new PinboardPostingColumns("UserId");
            }
        }

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.ColumnName;
        }
	}
}