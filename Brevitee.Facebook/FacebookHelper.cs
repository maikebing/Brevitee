using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Brevitee;
using Brevitee.Html;
using Brevitee.OAuth;
using System.Web;
using System.Net;
using Brevitee.Hatagi.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Brevitee.Configuration;
using Brevitee.JsProxy;
using Brevitee.JsProxy.Js;

namespace Brevitee.Facebook
{
    public static class FacebookHelper
    {
        //public const string GraphFormat = "https://graph.facebook.com/{0}/{1}";
        public const string ProfileImageUrlFormat = "https://graph.facebook.com/{0}/picture";
        public const string FriendsUrlFormat = "https://graph.facebook.com/{0}/friends";
                
        public static FriendsList GetFriendsJson(this JsProxyHelper helper, User user)
        {
            using (WebClient client = new WebClient())
            {
                string url = string.Format(FriendsUrlFormat, user.SourceId);
                url = AppendAccessToken(url);
                return new FriendsList(client.DownloadString(url));                
            }
        }

        public static MvcHtmlString FbProfilePic(this JsProxyHelper helper, User user, object htmlAttrs = null)
        {
            if (user.IsSystem)
            {
                TagBuilder tag = CreateImgTag(new { style = "width: 25px; height: 25px;" }, DefaultConfiguration.GetAppSetting("SystemProfileImage", "/favicon.ico"));
                return MvcHtmlString.Create(tag.ToString());
            }
            else
            {
                return FbProfilePic(helper, user.SourceId, htmlAttrs);
            }
        }

        public static MvcHtmlString FbProfilePic(this JsProxyHelper helper, string id, object htmlAttrs = null, bool addAccessToken = false)
        {
            string src = string.Format(ProfileImageUrlFormat, id);
            if (addAccessToken)
            {
                src = AppendAccessToken(src);
            }

            TagBuilder t = CreateImgTag(htmlAttrs, src);

            return MvcHtmlString.Create(t.ToString());
        }

        private static TagBuilder CreateImgTag(object htmlAttrs, string src)
        {
            TagBuilder t = new TagBuilder("img")
                    .Attr("src", src);

            if (htmlAttrs != null)
            {
                t.Attrs(htmlAttrs);
            }
            return t;
        }

        public static MvcHtmlString FbProfilePic(this User user, object htmlAttrs = null)
        {
            return FbProfilePic(null, user, htmlAttrs);
        }

        public static string AppendAccessToken(string src, string appender = "?")
        {
            FacebookIdentity user = HttpContext.Current.User.Identity as FacebookIdentity;
            if (user == null)
            {
                throw new ArgumentNullException("HttpContext.Current.User.Identity as FacebookIdentity");
            }
            src = string.Format("{0}{1}access_token={2}", src, appender, user.AccessToken);
            return src;
        }
    }
}
