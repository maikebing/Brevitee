using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using Brevitee.ServiceProxy;

namespace Brevitee.Server
{
    public interface IContext
    {
        IRequest Request { get; set; }
        IResponse Response { get; set; }
        IPrincipal User { get; set; }
    }
}
