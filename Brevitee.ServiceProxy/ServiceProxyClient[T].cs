using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using System.Reflection;

namespace Brevitee.ServiceProxy
{
    public class ServiceProxyClient<T> : ServiceProxyClient
    {
        public ServiceProxyClient(string baseAddress)
            : base(baseAddress)
        {
            if (!BaseAddress.EndsWith("/"))
            {
                BaseAddress = string.Format("{0}/", BaseAddress);
            }

            this.MethodUrlFormat = "{BaseAddress}{Verb}/{ClassName}/{MethodName}.json?{Parameters}";
        }

        public ServiceProxyClient(string baseAddress, string implementingClassName)
            : this(baseAddress)
        {
            this.ClassName = implementingClassName;
        }

        string _className;
        object _classNameLock = new object();
        /// <summary>
        /// The name of the implementing class on the server.  If typeof(T)
        /// is an interface as determined by typeof(T).IsInterface then it
        /// is assumed that the classname equals typeof(T).Name.Substring(1)
        /// which drops the first character of the name.
        /// </summary>
        public string ClassName
        {
            get
            {
                return _classNameLock.DoubleCheckLock(ref _className, () => typeof(T).IsInterface ? typeof(T).Name.Substring(1) : typeof(T).Name);
            }
            set
            {
                _className = value;
            }
        }

        string[] _methods;
        object _methodsLock = new object();
        public string[] Methods
        {
            get
            {
                return _methodsLock.DoubleCheckLock(ref _methods, () => ServiceProxySystem.GetProxiedMethods(typeof(T)).Select(m => m.Name).ToArray());
            }
        }

        public string MethodUrlFormat
        {
            get;
            set;
        }

        public string Format
        {
            get;
            set;
        }

        bool _numbered;
        public bool Numbered
        {
            get
            {
                return _numbered;
            }
            set
            {
                _numbered = value;
                _named = !value;
            }
        }

        bool _named;
        public bool Named
        {
            get
            {
                return _named;
            }
            set
            {
                _named = value;
                _numbered = !value;
            }
        }

        public string LastResponse { get; private set; }

        public T1 Invoke<T1>(string methodName, params object[] parameters)
        {
            string result = Invoke(methodName, parameters);
            return result.FromJson<T1>();
        }

        public string Invoke(string methodName, object[] parameters)
        {
            if (!Methods.Contains(methodName))
            {
                throw Args.Exception<InvalidOperationException>("{0} is not proxied from type {1}", methodName, typeof(T).Name);
            }

            string result = string.Empty;
            string queryStringParameters = Numbered ? ParametersToQueryString(parameters) : ParametersToQueryString(NameParameters(methodName, parameters));
            ServiceProxyVerbs verb = queryStringParameters.Length > 2048 ? ServiceProxyVerbs.POST : ServiceProxyVerbs.GET;

            if (verb == ServiceProxyVerbs.POST)
            {
                string requestUrl = MethodUrlFormat.NamedFormat(new { BaseAddress = BaseAddress, Verb = verb.ToString(), ClassName = ClassName, MethodName = methodName, Parameters = "nocache=".RandomLetters(4) });

                // create a string array
                string[] jsonParams = new string[parameters.Length];

                // for each parameter stringify it and shove it into the array
                parameters.Each((o, i) =>
                {
                    jsonParams[i] = parameters[i].ToJson();
                });

                // stringify the array
                string jsonParamsString = (new { jsonParams = jsonParams.ToJson() }).ToJson();

                // post the jsonArrayOfJsonStrings
                Headers.Add("Content-Type", "application/json; charset=utf-8");
                result = UploadString(requestUrl, jsonParamsString);
            }
            else
            {
                string requestUrl = MethodUrlFormat.NamedFormat(new { BaseAddress = BaseAddress, Verb = verb.ToString(), ClassName = ClassName, MethodName = methodName, Parameters = queryStringParameters });
                requestUrl = "{0}&{1}&nocache={2}"._Format(requestUrl, Numbered ? "numbered=1" : "named=1", "".RandomLetters(4));

                result = DownloadString(requestUrl);
            }

            LastResponse = result;
            return result;
        }

        private Dictionary<string, object> NameParameters(string methodName, object[] parameters)
        {
            if (!Methods.Contains(methodName))
            {
                throw Args.Exception<InvalidOperationException>("{0} is not proxied from type {1}", methodName, typeof(T).Name);
            }

            MethodInfo method = typeof(T).GetMethod(methodName);

            List<ParameterInfo> parameterInfos = new List<ParameterInfo>(method.GetParameters());
            parameterInfos.Sort((l, r) => l.MetadataToken.CompareTo(r.MetadataToken));

            if (parameters.Length != parameterInfos.Count)
            {
                throw new InvalidOperationException("Parameter count mismatch");
            }

            Dictionary<string, object> result = new Dictionary<string, object>();
            parameterInfos.Each((pi, i) =>
            {
                result[pi.Name] = parameters[i];
            });

            return result;
        }
    }
}
