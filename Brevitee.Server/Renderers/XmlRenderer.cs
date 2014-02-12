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
    public class XmlRenderer: RendererBase
    {
        public XmlRenderer()
            : base("application/xml", ".xml")
        { }

        public override void Render(object toRender, Stream output)
        {
            string xml = toRender.ToXml();

            byte[] data = Encoding.UTF8.GetBytes(xml);
            output.Write(data, 0, data.Length);
        }
    }
}
