using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public interface IDynamicDatabase
    {
        dynamic Insert(dynamic value);

        dynamic Select(dynamic query);

        dynamic SelectById(int id);

        dynamic SelectOne(dynamic query);
        
        bool Update(dynamic value);

        bool Delete(int id);
    }
}
