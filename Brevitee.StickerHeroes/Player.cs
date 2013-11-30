using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.StickerHeroes
{
    public partial class Player
    {
        public virtual void PrepareForBattle(Battle battle)
        {
            // place holder for bots to override
        }

        public void Acquire(params Character[] characters)
        {
            Characters.AddRange(characters);
            Save();
        }

        public void Acquire(params Weapon[] weapons)
        {
            Weapons.AddRange(weapons);
            Save();
        }

        public void Acquire(params Spell[] spells)
        {
            Spells.AddRange(spells);
            Save();
        }

        public void Acquire(params Skill[] skills)
        {
            Skills.AddRange(skills);
            Save();
        }

        public void Acquire(params Equipment[] equipment)
        {
            Equipments.AddRange(equipment);
            Save();
        }

        public void Acquire(Character character)
        {
            Characters.Add(character);
            Save();
        }

        public void Acquire(Weapon weapon)
        {
            Weapons.Add(weapon);
            Save();
        }

        public void Acquire(Spell spell)
        {
            Spells.Add(spell);
            Save();
        }

        public void Acquire(Skill skill)
        {
            Skills.Add(skill);
            Save();
        }

        public void Acquire(Equipment equipment)
        {
            Equipments.Add(equipment);
            Save();
        }
      
        public bool Has(Character character)
        {
            Args.ThrowIfNull(character, "character");
            return HasCharacter(character.Id.Value);
        }

        public bool HasCharacter(long characterId)
        {
            return Characters.Where(ch => ch.Id.Value == characterId).FirstOrDefault() != null;
        }

        public bool Has(Weapon weapon)
        {
            Args.ThrowIfNull(weapon, "weapon");
            return HasWeapon(weapon.Id.Value);
        }

        public bool HasWeapon(long weaponId)
        {
            return Weapons.Where(w => w.Id.Value == weaponId).FirstOrDefault() != null;
        }

        public bool Has(Spell spell)
        {
            Args.ThrowIfNull(spell, "spell");
            return HasSpell(spell.Id.Value);
        }

        public bool HasSpell(long spellId)
        {
            return Spells.Where(s => s.Id.Value == spellId).FirstOrDefault() != null;
        }

        public bool Has(Skill skill)
        {
            Args.ThrowIfNull(skill, "skill");
            return HasSkill(skill.Id.Value);
        }

        public bool HasSkill(long skillId)
        {
            return Skills.Where(sk => sk.Id.Value == skillId).FirstOrDefault() != null;
        }

        public bool Has(Equipment equipment)
        {
            Args.ThrowIfNull(equipment, "equipment");
            return HasEquipment(equipment.Id.Value);
        }

        public bool HasEquipment(long equipmentId)
        {
            return Equipments.Where(eq => eq.Id.Value == equipmentId).FirstOrDefault() != null;
        }

        /// <summary>
        /// Gets the player with the specified name
        /// creating them if necessary.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Player GetOne(string name)
        {
            Player p = Player.OneWhere(c => c.Name == name);
            if (p == null)
            {
                p = new Player();
                p.Name = name;
                p.Level = 1;
                p.ExperiencePoints = 0;
                p.Save();
            }

            return p;
        }

    }
}
