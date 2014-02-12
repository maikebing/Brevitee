using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Brevitee;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Yaml;
using System.IO;
using System.Reflection;
using Brevitee.ServiceProxy;
using Brevitee.Web;
using Brevitee.Server.Renderers;

namespace Brevitee.Server
{
    public class ServiceProxyResponder: ResponderBase, IInitialize<ServiceProxyResponder>
    {
        Incubator _commonServiceProvider;
        static Dictionary<string, IRenderer> _renderers;

        static ServiceProxyResponder()
        {
            
        }

        public ServiceProxyResponder(BreviteeConf conf, ILogger logger, RequestHandler requestHandler)
            : base(conf, logger, requestHandler)
        {
            this._commonServiceProvider = new Incubator();
            this._appServiceProviders = new Dictionary<string, Incubator>();
            this.SmartRenderer = new SmartRenderer(logger);
        }

        protected SmartRenderer SmartRenderer
        {
            get;
            private set;
        }

        public Incubator CommonServiceProvider
        {
            get
            {
                return _commonServiceProvider;
            }
        }

        Dictionary<string, Incubator> _appServiceProviders;
        public Dictionary<string, Incubator> AppServiceProviders
        {
            get
            {
                return _appServiceProviders;
            }
        }

        public void AddAppExecutor<T>(string appName, T instance)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set<T>(instance);
            }
        }

        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void AddCommonExecutor<T>(T instance)
        {
            _commonServiceProvider.Set<T>(instance);
        }

        public void AddAppExecutor(string appName, object instance)
        {
            AddAppExecutor(appName, instance.GetType(), instance);
        }

        public void AddAppExecutor(string appName, Type type, object instance)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(type, instance);
            }
        }
        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        public void AddCommonExecutor(object instance)
        {
            AddCommonExecutor(instance.GetType(), instance);
        }

        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        public void AddCommonExecutor(Type type, object instance)
        {
            _commonServiceProvider.Set(type, instance);
        }

        /// <summary>
        /// Remove the executor of the specified generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveCommonExecutor<T>()
        {
            _commonServiceProvider.Remove<T>();
        }

        /// <summary>
        /// Remove the executor of the specified type
        /// </summary>
        /// <param name="type"></param>
        public void RemoveCommonExecutor(Type type)
        {
            _commonServiceProvider.Remove(type);
        }

        /// <summary>
        /// Remove the executor with the specified className
        /// </summary>
        /// <param name="className"></param>
        public void RemoveCommonExecutor(string className)
        {
            _commonServiceProvider.Remove(className);
        }

        /// <summary>
        /// Returns true if the specified generic type has 
        /// been added as an executor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>()
        {
            return _commonServiceProvider.Contains<T>();
        }

        /// <summary>
        /// Returns true if the specified type has been 
        /// added as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Contains(Type type)
        {
            return _commonServiceProvider.Contains(type);
        }

        /// <summary>
        /// List of executor class names
        /// </summary>
        public string[] Executors
        {
            get
            {
                return _commonServiceProvider.ClassNames;
            }
        }
        
        /// <summary>
        /// Always returns true for a ServiceProxyResponder as
        /// this responder is last in line.
        /// </summary>
        /// <param name="context"></param>
        ///// <returns></returns>
        public override bool MayRespond(IContext context)
        {
            return true;
        }
        

        public void RegisterProxiedClasses()
        {
            string serviceProxyRelativePath = "~/ServiceProxies";
            List<string> registered = new List<string>();
            ForEachProxiedClass((type) =>
            {
                this.AddCommonExecutor(type, type.Construct());
            });

            BreviteeConf.AppConfigs.Each(appConf =>
            {
                AppServiceProviders[appConf.Name] = new Incubator();
                DirectoryInfo appServiceProxiesDir = new DirectoryInfo(appConf.AppRoot.GetAbsolutePath(serviceProxyRelativePath));
                ForEachProxiedClass(appServiceProxiesDir, (type) =>
                {
                    this.AddAppExecutor(appConf.Name, type.Construct());
                });
            });

            if (BreviteeConf.ServiceProxyPaths != null)
            {
                BreviteeConf.ServiceProxyPaths.Each(path =>
                {
                    DirectoryInfo serviceProxyDir = new DirectoryInfo(path);
                    if (serviceProxyDir.Exists)
                    {
                        ForEachProxiedClass(serviceProxyDir, (type) =>
                        {
                            this.AddCommonExecutor(type, type.Construct());
                        });
                    }
                });
            }
        }

        private void ForEachProxiedClass(Action<Type> doForEachProxiedType)
        {   
            string serviceProxyRelativePath = "~/ServiceProxies";
            DirectoryInfo ctrlrDir = new DirectoryInfo(Fs.GetAbsolutePath(serviceProxyRelativePath));
            if(ctrlrDir.Exists)
            {
                ForEachProxiedClass(ctrlrDir, doForEachProxiedType);
            }
            else
            {
                Logger.AddEntry("{0}:{1} directory was not found", LogEventType.Warning, this.Name, ctrlrDir.FullName);
            }
        }

        private void ForEachProxiedClass(DirectoryInfo ctrlrDir, Action<Type> doForEachProxiedType)
        {
            if (ctrlrDir.Exists)
            {
                FileInfo[] files = ctrlrDir.GetFiles("*.dll");
                int ol = files.Length;
                for (int i = 0; i < ol; i++)
                {
                    FileInfo file = files[i];
                    Assembly controllerAssembly = Assembly.LoadFrom(file.FullName);
                    Type[] controllerTypes = (from type in controllerAssembly.GetTypes()
                                              where type.HasCustomAttributeOfType<ProxyAttribute>()
                                              select type).ToArray();
                    controllerTypes.Each(t =>
                    {
                        ProxyAttribute attr = t.GetCustomAttributeOfType<ProxyAttribute>();
                        if (!string.IsNullOrEmpty(attr.VarName))
                        {
                            BreviteeConf.AddProxyAlias(attr.VarName, t);
                        }
                        doForEachProxiedType(t);
                    });
                }
            }
        }

        private ProxyAlias[] GetProxyAliases(Incubator incubator)
        {
            List<ProxyAlias> results = new List<ProxyAlias>();
            results.AddRange(BreviteeConf.ProxyAliases);
            incubator.ClassNames.Each(cn =>
            {
                Type currentType = incubator[cn];
                ProxyAttribute attr;
                if (currentType.HasCustomAttributeOfType<ProxyAttribute>(out attr))
                {
                    if (!string.IsNullOrEmpty(attr.VarName) && !attr.VarName.Equals(currentType.Name))
                    {
                        results.Add(new ProxyAlias(attr.VarName, currentType));
                    }
                }
            });

            return results.ToArray();
        }

        #region IResponder Members

        protected virtual string[] ProxyFileNames
        {
            get
            {
                return new string[] { "proxies.js", "proxies.cs", "javascriptproxies", "jsproxies", "csproxies", "csharpproxies" };
            }
        }

        protected virtual string[] JsProxyFileNames
        {
            get
            {
                return new string[] { "proxies.js", "javascriptproxies", "jsproxies" };
            }
        }

        protected virtual string[] CsProxyFileNames
        {
            get
            {
                return new string[] { "proxies.cs", "csproxies", "csharpproxies" };
            }
        }
        
        public override bool TryRespond(IContext context)
        {
            try
            {
                RequestWrapper request = context.Request as RequestWrapper;
                ResponseWrapper response = context.Response as ResponseWrapper;
                string appName = AppConf.AppNameFromUri(request.Url);

                bool returnValue = false;

                if (request != null && response != null)
                {
                    string path = request.Url.AbsolutePath.ToLowerInvariant();

                    if (path.StartsWith("/{0}"._Format(ResponderSignificantName.ToLowerInvariant())))
                    {
                        string[] split = path.DelimitSplit("/", ".");                       
                        
                        if (split.Length >= 2)
                        {
                            string fileName = Path.GetFileName(path);
                            if (JsProxyFileNames.Contains(fileName))
                            {
                                SendJsProxyScript(request, response);
                                returnValue = true;
                            }
                            else if (CsProxyFileNames.Contains(fileName))
                            {
                                SendCsProxyCode(request, response);
                                returnValue = true;
                            }
                        }
                    }
                    else
                    {
                        ProxyAlias[] aliases = GetProxyAliases(CommonServiceProvider);
                        ExecutionRequest execRequest = new ExecutionRequest(request, response, aliases, CommonServiceProvider);
                        ValidationResult validation = execRequest.Validate();
                        if (!validation.Success && AppServiceProviders.ContainsKey(appName))
                        {
                            Incubator appIncubator = AppServiceProviders[appName];
                            aliases = GetProxyAliases(appIncubator);
                            execRequest = new ExecutionRequest(request, response, aliases, appIncubator);
                        }

                        returnValue = execRequest.Execute();
                        if (returnValue)
                        {
                            string ext = Path.GetExtension(path).ToLowerInvariant();
                            if (string.IsNullOrEmpty(ext))
                            {
                                AppConf appConf = this.BreviteeConf[appName];
                                LayoutConf pageConf = new LayoutConf(appConf);
                                string fileName = Path.GetFileName(path);
                                string json = pageConf.ToJson(true);
                                appConf.AppRoot.WriteFile("~/pages/{0}.ba"._Format(fileName), json);
                            }

                            SmartRenderer.Respond(execRequest, RequestHandler.Content);
                        }
                    }
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

        protected void SendJsProxyScript(IRequest request, IResponse response)
        {
            string appName = AppConf.AppNameFromUri(request.Url);

            StringBuilder script = ServiceProxySystem.GenerateJsProxyScript(CommonServiceProvider, CommonServiceProvider.ClassNames);
            if(AppServiceProviders.ContainsKey(appName))
            {
                Incubator appProviders = AppServiceProviders[appName];
                script.AppendLine(ServiceProxySystem.GenerateJsProxyScript(appProviders, appProviders.ClassNames).ToString());
            }

            response.ContentType = "application/javascript";
            byte[] data = Encoding.UTF8.GetBytes(script.ToString());
            response.OutputStream.Write(data, 0, data.Length);
        }

        protected void SendCsProxyCode(IRequest request, IResponse response)
        {
            throw new NotImplementedException();
        }

        public event Action<ServiceProxyResponder> Initializing;
        protected void OnInitializing()
        {
            if(Initializing != null)
            {
                Initializing(this);
            }
        }

        public event Action<ServiceProxyResponder> Initialized;
        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        object _initializeLock = new object();
        public void Initialize()
        {
            OnInitializing();
            lock (_initializeLock)
            {
                AddCommonExecutor(new BreviteeApplicationManager(BreviteeConf));
                RegisterProxiedClasses();
            }
            OnInitialized();
        }
        List<ILogger> _subscribers = new List<ILogger>();
        object _subscriberLock = new object();
        public ILogger[] Subscribers
        {
            get
            {
                if (_subscribers == null)
                {
                    _subscribers = new List<ILogger>();
                }
                lock (_subscriberLock)
                {
                    return _subscribers.ToArray();
                }
            }
        }

        public bool IsSubscribed(ILogger logger)
        {
            lock (_subscriberLock)
            {
                return _subscribers.Contains(logger);
            }
        }
        public void Subscribe(ILogger logger)
        {
            if (!IsSubscribed(logger))
            {
                lock (_subscriberLock)
                {
                    _subscribers.Add(logger);
                }

                string className = typeof(ServiceProxyResponder).Name;
                Initialized += (sp) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                Initializing += (sp) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
            }
        }
    }
}
