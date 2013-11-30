using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Logging;

namespace Brevitee.StickerHeroes
{
    public class PlayerField
    {
        public PlayerField()
        {
            this.MaxActiveCharacters = 1;
            this.DownCharacters = new Character[] { };
            this.EquippedEquipment = new Equipment[] { };
            this.Weapons = new Weapon[] { };
            this.Skills = new Skill[] { };

        }

        public int MaxActiveCharacters
        {
            get;
            set;
        }

        public Battle Battle
        {
            get;
            set;
        }

        public Player Player
        {
            get;
            set;
        }
        
        public RockPaperScissors RockPaperScissors
        {
            get;
            set;
        }

        public void SetActive(long[] characterIds)
        {
            Character[] active = new Character[characterIds.Length];
            characterIds.Each((cid, i) =>
            {
                active[i] = AllCharacters.Where(c => c.Id == cid).FirstOrDefault();
            });

            ActiveCharacters = active;
        }
        

        public void Equip(long[] equipmentIds)
        {
            Equipment[] equipped = new Equipment[equipmentIds.Length];
            equipmentIds.Each((eid, i) =>
            {
                equipped[i] = AllEquipment.Where(c => c.Id == eid).FirstOrDefault();
            });

            EquippedEquipment = equipped;
        }

        public void AfterTurn(BattleLogger report)
        {
            List<Character> downed = new List<Character>();
            List<Character> active = new List<Character>();
            ActiveCharacters.Each(ch =>
            {
                if (ch.Health <= 0)
                {
                    downed.Add(ch);
                    report.AddEntry("{0}'s {1} is down"._Format(Player.Name, ch.Name));
                }
                else
                {
                    active.Add(ch);
                }
            });

            DownCharacters = downed.ToArray();
            ActiveCharacters = active.ToArray();
        }

        public Character[] ActiveCharacters
        {
            get;
            private set;
        }

        public Character[] DownCharacters
        {
            get;
            private set;
        }

        public Character[] AllCharacters
        {
            get;
            set;
        }

        public Weapon[] Weapons
        {
            get;
            set;
        }

        public Skill[] Skills
        {
            get;
            set;
        }

        public Spell[] AvailableSpells
        {
            get;
            set;
        }

        public Spell[] UsedSpells
        {
            get;
            set;
        }

        public Spell[] AllSpells
        {
            get;
            set;
        }

        public Equipment[] EquippedEquipment
        {
            get;
            set;
        }

        public Equipment[] AllEquipment
        {
            get;
            set;
        }
    }
}
