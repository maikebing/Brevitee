using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class ForeignKeyAttribute: ColumnAttribute
    {
        public ForeignKeyAttribute()
            : base()
        {
            this.Suffix = string.Empty;
        }

        public string ForeignKeyName
        {
            get
            {
                // added Random to ensure uniqueness when reproducing schema
                // this is less than ideal but it works.
                return string.Format("FK_{0}_{1}{2}", Table, ReferencedTable, Suffix);
            }
        }

        public string ReferencedKey { get; set; }
        public string ReferencedTable { get; set; }

        public string Suffix { get; set; }
    }
}
