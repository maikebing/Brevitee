using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brevitee.Data.Model;

namespace YourNameSpaceHere
{
    public class ExampleClass
    {
        public bool Did { get; set; }

        [ModelAction]
        public void DoSomething()
        {
            Did = true;
        }

        [ModelAction]
        public object ReturnSomething()
        {
            return new object();
        }

        [ModelAction]
        public ExampleClass2 ReturnExample()
        {
            return new ExampleClass2();
        }
    }
}
