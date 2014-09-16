using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Data;

namespace Brevitee.BattleStickers.Business.Data
{
    public class PlayerSelections
    {
        public PlayerSelections()
        {
            this.Characters = new long[] { };
            this.Weapons = new long[] { };
            this.Spells = new long[] { };
            this.Skills = new long[] { };
            this.Equipment = new long[] { };
            this.SuppressValidate = true;
        }

        public PlayerSelections(Player player)
            : this()
        {
            this.Player = player;
        }

        Player _player;
        public Player Player
        {
            get
            {
                return _player;
            }
            internal protected set
            {
                _player = value;
                SuppressValidate = _player == null;
                Validate();
            }
        }

        long _playerId;
        /// <summary>
        /// The id of the Player or -1 if the Player is not set
        /// </summary>
        public long PlayerId
        {
            get
            {
                if (Player != null)
                {
                    _playerId = Player.Id.Value;
                }
                else
                {
                    _playerId = -1;
                }

                return _playerId;
            }
            set
            {
                Player = Player.OneWhere(c => c.Id == value);
                SuppressValidate = Player == null;
            }
        }
        
        internal protected bool SuppressValidate
        {
            get;
            set;
        }

        public virtual PlayerSelectionsValidation TryValidate()
        {
            return Validate(false);
        }

        internal protected virtual PlayerSelectionsValidation Validate(bool throwOnFailure = false)
        {
            PlayerSelectionsValidation result = new PlayerSelectionsValidation();
            if (SuppressValidate)
            {
                result.Message = "Validation Suppressed: Be sure to set the player first";
                result.Success = false;
            }

            if (Player != null && !SuppressValidate)
            {
                Characters.Each(ch =>
                {
                    if (!Player.HasCharacter(ch))
                    {
                        result.AddMessage("Player ({0}) doesn't have character id ({1})"
                                ._Format(Player.Name, ch));
                        result.Success = false;
                    }
                });

                Weapons.Each(w =>
                {
                    if (!Player.HasWeapon(w))
                    {
                        result.AddMessage("Player ({0}) doesn't have weapon id ({1})"
                                ._Format(Player.Name, w));
                        result.Success = false;
                    }
                });

                Spells.Each(sp =>
                {
                    if (!Player.HasSpell(sp))
                    {
                        result.AddMessage("Player ({0}) doesn't have spell id ({1})"
                            ._Format(Player.Name, sp));
                        result.Success = false;
                    }
                });

                Skills.Each(sk =>
                {
                    if (!Player.HasSkill(sk))
                    {
                        result.AddMessage("Player ({0}) doesn't have skill id ({1})"
                            ._Format(Player.Name, sk));
                        result.Success = false;
                    }
                });

                Equipment.Each(e =>
                {
                    if (!Player.HasEquipment(e))
                    {
                        result.AddMessage("Player ({0}) doesn't have equipment id ({1})"
                            ._Format(Player.Name, e));
                        result.Success = false;
                    }
                });
            }

            if (!result.Success && throwOnFailure)
            {
                throw new Exception(result.Message);
            }
            return result;
        }

        long[] _characterIds;
        public long[] Characters
        {
            get
            {
                return _characterIds;
            }
            set
            {
                _characterIds = value;
                TryValidate();
            }
        }

        long[] _weaponIds;
        public long[] Weapons
        {
            get
            {
                return _weaponIds;
            }
            set
            {
                _weaponIds = value;
                TryValidate();
            }
        }

        long[] _spellIds;
        public long[] Spells
        {
            get
            {
                return _spellIds;
            }
            set
            {
                _spellIds = value;
                TryValidate();
            }
        }

        long[] _skillIds;
        public long[] Skills
        {
            get
            {
                return _skillIds;
            }
            set
            {
                _skillIds = value;
                TryValidate();
            }
        }

        long[] _equipmentIds;
        public long[] Equipment
        {
            get
            {
                return _equipmentIds;
            }
            set
            {
                _equipmentIds = value;
                TryValidate();
            }
        }
    }
}
