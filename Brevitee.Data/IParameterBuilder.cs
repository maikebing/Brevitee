using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Data
{
    public interface IParameterBuilder//<T> where T : FilterToken, new()
    {
        DbParameter[] GetParameters(IHasFilters filter);        
    }
}
