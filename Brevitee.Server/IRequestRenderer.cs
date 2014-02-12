using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Brevitee.Server.Renderers;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public interface IRequestRenderer: IRenderer
    {
        ExecutionRequest Request { get; set; }
    }
}
