using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H = Brevitee.Hatagi.Data;
using Brevitee.Hatagi;
using BA = Brevitee.Html;

namespace Brevitee.Facebook
{
    public class FbProfilePic: BA.Tag
    {
        private const string FbProfilePicFormat = "https://graph.facebook.com/{0}/picture?return_ssl_resources=1{1}";

        public FbProfilePic(H.User user, FbProfilePicSize size = FbProfilePicSize.None)
            : base("img")
        {
            this.User = user;
            string s = string.Empty;
            if (size != FbProfilePicSize.None)
            {
                s = string.Format("&type={0}", size.ToString().ToLower());
            }

            this.AttrFormat("src", FbProfilePicFormat, user.SourceId, s);
        }

        public H.User User
        {
            get;
            private set;
        }
    }
}
