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
using Brevitee.Html;

namespace Brevitee.Dust
{
    public class DustTemplateResult: ActionResult
    {
        public DustTemplateResult(dynamic value)
        {
            this.Data = value;
            this.Legend = value.GetType().Name;
        }

        public DustTemplateResult(string legend, dynamic value)
        {
            this.Data = value;
            this.Legend = legend;
        }
        
        public string Legend { get; set; }
        public dynamic Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;

            response.Write(HtmlHelperExtensions.FieldsetFor(null, Data, Legend));
            
        }
    }
}
