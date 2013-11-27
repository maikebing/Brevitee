using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Brevitee.Logging;

namespace Brevitee.BattleStickers
{
    public partial class Weapon: IAttackMethod
    {
        public static Weapon Create(string name, int strength, Elements element)
        {
            Weapon w = new Weapon();

            w.Name = name;
            w.Strength = strength;
            w.Element = element.ToString();
            w.Save();

            return w;
        }

        public AttackType AttackType
        {
            get
            {
                return BattleStickers.AttackType.Weapon;
            }
        }
    }
}
