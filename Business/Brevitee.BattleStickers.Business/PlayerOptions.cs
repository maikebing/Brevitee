using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerOptions
    {
        public PlayerOptions(Player player)
        {
            this.Player = player;
        }

        public Player Player
        {
            get;
            private set;
        }

        public Character[] Characters
        {
            get
            {
                return Player.Characters.ToArray();
            }
        }

        public Weapon[] Weapons
        {
            get
            {
                return Player.Weapons.ToArray();
            }
        }

        public Spell[] Spells
        {
            get
            {
                return Player.Spells.ToArray();
            }
        }

        public Skill[] Skills
        {
            get
            {
                return Player.Skills.ToArray();
            }
        }

        public Equipment[] Equipment
        {
            get
            {
                return Player.Equipments.ToArray();
            }
        }
    }
}
