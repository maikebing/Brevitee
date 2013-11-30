using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Logging;

namespace Brevitee.StickerHeroes
{
    public interface IAttackMethod
    {
        long? Id { get; set; }
        string Name { get; set; }
        int? Strength { get; set; }
        string Element { get; set; }

        AttackType AttackType { get; }
        //int GetAttackStrength(Character attacker, int multiplier, BattleLogger report = null);
    }
}
