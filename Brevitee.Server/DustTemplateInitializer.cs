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
    /// <summary>
    /// Class used to initialize dust templates for all 
    /// Dao components
    /// </summary>
    public class DustTemplateInitializer: TemplateInitializerBase
    {
        public DustTemplateInitializer(BreviteeServer server) : base(server) { }

        object _initializeLock = new object();
        public override void Initialize()
        {
            OnInitializing();
            lock (_initializeLock)
            {
                try
                {

                    // get the types that need templates
                    //  from DaoResponder
                    //      Common
                    CommonDustTemplateRenderer commonRenderer = new CommonDustTemplateRenderer(Server.ContentResponder);

                    Server.DaoResponder.CommonDaoProxyRegistrations.Values.Each((daoProxyReg) =>
                    {
                        OnInitializingCommonDaoTemplates(daoProxyReg);

                        RenderEachTable(commonRenderer, daoProxyReg);

                        OnInitializedCommonDaoTemplates(daoProxyReg);
                    });
                    //      App
                    Server.DaoResponder.AppDaoProxyRegistrations.Keys.Each((appName) =>
                    {
                        AppDustTemplateRenderer appRenderer = new AppDustTemplateRenderer(Server.ContentResponder.AppContentResponders[appName]);
                        Server.DaoResponder.AppDaoProxyRegistrations[appName].Each((daoProxyReg) =>
                        {
                            OnInitializingAppDaoTemplates(appName, daoProxyReg);

                            RenderEachTable(appRenderer, daoProxyReg);

                            OnInitializedAppDaoTemplates(appName, daoProxyReg);
                        });
                    });
                }
                catch (Exception ex)
                {
                    OnInitializationException(ex);
                }
            }

            OnInitialized();
        }

    }
}
