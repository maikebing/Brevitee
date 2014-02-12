using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Logging;
using System.IO;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public abstract class ResponderBase: IResponder
    {
        Dictionary<string, string> _contentTypes;
        public ResponderBase(BreviteeConf conf)
        {
            this.BreviteeConf = conf;
            this.Logger = Log.Default;

            this._contentTypes = new Dictionary<string, string>();
            this._contentTypes.Add(".json", "application/json");
            this._contentTypes.Add(".js", "application/javascript");
            this._contentTypes.Add(".css", "text/css");
            this._contentTypes.Add(".jpg", "image/jpg");
            this._contentTypes.Add(".gif", "image/gif");
            this._contentTypes.Add(".png", "image/png");
            this._contentTypes.Add(".html", "text/html");

            this._respondToPrefixes = new List<string>();
            this._ignorePrefixes = new List<string>();

            this.AddRespondToPrefix(ResponderSignificantName);            
        }

        protected virtual string ResponderSignificantName
        {
            get
            {
                string responderSignificantName = this.Name;
                if (responderSignificantName.EndsWith("responder"))
                {
                    responderSignificantName = responderSignificantName.Truncate(9);
                }
                return responderSignificantName;
            }
        }

        public ResponderBase(BreviteeConf conf, ILogger logger, RequestHandler requestHandler)
            : this(conf)
        {
            this.Logger = logger;
            this.RequestHandler = requestHandler;
        }

        protected string GetContentType(string path)
        {
            string contentType = string.Empty;
            string ext = Path.GetExtension(path);
            if (this._contentTypes.ContainsKey(ext))
            {
                contentType = this._contentTypes[ext];
            }

            return contentType;
        }

        protected void SetContentType(IResponse response, string path)
        {
            string contentType = GetContentType(path);
            if(!string.IsNullOrEmpty(contentType))
            {
                response.ContentType = contentType;
            }
        }
        
        Dictionary<string, byte[]> _pageCache;
        object _pageCacheLock = new object();
        protected Dictionary<string, byte[]> Cache
        {
            get
            {
                return _pageCacheLock.DoubleCheckLock(ref _pageCache, () => new Dictionary<string, byte[]>());
            }
        }


        protected Dictionary<string, string> ContentTypes
        {
            get
            {
                return this._contentTypes;
            }
        }

        protected internal RequestHandler RequestHandler
        {
            get;
            set;
        }

        public ILogger Logger
        {
            get;
            private set;
        }

        protected internal virtual string Name
        {
            get
            {
                return this.GetType().Name.ToLowerInvariant();
            }
        }

        /// <summary>
        /// The event that fires when a response is sent
        /// </summary>
        public event ResponderEventHandler Responded;

        protected void OnResponded(IContext context)
        {
            if (Responded != null)
            {
                Responded(this, context);
            }
        }

        /// <summary>
        /// The event that fires when a response is not sent
        /// </summary>
        public event ResponderEventHandler NotResponded;

        protected void OnNotResponded(IContext context)
        {
            if (NotResponded != null)
            {
                NotResponded(this, context);
            }
        }

        public BreviteeConf BreviteeConf
        {
            get;
            set;
        }

        public Fs Fs
        {
            get
            {
                return BreviteeConf.Fs;
            }
        }

        public Fs AppFs(string appName)
        {
            return BreviteeConf.AppFs(appName);
        }


        List<string> _respondToPrefixes;
        protected internal void AddRespondToPrefix(string prefix)
        {
            if (_ignorePrefixes.Contains(prefix))
            {
                _ignorePrefixes.Remove(prefix);
            }

            _respondToPrefixes.Add(prefix);
        }

        List<string> _ignorePrefixes;
        protected internal void AddIgnorPrefix(string prefix)
        {
            if (_respondToPrefixes.Contains(prefix))
            {
                _respondToPrefixes.Remove(prefix);
            }

            _ignorePrefixes.Add(prefix);
        }

        protected internal bool WillIgnore(IContext context)
        {
            return ShouldIgnore(context.Request.Url.AbsolutePath.ToLowerInvariant());
        }

        protected internal bool ShouldIgnore(string path)
        {
            bool result = false;
            _ignorePrefixes.Each(ignore =>
            {
                if (!result)
                {
                    result = path.StartsWith(string.Format("/{0}", ignore));
                }
            });

            return result;
        }

        /// <summary>
        /// Returns true if the AbsolutePath of the requested
        /// Url starts with the name of the current class.  Extenders
        /// will provide different implementations based on their
        /// requirements
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool MayRespond(IContext context)
        {
            string lowered = context.Request.Url.AbsolutePath.ToLowerInvariant();
            bool result = false;

            if (!ShouldIgnore(lowered))
            {
                _respondToPrefixes.Each(prefix =>
                {
                    if (!result)
                    {
                        result = lowered.StartsWith(string.Format("/{0}", prefix.ToLowerInvariant()));
                    }
                });
            }

            return result;         
        }
        
        public bool Respond(IContext context)
        {
            bool result = false;
            string path = context.Request.Url.AbsolutePath;
            if (MayRespond(context))
            {
                result = TryRespond(context);
                if (result)
                {
                    OnResponded(context);
                }
                else
                {
                    OnNotResponded(context);
                }
            }            

            return result;
        }

        protected static void SendResponse(IResponse response, byte[] data)
        {
            using (BinaryWriter bw = new BinaryWriter(response.OutputStream))
            {
                bw.Write(data);
                bw.Flush();
            }
        }

        protected static void SendResponse(IResponse response, string content)
        {
            using (StreamWriter sw = new StreamWriter(response.OutputStream))
            {
                sw.Write(content);
                sw.Flush();
            }
        }

        public abstract bool TryRespond(IContext context);        
    }
}
