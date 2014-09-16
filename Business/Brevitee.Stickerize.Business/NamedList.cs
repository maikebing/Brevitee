using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business
{
    public class NamedList<T>
    {
        public string Name { get; set; }
        public List<T> Items { get; set; }
    }
}
