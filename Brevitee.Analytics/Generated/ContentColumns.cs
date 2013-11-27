using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ContentColumns: QueryFilter<ContentColumns>, IFilterToken
    {
        public ContentColumns() { }
        public ContentColumns(string columnName)
            : base(columnName)
        { }
		
		public ContentColumns KeyColumn
		{
			get
			{
				return new ContentColumns("Id");
			}
		}	
				
        public ContentColumns Id
        {
            get
            {
                return new ContentColumns("Id");
            }
        }
        public ContentColumns Hash
        {
            get
            {
                return new ContentColumns("Hash");
            }
        }
        public ContentColumns HashAlgorithm
        {
            get
            {
                return new ContentColumns("HashAlgorithm");
            }
        }
        public ContentColumns Date
        {
            get
            {
                return new ContentColumns("Date");
            }
        }
        public ContentColumns Value
        {
            get
            {
                return new ContentColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Content);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}