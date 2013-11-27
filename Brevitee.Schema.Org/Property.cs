using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;

namespace Brevitee.Schema.Org
{
    public class Property
    {
        public string Name { get; set; }

        string _expectedType;
        public string ExpectedType
        {
            get
            {
                return _expectedType;
            }
            set
            {
                _expectedType = value;
                string[] split = _expectedType.Split(new string[] { "\r", "\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 3)
                {
                    _expectedType = string.Format("ThisOrThat<{0}, {1}>", split[0], split[2]);
                }
                else if (split.Length == 5)
                {
                    _expectedType = string.Format("ThisOrThat<{0}, {1}, {2}>", split[0], split[2], split[4]);
                }
            }
        }
        public string Description { get; set; }
    }
}
