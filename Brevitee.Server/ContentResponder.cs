using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Ionic.Zip;
using Brevitee.Yaml;
using Brevitee;
using Brevitee.ServiceProxy.Secure;
using Brevitee.Logging;
using Brevitee.ServiceProxy;
using Brevitee.Server.Renderers;
using Brevitee.Javascript;
using Brevitee.UserAccounts.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yahoo.Yui.Compressor;

namespace Brevitee.Server
{
    /// <summary>
    /// The primary responder for all content files found in ~s:/ (defined as BreviteeServer.ContentRoot)
    /// </summary>
    public class ContentResponder: ResponderBase, IInitialize<ContentResponder>
    {
        public ContentResponder(BreviteeConf  conf)
            : base(conf)
        {
            CommonDustRenderer = new CommonDustRenderer(this);
            this.UseCache = conf.UseCache;
        }
        
        public ContentResponder(BreviteeConf conf, ILogger logger, RequestHandler requestHandler)
            : base(conf, logger, requestHandler)
        {
            CommonDustRenderer = new CommonDustRenderer(this);
            this.UseCache = conf.UseCache;
        }

        /// <summary>
        /// The server content root path, not to be confused with the 
        /// application root which should be [Root]\apps\[appName]
        /// </summary>
        public string Root
        {
            get
            {
                return Fs.Root;
            }
        }

        public bool UseCache
        {
            get;
            set;
        }

        public override bool MayRespond(IHttpContext context)
        {
            return !WillIgnore(context);
        }

        public AppConf[] AppConfigs
        {
            get;
            internal set;
        }

        Dictionary<string, AppContentResponder> _appContentResponders;
        protected internal Dictionary<string, AppContentResponder> AppContentResponders
        {
            get
            {
                if (_appContentResponders == null)
                {
                    _appContentResponders = new Dictionary<string, AppContentResponder>();
                }

                return _appContentResponders;
            }
        }        

        public bool IsAppsInitialized
        {
            get;
            private set;
        }

        /// <summary>
        /// The event that fires when templates are being initialized.
        /// This occurs after file system initialization
        /// </summary>
        public event Action<ContentResponder> CommonDustRendererInitializing;
        public event Action<ContentResponder> CommonDustRendererInitialized;

        protected internal void OnCommonDustRendererInitializing()
        {
            if (CommonDustRendererInitializing != null)
            {
                CommonDustRendererInitializing(this);
            }
        }

        protected internal void OnCommonDustRendererInitialized()
        {
            if (CommonDustRendererInitialized != null)
            {
                CommonDustRendererInitialized(this);
            }
        }

        public CommonDustRenderer CommonDustRenderer
        {
            get;
            protected set;
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
        public virtual void Subscribe(ILogger logger)
        {
            if (!IsSubscribed(logger))
            {
                lock (_subscriberLock)
                {
                    _subscribers.Add(logger);
                }

                string className = typeof(ContentResponder).Name;
                this.FileSystemInitializing += (c) =>
                {
                    logger.AddEntry("{0}::FileSystemInitializ(ING)", className);
                };
                this.FileSystemInitialized += (c) =>
                {
                    logger.AddEntry("{0}::FileSystemInitializ(ED)", className);
                };
                this.AppContentRespondersInitializing += (c) =>
                {
                    logger.AddEntry("{0}::AppContentRespondersInitializ(ING)", className);
                };
                this.AppContentRespondersInitialized += (c) =>
                {
                    logger.AddEntry("{0}::AppContentRespondersInitializ(ED)", className);
                };
                this.AppContentResponderInitializing += (c, a) =>
                {
                    logger.AddEntry("{0}::AppContentResponderInitializ(ING):{1}", className, a.Name);
                };
                this.AppContentResponderInitialized += (c, a) =>
                {
                    logger.AddEntry("{0}::AppContentResponderInitializ(ED):{1}", className, a.Name);
                };
                this.AppInitializing += (c, a) =>
                {
                    logger.AddEntry("{0}::AppInitializ(ING):{1}", className, a.Name);
                };
                this.AppInitialized += (c, a) =>
                {
                    logger.AddEntry("{0}::AppInitializ(ED):{1}", className, a.Name);
                };
                this.Initializing += (c) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
                this.Initialized += (c) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                this.CommonDustRendererInitializing += (c) =>
                {
                    logger.AddEntry("{0}::TemplatesInitializ(ING)", className);
                };
                this.CommonDustRendererInitialized += (c) =>
                {
                    logger.AddEntry("{0}::TemplatesInitializ(ED)", className);
                };
            }
        }

        protected internal void InitializeCommonDustRenderer()
        {
            OnCommonDustRendererInitializing();

            string dustRoot = Path.Combine(Root, "dust");
            StringBuilder allCompiledTemplates = new StringBuilder();
            DirectoryInfo dir = new DirectoryInfo(dustRoot);
            if (dir.Exists)
            {
                CommonDustRenderer = new CommonDustRenderer(this);
            }

            OnCommonDustRendererInitialized();
        }

        public event Action<ContentResponder> AppContentRespondersInitializing;
        public event Action<ContentResponder> AppContentRespondersInitialized;
        
        protected internal void OnAppContentRespondersInitializing()
        {
            if (AppContentRespondersInitializing != null)
            {
                AppContentRespondersInitializing(this);
            }
        }

        public event Action<ContentResponder, AppConf> AppContentResponderInitializing;
        public event Action<ContentResponder, AppConf> AppContentResponderInitialized;

        protected internal void OnAppContentResponderInitializing(AppConf appConf)
        {
            if (AppContentResponderInitializing != null)
            {
                AppContentResponderInitializing(this, appConf);
            }
        }

        protected internal void OnAppContentResponderInitialized(AppConf appConf)
        {
            if (AppContentResponderInitialized != null)
            {
                AppContentResponderInitialized(this, appConf);
            }
        }

        protected internal void OnAppRespondersInitialized()
        {
            if (AppContentRespondersInitialized != null)
            {
                AppContentRespondersInitialized(this);
            }
        }

        object _initAppsLock = new object();
        /// <summary>
        /// Initialize all the AppContentResponders for the 
        /// apps found in the ~s:/apps folder
        /// </summary>
        protected internal void InitializeAppResponders()
        {
            OnAppContentRespondersInitializing();
            lock(_initAppsLock)
            {
                if(!IsAppsInitialized)
                {
                    InitializeAppResponders(BreviteeConf.AppConfigs);
                    
                    AppConfigs = BreviteeConf.AppConfigs;

                    IsAppsInitialized = true;
                }
            }
            OnAppRespondersInitialized();
        }
        
        private void InitializeAppResponders(AppConf[] configs)
        {
            configs.Each(ac =>
            {
                OnAppContentResponderInitializing(ac);
                
                AppContentResponder responder = new AppContentResponder(this, ac);                
                responder.Logger = this.Logger;
                Subscribers.Each(logger =>
                {
                    responder.Subscribe(logger);
                });
                string appName = ac.Name.ToLowerInvariant();
                AppContentResponders[appName] = responder;
                
                OnAppContentResponderInitialized(ac);
            });
        }

        public event Action<ContentResponder, AppConf> AppInitializing;
        protected void OnAppInitializing(AppConf conf)
        {
            if (AppInitializing != null)
            {
                AppInitializing(this, conf);
            }
        }
        public event Action<ContentResponder, AppConf> AppInitialized;
        protected void OnAppInitialized(AppConf conf)
        {
            if (AppInitialized != null)
            {
                AppInitialized(this, conf);
            }        
        }

        protected internal void InitializeApps()
        {
            InitializeApps(AppConfigs);
        }

        private void InitializeApps(AppConf[] configs)
        {
            configs.Each(ac =>
            {
                OnAppInitializing(ac);
                if (!string.IsNullOrEmpty(ac.AppInitializer))
                {
                    Type appInitializer = null;
                    if (!string.IsNullOrEmpty(ac.AppInitializerAssemblyPath))
                    {
                        Assembly assembly = Assembly.LoadFrom(ac.AppInitializerAssemblyPath);
                        appInitializer = assembly.GetType(ac.AppInitializer);
                        if (appInitializer == null)
                        {
                            appInitializer = assembly.GetTypes().Where(t => t.AssemblyQualifiedName.Equals(ac.AppInitializer)).FirstOrDefault();
                        }

                        if (appInitializer == null)
                        {
                            Args.Throw<InvalidOperationException>("The specified AppInitializer type ({0}) wasn't found in the specified assembly ({1})", ac.AppInitializer, ac.AppInitializerAssemblyPath);
                        }
                    }
                    else
                    {
                        appInitializer = Type.GetType(ac.AppInitializer);
                        if (appInitializer == null)
                        {
                            Args.Throw<InvalidOperationException>("The specified AppInitializer type ({0}) wasn't found", ac.AppInitializer);
                        }
                    }

                    IInitialize initializer = appInitializer.Construct<IInitialize>();
                    initializer.Subscribe(Logger);
                    initializer.Initialize();
                }
                OnAppInitialized(ac);
            });
        }

        public bool IsFileSystemInitialized { get; private set; }

        public event Action<ContentResponder> FileSystemInitializing;
        public event Action<ContentResponder> FileSystemInitialized;

        protected void OnFileSystemInitializing()
        {
            if (FileSystemInitializing != null)
            {
                FileSystemInitializing(this);
            }
        }

        protected void OnFileSystemInitialized()
        {
            if (FileSystemInitialized != null)
            {
                FileSystemInitialized(this);
            }
        }

        object _initFsLock = new object();
        /// <summary>
        /// Creates the default files in the path specified by
        /// BreviteeConf.ContentRoot.  Existing files will NOT be
        /// overwritten.  The source of the initial content will be retrieved
        /// either from the compiled resource zip file or
        /// the zip file specified in BreviteeConf.ZipPath
        /// overwritten depending on the value specified in
        /// BreviteeConf.InitializeFileSystemFrom.  Valid 
        /// values are "Resource" or "ZipPath"
        /// </summary>
        protected void InitializeFileSystem()
        {
            OnFileSystemInitializing();

            lock(_initFsLock)
            {
                if(!IsFileSystemInitialized)
                {
                    InitializeFileSystemFrom(BreviteeConf.InitializeFileSystemFromEnum);
                }
            }
            OnFileSystemInitialized();
        }

        private void InitializeFileSystemFrom(InitializeFrom from)
        {
            switch (from)
            {
                case InitializeFrom.Invalid:
                    Logger.AddEntry("Invalid InitializeFileSystemFrom entry specified", LogEventType.Warning);
                    break;
                case InitializeFrom.Resource:
                    InitializeFileSystemFromResource();
                    break;
                case InitializeFrom.ZipPath:
                    InitializeFileSystemFromZipFile(BreviteeConf.ZipPath);
                    break;
            }
            IsFileSystemInitialized = true;
        }

        private void InitializeFileSystemFromZipFile(string path)
        {
            if (File.Exists(path))
            {
                ZipFile zipFile = ZipFile.Read(path);
                zipFile.Each(entry =>
                {
                    entry.Extract(BreviteeConf.ContentRoot, ExtractExistingFileAction.DoNotOverwrite);
                });
            }
        }

        private void InitializeFileSystemFromResource()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] resourceNames = currentAssembly.GetManifestResourceNames();
            resourceNames.Each(rn =>
            {
                bool isRoot = Path.GetExtension(rn).ToLowerInvariant().Equals(".root"); //the resource file content.root is a zip file
                if (isRoot)
                {
                    Stream zipStream = currentAssembly.GetManifestResourceStream(rn);
                    ZipFile zipFile = ZipFile.Read(zipStream);
                    zipFile.Each(entry =>
                    {
                        entry.Extract(BreviteeConf.ContentRoot, ExtractExistingFileAction.DoNotOverwrite);
                    });
                }
            });
        }

        protected internal Includes GetCommonIncludesFromIncludeJs()
        {
            return GetCommonIncludesFromIncludeJs(Root, UseCache);
        }

        protected static internal Includes GetCommonIncludesFromIncludeJs(string root, bool useCache)
        {
            string includeJs = Path.Combine(root, "apps", "include.js");
            return GetIncludesFromIncludeJs(includeJs, useCache);
        }

        /// <summary>
        /// Gets the Includes for the specified AppConf prefixing each
        /// path with /bam/apps/[appName] before returning.  Also adds
        /// the init.js and all viewModel .js files.  Adding the prefix
        /// /bam/apps/[appName] will ensure that the AppContentResponder
        /// picks the scripts from the correct location.
        /// </summary>
        /// <param name="appConf"></param>
        /// <returns></returns>
        protected static internal Includes GetAppIncludes(AppConf appConf)
        {
            string includeJs = Path.Combine(appConf.AppRoot.Root, "include.js");
            string appRoot = "/";
            Includes includes = GetIncludesFromIncludeJs(includeJs, appConf.BreviteeConf.UseCache);
            includes.Scripts.Each((scr, i) =>
            {
                includes.Scripts[i] = Path.Combine(appRoot, scr).Replace("\\", "/");
            });
            includes.Css.Each((css, i) =>
            {
                includes.Css[i] = Path.Combine(appRoot, css).Replace("\\", "/");
            });

            GetPageScripts(appConf).Each(script =>
            {
                includes.AddScript(Path.Combine(appRoot, script).Replace("\\", "/"));
            });

            DirectoryInfo viewModelsDir = appConf.AppRoot.GetDirectory("viewModels");
            FileInfo[] viewModels = viewModelsDir.GetFiles("*.js");
            viewModels.Each(fi =>
            {
                includes.AddScript(Path.Combine(appRoot, "viewModels", fi.Name).Replace("\\", "/"));
            });

            includes.AddScript(Path.Combine(appRoot, "init.js").Replace("\\", "/"));
            
            return includes;
        }

        protected static internal string[] GetPageScripts(AppConf appConf)
        {
            BreviteeApplicationManager manager = new BreviteeApplicationManager(appConf.BreviteeConf);
            string[] pageNames = manager.GetPageNames(appConf.Name);
            List<string> results = new List<string>();
            pageNames.Each(pageName =>
            {
                string script = Path.Combine("pages", pageName + ".js").Replace("\\", "/");
                if (appConf.AppRoot.FileExists(script))
                {
                    results.Add(script);
                }
            });

            return results.ToArray();
        }

        static Dictionary<string, Includes> _includesCache;
        static object _includesCacheLock = new object();
        protected static internal Dictionary<string, Includes> IncludesCache
        {
            get
            {
                return _includesCacheLock.DoubleCheckLock(ref _includesCache, () => new Dictionary<string, Includes>());
            }
        }
        
        Dictionary<string, byte[]> _pageMinCache;
        object _pageMinCacheLock = new object();
        protected Dictionary<string, byte[]> MinCache
        {
            get
            {
                return _pageMinCacheLock.DoubleCheckLock(ref _pageMinCache, () => new Dictionary<string, byte[]>());
            }
        }

        protected static internal Includes GetIncludesFromIncludeJs(string includeJs, bool useCache)
        {
            Includes returnValue = new Includes();
            string[] result = new string[] { };
            if (IncludesCache.ContainsKey(includeJs) && useCache)
            {
                returnValue = IncludesCache[includeJs];
            }
            else if (File.Exists(includeJs))
            {
                lock (_includesCacheLock)
                {
                    dynamic include = includeJs.JsonFromJsLiteralFile("include").JsonToDynamic();
                    returnValue.Css = ((JArray)include["css"]).Select(v => (string)v).ToArray();
                    returnValue.Scripts = ((JArray)include["scripts"]).Select(v => (string)v).ToArray();
                    IncludesCache[includeJs] = returnValue;
                }
            }

            return returnValue;
        }
        #region IResponder Members

        object _handleLock = new object();
        public override bool TryRespond(IHttpContext context)
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            IRequest request = context.Request;
            IResponse response = context.Response;
            Session.Init(context);
            SecureSession.Init(context);

            bool handled = false;
            string path = request.Url.AbsolutePath;
            
            byte[] content = new byte[] { };
            string appName = AppConf.AppNameFromUri(request.Url);
            if(AppContentResponders.ContainsKey(appName))
            {
                handled = AppContentResponders[appName].TryRespond(context);
            }

            lock(_handleLock)
            {
                if (!handled)
                {
                    if (Cache.ContainsKey(path) && UseCache)
                    {
                        content = Cache[path];
                        handled = true;
                    }
                    else if (MinCache.ContainsKey(path) && UseCache) // check the min cache
                    {
                        content = MinCache[path];
                        handled = true;
                    }
                    else if (Fs.FileExists(path))
                    {
                        byte[] temp = ReadFile(Fs, path);

                        content = temp;
                        handled = true;
                    }
                }

                if (handled)
                {
                    SetContentType(response, path);
                    SendResponse(response, content);
                }
            }

            return handled;
        }

        protected byte[] ReadFile(Fs fs, string path)
        {
            byte[] temp = null;
            if (Path.GetExtension(path).ToLowerInvariant().Equals(".js"))
            {
                temp = ReadScript(fs, path);
            }
            else
            {
                temp = fs.ReadBytes(path);
                if (UseCache)
                {
                    Cache.Add(path, temp);
                }
            }
            return temp;
        }

        protected byte[] ReadScript(Fs fs, string path)
        {
            byte[] result = null;
            if (MinCache.ContainsKey(path) && UseCache)
            {
                result = MinCache[path];
            }
            else if (Cache.ContainsKey(path) && UseCache)
            {
                result = Cache[path];
            }
            else
            {
                string script = fs.ReadAllText(path);
                byte[] scriptBytes = SetCacheAndGetBytes(Cache, MinCache, path, script);
                result = scriptBytes;
            }

            return result;
        }

        protected internal byte[] SetCacheAndGetBytes(Dictionary<string, byte[]> cache, Dictionary<string, byte[]> minCache, string path, string script)
        {
            CompressionResult compression;
            if (!script.TryCompress(out compression))
            {
                string message = compression.Exception != null ? compression.Exception.Message: string.Empty;
                string stack = string.Empty;
                if (!string.IsNullOrEmpty(compression.Exception.StackTrace))
                {
                    stack = compression.Exception.StackTrace;
                }
                Logger.AddEntry("Compression of script at path ({0}) failed: {1}\r\n{2}", LogEventType.Warning, path, message, stack);
            }

            byte[] scriptBytes = Encoding.UTF8.GetBytes(script);
            byte[] minBytes = Encoding.UTF8.GetBytes(compression.MinScript);
            cache[path] = scriptBytes;
            minCache[path] = minBytes;
            minCache["{0}.min"._Format(path)] = minBytes;
            return scriptBytes;
        }
        
        #endregion

        public bool IsInitialized
        {
            get
            {
                return IsFileSystemInitialized && IsAppsInitialized;
            }
        }

        public virtual void Initialize()
        {
            if (!IsInitialized)
            {
                OnInitializing();
                
                InitializeFileSystem();
                InitializeCommonDustRenderer();
                InitializeAppResponders();
                InitializeApps();

                OnInitialized();
            }
        }

        public event Action<ContentResponder> Initializing;

        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }

        public event Action<ContentResponder> Initialized;
        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }
    }
}
