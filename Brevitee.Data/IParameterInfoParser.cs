using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public interface IParameterInfoParser: IHasParameterInfos, IHasFilters
    {
        string Parse();
        string Parse(int? number);
    }
}
