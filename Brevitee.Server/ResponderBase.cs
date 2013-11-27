using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Logging;
using System.IO;

namespace Brevitee.Server
{
    public abstract class ResponderBase: IResponder
    {
        Dictionary<string, string> _contentTypes;
        public ResponderBase(Fs fs)
        {
            this.Fs = fs;
            this.Logger = Log.Default;

            this._contentTypes = new Dictionary<string, string>();
            this._contentTypes.Add(".js", "application/javascript");
            this._contentTypes.Add(".css", "text/css");
            this._contentTypes.Add(".jpg", "image/jpg");
            this._contentTypes.Add(".gif", "image/gif");
            this._contentTypes.Add(".png", "image/png");
        }

        public ResponderBase(Fs fs, ILogger logger)
            : this(fs)
        {
            this.Logger = logger;
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
            response.ContentType = GetContentType(path);
        }

        protected Dictionary<string, string> ContentTypes
        {
            get
            {
                return this._contentTypes;
            }
        }

        public ILogger Logger
        {
            get;
            private set;
        }

        public string Name
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

        public Fs Fs { get; private set; }

        /// <summary>
        /// Returns true if the AbsolutePath of the requested
        /// Url starts with the name of the current class
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual bool MayHandle(IContext context)
        {
            string path = context.Request.Url.AbsolutePath.ToLowerInvariant();
            return path.StartsWith(string.Format("/{0}", this.Name));            
        }
        
        public bool Respond(IContext context)
        {
            bool result = false;
            string path = context.Request.Url.AbsolutePath;
            if (MayHandle(context))
            {
                LogRequest(path);
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

        protected virtual void LogRequest(string path)
        {
            Logger.AddEntry("request for {0}: {1}", this.Name, path);
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
