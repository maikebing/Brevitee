using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Brevitee.Analytics.Data
{
    public partial class ImageCrawler
    {
        public static void EnsureOne(Brevitee.Analytics.Crawlers.ImageCrawler crawler)
        {
            ImageCrawler value = ImageCrawler.OneWhere(c => c.Name == crawler.Name);
            if (value == null)
            {
                value = new ImageCrawler();
                value.Name = crawler.Name;
                value.RootUrl = crawler.Root;
                value.Save();
            }
            else
            {
                crawler.Root = value.RootUrl;
            }
        }
    }
}
