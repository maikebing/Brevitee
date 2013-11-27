using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.ServiceProxy;

namespace Brevitee.Html
{
    public static class ServiceProxyWebViewPageExtensions
    {
        public static Tag Tag<T>(this System.Web.Mvc.WebViewPage page, string tagName, object attributes = null)
        {
            return new Tag(tagName, attributes);
        }

        public static JsTag Scr<T>(this System.Web.Mvc.WebViewPage<T> page, string src)
        {
            return new JsTag(src);
        }

        public static Tag A<T>(this System.Web.Mvc.WebViewPage<T> page, string href, string name = "")
        {
            return new Tag("a").Attr("href", href).Name(name);
        }
    }
}
