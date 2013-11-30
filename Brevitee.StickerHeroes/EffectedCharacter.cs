using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;

namespace Brevitee.StickerHeroes
{
    public class EffectedCharacter: Character
    {
        public EffectedCharacter(Character character)
        {
            this.Actual = character;
            this.IHealth = character.IHealth;
            this.CopyProperties(character);
        }

        public Character Actual
        {
            get;
            set;
        }
    }
}
