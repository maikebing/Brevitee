using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Html
{
    public class Img: Tag
    {
        public Img(string src)
            : base("img", new { src = src })
        {
        }         
    }
}
