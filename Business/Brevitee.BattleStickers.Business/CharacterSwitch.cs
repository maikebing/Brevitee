using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers.Business.Data
{
    public class CharacterSwitch
    {
        public CharacterSwitch()
        {
            this.ActiveCharacter = -1;
            this.ReplacementCharacter = -1;
        }
        public long ActiveCharacter { get; set; }
        public long ReplacementCharacter { get; set; }
    }
}
