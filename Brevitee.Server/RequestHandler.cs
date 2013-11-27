using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Incubation;
using System.IO;
using System.Net;
using Brevitee.Html;
using Brevitee.Logging;

namespace Brevitee.Server
{
    public class RequestHandler: IRequestHandler
    {
        Incubator _incubator;
        List<IResponder> _responders;
        Content _content;
        Css _css;
        Images _images;
        Scripts _scripts;
        Execution _execution;
        ILogger _logger;

        public event Action<IRequestHandler, IResponder> ResponderAdded;

        public RequestHandler(Incubator serviceProvider, Fs fs, ILogger logger = null, bool initResponders = false)
        {
            this._logger = logger ?? Log.Default;
            this._incubator = serviceProvider;
            this.Fs = fs;            
        }

        public ILogger Logger
        {
            get
            {
                return _logger;
            }
        }

        public void InitializeResponders()
        {
            SetResponders(this.Fs, this._logger);
        }

        private void SetResponders(Fs fs, ILogger logger)
        {
            this._content = new Content(fs, logger);
            this._images = new Images(fs, logger);
            this._css = new Css(fs, this._images, logger);            
            this._scripts = new Scripts(fs, logger);
            this._execution = new Execution(fs, logger);
            this._scripts.ServiceProvider = this._execution.ServiceProvider;

            this._responders = new List<IResponder>();
            this.AddResponder(this._images);
            this.AddResponder(this._content);
            this.AddResponder(this._css);
            this.AddResponder(this._scripts);
            this.AddResponder(this._execution);
        }

        public Incubator ServiceProvider
        {
            get { return this._incubator; }
        }

        public Fs Fs
        {
            get;
            private set;
        }

        public void AddResponder(IResponder responder)
        {
            this._responders.Add(responder);
            if (ResponderAdded != null)
            {
                ResponderAdded(this, responder);
            }
        }

        public void RemoveResonder(IResponder responder)
        {
            if (_responders.Contains(responder))
            {
                _responders.Remove(responder);
            }
        }

        public IResponder[] Responders
        {
            get
            {
                return _responders.ToArray();
            }
        }

        public void AddExecutor(object instance)
        {
            _execution.Add(instance);
        }

        public void RemoveExecutor(object instance)
        {
            _execution.Remove(instance.GetType());
        }

        #region IRequestHandler Members

        public void HandleRequest(IContext context)
        {            
            IRequest request = context.Request;
            IResponse response = context.Response;
            IResponder responder = new ResponderList(Fs, _responders);            

            if (!responder.Respond(context))
            {
                using (StreamWriter sw = new StreamWriter(response.OutputStream))
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.StatusDescription = "handler not found";
                    sw.WriteLine("<!DOCTYPE html>");
                    Tag html = new Tag("html");
                    html.Child(new Tag("body")
                        .Child(new Tag("h1").Text("handler not found"))
                        .Child(new Tag("p").Text(string.Format("No handler was found for the path: {0}", request.Url.ToString())))
                    );
                    sw.WriteLine(html.ToHtmlString());
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        #endregion
    }
}
