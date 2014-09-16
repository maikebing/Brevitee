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
using Brevitee.ServiceProxy.Secure;
using Brevitee.Web;
using Brevitee.Server.Renderers;

namespace Brevitee.Server
{
    public class ServiceProxyResponder: ResponderBase, IInitialize<ServiceProxyResponder>
    {
        static Dictionary<string, IRenderer> _renderers;

        static ServiceProxyResponder()
        {
            
        }

        public ServiceProxyResponder(BreviteeConf conf, ILogger logger, RequestHandler requestHandler)
            : base(conf, logger, requestHandler)
        {
            this._commonServiceProvider = new Incubator();
            this._appServiceProviders = new Dictionary<string, Incubator>();
            this._appSecureChannels = new Dictionary<string, SecureChannel>();
            this._commonSecureChannel = new SecureChannel();
            this.SmartRenderer = new SmartRenderer(logger);

            AddCommonService(this._commonSecureChannel);            

            CommonServiceAdded += (type, obj) =>
            {
                CommonSecureChannel.ServiceProvider.Set(type, obj);
            };
            CommonServiceRemoved += (type) =>
            {
                CommonSecureChannel.ServiceProvider.Remove(type);
            };
            AppServiceAdded += (appName, type, instance) =>
            {
                if (!AppSecureChannels.ContainsKey(appName))
                {
                    SecureChannel channel = new SecureChannel();
                    channel.ServiceProvider.CopyFrom(CommonServiceProvider, true);
                    AppSecureChannels.Add(appName, channel);
                }

                AppSecureChannels[appName].ServiceProvider.Set(type, instance, false);
            };            
        }

        
        protected SmartRenderer SmartRenderer
        {
            get;
            private set;
        }

        Incubator _commonServiceProvider;
        public Incubator CommonServiceProvider
        {
            get
            {
                return _commonServiceProvider;
            }
        }

        SecureChannel _commonSecureChannel;
        public SecureChannel CommonSecureChannel
        {
            get
            {
                return _commonSecureChannel;
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

        Dictionary<string, SecureChannel> _appSecureChannels;
        public Dictionary<string, SecureChannel> AppSecureChannels
        {
            get
            {
                return _appSecureChannels;
            }
        }

        /// <summary>
        /// Add the specified instance to the specified appName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appName"></param>
        /// <param name="instance"></param>
        public void AddAppService<T>(string appName, T instance)
        {
            AddAppService(appName, typeof(T), instance);
        }

        /// <summary>
        /// Add the specified instance as a service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void AddCommonService<T>(T instance)
        {
            AddCommonService(typeof(T), instance);
        }

        public void AddAppService(string appName, object instance)
        {
            AddAppService(appName, instance.GetType(), instance);
        }

        public event Action<string, Type, object> AppServiceAdded;
        protected void OnAppServiceAdded(string appName, Type type, object instance)
        {
            if (AppServiceAdded != null)
            {
                AppServiceAdded(appName, type, instance);
            }
        }
        public void AddAppService<T>(string appName, Func<T> instanciator, bool throwIfSet = false)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(instanciator, throwIfSet);
                OnAppServiceAdded(appName, typeof(T), null);
            }
        }

        public void AddAppService<T>(string appName, Func<Type, T> instanciator, bool throwIfSet = false)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(instanciator, throwIfSet);
                OnAppServiceAdded(appName, typeof(T), null);
            }
        }

        public void AddAppService(string appName, Type type, object instance)
        {
            if (_appServiceProviders.ContainsKey(appName))
            {
                _appServiceProviders[appName].Set(type, instance);
                OnAppServiceAdded(appName, type, instance);
            }
        }
        
        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        public void AddCommonService(object instance)
        {
            AddCommonService(instance.GetType(), instance);
        }

        public event Action<Type, object> CommonServiceAdded;
        protected void OnCommonServiceAdded(Type type, object instance)
        {
            if (CommonServiceAdded != null)
            {
                CommonServiceAdded(type, instance);
            }
        }
        /// <summary>
        /// Add the specified instance as an executor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        public void AddCommonService(Type type, object instance)
        {
            _commonServiceProvider.Set(type, instance);
            OnCommonServiceAdded(type, instance);
        }

        public event Action<Type> CommonServiceRemoved;
        protected void OnCommonServiceRemoved(Type type)
        {
            if (CommonServiceRemoved != null)
            {
                CommonServiceRemoved(type);
            }
        }

        /// <summary>
        /// Remove the executor of the specified generic type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveCommonService<T>()
        {
            _commonServiceProvider.Remove<T>();
            OnCommonServiceRemoved(typeof(T));
        }

        /// <summary>
        /// Remove the executor of the specified type
        /// </summary>
        /// <param name="type"></param>
        public void RemoveCommonService(Type type)
        {
            _commonServiceProvider.Remove(type);
            OnCommonServiceRemoved(type);
        }

        /// <summary>
        /// Remove the executor with the specified className
        /// </summary>
        /// <param name="className"></param>
        public void RemoveCommonService(string className)
        {
            Type type;
            _commonServiceProvider.Remove(className, out type);
            OnCommonServiceRemoved(type);
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
        /// List of service class names
        /// </summary>
        public string[] Services
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
        public override bool MayRespond(IHttpContext context)
        {
            return true;
        }
        

        public void RegisterProxiedClasses()
        {
            string serviceProxyRelativePath = "~/bin";
            List<string> registered = new List<string>();
            ForEachProxiedClass((type) =>
            {
                this.AddCommonService(type, type.Construct());
            });

            BreviteeConf.AppConfigs.Each(appConf =>
            {
                string name = appConf.Name.ToLowerInvariant();
                Incubator serviceProvider = new Incubator();
                
                AppServiceProviders[name] = new Incubator();
                
                DirectoryInfo appServicesDir = new DirectoryInfo(appConf.AppRoot.GetAbsolutePath(serviceProxyRelativePath));
                ForEachProxiedClass(appServicesDir, (type) =>
                {
                    this.AddAppService(appConf.Name, type.Construct());
                });

                AddConfiguredServiceProxyTypes(appConf);
            });
        }

        private void AddConfiguredServiceProxyTypes(AppConf appConf)
        {
            appConf.ServiceProxyTypeNames.Each(typeName =>
            {
                Type type = Type.GetType(typeName);
                if (type != null)
                {
                    object instance = null;
                    if (type.TryConstruct(out instance))
                    {
                        this.AddAppService(appConf.Name, instance);
                    }
                }
            });
        }

        private void ForEachProxiedClass(Action<Type> doForEachProxiedType)
        {   
            string serviceProxyRelativePath = "~/bin";
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
                FileInfo[] files = ctrlrDir.GetFiles(BreviteeConf.ServiceSearchPattern);
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
        
        public override bool TryRespond(IHttpContext context)
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
                        Incubator temp = new Incubator();
                        
                        List<ProxyAlias> aliases = new List<ProxyAlias>(GetProxyAliases(ServiceProxySystem.Incubator));
                        temp.CopyFrom(ServiceProxySystem.Incubator, true);

                        aliases.AddRange(GetProxyAliases(CommonServiceProvider));                        
                        temp.CopyFrom(CommonServiceProvider, true);

                        if (AppServiceProviders.ContainsKey(appName))
                        {
                            Incubator appIncubator = AppServiceProviders[appName];
                            aliases.AddRange(GetProxyAliases(appIncubator));
                            temp.CopyFrom(appIncubator, true);
                        }

                        ExecutionRequest execRequest = new ExecutionRequest(context, aliases.ToArray(), temp);                        

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
            string appName = AppConf.AppNameFromUri(request.Url);
            string defaultBaseAddress = ServiceProxySystem.GetBaseAddress(request.Url);
            string nameSpace = request.QueryString["namespace"] ?? "ServiceProxyClients";
            string contractNameSpace = "{0}.Contracts"._Format(nameSpace);
            Incubator combined = new Incubator();
            combined.CopyFrom(CommonServiceProvider);

            if (AppServiceProviders.ContainsKey(appName))
            {
                Incubator appProviders = AppServiceProviders[appName];
                combined.CopyFrom(appProviders, true);
            }

            string[] classNames = request.QueryString["classes"]  == null ? combined.ClassNames: request.QueryString["classes"].DelimitSplit(",", ";");

            StringBuilder csharpCode = ServiceProxySystem.GenerateCSharpProxyCode(defaultBaseAddress, classNames, nameSpace, contractNameSpace, combined);

            response.Headers.Add("Content-Disposition", "attachment;filename=" + nameSpace + ".cs");
            response.Headers.Add("Content-Type", "text/plain");
            byte[] data = Encoding.UTF8.GetBytes(csharpCode.ToString());
            response.OutputStream.Write(data, 0, data.Length);
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
                AddCommonService(new BreviteeApplicationManager(BreviteeConf));                               
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
