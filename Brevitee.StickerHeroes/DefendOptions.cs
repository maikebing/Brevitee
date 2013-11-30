using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.StickerHeroes
{
    public class DefendOptions
    {
        public DefendOptions()
        {
            this.Spell = -1;
            this.Skill = -1;
            this.Switch = new CharacterSwitch();
        }
        public long Spell { get; set; }
        public long Skill { get; set; }

        public bool Quit { get; set; }

        public CharacterSwitch Switch { get; set; }
    }
}
