using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.ServiceProxy
{
    public delegate void DocRenderDelegate(Dictionary<string, List<DocInfo>> docInfosByType, StringBuilder renderInto);
    
}
