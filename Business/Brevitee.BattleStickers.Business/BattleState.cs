using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers.Business.Data
{
    public enum BattleState
    {
        Invalid,
        PendingSetBattleType,
        PendingSelections,
        PendingPlayerOneSelections,
        PendingPlayerTwoSelections,
        PendingRockPaperScissors,
        PendingPlayerOneRockPaperScissors,
        PendingPlayerTwoRockPaperScissors,
        //loop
        PendingTurn,
        PendingAttacker,
        PendingDefender,
        //-loop
        PlayerOneWin,
        PlayerTwoWin
    }
}
