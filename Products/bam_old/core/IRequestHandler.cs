using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.core
{
    public interface IRequestHandler
    {
        void HandleRequest(IContext context);
    }
}
