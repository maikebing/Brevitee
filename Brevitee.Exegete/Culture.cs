using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.Security;

namespace Brevitee.HelperMonkey
{
    public class Culture
    {
        public static event UnhandledExceptionEventHandler ResolveCultureException;

        private static void OnResolveCultureException(Exception ex)
        {
            if (ResolveCultureException != null)
            {
                ResolveCultureException(null, new UnhandledExceptionEventArgs(ex, false));
            }
        }

        public static event UnhandledExceptionEventHandler ResolveRegionException;

        private static void OnResolveRegionException(Exception ex)
        {
            if (ResolveRegionException != null)
            {
                ResolveRegionException(null, new UnhandledExceptionEventArgs(ex, false));
            }
        }

        public static CultureInfo ResolveCulture()
        {
            CultureInfo result = CultureInfo.CurrentCulture;

            if (HttpContext.Current != null &&
                HttpContext.Current.Request != null &&
                HttpContext.Current.Request.UserLanguages != null)
            {
                string[] languages = HttpContext.Current.Request.UserLanguages;

                if (languages == null || languages.Length == 0)
                {
                    try
                    {
                        string language = languages[0].ToLowerInvariant().Trim();
                        result = CultureInfo.CreateSpecificCulture(language);
                    }
                    catch (Exception ex)
                    {
                        OnResolveCultureException(ex);
                    } 
                }                
            }

            return result;
        }

        public static RegionInfo ResolveRegion()
        {
            CultureInfo culture = ResolveCulture();
            return new RegionInfo(culture.LCID);            
        }
    }
}
