using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Data.Schema
{
    public class AddIdKeyColumnAugmentation: AddColumnAugmentation
    {
        public AddIdKeyColumnAugmentation()
            : base()
        {
            this.ColumnName = "Id";
            this.DataType = DataTypes.Long;
            this.AllowNull = false;
        }
        public override void Execute(string tableName, SchemaManager manager)
        {
            base.Execute(tableName, manager);
            manager.SetKeyColumn(tableName, ColumnName);
        }
    }
}
