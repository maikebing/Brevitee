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
    public abstract class ContentRenderer: RendererBase
    {
        public ContentRenderer(ExecutionRequest request, ContentResponder content, string contentType, params string[] extensions)
            :base(contentType, extensions)
        {
            this.ExecutionRequest = request;
            this.ContentResponder = content;
        }


        protected ExecutionRequest ExecutionRequest
        {
            get;
            set;
        }

        protected ContentResponder ContentResponder
        {
            get;
            set;
        }

    }
}
