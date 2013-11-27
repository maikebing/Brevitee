using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brevitee.Logging;
using Brevitee.Configuration;

namespace Brevitee.ServiceProxy
{
    public abstract class BaseController : Controller
    {
        public string ApplicationName
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
            }
        }


        public dynamic GetSuccessWrapper(object toWrap, string methodName = "Unspecified")
        {
            Log.AddEntry("Success::{0}", methodName);
            return new { Success = true, Message = "", Data = toWrap };
        }

        public dynamic GetErrorWrapper(Exception ex, bool stack = true, string methodName = "Unspecified")
        {
            Log.AddEntry("Error::{0}\r\n***{1}\r\n***", ex, methodName, ex.Message);
            string message = GetMessage(ex, stack);
            return new { Success = false, Message = message };
        }

        private string GetMessage(Exception ex, bool stack)
        {
            string st = stack ? ex.StackTrace : "";
            return string.Format("{0}:\r\n\r\n{1}", ex.Message, st);
        }
    }
}
