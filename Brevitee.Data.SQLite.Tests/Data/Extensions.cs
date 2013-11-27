using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Brevitee;
using Brevitee.OAuth;
using Brevitee.Html;
using System.Web;
using SampleData;
using Newtonsoft.Json.Linq;

namespace Brevitee.Hatagi
{
    public static class Extensions
    {
        public static MvcHtmlString ItemImage(this Item item, object attrs = null)
        {
            return Image(item.MediumImageURL, attrs);
        }

        public static MvcHtmlString SmallImage(this Item item, object attrs = null)
        {
            return Image(item.SmallImageURL, attrs);
        }

        public static MvcHtmlString MediumImage(this Item item, object attrs = null)
        {
            return Image(item.MediumImageURL, attrs);
        }

        public static MvcHtmlString LargeImage(this Item item, object attrs = null)
        {
            return Image(item.LargeImageURL, attrs);
        }

        public static MvcHtmlString Image(string url, object attrs = null)
        {
            TagBuilder b = new TagBuilder("img").Attr("src", url);
            if(attrs != null)
            {
                b.Attrs(attrs);
            }

            return MvcHtmlString.Create(b.ToString());
        }

        public static void CleanQuotes(this JObject jObject)
        {
            foreach (JProperty prop in jObject.Properties())
            {
                jObject[prop.Name] = prop.Value.ToString().Replace("\"", "");
            }
        }
    }
}
