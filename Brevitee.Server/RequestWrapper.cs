using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using Brevitee.Configuration;

namespace Brevitee.Server
{
    public class RequestWrapper: IRequest
    {
        public RequestWrapper(HttpListenerRequest request)
        {
            DefaultConfiguration.CopyProperties(request, this);
            this.Wrapped = request;
        }

        Type _wrappedType;
        protected Type WrappedType
        {
            get
            {
                if (_wrappedType == null)
                {
                    if (Wrapped != null)
                    {
                        _wrappedType = Wrapped.GetType();
                    }
                }

                return _wrappedType;
            }
        }

        public object Wrapped
        {
            get;
            private set;
        }

        private void Set(string name, object value)
        {
            if (Wrapped != null)
            {
                WrappedType.GetProperty(name).SetValue(Wrapped, value, null);
            }
        }

        private object Get(string name)
        {
            if (Wrapped != null)
            {
                return WrappedType.GetProperty(name).GetValue(Wrapped, null);
            }

            return null;
        }

        #region IRequest Members

        public string[] AcceptTypes
        {
            get
            {
                return (string[])Get("AcceptTypes");
            }
            set
            {
                Set("AcceptTypes", value);
            }
        }

        public Encoding ContentEncoding
        {
            get
            {
                return (Encoding)Get("ContentEncoding");
            }
            set
            {
                Set("ContentEncoding", value);
            }
        }

        public long ContentLength64
        {
            get;
            private set;
        }

        public string ContentType
        {
            get;
            private set;
        }

        public System.Net.CookieCollection Cookies
        {
            get;
            private set;
        }

        public bool HasEntityBody
        {
            get;
            private set;
        }

        public System.Collections.Specialized.NameValueCollection Headers
        {
            get;
            private set;
        }

        public string HttpMethod
        {
            get;
            private set;
        }

        public System.IO.Stream InputStream
        {
            get;
            private set;
        }

        public Uri Url
        {
            get;
            private set;
        }

        public Uri UrlReferrer
        {
            get;
            private set;
        }

        public string UserAgent
        {
            get;
            private set;
        }

        public string UserHostAddress
        {
            get;
            private set;
        }

        public string UserHostName
        {
            get;
            private set;
        }

        public string[] UserLanguages
        {
            get;
            private set;
        }

        public string RawUrl
        {
            get;
            private set;
        }

        #endregion
    }
}
