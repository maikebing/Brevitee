using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Schema
{
    public class AddCreatedAugmentation: AddColumnAugmentation
    {
        public AddCreatedAugmentation()
        {
            this.ColumnName = "Created";
            this.DataType = DataTypes.DateTime;
            this.AllowNull = false;
        }
    }
}
