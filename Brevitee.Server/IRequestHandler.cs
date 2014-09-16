using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public interface IRequestHandler
    {
        void HandleRequest(IHttpContext context);
    }
}
