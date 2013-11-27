using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageColumns: QueryFilter<ImageColumns>, IFilterToken
    {
        public ImageColumns() { }
        public ImageColumns(string columnName)
            : base(columnName)
        { }
		
		public ImageColumns KeyColumn
		{
			get
			{
				return new ImageColumns("Id");
			}
		}	
				
        public ImageColumns Id
        {
            get
            {
                return new ImageColumns("Id");
            }
        }
        public ImageColumns Date
        {
            get
            {
                return new ImageColumns("Date");
            }
        }
        public ImageColumns CrawlerId
        {
            get
            {
                return new ImageColumns("CrawlerId");
            }
        }

        public ImageColumns UrlId
        {
            get
            {
                return new ImageColumns("UrlId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Image);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}