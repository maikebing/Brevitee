using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Brevitee.Incubation;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace Brevitee.ServiceProxy
{
    public class JsonpResult: JsonResult
    {
        JavaScriptSerializer jss;
        public JsonpResult()
        {
            jss = new JavaScriptSerializer();
        }

        public JsonpResult(object data, string callBack)
            : this()
        {
            this.Data = data;
            this.Callback = callBack;
        }

        public string Callback { get; set; }
        
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            
            string result = string.Format("{0}({1});", Callback, jss.Serialize(Data));

            response.ContentType = "application/javascript";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            response.Write(result);
        }
    }
}
