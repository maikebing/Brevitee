using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public interface IHasParameterInfos
    {
        IParameterInfo[] Parameters { get; set; }
    }
}
