using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Server.Tests
{
    public enum Tails
    {
        None,
        Short,
        Long,
        Prehensile
    }

    public class TestMonkey
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public Tails Tail
        {
            get;
            set;
        }
    }
}
