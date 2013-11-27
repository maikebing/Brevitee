using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee
{
    public abstract class RandomNumber
    {
        public static int UpTo(int max = 100)
        {
            return RandomHelper.Next(max);
        }

        public static int Between(int min, int max)
        {
            return RandomHelper.Next(min, max);
        }
    }
}
