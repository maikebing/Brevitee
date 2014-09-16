using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;
using System.Data;
using System.Data.Common;

namespace Brevitee.Data
{
    public delegate IQueryFilter WhereDelegate<C>(C where) where C : IQueryFilter, IFilterToken, new();
}
