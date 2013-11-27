using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brevitee.Data.Model
{
    public class ModelActionAttribute : Attribute
    {
        public ModelActionAttribute()
        {
        }

        public ModelActionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}
