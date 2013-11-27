using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Brevitee.Analytics.Data
{
    public partial class Url
    {
        public static Uri CurrentSite
        {
            get
            {
                if (HttpContext.Current != null &&
                    HttpContext.Current.Request != null)
                {
                    Uri url = HttpContext.Current.Request.Url;
                    string authority = url.Authority;
                    string scheme = url.Scheme;
                    return new Uri("{0}://{1}"._Format(scheme, authority));
                }

                return null;
            }
        }
    }
}
