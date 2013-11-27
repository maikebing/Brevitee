using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AuthorAttribute: Attribute
    {
        public AuthorAttribute()
        {
        }
    }
}
