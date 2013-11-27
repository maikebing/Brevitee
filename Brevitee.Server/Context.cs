using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Brevitee.Server
{
    public class Context: IContext
    {
        public Context(IRequest request, IResponse response)
        {
            this.Request = request;
            this.Response = response;
        }

        public IResponse Response { get; set; }
        public IRequest Request { get; set; }

        public IPrincipal User { get; set; }
    }
}
