using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageCrawlerCollection: DaoCollection<ImageCrawlerColumns, ImageCrawler>
    { 
		public ImageCrawlerCollection(){}
		public ImageCrawlerCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ImageCrawlerCollection(Query<ImageCrawlerColumns, ImageCrawler> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ImageCrawlerCollection(Query<ImageCrawlerColumns, ImageCrawler> q, bool load) : base(q, load) { }
    }
}