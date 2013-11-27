using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Brevitee.Facebook
{
    public class Paging
    {
        public Paging(JObject jObject)
        {
            this.Next = (string)jObject["next"];
        }

        public string Next { get; set; }
    }
}
