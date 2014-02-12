using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brevitee.Management;
using Brevitee.ServiceProxy;
using System.Reflection;

namespace $rootnamespace$.Controllers
{
    public class BamDotAppController : BaseController
    {
        //
        // GET: /Application/

        public ActionResult Start(string appName = "main")
        {
            Fs.RegisterProxy(appName);
            return View("Start", (object)appName);
        }

        public ActionResult DisableFsAccess()
        {
            try
            {
                Fs.UnregisterProxy();
                return Json(GetSuccessWrapper(true, MethodBase.GetCurrentMethod().Name));
            }
            catch (Exception ex)
            {
                return Json(GetErrorWrapper(ex, true, MethodBase.GetCurrentMethod().Name), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
