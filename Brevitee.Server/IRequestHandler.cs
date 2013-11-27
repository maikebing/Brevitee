using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Server
{
    public interface IRequestHandler
    {
        void HandleRequest(IContext context);
    }
}
