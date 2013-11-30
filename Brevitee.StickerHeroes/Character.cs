using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.StickerHeroes
{
    public partial class Character
    {
        public static Character Create(string name, 
            int strength, 
            int defense, 
            int speed,
            int magic,
            int acuracy,
            int maxHealth,
            Elements element)
        {
            Character ch = Character.OneWhere(c => c.Name == name);
            if (ch == null)
            {
                ch = new Character();
                ch.Name = name;
                ch.Element = element.ToString();
                ch.Strength = strength;
                ch.Defense = defense;
                ch.Speed = speed;
                ch.Magic = magic;
                ch.Acuracy = acuracy;
                ch.MaxHealth = maxHealth;
                ch.Save();
            }

            return ch;
        }

        internal IHealth IHealth
        {
            get;
            set;
        }
        
        public int Health
        {
            get
            {
                return IHealth.Health;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                IHealth.Health = value;
            }
        }
    }
}
