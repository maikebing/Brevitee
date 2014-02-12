using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using Brevitee.Html;
using Brevitee.Logging;
using Brevitee;
using Brevitee.Incubation;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Brevitee.Configuration;
using Brevitee.ServiceProxy;
using Brevitee.Web;
using System.IO;

namespace Brevitee.Server
{
    public class BreviteeServer: IInitialize<BreviteeServer>
    {
        HttpServer _server;
        
        #region configurable ctors
        public BreviteeServer()
        {
            this.EnableDao = true;
            this.EnableServiceProxy = true;
            this.Initialized += InitializeHandler;
            LoadConf();
        }

        public BreviteeServer(BreviteeConf conf)
        {
            this.EnableDao = true;
            this.EnableServiceProxy = true;
            this.Initialized += InitializeHandler;
            this.SetConf(conf);
        }

        private void InitializeHandler(BreviteeServer server)
        {
            if (server.InitializeTemplates)
            {
                TemplateInitializer.Initialize();
            }

            this.IsInitialized = true;
        }

        TemplateInitializerBase _templateInitializer;
        object _templateInitializerLock = new object();
        public TemplateInitializerBase TemplateInitializer
        {
            get
            {
                return _templateInitializerLock.DoubleCheckLock(ref _templateInitializer, () => new DustTemplateInitializer(this));
            }
            set
            {
                _templateInitializer = value;
            }
        }
        
        #endregion

        public bool IsInitialized
        {
            get;
            private set;
        }

        public event Action<BreviteeServer> Initializing;
        public event Action<BreviteeServer> Initialized;

        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }

        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }

        public virtual void Initialize()
        {
            if (!this.IsInitialized)
            {
                OnInitializing();
                LoadConf();

                Subscribe(Logger);
                SubscribeResponders(Logger);

                EnsureDefaults();
                Logger.AddEntry("{0} initializing: {1}", this.GetType().Name, this.PropertiesToString());
                ConfigureHttpServer();

                InitializeResponders();
                
                OnInitialized();
            }
            else
            {
                Logger.AddEntry("Initialize called but the {0} was already initialized", LogEventType.Warning, this.GetType().Name);
            }
        }

        protected virtual void InitializeResponders()
        {
            ContentResponder.Initialize();
            if (EnableDao)
            {
                DaoResponder.Initialize();
            }
            if (EnableServiceProxy)
            {
                ServiceProxyResponder.Initialize();
            }
        }

        protected virtual void SubscribeResponders(ILogger logger)
        {
            ContentResponder.Subscribe(logger);
            if (EnableDao)
            {
                DaoResponder.Subscribe(logger);
            }
            if (EnableServiceProxy)
            {
                ServiceProxyResponder.Subscribe(logger);
            }
        }

        private void ConfigureHttpServer()
        {
            int maxThreads = int.Parse(this.MaxThreads);
            if (maxThreads < 5)
            {
                maxThreads = 5;
            }
            int port = int.Parse(this.Port);
            _server = new HttpServer(maxThreads, Logger, port);
            _server.HostName = "+";
            _server.ProcessRequest += ProcessRequest;
        }

        private void EnsureDefaults()
        {
            if (string.IsNullOrEmpty(this.MaxThreads))
            {
                this.MaxThreads = "25";
                Logger.AddEntry("Set MaxThreads to default value {0}", this.MaxThreads);
            }

            if (string.IsNullOrEmpty(this.Port) || this.Port.Equals("true"))
            {
                this.Port = "8080";
                Logger.AddEntry("Set port to default value {0}", this.Port);
            }
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
                string className = typeof(BreviteeServer).Name;
                this.Initializing += (s) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };
                this.Initialized += (s) =>
                {
                    logger.AddEntry("{0}::Initializ(ED)", className);
                };
                this.LoadingConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Load(ING) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.LoadedConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Load(ED) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.SettingConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Sett(ING) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.SettedConf += (s, c) =>
                {
                    logger.AddEntry("{0}::Sett(ED) configuration, current config: \r\n{1}", className, c.PropertiesToString());
                };
                this.Starting += (s) =>
                {
                    logger.AddEntry("{0}::Start(ING) on port {1}", className, _server.Port.ToString());
                };

                this.Started += (s) =>
                {
                    logger.AddEntry("{0}::Start(ED)", className);
                };

                this.Stopping += (s) =>
                {
                    logger.AddEntry("{0}::stopping", className);
                };

                this.Stopped += (s) =>
                {
                    logger.AddEntry("{0}::stopped", className);
                };
            }
        }

        ILogger _logger;
        object _loggerLock = new object();
        public ILogger Logger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () =>
                {
                    Log.Start();
                    return Log.Default;
                });
            }
            set
            {
                if (_logger != null)
                {
                    _logger.StopLoggingThread();
                }

                _logger = value;
                _logger.RestartLoggingThread();
                if (IsRunning)
                {
                    Restart();
                }
            }
        }

        public ProxyAlias[] ProxyAliases
        {
            get;
            set;
        }

        public bool GenerateDao
        {
            get;
            set;
        }

        public bool InitializeTemplates
        {
            get;
            set;
        }

        string _maxThreads;
        public string MaxThreads
        {
            get
            {
                return _maxThreads;
            }
            set
            {
                _maxThreads = value;
            }
        }

        string _port;
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }


        public string ContentRoot
        {
            get
            {
                return Fs.Root;
            }
            set
            {
                Fs.Root = value;
            }
        }

        Fs _fs;
        object _fsLock = new object();
        protected internal Fs Fs
        {
            get
            {
                return _fsLock.DoubleCheckLock(ref _fs, () => new Fs("."));
            }
        }

        public event Action<BreviteeServer, BreviteeConf> LoadingConf;
        public event Action<BreviteeServer, BreviteeConf> LoadedConf;

        protected void OnLoadingConf()
        {
            if (LoadingConf != null)
            {
                LoadingConf(this, GetCurrentConf());
            }
        }

        protected void OnLoadedConf(BreviteeConf conf)
        {
            if (LoadedConf != null)
            {
                LoadedConf(this, conf);
            }
        }

        /// <summary>
        /// Loads the server configuration from either a json file, yaml file
        /// or the default config depending on which is found first in that 
        /// order.
        /// </summary>
        public BreviteeConf LoadConf()
        {
            OnLoadingConf();
            BreviteeConf conf = BreviteeConf.Load();
            SetConf(conf);
            OnLoadedConf(conf);
            return conf;
        }

        public event Action<BreviteeServer, BreviteeConf> SettingConf;
        public event Action<BreviteeServer, BreviteeConf> SettedConf;

        protected void OnSettingConf(BreviteeConf conf)
        {
            if (SettingConf != null)
            {
                SettingConf(this, conf);
            }
        }

        protected void OnSettedConf(BreviteeConf conf)
        {
            if (SettedConf != null)
            {
                SettedConf(this, conf);
            }
        }

        public void SetConf(BreviteeConf conf)
        {
            OnSettingConf(conf);
            DefaultConfiguration.CopyProperties(conf, this);
            Type loggerType;
            this.Logger = conf.GetLogger(out loggerType);            
            if (!loggerType.Name.Equals(conf.LoggerName))
            {
                Logger.AddEntry("Configured Logger was ({0}) but the Logger found was ({1})", LogEventType.Warning, conf.LoggerName, loggerType.Name);
            }

            conf.Server = this;

            OnSettedConf(conf);
        }

        public event Action<BreviteeServer, BreviteeConf> SavedConf;
        
        protected void OnSavedConf(BreviteeConf conf)
        {
            if (SavedConf != null)
            {
                SavedConf(this, conf);
            }
        }

        /// <summary>
        /// Saves the current configuration if the config 
        /// file doesn't currently exist
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public BreviteeConf SaveConf(bool overwrite = false, ConfFormat format = ConfFormat.Json)
        {
            BreviteeConf conf = GetCurrentConf();
            conf.Save(overwrite, format);
            OnSavedConf(conf);
            return conf;
        }

        BreviteeConf _conf;
        object _confLock = new object();
        /// <summary>
        /// Get a BreviteeConf instance which represents the current
        /// state of the BreviteeServer
        /// </summary>
        /// <returns></returns>
        internal protected BreviteeConf GetCurrentConf(bool reload = true)
        {
            lock (_confLock)
            {
                if (reload || _conf == null)
                {
                    BreviteeConf conf = new BreviteeConf();
                    DefaultConfiguration.CopyProperties(this, conf);
                    conf.Server = this;
                    _conf = conf;
                }
            }
            return _conf;
        }

        protected HttpServer HttpServer
        {
            get { return _server; }
        }

        public ContentResponder ContentResponder
        {
            get
            {
                return RequestHandler.Content;
            }
        }

        public DaoResponder DaoResponder
        {
            get
            {
                return RequestHandler.Dao;
            }
        }

        public ServiceProxyResponder ServiceProxyResponder
        {
            get
            {
                return RequestHandler.ServiceProxy;
            }
        }
        
        /// <summary>
        /// If true will cause the initialization of the 
        /// DaoResponder which will process all *.db.js
        /// and *.db.json files.  See http://breviteedocs.wordpress.com/dao/
        /// for information about the expected format 
        /// of a *.db.js file.  The format of *db.json 
        /// would be the json equivalent of the referenced
        /// database object (see link).  See
        /// Brevitee.Data.Schema.DataTypes for valid
        /// data types.
        /// </summary>
        protected bool EnableDao
        {
            get;
            set;
        }

        /// <summary>
        /// If true will cause the initialization of the
        /// ServiceProxyResponder which will register
        /// all classes addorned with the Proxy attribute
        /// as service proxy executors
        /// </summary>
        protected bool EnableServiceProxy
        {
            get;
            set;
        }

        RequestHandler _requestHandler;
        object _requestHandlerLock = new object();
        public RequestHandler RequestHandler
        {
            get
            {
                return _requestHandlerLock.DoubleCheckLock(ref _requestHandler, RequestHandlerInstanciator);
            }
            private set
            {
                _requestHandler = value;
                OnRequestHandlerSet(_requestHandler);
            }
        }

        Func<RequestHandler> _requestHandlerInstanciator;
        object _requestHandlerInstanciatorLock = new object();
        public Func<RequestHandler> RequestHandlerInstanciator
        {
            get
            {
                return _requestHandlerInstanciatorLock.DoubleCheckLock(ref _requestHandlerInstanciator, () =>
                {
                    return () =>
                    {
                        BreviteeConf conf = GetCurrentConf(true);
                        RequestHandler handler = new RequestHandler(conf, Logger, false);
                        handler.AddResponder(handler.Content);
                        if (EnableDao)
                        {
                            handler.AddResponder(handler.Dao);
                        }
                        if (EnableServiceProxy)
                        {
                            handler.AddResponder(handler.ServiceProxy);
                        }

                        OnRequestHandlerSet(handler);
                        return handler;
                    };
                });
            }
            set
            {
                _requestHandlerInstanciator = value;
            }
        }

        public void SubscribeToResponded<T>(ResponderEventHandler subscriber) where T : class, IResponder
        {
            RequestHandler.Responders.Each(r =>
            {
                T responder = r as T;
                if (responder != null)
                {
                    responder.Responded += subscriber;
                }
            });
        }

        public void SubscribeToNotResponded<T>(ResponderEventHandler subscriber) where T : class, IResponder
        {
            RequestHandler.Responders.Each(r =>
            {
                T responder = r as T;
                if (responder != null)
                {
                    responder.NotResponded += subscriber;
                }
            });
        }

        public void SubscribeToResponded(ResponderEventHandler subscriber)
        {
            RequestHandler.Responders.Each(r =>
            {
                r.Responded += subscriber;
            });
        }

        public void SubscribeToNotResponded(ResponderEventHandler subscriber)
        {
            RequestHandler.Responders.Each(r =>
            {
                r.NotResponded += subscriber;
            });
        }

        public event Action<RequestHandler> RequestHandlerSet;

        protected void OnRequestHandlerSet(RequestHandler handler)
        {
            if (RequestHandlerSet != null)
            {
                RequestHandlerSet(handler);
            }
        }
        
        public event Action<BreviteeServer> Starting;
        public event Action<BreviteeServer> Started;
        protected void OnStarting()
        {
            if (Starting != null)
            {
                Starting(this);
            }
        }

        protected void OnStarted()
        {
            if (Started != null)
            {
                Started(this);
            }
        }        

        public event Action<BreviteeServer> Stopping;
        public event Action<BreviteeServer> Stopped;

        protected void OnStopping()
        {
            if (Stopping != null)
            {
                Stopping(this);
            }
        }

        protected void OnStopped()
        {
            if (Stopped != null)
            {
                Stopped(this);
            }
        }

        protected internal bool IsRunning
        {
            get;
            private set;
        }

        public void Start()
        {
            Initialize();

            OnStarting();
            _server.Start();
            IsRunning = true;
            OnStarted();
        }

        public void Stop()
        {
            if(IsInitialized)
            {
                SaveConf();

                OnStopping();
                _server.Stop();
                IsRunning = false;
                OnStopped();
            }
        }

        public void Restart()
        {
            Stop();
            this.IsInitialized = false;
            lock (_requestHandlerLock)
            {
                _requestHandler = null; // causes the request handler to be reinitialized
            }
            Start();
        }

        
        public void AddLogger(ILogger logger)
        {
            MultiTargetLogger mtl = new MultiTargetLogger();
            if(Logger != null)
            {
                if(Logger.GetType() == typeof(MultiTargetLogger))
                {
                    mtl = (MultiTargetLogger)Logger;
                }
                else
                {
                    mtl.AddLogger(Logger);
                }
            }

            mtl.AddLogger(logger);
            Logger = mtl;
        }

        protected void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            RequestHandler.HandleRequest(new Context(new RequestWrapper(request), new ResponseWrapper(response)));
        }
    }

}