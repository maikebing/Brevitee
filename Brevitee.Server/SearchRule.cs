using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server
{
    public class SearchRule
    {
        public SearchRule()
        {
            this._searchDirectories = new List<SearchDirectory>();
        }

        public SearchRule(string ext, params SearchDirectory[] searchDirectories)
        {
            this.Ext = ext;
            this.SearchDirectories = searchDirectories;
        }

        public string Ext
        {
            get;
            set;
        }

        List<SearchDirectory> _searchDirectories;
        public SearchDirectory[] SearchDirectories
        {
            get
            {
                _searchDirectories.Sort((l, r) => l.SortOrder.CompareTo(r.SortOrder));
                return _searchDirectories.ToArray();
            }
            set
            {
                _searchDirectories = new List<SearchDirectory>(value);
            }
        }
    }
}
