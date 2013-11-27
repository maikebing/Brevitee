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
using Brevitee.Data;
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
        }

        public ExecutionRequest(string className, string methodName, string ext)
        {
            this.ViewName = "Default";
            this.ClassName = className;
            this.MethodName = methodName;
            this.Ext = ext;
        }

        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Ext { get; set; }

        /// <summary>
        /// An array of strings stringified twice.  Parsing as Json will return an array of strings,
        /// each string can be individually parsed into separate objects
        /// </summary>
        public string JsonParams { get; set; }

        Type _targetType;
        public Type TargetType
        {
            get
            {
                if (_targetType == null && !string.IsNullOrWhiteSpace(ClassName))
                {
                    Instance = ServiceProxySystem.Incubator.Get(ClassName, out _targetType);
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
                    _instance = ServiceProxySystem.Incubator.Get(ClassName, out _targetType);
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
                    _methodInfo = TargetType.GetMethod(MethodName);
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
            object[] result = null;

            if (!string.IsNullOrEmpty(JsonParams))
            {
                // POST: bam.invoke
                string[] jsonStrings = JsonParams.FromJson<string[]>();

                result = GetJsonParameters(jsonStrings);
            }
            else if (Request != null && Request.ContentLength > 0)
            {
                // POST: probably from a form
                Queue<string> inputValues;
                using (StreamReader sr = new StreamReader(Request.InputStream))
                {
                    inputValues = new Queue<string>(sr.ReadToEnd().Split('&'));
                }

                result = GetFormParameters(inputValues);
            }
            else
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
            }
            return paramInstances;
        }

        private object[] GetNamedQueryStringParameters()
        {
            object[] results = new object[ParameterInfos.Length];
            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                Type paramType = ParameterInfos[i].ParameterType;
                string value = Request.QueryString[ParameterInfos[i].Name];
                SetValue(results, i, paramType, value);
            }

            return results;
        }

        private object[] GetNumberedParameters()
        {
            object[] results = new object[ParameterInfos.Length];
            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                Type paramType = ParameterInfos[i].ParameterType;
                string value = Request.QueryString[i.ToString()];
                SetValue(results, i, paramType, value);
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

        protected internal HttpRequestBase Request
        {
            get;
            set;
        }

        protected internal HttpResponseBase Response
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
            ValidationResult result = new ValidationResult(this);
            result.Execute();
            return result;
        }

        public void Execute()
        {
            ValidationResult validation = Validate();
            if (!validation.Success)
            {
                Result = validation;
            }
            else
            {
                Execute(Instance, false);
            }
        }

        public void Execute(object target, bool validate = true)
        {
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
                Result = MethodInfo.Invoke(target, Parameters);
            }
        }
    }
}
