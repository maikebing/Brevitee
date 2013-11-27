using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee;

namespace Bam.core
{
    public class LinkModel
    {
        public string Href { get; set; }
        public string Text { get; set; }

        string _name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = Text.CamelCase(true, " ", "_");
                }

                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public LinkModel[] SubLinks { get; set; }
    }
}
