using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee.Web;
using Brevitee.Html;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Server;
using Brevitee.ServiceProxy;

namespace Brevitee.Server.Renderers
{
    public class SmartRenderer: RendererBase
    {
        Dictionary<string, IRenderer> _renderers;
        public SmartRenderer(ILogger logger)
            : base("text/plain", "")
        {
            this._renderers = new Dictionary<string, IRenderer>();
            this.Logger = logger;
            this.AddBaseRenderers();
        }

        protected ILogger Logger { get; set; }

        public void Respond(ExecutionRequest request, ContentResponder contentResponder)
        {
            IRenderer renderer = ResolveRenderer(request, contentResponder);

            renderer.Respond(request);
        }

        private IRenderer ResolveRenderer(ExecutionRequest request, ContentResponder contentResponder)
        {
            OnResolvingRenderer(request);
            
            string path = request.Request.Url.AbsolutePath;
            string ext = Path.GetExtension(path);
            //string callBack = request.Request.QueryString["callback"];

            IRenderer renderer = null;

            if (_renderers.ContainsKey(ext))
            {
                renderer = _renderers[ext];
            }
            else
            {
                renderer = new HtmlRenderer(request, contentResponder);
            }

            if (string.IsNullOrEmpty(ext))
            {
                renderer = _renderers[".json"];

                // check for a format querystring param
                string format = request.Request.QueryString["format"];
                if (!string.IsNullOrEmpty(format))
                {
                    if (_renderers.ContainsKey(format))
                    {
                        renderer = _renderers[format];
                    }
                }

                SetContentType(request);
            }
            
            if (request.HasCallback || ScriptRenderer.Extensions.Contains(ext.ToLowerInvariant()))
            {
                renderer = new ScriptRenderer(request, contentResponder);
            }

            OnResolvedRenderer(request, renderer);
            return renderer;
        }

        public event Action<SmartRenderer, ExecutionRequest> ResolvingRenderer;
        protected void OnResolvingRenderer(ExecutionRequest request)
        {
            if (ResolvingRenderer != null)
            {
                ResolvingRenderer(this, request);
            }
        }
        public event Action<SmartRenderer, ExecutionRequest, IRenderer> ResolvedRenderer;
        protected void OnResolvedRenderer(ExecutionRequest request, IRenderer renderer)
        {
            if (ResolvedRenderer != null)
            {
                ResolvedRenderer(this, request, renderer);
            }
        }


        private void SetContentType(ExecutionRequest request)
        {
            // determine what the requestor wants the content type to be
            // set to in the response
            string contentType = ContentType;
            string queryContentType = request.Request.QueryString["contenttype"];
            if (!string.IsNullOrEmpty(queryContentType))
            {
                contentType = queryContentType;
            }

            request.Response.ContentType = contentType;
        }

        public override void Render(object toRender, Stream output)
        {
            // if there is no extension ensure the result is a string
            // use our own Render method
            string msg = "No renderer was found and the specified object to render was not a string or byte array: ({0}):: ({1})"._Format(toRender.GetType().Name, toRender.ToString());
            if (toRender == null)
            {
                msg = "toRender was null";
            }

            string toRenderAsString = toRender as string;
            byte[] toRenderAsByteArray = toRender as byte[];            
            if (toRenderAsString == null && toRenderAsByteArray == null)
            {
                Tag div = new Tag("div").Class("error").Text(msg);
                toRenderAsString = div.ToHtmlString();
                toRenderAsByteArray = Encoding.UTF8.GetBytes(toRenderAsString);
            }
            else if(toRenderAsString != null)
            {
                toRenderAsByteArray = Encoding.UTF8.GetBytes(toRenderAsString);
            }

            output.Write(toRenderAsByteArray, 0, toRenderAsByteArray.Length);
        }

        private void AddBaseRenderers()
        {
            AddRenderer(new JsonRenderer());
            AddRenderer(new XmlRenderer());
            AddRenderer(new YamlRenderer());
            AddRenderer(new TxtRenderer());
        }

        public void AddRenderer(IRenderer renderer)
        {
            if (_renderers == null)
            {
                _renderers = new Dictionary<string, IRenderer>();
            }

            renderer.Extensions.Each(ext =>
            {
                if (!_renderers.ContainsKey(ext))
                {
                    _renderers.Add(ext, renderer);
                }
                else
                {
                    IRenderer alreadySet = _renderers[ext];
                    Logger.AddEntry("{0}::Renderer of type ({1}) for extension ({2}) has already been added, Renderer of type ({3}) will not be added",
                        LogEventType.Warning,
                        typeof(ServiceProxyResponder).Name,
                        alreadySet.GetType().Name,
                        ext,
                        renderer.GetType().Name
                    );
                }
            });
        }
    }
}
