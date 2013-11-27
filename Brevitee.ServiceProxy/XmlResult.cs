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
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Brevitee.ServiceProxy
{
    public class XmlResult: ActionResult
    {
        public XmlResult(object data)
        {
            this.Data = data;
        }

        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            WriteXml(Data, context.HttpContext.Response.OutputStream);
        }

        private void WriteXml(object data, Stream output)
        {
            XmlSerializer ser = new XmlSerializer(data.GetType());
            ser.Serialize(output, data);            
        }
    }
}
