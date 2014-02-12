using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Analytics.Crawlers
{
    public abstract class BaseCrawler: ICrawler
    {        
        static Dictionary<string, ICrawler> _crawlers;
        static object _crawlerLock = new object();
        public static Dictionary<string, ICrawler> Instances
        {
            get
            {
                return _crawlerLock.DoubleCheckLock(ref _crawlers, () => new Dictionary<string, ICrawler>());
            }
        }

        #region ICrawler Members

        public virtual string Root
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public string ThreadName
        {
            get
            {
                return "Crawler_{0}"._Format(this.Name);
            }
        }
        /// <summary>
        /// When implemented by a derived class will extract
        /// more targets to be processed from the specified target.  
        /// (Think filepath or url)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract string[] ExtractTargets(string target);

        public abstract void ProcessTarget(string target);

        public virtual bool WasProcessed(string target = "")
        {
            if (_processed != null && !string.IsNullOrEmpty(target))
            {
                return _processed.Contains(target);
            }

            return false;
        }

        Queue<string> _targets;
        public string[] QueuedTargets
        {
            get
            {
                return _targets.ToArray();
            }
        }

        int _maxQueue;
        public int MaxQueueSize
        {
            get
            {
                return _maxQueue;
            }
            set
            {
                _maxQueue = value;
            }
        }

        List<string> _processed;
        public string[] Processed
        {
            get
            {
                return _processed.ToArray();
            }
        }
        
        protected internal string Current
        {
            get;
            set;
        }

        CrawlerState.Action _currentAction;
        protected internal CrawlerState.Action CurrentAction
        {
            get
            {
                return _currentAction;
            }
            set
            {
                CrawlerState.Action old = _currentAction;
                _currentAction = value;
                OnActionChanged(old, _currentAction);
            }
        }

        public event ActionChangedDelegate ActionChanged;

        protected void OnActionChanged(CrawlerState.Action oldAction, CrawlerState.Action newAction)
        {
            if (ActionChanged != null)
            {
                ActionChanged(this, new ActionChangedEventArgs(oldAction, newAction));
            }
        }

        public CrawlerState GetState()
        {
            return new CrawlerState(this);
        }

        public void Crawl()
        {
            if (string.IsNullOrEmpty(Root))
            {
                throw new InvalidOperationException("Root not set");
            }

            Crawl(Root);
        }

        public void Crawl(string rootTarget)
        {
            Exec.Start(ThreadName, () =>
            {

                Root = rootTarget;
                _targets = new Queue<string>();
                _processed = new List<string>();

                _targets.Enqueue(rootTarget);
                while (_targets.Count > 0)
                {
                    Current = _targets.Dequeue();
                    CurrentAction = CrawlerState.Action.Extracting;
                    string[] more = ExtractTargets(Current);
                    for (int i = 0; i < more.Length; i++)
                    {
                        if (more[i] == null)
                        {
                            continue;
                        }
                        string extracted = more[i].ToLowerInvariant();
                        if (!WasProcessed(extracted))
                        {
                            if (MaxQueueSize == 0 || (MaxQueueSize > 0 && _targets.Count < MaxQueueSize))
                            {
                                _targets.Enqueue(extracted);
                            }
                        }
                    }

                    CurrentAction = CrawlerState.Action.Processing;
                    ProcessTarget(Current);
                    _processed.Add(Current.ToLowerInvariant());
                }
                CurrentAction = CrawlerState.Action.Idle;
            });
        }

        #endregion
    }
}
