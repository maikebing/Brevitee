using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Brevitee;
using Brevitee.Data;
using Brevitee.Logging;
using Brevitee.ServiceProxy;
using Brevitee.DaoRef;
using EntitySpaces.Interfaces;

namespace Platform
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (esProviderFactory.Factory == null)
            {
                esProviderFactory.Factory = new EntitySpaces.Loader.esDataProviderFactory();
            }

            Brevitee.BeAwesome();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_Error()
        {
        }
    }
}