using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.core
{
    public interface IResponder
    {
        event ResponderEventHandler Responded;
        event ResponderEventHandler NotResponded;
        bool Respond(IContext context);
        bool TryRespond(IContext context);
    }
}
