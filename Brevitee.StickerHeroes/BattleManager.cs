using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using Brevitee.Data;
using Brevitee.Logging;

namespace Brevitee.StickerHeroes
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

        //object _setBattleStateLock = new object();
        //internal void SetBattleState(Battle battle)
        //{
        //    lock (_setBattleStateLock)
        //    {
        //        if (battle.State == BattleState.Invalid) // just started
        //        {
        //            battle.State = BattleState.PendingSetBattleType;
        //        }
        //        else if (battle.MaxActiveCharacters.Value == -1)
        //        {
        //            battle.State = BattleState.PendingSetBattleType;
        //        }
        //        else
        //        {
        //            if (!battle.PlayerOneSelectionsSet && !battle.PlayerTwoSelectionsSet)
        //            {
        //                battle.State = BattleState.PendingSelections;
        //            }
        //            else if (battle.PlayerOneSelectionsSet && !battle.PlayerTwoSelectionsSet)
        //            {
        //                battle.State = BattleState.PendingPlayerTwoSelections;
        //            }
        //            else if (!battle.PlayerOneSelectionsSet && battle.PlayerTwoSelectionsSet)
        //            {
        //                battle.State = BattleState.PendingPlayerOneSelections;
        //            }
        //            else
        //            {
        //                if (battle.PlayerOneField.RockPaperScissors == RockPaperScissors.Invalid &&
        //                    battle.PlayerTwoField.RockPaperScissors == RockPaperScissors.Invalid)
        //                {
        //                    battle.State = BattleState.PendingRockPaperScissors;
        //                }
        //                else if (battle.PlayerOneField.RockPaperScissors != RockPaperScissors.Invalid &&
        //                   battle.PlayerTwoField.RockPaperScissors == RockPaperScissors.Invalid)
        //                {
        //                    battle.State = BattleState.PendingPlayerTwoRockPaperScissors;
        //                }
        //                else if (battle.PlayerOneField.RockPaperScissors == RockPaperScissors.Invalid &&
        //                   battle.PlayerTwoField.RockPaperScissors != RockPaperScissors.Invalid)
        //                {
        //                    battle.State = BattleState.PendingPlayerOneRockPaperScissors;
        //                }
        //                else if (battle.State == BattleState.PendingTurn ||
        //                    battle.State == BattleState.PendingAttacker ||
        //                    battle.State == BattleState.PendingDefender)
        //                {
        //                    if (battle.AttackOptions != null && battle.DefendOptions != null)
        //                    {
        //                        ResolveTurn(battle);
        //                    }
        //                    else if (battle.AttackOptions != null && battle.DefendOptions == null)
        //                    {
        //                        battle.State = BattleState.PendingDefender;
        //                    }
        //                    else if (battle.AttackOptions == null && battle.DefendOptions != null)
        //                    {
        //                        battle.State = BattleState.PendingAttacker;
        //                    }
        //                }
        //                else if (battle.PlayerOneField.RockPaperScissors != RockPaperScissors.Invalid &&
        //                        battle.PlayerTwoField.RockPaperScissors != RockPaperScissors.Invalid)
        //                {
        //                    battle.State = BattleState.PendingTurn;
        //                    battle.SetRockPaperScissorsWinner();
        //                }
        //            }
        //        }
        //    }
        //}

        //public event BattleDelegate TurnEnding;

        //protected void OnTurnEnding(Battle battle)
        //{
        //    if (TurnEnding != null)
        //    {
        //        TurnEnding(battle);
        //    }
        //}

        //internal protected void ResolveTurn(Battle battle)
        //{
        //    OnTurnEnding(battle);

        //    int winner = battle.ResolveTurn();//DamageCalculator.ResolveTurn(battle);
        //    if (winner == 0)
        //    {
        //        battle.State = BattleState.PendingTurn;
        //    }
        //    else if(winner == -1)
        //    {
        //        battle.State = BattleState.PlayerOneWin;
        //    }
        //    else if (winner == 1)
        //    {
        //        battle.State = BattleState.PlayerTwoWin;
        //    }

        //    OnTurnEnded(battle);
        //}

        //public event BattleDelegate TurnEnded;

        //protected void OnTurnEnded(Battle battle)
        //{
        //    if (TurnEnded != null)
        //    {
        //        TurnEnded(battle);
        //    }
        //}

        private void ThrowIfBattleNotFound(long battleId)
        {
            if (!ActiveBattles.ContainsKey(battleId))
            {
                throw new InvalidOperationException("Battle ({0}) not found"._Format(battleId));
            }
        }
    }
}
