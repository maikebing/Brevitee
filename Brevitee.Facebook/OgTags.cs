using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Html;
using System.Web.Mvc;

namespace Brevitee.Facebook
{
    public class OgTags
    {
        public OgTags() { }
        public static implicit operator MvcHtmlString(OgTags tags)
        {
            return tags.Render();
        }

        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public MvcHtmlString Render()
        {
            Tag title = new Tag("meta")
                .Attr("property", "og:title")
                .Attr("content", this.Title);
            Tag image = new Tag("meta")
                .Attr("property", "og:image")
                .Attr("content", this.Image);
            Tag description = new Tag("meta")
                .Attr("property", "og:description")
                .Attr("content", this.Description);

            return MvcHtmlString.Create(string.Format("{0}{1}{3}", title.ToString(), image.ToString(), description.ToString()));
        }
    }
}
