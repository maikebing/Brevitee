using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Brevitee.Facebook
{
    public class Friend
    {
        public Friend(JObject jObject)
        {
            this.Name = (string)jObject["name"];
            this.Id = (string)jObject["id"];
        }

        public string Name { get; set; }
        public string Id { get; set; }
    }
}
