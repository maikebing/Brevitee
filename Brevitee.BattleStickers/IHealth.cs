using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers
{
    public interface IHealth
    {
        int Health { get; set; }
        void Save();
    }
}
