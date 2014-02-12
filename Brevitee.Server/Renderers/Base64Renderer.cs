using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Brevitee.Server.Renderers
{
    public class Base64Renderer: RendererBase
    {
        public Base64Renderer()
            : base("text/plain")
        { }

        public override void Render(object toRender, Stream output)
        {
            byte[] data = toRender.ToBinaryBytes();
            string base64 = Convert.ToBase64String(data);
            byte[] base64data = Encoding.UTF8.GetBytes(base64);
            output.Write(base64data, 0, data.Length);
        }
    }
}
