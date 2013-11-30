using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.StickerHeroes
{
    public partial class Equipment
    {
        public EffectCollection Effects
        {
            get
            {
                return EffectsByEquipmentId;
            }
        }

        public static Equipment Create(string name, Dictionary<CharacterAttributes, int> effects)
        {
            return Create(name, Effect.FromDictionary(effects));
        }

        public static Equipment Create(string name, Dictionary<CharacterAttributes, int> effects, Elements element)
        {
            return Create(name, element, Effect.FromDictionary(effects));
        }

        public static Equipment Create(string name, params Effect[] effects)
        {
            return Create(name, Elements.None, effects);
        }

        public static Equipment Create(string name, Elements element, params Effect[] effects)
        {
            Equipment e = new Equipment();
            e.Name = name;
            e.Element = element.ToString();
            e.Save();

            if (effects != null)
            {
                effects.Each(ef =>
                {
                    e.EffectsByEquipmentId.Add(ef);
                });

                e.Save();
            }

            return e;
        }
    }
}
