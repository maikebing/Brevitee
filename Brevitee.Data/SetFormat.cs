using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public class SetFormat: FormatPart
    {
        public void AddAssignment(string columnName, object value)
        {
            AddAssignment(new AssignValue(columnName, value));
        }

        public void AddAssignment(AssignValue value)
        {
            this.AddParameter(value);
        }

        public override string Parse()
        {
            AssignNumbers();

            return string.Format("SET {0} ", this.Parameters.ToArray().ToDelimited(p => p.ToString()));
        }

        protected void AssignNumbers()
        {
            for (int? i = this.StartNumber; i < this.NextNumber; i++)
            {
                int? index = i - this.StartNumber;
                this.Parameters[index.Value].Number = i;
            }
        }
    }
}
