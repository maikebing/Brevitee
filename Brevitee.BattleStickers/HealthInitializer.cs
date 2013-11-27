using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers
{
    public class HealthInitializer
    {
        public HealthInitializer(PlayerField playerField)
        {
            this.PlayerField = playerField;
        }

        public PlayerField PlayerField
        {
            get;
            set;
        }

        public virtual void InitCharacterHealth(PlayerTwo pt, Character character)
        {
            PlayerTwoCharacterHealth ptch = pt.PlayerTwoCharacterHealthsByPlayerTwoId.AddNew();
            ptch.Value = GetInitialHealth(character);//character.MaxHealth;
            character.IHealth = ptch;//.SetHealth(ptch);
        }

        public virtual void InitCharacterHealth(PlayerOne po, Character character)
        {
            PlayerOneCharacterHealth poch = po.PlayerOneCharacterHealthsByPlayerOneId.AddNew();
            poch.Value = GetInitialHealth(character);
            character.IHealth = poch;//.SetHealth(poch);
        }

        public virtual int GetInitialHealth(Character character)
        {            
            Equipment[] equipment = PlayerField.EquippedEquipment;

            return GetInitialHealth(character, equipment);
        }

        private static int GetInitialHealth(Character character, Equipment[] equipment)
        {
            int result = character.MaxHealth.Value;
            equipment.Each(eq =>
            {
                eq.EffectsByEquipmentId.Where(ef => ef.Attribute.Equals(CharacterAttributes.MaxHealth.ToString())).Each(ef =>
                {
                    result += ef.Value.Value;
                });
            });

            return result;
        }
    }
}
