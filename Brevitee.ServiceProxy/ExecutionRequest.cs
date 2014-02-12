using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.WebPages;
using System.Web.Script.Serialization;
using System.IO;
using System.Net;
using Brevitee.Logging;
using Brevitee;
using Brevitee.Web;
using Brevitee.Data;
using Brevitee.Incubation;
using Newtonsoft.Json;

namespace Brevitee.ServiceProxy
{
    public class ExecutionRequest
    {
        static ExecutionRequest()
        {
            MaxRecursion = 5;
        }

        public ExecutionRequest()
        {
            this.ViewName = "Default";
            this.IsInitialized = true;
        }

        public ExecutionRequest(string className, string methodName, string ext)
        {
            this.ViewName = "Default";
            this.ClassName = className;
            this.MethodName = methodName;
            this.Ext = ext;

            this.IsInitialized = true;
        }

        public ExecutionRequest(RequestWrapper request, ResponseWrapper response)
        {
            this.Request = request;
            this.Response = response;
        }

        public ExecutionRequest(RequestWrapper request, ResponseWrapper response, ProxyAlias[] aliases)
        {
            this.Request = request;
            this.Response = response;
            this.ProxyAliases = aliases;
        }

        public ExecutionRequest(RequestWrapper request, ResponseWrapper response, ProxyAlias[] aliases, Incubator serviceProvider)
            : this(request, response, aliases)
        {
            this.ServiceProvider = serviceProvider;
        }

        protected internal ProxyAlias[] ProxyAliases
        {
            get;
            set;
        }

        protected internal bool IsInitialized
        {
            get;
            set;
        }

        protected string HttpMethod
        {
            get
            {
                if (Request != null)
                {
                    return Request.HttpMethod.ToUpperInvariant();
                }

                return "GET";
            }
        }

        string _inputString;
        /// <summary>
        /// The input stream of the request read in as 
        /// a string
        /// </summary>
        protected string InputString
        {
            get
            {
                if (Request == null || Request.InputStream == null)
                {
                    _inputString = string.Empty;
                }
                else if (string.IsNullOrEmpty(_inputString))
                {
                    using (StreamReader sr = new StreamReader(Request.InputStream))
                    {
                        _inputString = sr.ReadToEnd();
                    }
                }

                return _inputString;
            }
        }

        HttpArgs _httpArgs;
        protected HttpArgs HttpArgs
        {
            get
            {
                if (_httpArgs == null)
                {
                    string contentType = null;
                    if (Request != null)
                    {
                        contentType = Request.ContentType;
                    }
                 
                    _httpArgs = new HttpArgs(InputString, contentType);
                }

                return _httpArgs;
            }
        }

        object _initLock = new object();
        protected internal void Initialize()
        {
            lock(_initLock)
            {
                if (!IsInitialized)
                {                   
                    if (HttpMethod.Equals("POST") && string.IsNullOrEmpty(Path.GetExtension(Request.Url.AbsolutePath)))
                    {
                        HttpArgs args = HttpArgs;
                        string jsonParams;
                        if (args.Has("jsonParams", out jsonParams))
                        {
                            JsonParams = jsonParams;
                        }
                    }
                    else if (InputString.StartsWith("{"))
                    {
                        JsonParams = InputString;
                    }

                    ParseRequestUrl();
                    IsInitialized = true;
                }
            }
        }

        Uri _requestUrl;
        protected internal Uri RequestUrl
        {
            get
            {
                if (_requestUrl == null)
                {
                    _requestUrl = Request.Url;
                }

                return _requestUrl;
            }
            set
            {
                _requestUrl = value;
            }
        }

        protected internal void ParseRequestUrl()
        {
            // parse the request url to set the className, methodName and ext
            //string[] split = Request.Url.AbsolutePath.DelimitSplit("/");
            Queue<string> split = new Queue<string>(RequestUrl.AbsolutePath.DelimitSplit("/", "."));
            while (split.Count > 0)
            {
                string currentChunk = split.Dequeue();
                string upperred = currentChunk.ToUpperInvariant();
                if (upperred.Equals("GET") || upperred.Equals("POST"))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(_className))
                {
                    if (!ServiceProvider.HasClass(currentChunk) && ProxyAliases != null)
                    {
                        ProxyAlias alias = ProxyAliases.Where(pa => pa.Alias.Equals(currentChunk)).FirstOrDefault();
                        if (alias != null)
                        {
                            _className = alias.ClassName;
                        }
                        else
                        {
                            _className = currentChunk;
                        }
                    }
                    else
                    {
                        _className = currentChunk;
                    }
                }
                else if (string.IsNullOrEmpty(_methodName))
                {
                    _methodName = currentChunk;
                }
                else if (string.IsNullOrEmpty(_ext))
                {
                    _ext = currentChunk;
                }
            }
        }

        string _className;
        public string ClassName
        {
            get
            {
                if (!IsInitialized)
                {
                    Initialize();
                }

                return _className;
            }
            set
            {
                _className = value;
            }
        }

        string _methodName;
        public string MethodName
        {
            get
            {
                if (!IsInitialized)
                {
                    Initialize();
                }

                return _methodName;
            }
            set
            {
                _methodName = value;
            }
        }

        string _ext;
        public string Ext
        {
            get
            {
                if (!IsInitialized)
                {
                    Initialize();
                }

                return _ext;
            }
            set
            {
                _ext = value;
            }
        }

        private void Reset()
        {
            IsInitialized = false;
            Result = null;
        }

        /// <summary>
        /// An array of strings stringified twice.  Parsing as Json will return an array of strings,
        /// each string can be individually parsed into separate objects
        /// </summary>
        public string JsonParams { get; set; }

        Incubator _serviceProvider;
        object _serviceProviderLock = new object();
        public Incubator ServiceProvider
        {
            get
            {
                return _serviceProviderLock.DoubleCheckLock(ref _serviceProvider, () => ServiceProxySystem.Incubator);
            }
            set
            {
                Reset();
                _serviceProvider = value;
            }
        }

        Type _targetType;
        public Type TargetType
        {
            get
            {
                if (_targetType == null && !string.IsNullOrWhiteSpace(ClassName))
                {
                    Instance = ServiceProvider.Get(ClassName, out _targetType);//ServiceProxySystem.Incubator.Get(ClassName, out _targetType);
                }

                return _targetType;
            }
            set
            {
                _targetType = value;
            }
        }
        object _instance;
        public object Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = ServiceProvider.Get(ClassName, out _targetType);//ServiceProxySystem.Incubator.Get(ClassName, out _targetType);
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        MethodInfo _methodInfo;
        public MethodInfo MethodInfo
        {
            get
            {
                if (_methodInfo == null && TargetType != null)
                {
                    _methodInfo = TargetType.GetMethod(MethodName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                }
                return _methodInfo;
            }
        }

        ParameterInfo[] _parameterInfos;
        public ParameterInfo[] ParameterInfos
        {
            get
            {
                if (_parameterInfos == null && MethodInfo != null)
                {
                    _parameterInfos = MethodInfo.GetParameters();
                }

                return _parameterInfos;
            }
        }

        object[] _parameters;
        public object[] Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = GetParameters();
                }
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }

        public static int MaxRecursion
        {
            get;
            set;
        }

        protected object[] GetParameters()
        {
            object[] result = new object[] { }; ;

            if (HttpArgs.Ordered.Length > 0)
            {
                result = new object[HttpArgs.Ordered.Length];
                HttpArgs.Ordered.Each((val, i) =>
                {
                    result[i] = val;
                });
            }
            else if (!string.IsNullOrEmpty(JsonParams))
            {
                // POST: bam.invoke
                string[] jsonStrings = JsonParams.FromJson<string[]>();

                result = GetJsonParameters(jsonStrings);
            }
            else if (Request != null && InputString.Length > 0)
            {
                // POST: probably from a form
                Queue<string> inputValues = new Queue<string>(InputString.Split('&'));

                result = GetFormParameters(inputValues);
            }
            else if (Request != null)
            {
                // GET: parse the querystring
                ViewName = Request.QueryString["view"];
                if (string.IsNullOrEmpty(ViewName))
                {
                    ViewName = "Default";
                }

                string jsonParams = Request.QueryString["jsonParams"];
                bool numbered = !string.IsNullOrEmpty(Request.QueryString["numbered"]) ? true : false;
                bool named = !numbered;

                if (!string.IsNullOrEmpty(jsonParams))
                {
                    dynamic o = JsonConvert.DeserializeObject<dynamic>(jsonParams);
                    string[] jsonStrings = ((string)(o["jsonParams"])).FromJson<string[]>();
                    result = GetJsonParameters(jsonStrings);
                }
                else if (named)
                {
                    result = GetNamedQueryStringParameters();
                }
                else
                {
                    result = GetNumberedParameters();
                }
            }

            return result;
        }
        
        public dynamic GetSuccessWrapper(object toWrap, string methodName = "Unspecified")
        {
            Log.AddEntry("Success::{0}", methodName);
            return new { Success = true, Message = "", Data = toWrap };
        }

        public dynamic GetErrorWrapper(Exception ex, bool stack = true, string methodName = "Unspecified")
        {
            Log.AddEntry("Error::{0}\r\n***{1}\r\n***", ex, methodName, ex.Message);
            string message = GetMessage(ex, stack);
            return new { Success = false, Message = message };
        }

        private string GetMessage(Exception ex, bool stack)
        {
            string st = stack ? ex.StackTrace : "";
            return string.Format("{0}:\r\n\r\n{1}", ex.Message, st);
        }

        private object[] GetJsonParameters(string[] jsonStrings)
        {
            if (jsonStrings.Length != ParameterInfos.Length)
            {
                throw new TargetParameterCountException();
            }

            object[] paramInstances = new object[ParameterInfos.Length];
            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                string paramJson = jsonStrings[i];
                Type paramType = ParameterInfos[i].ParameterType;
                paramInstances[i] = paramJson.FromJson(paramType);

                SetDefault(paramInstances, i);
            }
            return paramInstances;
        }

        private object[] GetNamedQueryStringParameters()
        {
            object[] results = new object[ParameterInfos.Length];
            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                ParameterInfo paramInfo = ParameterInfos[i];
                Type paramType = paramInfo.ParameterType;
                string value = Request.QueryString[paramInfo.Name];
                SetValue(results, i, paramType, value);

                SetDefault(results, i);
            }

            return results;
        }

        private void SetDefault(object[] parameters, int i)
        {
            object val = parameters[i];
            if(val == null && ParameterInfos[i].HasDefaultValue)
            {
                parameters[i] = ParameterInfos[i].DefaultValue;
            }
        }

        private object[] GetNumberedParameters()
        {
            object[] results = new object[ParameterInfos.Length];
            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                Type paramType = ParameterInfos[i].ParameterType;
                string value = Request.QueryString[i.ToString()];
                SetValue(results, i, paramType, value);

                SetDefault(results, i);
            }

            return results;
        }

        private static void SetValue(object[] results, int i, Type paramType, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                results[i] = null;
            }
            else
            {
                if (paramType == typeof(string) ||
                   paramType == typeof(int) ||
                   paramType == typeof(decimal) ||
                   paramType == typeof(long))
                {
                    results[i] = Convert.ChangeType(value, paramType);
                }
                else
                {
                    results[i] = value.FromJson(paramType);
                }
            }
        }
        // parse form input
        private object[] GetFormParameters(Queue<string> inputValues)
        {
            object[] result = new object[ParameterInfos.Length]; // holder for results

            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                ParameterInfo param = ParameterInfos[i];
                Type currentParameterType = param.ParameterType;
                object parameterValue = GetParameterValue(inputValues, currentParameterType);

                result[i] = parameterValue;
            }
            return result;
        }

        private static object GetParameterValue(Queue<string> inputValues, Type currentParameterType)
        {
            return GetParameterValue(inputValues, currentParameterType, 0);
        }

        // this implementation accounts for a complex object having properties of types that potentially have properties named the same
        // as the parent type
        // {Name: "man", Son: {Name: "boy"}}
        // comma delimits Name as man,boy
        private static object GetParameterValue(Queue<string> inputValues, Type currentParameterType, int recursionThusFar)
        {
            object parameterValue = currentParameterType.Construct();

            List<PropertyInfo> properties = new List<PropertyInfo>(currentParameterType.GetProperties());
            properties.Sort((l, r) => l.MetadataToken.CompareTo(r.MetadataToken));

            foreach (PropertyInfo propertyOfCurrentType in properties)
            {
                if (!propertyOfCurrentType.HasCustomAttributeOfType<ExcludeAttribute>())
                {
                    Type typeOfCurrentProperty = propertyOfCurrentType.PropertyType;
                    // string 
                    // int 
                    // long
                    // decimal
                    if (typeOfCurrentProperty == typeof(string) ||
                        typeOfCurrentProperty == typeof(int) ||
                        typeOfCurrentProperty == typeof(long) ||
                        typeOfCurrentProperty == typeof(decimal))
                    {
                        string input = inputValues.Dequeue();
                        string[] keyValue = input.Split('=');
                        string key = null;
                        object value = null;
                        if (keyValue.Length > 0)
                        {
                            key = keyValue[0];
                        }

                        if (keyValue.Length == 1)
                        {
                            value = Convert.ChangeType(string.Empty, typeOfCurrentProperty);
                        }
                        else if (keyValue.Length == 2)
                        {
                            // 4.0 implementation 
                            value = Convert.ChangeType(Uri.UnescapeDataString(keyValue[1]), typeOfCurrentProperty);

                            // 4.5 implementation
                            //value = Convert.ChangeType(WebUtility.UrlDecode(keyValue[1]), typeOfCurrentProperty);
                        }

                        if (propertyOfCurrentType.Name.Equals(key))
                        {
                            propertyOfCurrentType.SetValue(parameterValue, value, null);
                        }
                        else
                        {
                            throw Args.Exception("Unexpected key value {0}, expected {1}", key, propertyOfCurrentType.Name);
                        }
                    }
                    else
                    {
                        if (recursionThusFar <= MaxRecursion)
                        {
                            // object
                            propertyOfCurrentType.SetValue(parameterValue, GetParameterValue(inputValues, propertyOfCurrentType.PropertyType, ++recursionThusFar), null);
                        }
                    }
                }
            }
            return parameterValue;
        }

        public bool HasCallback
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["callback"]);
            }
        }

        string _callBack;
        object _callBackLock = new object();
        public string Callback
        {
            get
            {
                if (string.IsNullOrEmpty(_callBack))
                {
                    lock (_callBackLock)
                    {
                        if (string.IsNullOrEmpty(_callBack))
                        {
                            _callBack = "callback";
                            if (Request != null)
                            {
                                string qCb = Request.QueryString["callback"];
                                if (!string.IsNullOrEmpty(qCb))
                                {
                                    _callBack = qCb;
                                }
                            }
                        }
                    }
                }

                return _callBack;
            }
            set
            {
                _callBack = value;
            }
        }
        public string ViewName { get; set; }

        protected internal RequestWrapper Request
        {
            get;
            set;
        }

        protected internal ResponseWrapper Response
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// The result of executing the request
        /// </summary>
        public object Result
        {
            get;
            internal set;
        }

        public virtual ValidationResult Validate()
        {
            Initialize();
            ValidationResult result = new ValidationResult(this);
            result.Execute();
            return result;
        }

        public bool Execute()
        {
            bool result = false;
            ValidationResult validation = Validate();
            if (!validation.Success)
            {
                Result = validation;
            }
            else
            {
                result = Execute(Instance, false);
            }

            return result;
        }

        public bool WasExecuted
        {
            get;
            private set;
        }
        

        public bool Execute(object target, bool validate = true)
        {
            bool result = false;
            if (validate)
            {
                ValidationResult validation = Validate();
                if (!validation.Success)
                {
                    Result = validation;
                }
            }

            if (Result == null)
            {
                try
                {
                    Initialize();
                    Result = MethodInfo.Invoke(target, Parameters);
                    result = true;
                }
                catch (Exception ex)
                {
                    Result = ex;
                    result = false;
                }
            }

            WasExecuted = true;
            Success = result;
            return result;
        }
    }
}
