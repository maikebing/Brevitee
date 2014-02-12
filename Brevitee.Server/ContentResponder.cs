using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Ionic.Zip;
using Brevitee.Yaml;
using Brevitee;
using Brevitee.Logging;
using Brevitee.ServiceProxy;
using Brevitee.Server.Renderers;
using Brevitee.Javascript;
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

        public override bool MayRespond(IContext context)
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
        public void Subscribe(ILogger logger)
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
                this.AppsInitializing += (c) =>
                {
                    logger.AddEntry("{0}::AppsInitializ(ING)", className);
                };
                this.AppsInitialized += (c) =>
                {
                    logger.AddEntry("{0}::AppsInitializ(ED)", className);
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

        public event Action<ContentResponder> AppsInitializing;
        public event Action<ContentResponder> AppsInitialized;
        
        protected internal void OnAppsInitializing()
        {
            if (AppsInitializing != null)
            {
                AppsInitializing(this);
            }
        }

        protected internal void OnAppsInitialized()
        {
            if (AppsInitialized != null)
            {
                AppsInitialized(this);
            }
        }

        object _initAppsLock = new object();
        /// <summary>
        /// 
        /// </summary>
        protected internal void InitializeApps()
        {
            OnAppsInitializing();
            lock(_initAppsLock)
            {
                if(!IsAppsInitialized)
                {
                    InitializeAppResponders(BreviteeConf.AppConfigs);
                    
                    AppConfigs = BreviteeConf.AppConfigs;

                    IsAppsInitialized = true;
                }
            }
            OnAppsInitialized();
        }
        
        private void InitializeAppResponders(AppConf[] configs)
        {
            configs.Each(ac =>
            {
                AppContentResponders[ac.Name] = new AppContentResponder(this, ac);
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
            return GetIncludes(includeJs, useCache);
        }

        /// <summary>
        /// Gets the Includes for the specified AppConf prefixing each
        /// path with /bam/apps/[appName] before returning.  Also adds
        /// the init.js and all viewModel .js files.  Adding the prefix
        /// /bam/apps/[appName] will ensure that the AppResponder
        /// picks the scripts from the correct location.
        /// </summary>
        /// <param name="appConf"></param>
        /// <returns></returns>
        protected static internal Includes GetAppIncludesFromIncludeJs(AppConf appConf)
        {
            string includeJs = Path.Combine(appConf.AppRoot.Root, "include.js");
            string appRoot = Path.Combine("/bam", "apps", appConf.Name).Replace("\\", "/");
            Includes includes = GetIncludes(includeJs, appConf.BreviteeConf.UseCache);
            includes.Scripts.Each((scr, i) =>
            {
                includes.Scripts[i] = Path.Combine(appRoot, scr).Replace("\\", "/");
            });
            includes.Css.Each((css, i) =>
            {
                includes.Css[i] = Path.Combine(appRoot, css).Replace("\\", "/");
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

        protected static internal Includes GetIncludes(string includeJs, bool useCache)
        {
            Includes returnValue = new Includes();
            string[] result = new string[] { };
            if (IncludesCache.ContainsKey(includeJs) && useCache)
            {
                returnValue = IncludesCache[includeJs];
            }
            else if (File.Exists(includeJs))
            {
                dynamic include = includeJs.JsonFromJsLiteralFile("include").JsonToDynamic();
                returnValue.Css = ((JArray)include["css"]).Select(v => (string)v).ToArray();
                returnValue.Scripts = ((JArray)include["scripts"]).Select(v => (string)v).ToArray();
                IncludesCache.Add(includeJs, returnValue);
            }

            return returnValue;
        }
        #region IResponder Members

        object _handleLock = new object();
        public override bool TryRespond(IContext context)
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            IRequest request = context.Request;
            IResponse response = context.Response;
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
            JavaScriptCompressor compressor = new JavaScriptCompressor();
            string min = compressor.Compress(script);
            byte[] scriptBytes = Encoding.UTF8.GetBytes(script);
            byte[] minBytes = Encoding.UTF8.GetBytes(min);
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

        public void Initialize()
        {
            if (!IsInitialized)
            {
                OnInitializing();
                
                InitializeFileSystem();
                InitializeCommonDustRenderer();
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
