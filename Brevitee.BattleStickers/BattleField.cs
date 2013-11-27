using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Logging;

namespace Brevitee.BattleStickers
{
    /// <summary>
    /// Represents both the PlayerFields of player one and two
    /// </summary>
    public class BattleField
    {
        public BattleField(Battle battle, PlayerField attacker, PlayerField defender)
        {
            this.Battle = battle;
            this.Attacker = attacker;
            this.Defender = defender;
        }

        public bool BattleIsDone()
        {
            bool gameOver = Defender.ActiveCharacters.Length == 0;
            //foreach (Character ch in Defender.ActiveCharacters)
            //{
            //    if (!Defender.DownCharacters.Contains(ch))
            //    {
            //        gameOver = false;
            //        break;
            //    }
            //}

            return gameOver;
        }

        internal void AfterTurn(BattleLogger report = null)
        {
            Defender.AfterTurn(report);
            Attacker.AfterTurn(report);
        }

        public Battle Battle
        {
            get;
            private set;
        }

        public AttackOptions AttackOptions
        {
            get;
            set;
        }
        
        public PlayerField Attacker
        {
            get;
            set;
        }

        public DefendOptions DefendOptions
        {
            get;
            set;
        }

        public PlayerField Defender
        {
            get;
            set;
        }
    }
}
