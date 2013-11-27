using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data
{
    public abstract class ParameterParser: IParameterInfoParser
    {
        #region IComparisonParser Members

        public abstract IEnumerable<IFilterToken> Filters
        {
            get;
        }

        IEnumerable<IParameterInfo> parameters;
        public virtual IParameterInfo[] Parameters 
        {
            get
            {
                List<IParameterInfo> temp = new List<IParameterInfo>();
                foreach (IFilterToken token in Filters)
                {
                    IParameterInfo parameter = token as IParameterInfo;
                    if (parameter != null)
                    {
                        temp.Add(parameter);
                    }
                }

                return temp.ToArray();
            }
            set
            {
                parameters = value;
            }
        }

        public string Parse()
        {
            return Parse(1);
        }

        public string Parse(int? number)
        {
            StringBuilder builder = new StringBuilder();

            foreach (IFilterToken token in this.Filters)
            {
                IParameterInfo c = token as IParameterInfo;
                if (c != null)
                {
                    number = c.SetNumber(number);
                }
                builder.Append(token.ToString());
            }

            return builder.ToString();
        }

        #endregion
    }
}
