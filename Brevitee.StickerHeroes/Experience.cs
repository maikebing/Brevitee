using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.StickerHeroes
{
    public class Experience
    {
        public static implicit operator Experience(int value)
        {
            return new Experience { Value = value };
        }

        public static implicit operator int(Experience exp)
        {
            return exp.Value;
        }

        public int Value { get; set; }
    }
}
