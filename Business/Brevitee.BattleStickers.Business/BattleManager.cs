using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.Logging;

namespace Brevitee.BattleStickers.Business.Data
{
    public class BattleManager
    {
        public BattleManager()
        {
            this.ActiveBattles = new Dictionary<long, Battle>();
        }

        static BattleManager _default;
        static object _defaultLock = new object();
        public static BattleManager Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new BattleManager());
            }
        }
        
        /// <summary>
        /// Start a battle between the specified characters
        /// </summary>
        /// <param name="playerOne"></param>
        /// <param name="playerTwo"></param>
        /// <returns></returns>
        public long StartBattle(long playerOne, long playerTwo)
        {
            Player one = Player.OneWhere(c => c.Id == playerOne);
            Args.ThrowIfNull(one, "playerOne");

            Player two = Player.OneWhere(c => c.Id == playerTwo);
            Args.ThrowIfNull(two, "playerTwo");

            Battle battle = Battle.StartNew(one, two);

            ActiveBattles[battle.Id.Value] = battle;
            return battle.Id.Value;
        }

        public void SetBattleType(long battleId, BattleType type)
        {
            ThrowIfBattleNotFound(battleId);

            ActiveBattles[battleId].SetBattleType(type);
        }

        public void SetPlayerOneSelections(long battleId, PlayerSelections selections)
        {
            ThrowIfBattleNotFound(battleId);

            ActiveBattles[battleId].SetPlayerOneSelections(selections);
        }

        public void SetPlayerTwoSelections(long battleId, PlayerSelections selections)
        {
            ThrowIfBattleNotFound(battleId);

            ActiveBattles[battleId].SetPlayerTwoSelections(selections);
        }

        public void SetPlayerOneRockPaperScissors(long battleId, RockPaperScissors option)
        {
            ThrowIfBattleNotFound(battleId);

            ActiveBattles[battleId].SetPlayerOneRockPaperScissors(option);
        }

        public void SetPlayerTwoRockPaperScissors(long battleId, RockPaperScissors option)
        {
            ThrowIfBattleNotFound(battleId);

            ActiveBattles[battleId].SetPlayerTwoRockPaperScissors(option);
        }

        public void Attack(long battleId, AttackOptions options)
        {
            ThrowIfBattleNotFound(battleId);

            ActiveBattles[battleId].Attack(options);
        }

        public Dictionary<long, Battle> ActiveBattles
        {
            get;
            private set;
        }

        private void ThrowIfBattleNotFound(long battleId)
        {
            if (!ActiveBattles.ContainsKey(battleId))
            {
                throw new InvalidOperationException("Battle ({0}) not found"._Format(battleId));
            }
        }
    }
}
