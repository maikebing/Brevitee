using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.StickerHeroes
{
    public partial class Effect
    {
        public static Effect[] FromDictionary(Dictionary<CharacterAttributes, int> effects)
        {
            Effect[] results = new Effect[effects.Count];
            effects.Keys.Each((attr, i) =>
            {
                Effect effect = new Effect();
                effect.Attribute = attr.ToString();
                effect.Value = effects[attr];
                results[i] = effect;
            });

            return results;
        }
    }
}
