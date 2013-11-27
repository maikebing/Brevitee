using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Brevitee;
using Brevitee.Data;
//using Brevitee.FileExt;
//using Brevitee.FileExt.Js;

namespace Brevitee.Data
{
    public class InComparison: Comparison
    {
        public class ParameterInfo: IParameterInfo
        {
            public ParameterInfo(int number, object value)
            {
                this.Number = number;
                this.Value = value;
            }
            #region IParameterInfo Members

            public string ColumnName
            {
                get { return "P"; }
                set { }
            }
                        
            public int? Number
            {
                get;
                set;
            }

            public int? SetNumber(int? value)
            {
                throw new NotImplementedException();
            }

            public object Value
            {
                get;
                set;
            }

            #endregion

            #region IFilterToken Members

            public string Operator
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            #endregion
        }

        public InComparison(string columnName, object[] values)
            :base(columnName, " IN ", values)
        {
            Args.ThrowIf<InvalidOperationException>(values.Length == 0, "At least one value must be specified");
            ThrowIfNullOrEmpty(values, "values");
            this.Values = values;
            this.numbers = new int[] { };
        }

        public InComparison(string columnName, long[] values)
            :base(columnName, " IN ", values)
        {
            Args.ThrowIf<InvalidOperationException>(values.Length == 0, "At least one value must be specified for 'InComparison'");
            ThrowIfNullOrEmpty(values, "values");
            this.Values = new object[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                this.Values[i] = values[i];
            }

            this.numbers = new int[] { };
        }

        public InComparison(string columnName, string[] values)
            : base(columnName, " IN ", values)
        {
            Args.ThrowIf<InvalidOperationException>(values.Length == 0, "At least one value must be specified for 'InComparison'");
            ThrowIfNullOrEmpty(values, "values");
            this.Values = values;
            this.numbers = new int[] { };
        }
        
        public object[] Values { get; set; }

        // the number of the parameter names (@P1)
        int[] numbers;
        public override int? SetNumber(int? value)
        {
            if (Values.Length > 0)
            {
                numbers = new int[Values.Length];
                for (int i = 0; i < Values.Length; i++)
                {
                    numbers[i] = value.Value;
                    value = numbers[i] + 1;
                }
                return value;
            }
            else
            {
                this.Number = value;
                return value;
            }
        }

        public ParameterInfo[] Parameters
        {
            get
            {
                List<ParameterInfo> results = new List<ParameterInfo>();

                for (int i = 0; i < Values.Length; i++)
                {
                    results.Add(new ParameterInfo(numbers[i], Values[i]));
                }

                return results.ToArray();
            }
        }

        public override string ToString()
        {
            List<string> paramNames = new List<string>();
            foreach(int i in numbers)
            {
                paramNames.Add(string.Format("@P{0}", i));
            }

            return string.Format("{0} IN ({1})", ColumnName, paramNames.ToArray().ToDelimited(s => s));
        }

        private void ThrowIfNullOrEmpty(IEnumerable values, string name)
        {
            if (values == null)
            {
                throw new InvalidOperationException(string.Format("{0} can't be null or empty", name));
            }
        }
    }
}
