using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using Brevitee;
using Brevitee.Incubation;
using Brevitee.Logging;

namespace Brevitee.Server
{
    public class ExecutionRequest
    {
        Incubator _serviceProvider;
        public ExecutionRequest()
        {
            this.IsValid = true;
            this._serviceProvider = new Incubator();
            this.ClassName = string.Empty;
        }

        public ExecutionRequest(IContext context, Incubator serviceProvider = null, ILogger logger = null)
            : this()
        {
            this._serviceProvider = serviceProvider;
            this.SetContext(context);
            this.Logger = logger;
        }

        protected ILogger Logger
        {
            get;
            private set;
        }

        public bool IsValid
        {
            get;
            private set;
        }

        protected string[] Chunks
        {
            get;
            set;
        }

        protected void SetContext(IContext context)
        {
            string[] chunks = GetChunksAndValidate(context);

            if (chunks.Length >= 2)
            {
                ClassName = chunks[0];
                SetMethodAndExt(chunks);
                object executor = _serviceProvider.Get(ClassName);
                if (executor == null)
                {
                    IsValid = false;
                }
                else
                {
                    Type type = executor.GetType();
                    MethodInfo method = type.GetMethod(MethodName);
                    if (method == null)
                    {
                        IsValid = false;
                    }
                }
            }
            else
            {
                IsValid = false;
            }
            Chunks = chunks;
        }

        internal virtual string[] GetChunksAndValidate(IContext context)
        {
            this.Context = context;
            IRequest request = Context.Request;
            string path = request.Url.AbsolutePath;
            return GetChunksAndValidate(path);
        }

        internal string[] GetChunksAndValidate(string path)
        {
            string[] chunks = path.DelimitSplit("/", "\\");
            if (chunks.Length <= 1)
            {
                IsValid = false;
            }
            return chunks;
        }

        internal virtual void SetMethodAndExt(string[] chunks)
        {
            string[] methodChunks = chunks[1].DelimitSplit(".");

            if (methodChunks.Length == 1)
            {
                MethodName = methodChunks[0];
                Ext = ".json";
            }
            else if(methodChunks.Length > 1)
            {
                MethodName = methodChunks[0];
                StringBuilder ext = new StringBuilder();
                for (int i = 1; i < methodChunks.Length; i++)
                {
                    ext.AppendFormat(".{0}", methodChunks[i].ToLowerInvariant());
                }
                Ext = ext.ToString();
            }
        }

        public IContext Context { get; internal set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Ext { get; set; }
        string _postData;
        public string PostData
        {
            get
            {
                if (Context != null && string.IsNullOrEmpty(_postData))
                {
                    using (StreamReader sr = new StreamReader(Context.Request.InputStream))
                    {
                        _postData = sr.ReadToEnd();
                    }
                }

                return _postData;
            }
        }
        public QueryStringArgs QueryString
        {
            get
            {
                return new QueryStringArgs(Context.Request.Url.Query);
            }
        }

        public object Result
        {
            get;
            internal set;
        }

        /// <summary>
        /// The exception, if any, that occurred during execution
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }

        public bool Wrapped
        {
            get
            {
                string val;
                if (QueryString.Has("wrapped", out val))
                {
                    return val.Equals("1") || val.Equals("true") | val.Equals("yes");
                }

                return false;
            }
        }

        public void Execute()
        {
            Execute(_serviceProvider);
        }

        public void Execute(Incubator serviceProvider)
        {
            object executor = serviceProvider.Get(ClassName.PascalCase());
            if (executor != null)
            {
                Execute(executor);
            }
        }

        public void Execute(object target)
        {
            Logger.AddEntry("\t## - Executing Request contentType: {0}", Context.Request.ContentType);
            try
            {
                Type currentType = target.GetType();
                MethodInfo method = currentType.GetMethod(MethodName.PascalCase());
                ParameterInfo[] paramInfos = method.GetParameters();

                object[] paramInstances;
                if (!TryGetPostedJsonParameters(paramInfos, out paramInstances))
                {
                    if (!TryGetQueryStringParameters(paramInfos, out paramInstances))
                    {
                        if (!TryGetChunkParameters(paramInfos, out paramInstances))
                        {
                            //TODO: parse Form post data
                            throw new InvalidOperationException("Unable to extract method parameters");
                        }
                    }                    
                }

                object result = method.Invoke(target, paramInstances);
                Result = Wrapped ? new ExecutionResult(result) : result;
            }
            catch (Exception ex)
            {
                Result = new ExecutionResult(ex);
            }
        }

        protected bool TryGetChunkParameters(ParameterInfo[] paramInfos, out object[] paramInstances)
        {
            paramInstances = new object[paramInfos.Length];
            try
            {
                int availableChunks = Chunks.Length - 2; // minus class and method
                if (availableChunks == paramInfos.Length)
                {
                    for (int i = 0; i < paramInfos.Length; i++)
                    {
                        ParameterInfo info = paramInfos[i];
                        paramInstances[i] = GetParameterValue(info, Chunks[i + 2]);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Exception = ex;
                paramInstances = null;
                return false;
            }
        }

        protected bool TryGetQueryStringParameters(ParameterInfo[] paramInfos, out object[] paramInstances)
        {
            paramInstances = new object[paramInfos.Length];
            try
            {
                QueryStringArgs args = QueryString;
                if (args.Count == paramInfos.Length)
                {
                    for (int i = 0; i < paramInfos.Length; i++)
                    {
                        ParameterInfo info = paramInfos[i];
                        string currentArg;
                        object val = null;
                        if (args.Has(info.Name, out currentArg))
                        {
                            val = GetParameterValue(info, currentArg);
                        }
                        paramInstances[i] = val;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Exception = ex;
                paramInstances = null;
                return false;
            }
        }

        protected bool TryGetPostedJsonParameters(ParameterInfo[] paramInfos, out object[] paramInstances)
        {
            
            paramInstances = new object[paramInfos.Length];
            try
            {
                string[] jsonStrings = JsonConvert.DeserializeObject<string[]>(PostData);//jss.Deserialize<string[]>(PostData);
                if (jsonStrings.Length != paramInfos.Length)
                {
                    throw new TargetParameterCountException();
                }

                for (int i = 0; i < paramInfos.Length; i++)
                {
                    string paramJson = jsonStrings[i];
                    Type paramType = paramInfos[i].ParameterType;
                    paramInstances[i] = JsonConvert.DeserializeObject(paramJson, paramType);//jss.Deserialize(paramJson, paramType);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                this.Exception = ex;
                paramInstances = null;
                return false;
            }
        }

        private static object GetParameterValue(ParameterInfo info, string stringValue)
        {
            object val = null;
            if (info.ParameterType == typeof(string))
            {
                val = stringValue;
            }
            else if (info.ParameterType == typeof(int))
            {
                int n;
                if (int.TryParse(stringValue, out n))
                {
                    val = n;
                }
            }
            else if (info.ParameterType == typeof(long))
            {
                long n;
                if (long.TryParse(stringValue, out n))
                {
                    val = n;
                }
            }
            else if (info.ParameterType == typeof(DateTime))
            {
                DateTime date;
                if (DateTime.TryParse(stringValue, out date))
                {
                    val = date;
                }
            }
            else if (info.ParameterType.IsEnum)
            {
                val = Enum.Parse(info.ParameterType, stringValue);
            }
            return val;
        }

    }
}
