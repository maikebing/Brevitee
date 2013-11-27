using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data;

namespace Brevitee.Data
{
    public class Comparison : FilterToken, IParameterInfo
    {
        public Comparison(string column, string oper, object value, int? number = null)
            : base(oper)
        {
            this.ColumnName = column;
            this.Operator = oper;
            if (value == null)
            {
                value = DBNull.Value;
            }
            this.Value = value;
            if (number != null)
            {
                this.Number = number.GetValueOrDefault();
            }
        }

        public string ColumnName { get; set; }
        public object Value { get; set; }
        public int? Number { get; set; }
        
        public virtual int? SetNumber(int? value)
        {
            Number = value;
            return ++value;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", ColumnName, this.Operator, string.Format("@{0}{1}", ColumnName, Number));
        }
    }
}
