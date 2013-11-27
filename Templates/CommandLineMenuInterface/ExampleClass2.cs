using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data.Model;

namespace YourNameSpaceHere
{
    public class ExampleClass2
    {
        [ModelAction]
        public string ReturnString()
        {
            return "Hello world!";
        }
    }
}
