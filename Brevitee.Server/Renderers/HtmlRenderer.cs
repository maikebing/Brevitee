using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee.Web;
using Brevitee.Html;
using Brevitee.Server;
using Brevitee.ServiceProxy;
using System.Reflection;

namespace Brevitee.Server.Renderers
{
    public class HtmlRenderer: ContentRenderer
    {
        public HtmlRenderer(ExecutionRequest request, ContentResponder contentResponder)
            : base(request, contentResponder, "text/html", ".htm", ".html")
        {
            this.AppName = AppConf.AppNameFromUri(request.Request.Url);
            this.ContentResponder = contentResponder;
            this.ExecutionRequest = request;
        }

        public string AppName { get; set; }

        public string TemplateName
        {
            get
            {
                HttpArgs args = new HttpArgs(ExecutionRequest.Request.Url.Query);
                string result = "default";
                args.Has("view", out result);

                return result;
            }
        }


        /// <summary>
        /// Render the response to the output stream of ExecutionRequest.Response
        /// </summary>
        public void Render()
        {
            Render(ExecutionRequest.Result, ExecutionRequest.Response.OutputStream);
        }

        public override void Render(object toRender, Stream output)
        {
            string templates = ContentResponder.AppContentResponders[AppName].AppDustRenderer.CompiledDustTypeTemplates;
            string result = DustScript.Render(templates, TemplateName, toRender);
            byte[] data = Encoding.UTF8.GetBytes(result);
            output.Write(data, 0, data.Length);
        }

       
    }
}
