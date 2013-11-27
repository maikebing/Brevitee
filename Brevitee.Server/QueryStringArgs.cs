using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;

namespace Brevitee.Server
{
    public class QueryStringArgs
    {
        Dictionary<string, string> _pairs;
        public QueryStringArgs(string queryString)
        {
            this._pairs = new Dictionary<string, string>();
            string[] pairs = queryString.DelimitSplit("?", "&");
            foreach (string pair in pairs)
            {
                string[] keyVal = pair.DelimitSplit("=");
                if (keyVal.Length == 2)
                {
                    this._pairs.Add(keyVal[0], keyVal[1]);
                }
                else
                {
                    this._pairs.Add(keyVal[0], "");
                }
            }
        }

        public string[] Keys
        {
            get
            {
                return _pairs.Keys.ToArray();                    
            }
        }

        public int Count
        {
            get
            {
                return _pairs.Values.Count;
            }
        }

        /// <summary>
        /// Returns true if the current QueryStringArgs instance
        /// has the specified key
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <param name="value">The value associated with key</param>
        /// <returns></returns>
        public bool Has(string key, out string value)
        {
            value = this[key];
            return !string.IsNullOrEmpty(value);
        }

        public string this[string key]
        {
            get
            {
                if (_pairs.ContainsKey(key))
                {
                    return _pairs[key];
                }

                return string.Empty;
            }
        }

    }
}
