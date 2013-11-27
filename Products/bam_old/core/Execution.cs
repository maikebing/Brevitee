using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Dust;
using Brevitee.Yaml;
using System.IO;
using System.Reflection;

namespace Bam.core
{
    public class Execution: ResponderBase
    {
        Incubator _serviceProvider;
        static Dictionary<string, Action<ExecutionRequest, IResponse>> _renderers;

        static Execution()
        {
            _renderers = new Dictionary<string, Action<ExecutionRequest, IResponse>>();
            _renderers.Add(".html", Html);
            _renderers.Add(".json", Json);
            _renderers.Add(".yaml", Yaml);
            _renderers.Add(".txt", Txt);
        }

        public Execution(Fs fs)
            : base(fs)
        {
            this._serviceProvider = new Incubator();
        }

        public Execution(Fs fs, ILogger logger)
            : base(fs, logger)
        {
            this._serviceProvider = new Incubator();
        }

        public Incubator ServiceProvider
        {
            get
            {
                return this._serviceProvider;
            }
        }

        public static Dictionary<string, Action<ExecutionRequest, IResponse>> ExtensionRenderActions
        {
            get
            {
                return _renderers;
            }
        }

        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void Add<T>(T instance)
        {
            _serviceProvider.Set<T>(instance);
        }

        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        public void Add(object instance)
        {
            Add(instance.GetType(), instance);
        }

        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        public void Add(Type type, object instance)
        {
            _serviceProvider.Set(type, instance);
        }

        /// <summary>
        /// Remove the executor of the specified generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>()
        {
            _serviceProvider.Remove<T>();
        }

        /// <summary>
        /// Remove the executor of the specified type
        /// </summary>
        /// <param name="type"></param>
        public void Remove(Type type)
        {
            _serviceProvider.Remove(type);
        }

        /// <summary>
        /// Remove the executor with the specified className
        /// </summary>
        /// <param name="className"></param>
        public void Remove(string className)
        {
            _serviceProvider.Remove(className);
        }

        /// <summary>
        /// Returns true if the specified generic type has 
        /// been added as an executor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>()
        {
            return _serviceProvider.Contains<T>();
        }

        /// <summary>
        /// Returns true if the specified type has been 
        /// added as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Contains(Type type)
        {
            return _serviceProvider.Contains(type);
        }

        /// <summary>
        /// List of executor class names
        /// </summary>
        public string[] Executors
        {
            get
            {
                return _serviceProvider.ClassNames;
            }
        }
        
        /// <summary>
        /// Returns true if the AbsolutePath of the current request
        /// does not start with the name of the current class
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool MayHandle(IContext context)
        {
            return !base.MayHandle(context);
        }

        protected static void Html(ExecutionRequest request, IResponse response)
        {
            Stream output = response.OutputStream;
            QueryStringArgs qs = request.QueryString;
            string view = string.IsNullOrEmpty(qs["view"]) ? "Default" : qs["view"].ToLowerInvariant();
            
            // TODO: write the default template if it doesn't exist
            // use Dust.TemplateIsRegistered(view);
            if (!Dust.TemplateIsRegistered(view))
            {
                
            }

            using (StreamWriter sw = new StreamWriter(output))
            {
                sw.Write(Dust.Render(view, request.Result));
                sw.Flush();
                sw.Close();
            }
        }

        protected static void Json(ExecutionRequest request, IResponse response)
        {
            response.ContentType = "application/json";
            SendResponse(response, request.Result.ToJson());
            response.OutputStream.Close();
        }

        protected static void Yaml(ExecutionRequest request, IResponse response)
        {
            response.ContentType = "application/x-yaml";
            SendResponse(response, request.Result.ToYaml());
            response.OutputStream.Close();
        }

        protected static void Txt(ExecutionRequest request, IResponse response)
        {
            string value = request.Result == null ? "" : request.Result.PropertiesToString();
            SendResponse(response, value);
        }
        
        #region IResponder Members

        public override bool TryRespond(IContext context)
        {
            try
            {
                ExecutionRequest request = new ExecutionRequest(context, _serviceProvider, Logger);
                bool returnValue = false;
                if (request.IsValid)
                {
                    request.Execute(_serviceProvider);
                    string ext = ".json";

                    if (_renderers.ContainsKey(request.Ext))
                    {
                        ext = request.Ext;
                    }

                    _renderers[ext](request, context.Response);
                    returnValue = true;
                }

                return returnValue;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("An error occurred in {0}.{1}: {2}", ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                return false;
            }
        }

        #endregion
    }
}
