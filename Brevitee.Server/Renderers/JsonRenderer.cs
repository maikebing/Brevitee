using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using System.Reflection;
using System.IO;

namespace Brevitee.Server.Renderers
{
    public class JsonRenderer: RendererBase
    {
        public JsonRenderer()
            : base("application/json", ".json")
        { }

        public override void Render(object toRender, Stream output)
        {
            string json = toRender.ToJson();

            byte[] data = Encoding.UTF8.GetBytes(json);
            output.Write(data, 0, data.Length);
        }
    }
}
