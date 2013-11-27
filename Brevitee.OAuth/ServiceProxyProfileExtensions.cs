using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Html;
using Brevitee;
using System.Web.Mvc;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace Brevitee.OAuth
{
    public static class ServiceProxyProfileExtensions
    {
        public const string ProfileImageUrlFormat = "https://graph.facebook.com/{0}/picture";
        
        public static MvcHtmlString FacebookProfileImage(this ServiceProxyHelper helper, object htmlAttributes = null)
        {
            return helper.FacebookProfileImage("me", htmlAttributes);
        }

        public static MvcHtmlString FacebookProfileImage(this ServiceProxyHelper helper, string id, object htmlAttributes = null)
        {
            return new TagBuilder("img")
                .Attr("src", string.Format(ProfileImageUrlFormat, id))
                .AttrsIf(htmlAttributes != null, htmlAttributes)
                .ToHtml();
        }
    }
}
