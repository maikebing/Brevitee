using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Schema
{
    public class AddModifiedAugmentation: AddColumnAugmentation
    {
        public AddModifiedAugmentation()
        {
            this.ColumnName = "Modified";
            this.DataType = DataTypes.DateTime;
            this.AllowNull = false;
        }
    }
}
