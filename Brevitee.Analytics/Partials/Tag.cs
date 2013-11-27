using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public partial class Tag
    {
        public static Tag EnsureOne(string tag)
        {
            Tag tagObj = Brevitee.Analytics.Data.Tag.OneWhere(c => c.Value == tag);
            if (tagObj == null)
            {
                tagObj = new Tag();
                tagObj.Value = tag;
                tagObj.Save();
            }

            return tagObj;
        }
    }
}
