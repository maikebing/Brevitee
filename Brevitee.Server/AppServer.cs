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
using System.Net;

namespace Brevitee.Server
{
    public class AppServer
    {
        HttpServer _server;
        ILogger _logger;
        Incubator _serviceProvider;

        static AppServer()
        {
        }

        public AppServer()
            : this(50, Log.Default)
        {
            this._hostName = "+";
        }

        public AppServer(Incubator serviceProvider)
            : this(50, Log.Default, 8080, serviceProvider)
        {
            
        }

        public AppServer(int maxThreads, ILogger logger, int port = 8080, Incubator serviceProvider = null)
        {
            this.MaxThreads = maxThreads.ToString();
            this.Port = port.ToString();
            Init(logger);
        }

        private void Init()
        {
            Init(Logger);
        }

        private void Init(ILogger logger, Incubator serviceProvider = null)
        {
            if (serviceProvider == null)
            {
                serviceProvider = Incubator.Default;
            }

            if (string.IsNullOrEmpty(this.MaxThreads))
            {
                this.MaxThreads = "25";
            }

            if (string.IsNullOrEmpty(this.Port) || this.Port.Equals("true"))
            {
                this.Port = "8080";
            }

            int maxThreads = int.Parse(this.MaxThreads);
            int port = int.Parse(this.Port);
            this._server = new HttpServer(maxThreads, logger, port);
            this._server.HostName = _hostName;
            this._server.ProcessRequest += ProcessRequest;
            this._logger = logger;
            this._serviceProvider = serviceProvider;
        }

        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
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
                Init();
            }
        }

        string _hostName;
        public string HostName
        {
            get
            {
                return _hostName;
            }
            set
            {
                _hostName = value;
                Init();
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
                Init();
            }
        }

        public HttpServer Server
        {
            get { return _server; }
        }

        /// <summary>
        /// The dependency injection container used to 
        /// retrieve the IRequestHandler instance
        /// </summary>
        public Incubator ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
            set
            {
                _serviceProvider = value;
            }
        }

        public void Start()
        {
            _logger.AddEntry("Brevitee server starting on port {0}", _server.Port.ToString());
            _server.Start();
            _logger.AddEntry("Brevitee server started");
        }

        public void Stop()
        {
            _logger.AddEntry("Brevitee server stopping");
            _server.Stop();
            _logger.AddEntry("Brevitee server stopped");
        }

        protected void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            IRequestHandler handler = ServiceProvider.Get<IRequestHandler>();
            handler.HandleRequest(new Context(new RequestWrapper(request), new ResponseWrapper(response)));
        }
    }

}