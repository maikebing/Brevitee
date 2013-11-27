using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class KeyColumnAttribute: ColumnAttribute
    {
        public KeyColumnAttribute()
            : base()
        { }

        public override bool AllowNull
        {
            get
            {
                return false;
            }
            set
            {
                //
            }
        }
    }
}
