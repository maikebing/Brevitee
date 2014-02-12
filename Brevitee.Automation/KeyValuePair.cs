using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Automation
{
    public class KeyValuePair
    {
        public KeyValuePair() { }
        public KeyValuePair(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
