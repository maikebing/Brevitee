using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema;
using Brevitee.Configuration;
using Brevitee.Logging;
using Brevitee.Incubation;
using Brevitee.Javascript;
using Brevitee.ServiceProxy;
using Yahoo.Yui.Compressor;


namespace Brevitee.Server.Renderers
{
    public class DaoProxyScriptsRenderer: RendererBase
    {
        public DaoProxyScriptsRenderer()
            : base("application/javascript", ".js")
        {
        }
        
        public override void Render(object toRender, Stream output)
        {
            //DaoProxyRegistration.GetScript()
        }

        public void RegisterDaoTypes()
        {
            //RegisterDaoTypes(this.Con)
        }

        public void RegisterDaoType(Fs fsRoot)
        {

        }

        public void Generate(string dbJsPath, Fs fsRoot)
        {
            SchemaManager schemaManager = new SchemaManager();
            FileInfo dbFile = new FileInfo(dbJsPath);
            schemaManager.RootDir = fsRoot.GetAbsolutePath("~/dao");
            schemaManager.Generate(dbFile, true, false);
        }
         //this.Script = DaoProxyRegistration.GetScript().ToString();
         //   if (min) Compress();
    }
}
