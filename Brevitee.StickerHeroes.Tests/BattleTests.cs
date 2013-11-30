using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Brevitee.CommandLine;
using Brevitee;
using Brevitee.Data;
using Brevitee.Testing;
using Brevitee.Encryption;
using Brevitee.StickerHeroes;

namespace Brevitee.StickerHeroes.Tests
{
    [Serializable]
    public class BattleTests: CommandLineTestInterface
    {
        public void First()
        {
            SQLiteRegistrar.Register("BattleStickers");
            _.TryEnsureSchema<Battle>();
        }

        [UnitTest("First", "", "")]
        public void BattleLoop()
        {
            PlayerSelections playerOneSelections = Program.CreatePlayerOneSelections();
            PlayerSelections playerTwoSelections = Program.CreatePlayerTwoSelections();
            Battle battle = Program.StartTestBattle();
            battle.SetBattleType(BattleType.Three);
            battle.SetPlayerOneSelections(playerOneSelections);
            long[] activeIds = battle.MaxActiveCharacters.Value.Times(i =>
            {
                return playerOneSelections.Characters[i];
            });
            battle.SetPlayerOneActiveCharacters(activeIds);

            battle.SetPlayerTwoSelections(playerTwoSelections);
            activeIds = battle.MaxActiveCharacters.Value.Times(i =>
            {
                return playerTwoSelections.Characters[i];
            });
            battle.SetPlayerTwoActiveCharacters(activeIds);

            Expect.IsTrue(battle.State == BattleState.PendingRockPaperScissors);
            battle.SetPlayerTwoRockPaperScissors(RockPaperScissors.Rock);
            battle.SetPlayerOneRockPaperScissors(RockPaperScissors.Paper);
            Player one = battle.PlayerOne;
            Expect.IsTrue(battle.RockPaperScissorsWinner.Equals(one));
            Expect.IsTrue(battle.State == BattleState.PendingTurn);


            BattleLoop(battle);
        }

        private void BattleLoop(Battle battle)
        {
            battle.Logger.EntryAdded += (b, l) =>
            {
                Out(l, ConsoleColor.Green);
            };
            battle.TurnEnded += (b) =>
            {
                OutFormat("Battle Multiplier: ({0})"._Format(battle.Multiplier));
                ShowPlayerOneCharacters(b);
                ShowPlayerTwoCharacters(b);
            };
            int turnCount = 0;
            while (battle.State != BattleState.PlayerOneWin &&
                battle.State != BattleState.PlayerTwoWin)
            {
                PlayerField attacker = battle.Field.Attacker;
                PlayerField defender = battle.Field.Defender;
                AttackOptions attack = CreateAttack(attacker, defender);
                battle.Attack(attack);
                battle.Defend(new DefendOptions());
                turnCount++;
            }

            OutFormat("{0}", ConsoleColor.Green, battle.State.ToString());
            OutFormat("\tTurn Count: {0}", ConsoleColor.Yellow, turnCount);
        }

        private void ShowPlayerOneCharacters(Battle battle)
        {
            ShowCharacters(battle.PlayerOneField, ConsoleColor.Blue);
        }

        private void ShowPlayerTwoCharacters(Battle battle)
        {
            ShowCharacters(battle.PlayerTwoField, ConsoleColor.Magenta);
        }

        private void ShowCharacters(PlayerField field, ConsoleColor color)
        {
            field.AllCharacters.Each(ch =>
            {
                OutFormat("{0}: ({1}), D: ({2}), S: ({3}), M: ({4}), A: ({5}), MH: ({6})",
                    color, ch.Name, ch.Health, ch.Defense, ch.Speed, ch.Magic, ch.Acuracy, ch.MaxHealth);
            });
        }

        private AttackOptions CreateAttack(PlayerField attackerField, PlayerField defenderField)
        {
            AttackType attackType = ChooseAttackType();
            MethodInfo method = this.GetType().GetMethod("Choose{0}"._Format(attackType.ToString()));
            IAttackMethod attack = (IAttackMethod)method.Invoke(this, new object[]{attackerField});
            Character attacker = ChooseCharacter(attackerField);
            Character target = ChooseCharacter(defenderField);
            return AttackOptions.Create(attacker.Id.Value, attackType, attack.Id.Value, target.Id.Value); 
        }

        private AttackType ChooseAttackType()
        {
            AttackType[] types = new AttackType[] { AttackType.Weapon, AttackType.Spell, AttackType.Skill };
            int choice = RandomNumber.Between(0, types.Length);
            return types[choice];
        }

        private Character ChooseCharacter(PlayerField field)
        {
            int count = field.ActiveCharacters.Length;
            int choice = RandomNumber.Between(0, count);
            return field.ActiveCharacters[choice];
        }
        
        private IAttackMethod ChooseAttackMethod(PlayerField field)
        {
            IAttackMethod result = ChooseWeapon(field);
            if (RandomHelper.Bool())
            {
                // spell
                if (field.AvailableSpells.Length > 0)
                {
                    result = ChooseSpell(field);
                }
            }
            else if (RandomHelper.Bool())
            {
                // skill
                if (field.Skills.Length > 0)
                {
                    result = ChooseSkill(field);
                }
            }

            return result;
        }

        public Weapon ChooseWeapon(PlayerField field)
        {
            int count = field.Weapons.Length;
            int choice = RandomNumber.Between(0, count);
            return field.Weapons[choice];
        }

        public Spell ChooseSpell(PlayerField field)
        {
            int count = field.AvailableSpells.Length;
            int choice = RandomNumber.Between(0, count);
            return field.AvailableSpells[choice];
        }

        public Skill ChooseSkill(PlayerField field)
        {
            int count = field.Skills.Length;
            int choice = RandomNumber.Between(0, count);
            return field.Skills[choice];
        }
    }
}
