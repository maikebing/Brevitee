using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Brevitee.Logging;

namespace Brevitee.BattleStickers.Business.Data
{
    public class DamageCalculator
    {
        public DamageCalculator(Battle battle, BattleLogger logger)
        {
            this.CounterSpellChancePercentage = 10;
            this.CounterSkillChancePercentage = 10;
            this.Logger = logger;
            this.Battle = battle;
        }

        public int CounterSpellChancePercentage
        {
            get;
            private set;
        }

        public int CounterSkillChancePercentage
        {
            get;
            private set;
        }

        public BattleLogger Logger
        {
            get;
            internal set;
        }

        public Battle Battle
        {
            get;
            internal set;
        }

        public int ResolveTurn()
        {
            return ResolveTurn(Battle);
        }

        /// <summary>
        /// Returns -1 if player one is victorious,
        /// returns 1 if player two is victorious
        /// otherwise returns 0
        /// </summary>
        /// <param name="battle"></param>
        /// <returns></returns>
        public int ResolveTurn(Battle battle)
        {
            int result = 0;

            SetDamage(battle);

            if (battle.Field.BattleIsDone())
            {
                Player winner = battle.Field.Attacker.Player;
                result = battle.PlayerOne.Equals(winner) ? -1 : 1;
            }
            else
            {
                BattleField field = battle.Field;
                PlayerField nextAttacker = field.Defender;
                PlayerField nextDefender = field.Attacker;
                field.AttackOptions = null;
                field.DefendOptions = null;
                field.Attacker = nextAttacker;
                field.Defender = nextDefender;            
            }

            return result;
        }

        public void SetDamage(Battle battle)
        {
            BattleField field = battle.Field;
            BattleLogger logger = battle.Logger;

            long attackerCharacterId = field.AttackOptions.Character;
            IAttackMethod attackMethod = field.AttackOptions.GetAttackMethod();
            long targetCharacterId = field.AttackOptions.Target;

            DefendOptions defendOptions = field.DefendOptions;

            Character attacker = field.Attacker.ActiveCharacters.Where(ch => ch.Id == attackerCharacterId).FirstOrDefault();			
            Character target = field.Defender.ActiveCharacters.Where(ch => ch.Id == targetCharacterId).FirstOrDefault();

            EffectedCharacter effectedAttacker = new EffectedCharacter(attacker);
            ApplyEquipment(field.Attacker, effectedAttacker, logger);
            EffectedCharacter effectedTarget = new EffectedCharacter(target);
            ApplyEquipment(field.Defender, target, logger);

            attacker = effectedAttacker;
            target = effectedTarget;

            int? attackStrength = GetAttackStrength(attacker, attackMethod, logger);
            int? attackStrengthWithElementEffect = ApplyAttackElement(attacker, attackMethod, attackStrength, logger);
            
            target = ApplyDefenseSwitch(target, defendOptions, logger);

            int? damage = ApplyDefense(target, attackStrengthWithElementEffect, logger);
            int? damageWithElementEffect = ApplyDefenseElement(target, attackMethod, damage, logger);
            
            if (damageWithElementEffect.Value <= 0)
            {
                damageWithElementEffect = 1;
            }

            Chance willHit = new Chance(attacker.Acuracy.Value, () =>
            {
                target.Health -= damageWithElementEffect.Value;
            });

            bool hit = willHit.MightHappen();
            logger.AddEntry(hit ? "HIT: {0}"._Format(attackStrength) : "MISS");

            int counterDamage = GetCounterAttackStrength(field.Defender, target, defendOptions, logger);
            attacker.Health -= counterDamage;

            field.AfterTurn(logger);
        }

        int _multiplier;
        public int Multiplier
        {
            get
            {
                if (_multiplier == 0)
                {
                    _multiplier = Battle.Multiplier;
                }
                return _multiplier;
            }
            set
            {
                _multiplier = value;
            }
        }

        private int? GetAttackStrength(Character attacker, IAttackMethod attackMethod, BattleLogger logger = null)
        {
            int? result = 0;
            switch (attackMethod.AttackType)
            {
                case AttackType.Weapon:
                    result = GetWeaponStrength(attacker, (Weapon)attackMethod, logger);
                    break;
                case AttackType.Spell:
                    result = GetSpellStrength(attacker, (Spell)attackMethod, logger);
                    break;
                case AttackType.Skill:
                    result = GetSkillStrength(attacker, (Skill)attackMethod, logger);
                    break;
            }

            return result;
        }

        protected virtual int? GetSpellStrength(Character attacker, Spell attackMethod, BattleLogger logger)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            int result = (attacker.Magic.Value * attackMethod.Strength.Value) * Multiplier;
            logger.AddEntry("{0}:MAGIC-FORMULA: ({1})Attacker.Magic ({2}) x ({3})Spell.Strength ({4}) x ({5})Multiplier: {6}"
                ._Format(methodName, attacker.Name, attacker.Magic.Value, attackMethod.Name, attackMethod.Strength, Multiplier, result));

            return result;
        }

        protected virtual int? GetSkillStrength(Character attacker, Skill attackMethod, BattleLogger logger)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            int result = (attacker.Speed.Value * attackMethod.Strength.Value) * Multiplier;
            logger.AddEntry("{0}:SKILL-FORMULA: ({1})Attacker.Speed ({2}) x ({3})Skill.Strength ({4}) x ({5})Multiplier: {6}"
                  ._Format(methodName, attacker.Name, attacker.Speed.Value, attackMethod.Name, attackMethod.Strength, Multiplier, result));

            return result;
        }

        protected virtual int? GetWeaponStrength(Character attacker, Weapon attackMethod, BattleLogger logger)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            int result = (attacker.Strength.Value * attackMethod.Strength.Value) * Multiplier;
            logger.AddEntry("{0}:WEAPON-FORMULA: ({1})Attacker.Weapon ({2}) x ({3})Spell.Strength ({4}) x ({5})Multiplier: {6}"
                ._Format(methodName, attacker.Name, attacker.Magic.Value, attackMethod.Name, attackMethod.Strength, Multiplier, result));

            return result;
        }

        protected virtual int? ApplyAttackElement(Character attacker, IAttackMethod attackMethod, int? damageSoFar, BattleLogger logger = null)
        {
            int? result = damageSoFar;
            if (attacker.Element.Equals(attackMethod.Element))
            {
                ElementEffect effect = new ElementEffect(Multiplier, 1, 5);
                int value = effect.GetValue();
                logger.AddEntry("{0} OF ELEMENT {1} USED {2} ALSO OF ELEMENT {1} INCREASED DAMAGE BY {3}"
                    ._Format(attacker.Name, attacker.Element, attackMethod.Name, value));
                result = damageSoFar.Value + value;
            }

            return result;
        }

        protected virtual void ApplyEquipment(PlayerField field, Character character, BattleLogger logger)
        {
            field.EquippedEquipment.Each(eq =>
            {
                ApplyEquipmentElement(character, eq, logger);

                eq.EffectsByEquipmentId.Each(effect =>
                {
                    PropertyInfo prop = typeof(Character).GetProperty(effect.Attribute);
                    int? currentValue = (int?)prop.GetValue(character);
                    int? newValue = effect.Value += currentValue.Value;
                    prop.SetValue(character, newValue);
                });
            });
        }

        protected virtual void ApplyEquipmentElement(Character character, Equipment eq, BattleLogger logger)
        {
            Elements targetElement = Elements.None;
            Elements invokerElement = Elements.None;
            Enum.TryParse<Elements>(character.Element, out targetElement);
            Enum.TryParse<Elements>(eq.Element, out invokerElement);
            ElementEffect elementEffect = GetElementEffect(targetElement, invokerElement);
            character.Speed += elementEffect.LastValue;
            character.Strength += elementEffect.LastValue;
            character.Magic += elementEffect.LastValue;
            logger.AddEntry("EQUIPMENT:{0}:SPEED;STRENGTH;MAGIC:ELEMENT EFFECT: ({1})"._Format(eq.Name, elementEffect.Message));

            if (character.Speed <= 0)
            {
                character.Speed = 1;
            }

            if (character.Strength <= 0)
            {
                character.Strength = 1;
            }

            if (character.Magic <= 0)
            {
                character.Magic = 1;
            }
        }

        protected virtual Character ApplyDefenseSwitch(Character target, DefendOptions defense, BattleLogger report = null)
        {
            Character result = target;
            if (defense.Switch != null && defense.Switch.ActiveCharacter.Equals(target.Id.Value))
            {
                result = Character.OneWhere(c => c.Id == defense.Switch.ReplacementCharacter);
                AddLine(report, "{0} SWITCHED TO {1}"._Format(target.Name, result.Name));
            }
            return result;
        }

        protected virtual int GetCounterAttackStrength(PlayerField defenderField, Character target, DefendOptions defense, BattleLogger report = null)
        {
            int counterDamage = 0;
            if (defense.Skill > 0)
            {
                counterDamage += GetCounterSkillDamage(defenderField, target, defense, report);
            }
            
            if (defense.Spell > 0)
            {
                counterDamage += GetCounterSpellDamage(defenderField, target, defense, report);
            }

            return counterDamage;
        }

        protected virtual int GetCounterSkillDamage(PlayerField defenderField, Character target, DefendOptions defense, BattleLogger logger = null)
        {
            int? counterDamage = 0;
            Skill theSkill = defenderField.Skills.Where(s => s.Id.Value == defense.Skill).FirstOrDefault();
            Chance execution = new Chance(this.CounterSkillChancePercentage, () =>
            {
                counterDamage = GetSkillStrength(target, theSkill, logger);
            });

            bool counterSkillWorked = execution.MightHappen();
            if (logger != null)
            {
                string result = counterSkillWorked ? "COUNTER SKILL HIT: {0}"._Format(counterDamage) : "COUNTER SKILL MISSED";
                AddLine(logger, "{0}:{1}"._Format(theSkill.Name, result));
            }
            return counterDamage.Value;
        }

        protected virtual int GetCounterSpellDamage(PlayerField defenderField, Character target, DefendOptions defense, BattleLogger logger = null)
        {
            int? counterDamage = 0;
            Spell theSpell = defenderField.AvailableSpells.Where(s => s.Id.Value == defense.Spell).FirstOrDefault();
            Chance cast = new Chance(this.CounterSpellChancePercentage, () =>
            {
                List<Spell> available = new List<Spell>(defenderField.AvailableSpells);
                List<Spell> used = new List<Spell>(defenderField.UsedSpells);

                available.Remove(theSpell);
                used.Add(theSpell);
                defenderField.AvailableSpells = available.ToArray();
                defenderField.UsedSpells = used.ToArray();
                counterDamage = GetSpellStrength(target, theSpell, logger);//theSpell.GetAttackStrength(target, Multiplier, report);
            });

            bool counterSpellWorked = cast.MightHappen();
            if (logger != null)
            {
                string result = counterSpellWorked ? "SPELL HIT: {0}"._Format(counterDamage) : "SPELL MISSED";
                AddLine(logger, "{0}:{1}"._Format(theSpell.Name, result));
            }
            return counterDamage.Value;
        }
        
        /// <summary>
        /// Applies defense by dividing the damage by the targets
        /// Defense attribute times the game multiplier
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageSoFar"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        protected virtual int? ApplyDefense(Character target, int? damageSoFar, BattleLogger logger)
        {
            string methodName = MethodBase.GetCurrentMethod().Name;
            int maxReduction = damageSoFar.Value / 3;
            int reduction = target.Defense.Value * maxReduction / 100;
            int damage = damageSoFar.Value - reduction;

            logger.AddEntry("{0}:DEFENSE-FORMULA: ({1})Target.Defense x ({2})MaxReduction / 100: Reduction = {3}: Resulting Damage = {4}"
                ._Format(methodName, target.Defense.Value, maxReduction, reduction, damage));

            return damage;
        }

        protected virtual int? ApplyDefenseElement(Character target, IAttackMethod attackMethod, int? damageSoFar, BattleLogger logger = null)
        {
            int? result = damageSoFar;
            Elements targetElement = Elements.None;
            Elements attackElement = Elements.None;

            Enum.TryParse<Elements>(target.Element, out targetElement);
            Enum.TryParse<Elements>(attackMethod.Element, out attackElement);
            logger.AddEntry("DAMAGE BEFORE ELEMENT EFFECT: {0}"._Format(result));

            ElementEffect effect = GetElementEffect(targetElement, attackElement);
            result = effect.Cause(result.Value);
            string logMsg = "{0}: DAMAGE POST ELEMENT EFFECT: {1}"._Format(effect.Message, result);
            logger.AddEntry(logMsg);
            
            return result;
        }

        protected virtual ElementEffect GetElementEffect(Elements target, Elements invoker)
        {
            ElementEffect effect = new ElementEffect(Multiplier, 1, 5);
            effect.Target = target;
            effect.Invoker = invoker;
            return effect;
        }

        protected void AddLine(BattleLogger report, string line)
        {
            if (report != null)
            {
                report.AddEntry(line);
            }
        }
        
    }
}
