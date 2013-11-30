using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee;
using System.Reflection;

namespace Brevitee.StickerHeroes
{
    public partial class Battle
    {
        static int _defaultMultiplier;
        public static int DefaultMultiplier
        {
            get
            {
                if (_defaultMultiplier == 0)
                {
                    _defaultMultiplier = 4;
                }
                return _defaultMultiplier;
            }
            set
            {
                _defaultMultiplier = value;
            }
        }

        int _multiplier;
        public int Multiplier
        {
            get
            {
                if (_multiplier == 0)
                {
                    _multiplier = DefaultMultiplier;
                }
                return _multiplier;
            }
            set
            {
                _multiplier = value;
            }
        }

        PlayerField _playerOneField;
        object _playerOneLock = new object();
        public PlayerField PlayerOneField
        {
            get
            {
                _playerOneLock.DoubleCheckLock(ref _playerOneField, () =>
                {
                    PlayerField pf = new PlayerField();
                    return pf;
                });
                
                return _playerOneField;
            }
        }

        PlayerField _playerTwoField;
        object _playerTwoLock = new object();
        public PlayerField PlayerTwoField
        {
            get
            {
                _playerTwoLock.DoubleCheckLock(ref _playerTwoField, () =>
                {
                    PlayerField pf = new PlayerField();
                    return pf;
                });

                return _playerTwoField;
            }
        }

        BattleLogger _logger;
        object _loggerLock = new object();
        public BattleLogger Logger
        {
            get
            {
                return _loggerLock.DoubleCheckLock(ref _logger, () => new BattleLogger(this));
            }
            set
            {
                _logger = value;
            }
        }

        DamageCalculator _damageCalculator;
        object _damageCalculatorLock = new object();
        public DamageCalculator DamageCalculator
        {
            get
            {
                return _damageCalculatorLock.DoubleCheckLock(ref _damageCalculator, () => new DamageCalculator(this, Logger));
            }
            set
            {
                _damageCalculator = value;
            }
        }
        
        HealthInitializer _playerOneHealthInitializer;
        object _playerOneHealthInitializerLock = new object();
        public HealthInitializer PlayerOneHealthInitializer
        {
            get
            {

                return _playerOneHealthInitializerLock.DoubleCheckLock(ref _playerOneHealthInitializer, () => new HealthInitializer(this.PlayerOneField));
            }
            set
            {
                _playerOneHealthInitializer = value;
            }
        }

        HealthInitializer _playerTwoHealthInitializer;
        object _playerTwoHealthInitializerLock = new object();
        public HealthInitializer PlayerTwoHealthInitializer
        {
            get
            {

                return _playerTwoHealthInitializerLock.DoubleCheckLock(ref _playerTwoHealthInitializer, () => new HealthInitializer(this.PlayerTwoField));
            }
            set
            {
                _playerTwoHealthInitializer = value;
            }
        }


        private void SetupPlayerOneField(PlayerField field)
        {
            PlayerOne po = this.PlayerOnesByBattleId.FirstOrDefault();
            
            if (po != null)
            {
                field.Battle = this;
                field.Player = po.PlayerOfPlayerId;
                field.AllCharacters = po.Characters.ToArray();
                po.Save();
                field.AllCharacters.Each(ch =>
                {
                    PlayerOneHealthInitializer.InitCharacterHealth(po, ch);
                });

                field.Weapons = po.Weapons.ToArray();
                field.AllSpells = po.Spells.ToArray();
                field.AvailableSpells = po.Spells.ToArray();
                field.Skills = po.Skills.ToArray();
                field.AllEquipment = po.Equipments.ToArray();
            }
        }

        private void SetupPlayerTwoField(PlayerField field)
        {
            PlayerTwo pt = this.PlayerTwosByBattleId.FirstOrDefault();
            if (pt != null)
            {
                field.Battle = this;
                field.Player = pt.PlayerOfPlayerId;
                field.AllCharacters = pt.Characters.ToArray();
                pt.Save();
                field.AllCharacters.Each(ch =>
                {
                    PlayerTwoHealthInitializer.InitCharacterHealth(pt, ch);
                });

                field.Weapons = pt.Weapons.ToArray();
                field.AllSpells = pt.Spells.ToArray();
                field.AvailableSpells = pt.Spells.ToArray();
                field.Skills = pt.Skills.ToArray();
                field.AllEquipment = pt.Equipments.ToArray();
            }
        }

        public static Battle StartNew(Player playerOne, Player playerTwo)
        {
            Battle battle = new Battle();
            battle.MaxActiveCharacters = -1;
            battle.RockPaperScissorsWinnerId = -1;
            battle.Save();

            PlayerOne one = battle.PlayerOnesByBattleId.AddNew();
            one.PlayerId = playerOne.Id;

            PlayerTwo two = battle.PlayerTwosByBattleId.AddNew();
            two.PlayerId = playerTwo.Id;

            battle.Save();

            playerOne.PrepareForBattle(battle);
            playerTwo.PrepareForBattle(battle);
            battle.SetState();

            return battle;
        }

        /// <summary>
        /// The event that is raised every time the 
        /// Battle.State property is set regardless of
        /// what the state was previously
        /// </summary>
        public event BattleDelegate StateSet;

        /// <summary>
        /// The event that is raised when the 
        /// Battle.State property is set to a new
        /// value
        /// </summary>
        public event BattleDelegate StateChanged;

        protected void OnStateSet()
        {
            if (StateSet != null)
            {
                StateSet(this);
            }

            if (StateChanged != null && (_currentState != _previousState))
            {
                StateChanged(this);
            }
        }

        BattleState _currentState;
        BattleState _previousState;
        public BattleState State
        {
            get
            {
                return _currentState;
            }
            set
            {
                _previousState = _currentState;
                _currentState = value;
            }
        }

        public void SetBattleType(BattleType type)
        {
            this.MaxActiveCharacters = (int)type;
            this.PlayerOneField.MaxActiveCharacters = this.MaxActiveCharacters.Value;
            this.PlayerTwoField.MaxActiveCharacters = this.MaxActiveCharacters.Value;
            this.Save();
            this.SetState();
        }

        public void SetPlayerOneActiveCharacters(long[] characterIds)
        {
            this.PlayerOneField.SetActive(characterIds);
        }

        public void SetPlayerTwoActiveCharacters(long[] characterIds)
        {
            this.PlayerTwoField.SetActive(characterIds);
        }

        internal bool PlayerOneSelectionsSet
        {
            get;
            set;
        }

        // TODO: change the implementation of this to use IN queries
        //
        /// <summary>
        /// Sets playerones selections
        /// </summary>
        /// <param name="selections"></param>
        public PlayerSelectionsValidation SetPlayerOneSelections(PlayerSelections selections)
        {
            PlayerSelectionsValidation validation = selections.TryValidate();

            if (validation.Success)
            {

                PlayerOne po = this.PlayerOnesByBattleId.FirstOrDefault();

                if (selections.Characters.Length > 0)
                {
                    CharacterCollection characters = Character.Where(c => c.Id.In(selections.Characters));
                    po.Characters.AddRange(characters);
                }

                //selections.Characters.Each(id =>
                //{
                //    Character ch = Character.OneWhere(c => c.Id == id);

                //    po.Characters.Add(ch);
                //});

                if (selections.Weapons.Length > 0)
                {
                    WeaponCollection weapons = Weapon.Where(c => c.Id.In(selections.Weapons));
                    po.Weapons.AddRange(weapons);
                }

                //selections.Weapons.Each(id =>
                //{
                //    Weapon w = Weapon.OneWhere(c => c.Id == id);
                //    po.Weapons.Add(w);
                //});

                if (selections.Spells.Length > 0)
                {
                    SpellCollection spells = Spell.Where(c => c.Id.In(selections.Spells));
                    po.Spells.AddRange(spells);
                }

                //selections.Spells.Each(id =>
                //{
                //    Spell s = Spell.OneWhere(c => c.Id == id);
                //    po.Spells.Add(s);
                //});

                if (selections.Skills.Length > 0)
                {
                    SkillCollection skills = Skill.Where(c => c.Id.In(selections.Skills));
                    po.Skills.AddRange(skills);
                }

                //selections.Skills.Each(id =>
                //{
                //    Skill s = Skill.OneWhere(c => c.Id == id);
                //    po.Skills.Add(s);
                //});

                if (selections.Equipment.Length > 0)
                {
                    EquipmentCollection equipment = Equipment.Where(c => c.Id.In(selections.Equipment));
                    po.Equipments.AddRange(equipment);
                }

                //selections.Equipment.Each(id =>
                //{
                //    Equipment eq = Equipment.OneWhere(c => c.Id == id);
                //    po.Equipments.Add(eq);
                //});

                po.Save();
                SetupPlayerOneField(PlayerOneField);

                PlayerOneSelectionsSet = true;

                this.SetState();
            }

            return validation;
        }

        internal bool PlayerTwoSelectionsSet
        {
            get;
            set;
        }

        public void SetPlayerTwoSelections(PlayerSelections selections)
        {
            PlayerTwo pt = this.PlayerTwosByBattleId.FirstOrDefault();

            selections.Characters.Each(id =>
            {
                Character ch = Character.OneWhere(c => c.Id == id);
                
                pt.Characters.Add(ch);				
            });

            selections.Weapons.Each(id =>
            {
                Weapon w = Weapon.OneWhere(c => c.Id == id);
                pt.Weapons.Add(w);
            });

            selections.Spells.Each(id =>
            {
                Spell s = Spell.OneWhere(c => c.Id == id);
                pt.Spells.Add(s);
            });

            selections.Skills.Each(id =>
            {
                Skill s = Skill.OneWhere(c => c.Id == id);
                pt.Skills.Add(s);
            });

            selections.Equipment.Each(id =>
            {
                Equipment eq = Equipment.OneWhere(c => c.Id == id);
                pt.Equipments.Add(eq);
            });

            pt.Save();
            SetupPlayerTwoField(PlayerTwoField);

            PlayerTwoSelectionsSet = true;

            this.SetState();
        }

        public void SetPlayerOneRockPaperScissors(RockPaperScissors option)
        {
            PlayerOneField.RockPaperScissors = option;
            this.SetState();
        }

        public void SetPlayerTwoRockPaperScissors(RockPaperScissors option)
        {
            PlayerTwoField.RockPaperScissors = option;
            this.SetState();
        }

        public void SetRockPaperScissorsWinner()
        {
            if (this.State == BattleState.PendingTurn)
            {
                RockPaperScissors playerOneChoice = PlayerOneField.RockPaperScissors;
                RockPaperScissors playerTwoChoice = PlayerTwoField.RockPaperScissors;
                
                this.RockPaperScissorsWinnerId = GetRockPaperScissorsWinner(playerOneChoice, playerTwoChoice);
                if (this.RockPaperScissorsWinnerId == 0)
                {
                    PlayerOneField.RockPaperScissors = RockPaperScissors.Invalid;
                    PlayerTwoField.RockPaperScissors = RockPaperScissors.Invalid;
                    this.Field = null;
                    this.SetState();
                }
                else
                {
                    PlayerField attacker = PlayerOneField.Player.Equals(RockPaperScissorsWinner) ? PlayerOneField : PlayerTwoField;
                    PlayerField defender = PlayerOneField == attacker ? PlayerTwoField : PlayerOneField;
                    attacker.MaxActiveCharacters = this.MaxActiveCharacters.Value;
                    defender.MaxActiveCharacters = this.MaxActiveCharacters.Value;
                    this.Field = new BattleField(this, attacker, defender);
                }
            }
        }

        internal long GetRockPaperScissorsWinner(RockPaperScissors one, RockPaperScissors two)
        {
            if (one == RockPaperScissors.Invalid)
            {
                throw new InvalidOperationException(
                    "{0}: Player One hasn't made a selection"
                    ._Format(MethodBase.GetCurrentMethod().Name)
                );
            }

            if (two == RockPaperScissors.Invalid)
            {
                throw new InvalidOperationException(
                    "{0}: Player Two hasn't made a selection"
                    ._Format(MethodBase.GetCurrentMethod().Name)
                );
            }

            long playerOne = PlayerOne.Id.Value;
            long playerTwo = PlayerTwo.Id.Value;
            long winner = 0;

            switch (one)
            {
                case RockPaperScissors.Rock:
                    switch (two)
                    {
                        case RockPaperScissors.Rock:
                            // tie send winner = 0 
                            break;
                        case RockPaperScissors.Paper:
                            winner = playerTwo;
                            break;
                        case RockPaperScissors.Scissors:
                            winner = playerOne;
                            break;
                    }
                    break;
                case RockPaperScissors.Paper:
                    switch (two)
                    {
                        case RockPaperScissors.Rock:
                            winner = playerOne;
                            break;
                        case RockPaperScissors.Paper:
                            // tie
                            break;
                        case RockPaperScissors.Scissors:
                            winner = playerTwo;
                            break;
                    }
                    break;
                case RockPaperScissors.Scissors:
                    switch (two)
                    {
                        case RockPaperScissors.Rock:
                            winner = playerTwo;
                            break;
                        case RockPaperScissors.Paper:
                            winner = playerOne;
                            break;
                        case RockPaperScissors.Scissors:
                            // tie
                            break;
                    }
                    break;
            }

            return winner;
        }

        public BattleField Field
        {
            get;
            set;
        }

        internal AttackOptions AttackOptions
        {
            get
            {
                return Field.AttackOptions;
            }
            set
            {
                Field.AttackOptions = value;
            }
        }

        public void Attack(AttackOptions options)
        {
            AttackOptions = options;
            this.SetState();
        }

        internal DefendOptions DefendOptions
        {
            get
            {
                return Field.DefendOptions;
            }
            set
            {
                Field.DefendOptions = value;
            }
        }

        public void Defend(DefendOptions options)
        {
            DefendOptions = options;
            this.SetState();
        }

        public Player PlayerOne
        {
            get
            {
                PlayerOne p = this.PlayerOnesByBattleId.FirstOrDefault();
                Player value = null;
                if (p != null)
                {
                    value = p.PlayerOfPlayerId;
                }

                return value;
            }
        }

        public Player PlayerTwo
        {
            get
            {
                PlayerTwo p = this.PlayerTwosByBattleId.FirstOrDefault();
                Player value = null;
                if (p != null)
                {
                    value = p.PlayerOfPlayerId;
                }

                return value;
            }         
        }

        public Player RockPaperScissorsWinner
        {
            get
            {
                return this.PlayerOfRockPaperScissorsWinnerId;
            }
        }

        public Player GetOponent(Player self)
        {
            Player result = null;
            if (PlayerOne == self)
            {
                result = PlayerTwo;
            }
            else if (PlayerTwo == self)
            {
                result = PlayerOne;
            }

            if (result == null)
            {
                throw new InvalidOperationException("The specified character was not in the battle");
            }

            return result;
        }

        object _setBattleStateLock = new object();
        internal void SetState()
        {
            lock (_setBattleStateLock)
            {
                if (this.State == BattleState.Invalid) // just started
                {
                    this.State = BattleState.PendingSetBattleType;
                }
                else if (this.MaxActiveCharacters.Value == -1)
                {
                    this.State = BattleState.PendingSetBattleType;
                }
                else
                {
                    if (!this.PlayerOneSelectionsSet && !this.PlayerTwoSelectionsSet)
                    {
                        this.State = BattleState.PendingSelections;
                    }
                    else if (this.PlayerOneSelectionsSet && !this.PlayerTwoSelectionsSet)
                    {
                        this.State = BattleState.PendingPlayerTwoSelections;
                    }
                    else if (!this.PlayerOneSelectionsSet && this.PlayerTwoSelectionsSet)
                    {
                        this.State = BattleState.PendingPlayerOneSelections;
                    }
                    else
                    {
                        if (this.PlayerOneField.RockPaperScissors == RockPaperScissors.Invalid &&
                            this.PlayerTwoField.RockPaperScissors == RockPaperScissors.Invalid)
                        {
                            this.State = BattleState.PendingRockPaperScissors;
                        }
                        else if (this.PlayerOneField.RockPaperScissors != RockPaperScissors.Invalid &&
                           this.PlayerTwoField.RockPaperScissors == RockPaperScissors.Invalid)
                        {
                            this.State = BattleState.PendingPlayerTwoRockPaperScissors;
                        }
                        else if (this.PlayerOneField.RockPaperScissors == RockPaperScissors.Invalid &&
                           this.PlayerTwoField.RockPaperScissors != RockPaperScissors.Invalid)
                        {
                            this.State = BattleState.PendingPlayerOneRockPaperScissors;
                        }
                        else if (this.State == BattleState.PendingTurn ||
                            this.State == BattleState.PendingAttacker ||
                            this.State == BattleState.PendingDefender)
                        {
                            if (this.AttackOptions != null && this.DefendOptions != null)
                            {
                                ResolveTurn();
                            }
                            else if (this.AttackOptions != null && this.DefendOptions == null)
                            {
                                this.State = BattleState.PendingDefender;
                            }
                            else if (this.AttackOptions == null && this.DefendOptions != null)
                            {
                                this.State = BattleState.PendingAttacker;
                            }
                        }
                        else if (this.PlayerOneField.RockPaperScissors != RockPaperScissors.Invalid &&
                                this.PlayerTwoField.RockPaperScissors != RockPaperScissors.Invalid)
                        {
                            this.State = BattleState.PendingTurn;
                            this.SetRockPaperScissorsWinner();
                        }
                    }
                }
            }
        }

        public event BattleDelegate TurnEnding;

        protected void OnTurnEnding()
        {
            if (TurnEnding != null)
            {
                TurnEnding(this);
            }
        }

        internal protected void ResolveTurn()
        {
            OnTurnEnding();

            int winner = DamageCalculator.ResolveTurn(this);
            if (winner == 0)
            {
                this.State = BattleState.PendingTurn;
            }
            else if (winner == -1)
            {
                this.State = BattleState.PlayerOneWin;
            }
            else if (winner == 1)
            {
                this.State = BattleState.PlayerTwoWin;
            }

            OnTurnEnded();
        }

        public event BattleDelegate TurnEnded;

        protected void OnTurnEnded()
        {
            if (TurnEnded != null)
            {
                TurnEnded(this);
            }
        }
    }
}
