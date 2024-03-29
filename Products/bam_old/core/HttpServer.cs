using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using Brevitee;
using Brevitee.Logging;

namespace Brevitee.Html
{
    public class HttpServer : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly Thread _listenerThread;
        private readonly Thread[] _workers;
        private readonly ManualResetEvent _stop, _ready;
        private Queue<HttpListenerContext> _queue;
        private int _port;
        private ILogger _logger;

        public HttpServer(int maxThreads, ILogger logger, int port = 80)
        {
            _port = port;
            _workers = new Thread[maxThreads];
            _queue = new Queue<HttpListenerContext>();
            _stop = new ManualResetEvent(false);
            _ready = new ManualResetEvent(false);
            _listener = new HttpListener();
            _listenerThread = new Thread(HandleRequests);
            _logger = logger;
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        string _hostName;
        public string HostName
        {
            get
            {
                if (string.IsNullOrEmpty(_hostName))
                {
                    _hostName = "+";
                }

                return _hostName;
            }
            set
            {
                _hostName = value;
            }
        }

        public void Start()
        {
            Start(HostName, _port);
        }

        public void Start(int port)
        {
            Start(HostName, port);
        }

        public void Start(string hostName, int port)
        {
            string path = string.Format(@"http://{0}:{1}/", hostName, port);
            _logger.AddEntry("HttpServer: {0}", path);
            _listener.Prefixes.Add(path);
            _listener.Start();
            _listenerThread.Start();

            for (int i = 0; i < _workers.Length; i++)
            {
                _workers[i] = new Thread(Worker);
                _workers[i].Start();
            }
        }

        public void Dispose()
        {
            Stop();
        }

        public void Stop()
        {
            _stop.Set();
            _listenerThread.Join();
            foreach (Thread worker in _workers)
            {
                worker.Join();
            }
            _listener.Stop();
        }

        private void HandleRequests()
        {
            while (_listener.IsListening)
            {
                IAsyncResult context = _listener.BeginGetContext(ContextReady, null);

                if (0 == WaitHandle.WaitAny(new[] { _stop, context.AsyncWaitHandle }))
                    return;
            }
        }

        private void ContextReady(IAsyncResult ar)
        {
            try
            {
                lock (_queue)
                {
                    _queue.Enqueue(_listener.EndGetContext(ar));
                    _ready.Set();
                }
            }
            catch { return; }
        }

        private void Worker()
        {
            WaitHandle[] wait = new[] { _ready, _stop };
            while (0 == WaitHandle.WaitAny(wait))
            {
                HttpListenerContext context;
                lock (_queue)
                {
                    if (_queue.Count > 0)
                        context = _queue.Dequeue();
                    else
                    {
                        _ready.Reset();
                        continue;
                    }
                }

                try
                {
                    ProcessRequest(context);
                }
                catch (Exception ex)
                {
                    _logger.AddEntry("An error occurred processing request: {0}", ex, ex.Message);
                }
            }
        }

        public event Action<HttpListenerContext> ProcessRequest;
    }
}
