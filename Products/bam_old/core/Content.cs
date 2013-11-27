using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Brevitee;
using Brevitee.Logging;

namespace Bam.core
{
    public class Content: ResponderBase
    {
        Dictionary<string, string> _cache;

        public Content(Fs fs)
            : base(fs)
        {
            this._cache = new Dictionary<string, string>();                        
        }

        public Content(Fs fs, ILogger logger)
            : base(fs, logger)
        {
            this._cache = new Dictionary<string, string>();
        }

        public override bool MayHandle(IContext context)
        {
            return true;
        }

        #region IResponder Members

        public override bool TryRespond(IContext context)
        {
            IRequest request = context.Request;
            IResponse response = context.Response;

            string path = request.Url.AbsolutePath;
            string[] split = path.DelimitSplit("/");
            if (path.Equals("/"))
            {
                path = "/index.html";
            }

            bool foundcontent = false;
            path = string.Format("~/content{0}", path);

            string content = string.Empty;
            if (_cache.ContainsKey(path))
            {
                content = _cache[path];
                foundcontent = true;
            }
            else
            {
                if (Fs.FileExists(path))
                {
                    content = Fs.ReadAllText(path);
                    _cache.Add(path, content);                    
                    foundcontent = true;
                }
            }

            if (foundcontent)
            {
                SetContentType(response, path);
                SendResponse(response, content);
            }
            return foundcontent;
        }
        
        #endregion
    }
}
