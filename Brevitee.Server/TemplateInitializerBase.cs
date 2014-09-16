using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Logging;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Server.Renderers;
using System.Reflection;
using System.IO;

namespace Brevitee.Server
{
    public abstract class TemplateInitializerBase: IInitialize<TemplateInitializerBase>
    {
        public TemplateInitializerBase(BreviteeServer server)
        {
            this.Server = server;
            this._subscribers = new List<ILogger>();
        }

        public BreviteeServer Server
        {
            get;
            set;
        }

        public event Action<TemplateInitializerBase> Initializing;
        protected void OnInitializing()
        {
            if (Initializing != null)
            {
                Initializing(this);
            }
        }
        public event Action<TemplateInitializerBase> Initialized;
        protected void OnInitialized()
        {
            if (Initialized != null)
            {
                Initialized(this);
            }
        }
        
        public event Action<DaoProxyRegistration> InitializingCommonDaoTemplates;
        protected void OnInitializingCommonDaoTemplates(DaoProxyRegistration reg)
        {
            if (InitializingCommonDaoTemplates != null)
            {
                InitializingCommonDaoTemplates(reg);
            }
        }

        public event Action<DaoProxyRegistration> InitializedCommonDaoTemplates;
        protected void OnInitializedCommonDaoTemplates(DaoProxyRegistration reg)
        {
            if (InitializedCommonDaoTemplates != null)
            {
                InitializedCommonDaoTemplates(reg);
            }
        }

        public event Action<string, DaoProxyRegistration> InitializingAppDaoTemplates;
        protected void OnInitializingAppDaoTemplates(string appName, DaoProxyRegistration reg)
        {
            if (InitializingAppDaoTemplates != null)
            {
                InitializingAppDaoTemplates(appName, reg);
            }
        }

        public event Action<string, DaoProxyRegistration> InitializedAppDaoTemplates;
        protected void OnInitializedAppDaoTemplates(string appName, DaoProxyRegistration reg)
        {
            if (InitializedAppDaoTemplates != null)
            {
                InitializedAppDaoTemplates(appName, reg);
            }
        }

        public event Action<Exception> InitializationException;
        protected void OnInitializationException(Exception ex)
        {
            if (InitializationException != null)
            {
                InitializationException(ex);
            }
        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        public abstract void Initialize();
        
        protected internal static void RenderEachTable(IRenderer renderer, DaoProxyRegistration daoProxyReg)
        {
            Assembly currentAssembly = daoProxyReg.Assembly;
            Type[] tableTypes = currentAssembly.GetTypes().Where(type => type.HasCustomAttributeOfType<TableAttribute>()).ToArray();
            tableTypes.Each(type =>
            {
                renderer.Render(type.Construct());
            });
        }

        List<ILogger> _subscribers;
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

                string className = this.GetType().Name;//typeof(TemplateInitializerBase).Name;
                Initializing += (ti) =>
                {
                    logger.AddEntry("{0}::Initializ(ING)", className);
                };

                Initialized += (ti) =>
                {
                    logger.AddEntry("{0}::Initialz(ED)", className);
                };

                InitializingAppDaoTemplates += (appName, daoReg) =>
                {
                    logger.AddEntry("{0}::Initializ(ING) App[{1}] Templates for ({2})", className, appName, daoReg.ContextName);
                };

                InitializedAppDaoTemplates += (appName, daoReg) =>
                {
                    logger.AddEntry("{0}::Initializ(ED) App[{1}] Templates for ({2})", className, appName, daoReg.ContextName);
                };

                InitializingCommonDaoTemplates += (daoReg) =>
                {
                    logger.AddEntry("{0}::Initializ(ING) Common Templates for ({0})", className, daoReg.ContextName);
                };

                InitializedCommonDaoTemplates += (daoReg) =>
                {
                    logger.AddEntry("{0}::Initializ(ED) Common Templates for ({0})", className, daoReg.ContextName);
                };

                InitializationException += (ex) =>
                {
                    logger.AddEntry("{0}::Initialization EXCEPTION: {1}", LogEventType.Warning, ex.Message);
                };
            }
        }
    }
}
