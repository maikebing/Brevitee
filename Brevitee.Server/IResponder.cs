using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public interface IResponder
    {
        event ResponderEventHandler Responded;
        event ResponderEventHandler NotResponded;
        bool Respond(IHttpContext context);
        bool TryRespond(IHttpContext context);
    }
}
