using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Brevitee.Logging;

namespace Brevitee.StickerHeroes
{
    public partial class Skill: IAttackMethod
    {
        public static Skill Create(string name, int strength)
        {
            Skill s = new Skill();
            s.Name = name;
            s.Element = Elements.None.ToString();
            s.Strength = strength;
            s.Save();
            return s;
        }

        public AttackType AttackType
        {
            get
            {
                return StickerHeroes.AttackType.Skill;
            }
        }
    }
}
