using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Syndication.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChannelAttribute: Attribute
    {
        public ChannelAttribute() { }
        public ChannelAttribute(string title)
        {
            this.Title = title;
        }

        public string Title { get; set; }
    }
}
