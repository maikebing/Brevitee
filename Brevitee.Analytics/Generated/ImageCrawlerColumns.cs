using System;
using System.Collections.Generic;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageCrawlerColumns: QueryFilter<ImageCrawlerColumns>, IFilterToken
    {
        public ImageCrawlerColumns() { }
        public ImageCrawlerColumns(string columnName)
            : base(columnName)
        { }
		
		public ImageCrawlerColumns KeyColumn
		{
			get
			{
				return new ImageCrawlerColumns("Id");
			}
		}	
				
        public ImageCrawlerColumns Id
        {
            get
            {
                return new ImageCrawlerColumns("Id");
            }
        }
        public ImageCrawlerColumns Name
        {
            get
            {
                return new ImageCrawlerColumns("Name");
            }
        }
        public ImageCrawlerColumns RootUrl
        {
            get
            {
                return new ImageCrawlerColumns("RootUrl");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ImageCrawler);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}