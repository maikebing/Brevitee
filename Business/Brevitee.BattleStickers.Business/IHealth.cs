using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers.Business.Data
{
    public interface IHealth
    {
        int Health { get; set; }
        void Save();
    }
}
