using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brevitee.Server.Renderers
{
    public class AppDustTemplateRenderer: CommonDustTemplateRenderer
    {
        public AppDustTemplateRenderer(AppContentResponder appResponder)
            : base(appResponder.ContentResponder)
        {
            this.AppContentResponder = appResponder;
        }

        public AppContentResponder AppContentResponder
        {
            get;
            set;
        }

        protected override DirectoryInfo GetDustRoot()
        {
            DirectoryInfo dustDir = new DirectoryInfo(Path.Combine(AppContentResponder.Root, "dust"));
            return dustDir;
        }

        object _renderLock = new object();
        public override void Render(object toRender)
        {
            base.Render(toRender);
            DirectoryInfo dustDir = GetDustRoot();
            if (!dustDir.Exists)
            {
                dustDir.Create();
            }

            lock (_renderLock)
            {
                string typeName = toRender.GetType().Name;
                string fileName = "default.dust";
                using (FileStream fs = File.Create(Path.Combine(dustDir.FullName, typeName, fileName), (int)OutputStream.Length))
                {
                    OutputStream.Seek(0, SeekOrigin.Begin);
                    OutputStream.CopyTo(fs);
                }
            }
        }
    }
}
