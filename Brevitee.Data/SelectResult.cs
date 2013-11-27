using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Brevitee.Data
{
    public class SelectResult : QueryResult
    {
        public SelectResult()
        {

        }

        public object Value { get; set; }

        #region IHasDataTable Members
        
        public override void SetDataTable(DataTable table)
        {
            DataTable = table;
        }

        #endregion
    }
}
