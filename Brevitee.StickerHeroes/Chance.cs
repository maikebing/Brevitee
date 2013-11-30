using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Brevitee.StickerHeroes
{
    public class Chance
    {
        public Chance(int percent, Action action)
        {
            this.Percent = percent;
            this.Action = action;
        }

        public int Percent
        {
            get;
            private set;
        }

        protected Action Action { get; private set; }

        public bool MightHappen()
        {
            int upperbound = Percent * 1000;
            Thread.Sleep(RandomNumber.Between(1, 30)); // allow additional entropy

            int number = RandomNumber.Between(1, 100 * 1000);
            bool will = number <= upperbound;
            if (will)
            {
                Action();
            }

            return will;
        }
    }
}
