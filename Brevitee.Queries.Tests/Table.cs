using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Data.Tests
{
    public class Table: Dao // should be generated
    {

        public override IQueryFilter GetUniqueFilter()
        {
            return null;
            throw new NotImplementedException();
        }

        public override void Delete(Database db = null)
        {
            throw new NotImplementedException();
        }
    }
      
}
