using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml;
using System.IO;
using Brevitee;
using Brevitee.Yaml;

namespace Brevitee.Server.Renderers
{
    public class YamlRenderer: RendererBase
    {
        public YamlRenderer()
            : base("application/x-yaml", ".yaml")
        { }

        public override void Render(object toRender, Stream output)
        {
            string yaml = toRender.ToYaml();

            byte[] data = Encoding.UTF8.GetBytes(yaml);
            output.Write(data, 0, data.Length);
        }
    }
}
