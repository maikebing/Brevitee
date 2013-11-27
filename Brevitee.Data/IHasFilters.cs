using System;
using System.Collections.Generic;

namespace Brevitee.Data
{
    public interface IHasFilters
    {
        IEnumerable<IFilterToken> Filters { get; }
    }
}
