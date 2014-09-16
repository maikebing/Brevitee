using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers.Business.Data
{
    public class AttackOptions
    {
        Dictionary<AttackType, Func<IAttackMethod>> _attackMethodResolvers;

        public AttackOptions()
        {
            this.Type = AttackType.Weapon;
            this._attackMethodResolvers = new Dictionary<AttackType, Func<IAttackMethod>>();
            this._attackMethodResolvers[AttackType.Weapon] = () =>
            {
                return (IAttackMethod)Brevitee.BattleStickers.Business.Data.Weapon.OneWhere(c => c.Id == Weapon);
            };
            this._attackMethodResolvers[AttackType.Spell] = () =>
            {
                return (IAttackMethod)Brevitee.BattleStickers.Business.Data.Spell.OneWhere(c => c.Id == Spell);
            };
            this._attackMethodResolvers[AttackType.Skill] = () =>
            {
                return (IAttackMethod)Brevitee.BattleStickers.Business.Data.Skill.OneWhere(c => c.Id == Skill);
            };
        }

        public long Character { get; set; }
        public long Weapon { get; set; }
        public long Spell { get; set; }
        public long Skill { get; set; }

        public long Target { get; set; }

        public AttackType Type { get; private set; }

        public CharacterSwitch Switch { get; set; }

        public IAttackMethod GetAttackMethod()
        {
            return _attackMethodResolvers[Type]();
        }

        public static AttackOptions Create(CharacterSwitch s)
        {
            AttackOptions result = new AttackOptions { Switch = s };
            return result;
        }

        public static AttackOptions Create(long characterId, AttackType type, long attackMethodId, long targetId)
        {
            Args.ThrowIf<InvalidOperationException>(type == AttackType.Invalid, "Invalid attatck type");

            AttackOptions result = new AttackOptions();
            result.Character = characterId;
            result.Target = targetId;
            result.Type = type;

            switch (type)
            {
                case AttackType.Weapon:
                    result.Weapon = attackMethodId;
                    break;
                case AttackType.Spell:
                    result.Spell = attackMethodId;
                    break;
                case AttackType.Skill:
                    result.Skill = attackMethodId;
                    break;
            }

            return result;
        }
    }
}
