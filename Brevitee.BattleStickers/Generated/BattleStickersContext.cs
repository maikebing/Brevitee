// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.BattleStickers
{
	// schema = BattleStickers 
    public static class BattleStickersContext
    {
		public static Database Db
		{
			get
			{
				return _.Db.For("BattleStickers");
			}
		}


	public class BattleQueryContext
	{
			public BattleCollection Where(WhereDelegate<BattleColumns> where, Database db = null)
			{
				return Battle.Where(where, db);
			}
		   
			public BattleCollection Where(WhereDelegate<BattleColumns> where, OrderBy<BattleColumns> orderBy = null, Database db = null)
			{
				return Battle.Where(where, orderBy, db);
			}

			public Battle OneWhere(WhereDelegate<BattleColumns> where, Database db = null)
			{
				return Battle.OneWhere(where, db);
			}
		
			public Battle FirstOneWhere(WhereDelegate<BattleColumns> where, Database db = null)
			{
				return Battle.FirstOneWhere(where, db);
			}

			public BattleCollection Top(int count, WhereDelegate<BattleColumns> where, Database db = null)
			{
				return Battle.Top(count, where, db);
			}

			public BattleCollection Top(int count, WhereDelegate<BattleColumns> where, OrderBy<BattleColumns> orderBy, Database db = null)
			{
				return Battle.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<BattleColumns> where, Database db = null)
			{
				return Battle.Count(where, db);
			}
	}

	static BattleQueryContext _battles;
	static object _battlesLock = new object();
	public static BattleQueryContext Battles
	{
		get
		{
			return _battlesLock.DoubleCheckLock<BattleQueryContext>(ref _battles, () => new BattleQueryContext());
		}
	}
	public class PlayerQueryContext
	{
			public PlayerCollection Where(WhereDelegate<PlayerColumns> where, Database db = null)
			{
				return Player.Where(where, db);
			}
		   
			public PlayerCollection Where(WhereDelegate<PlayerColumns> where, OrderBy<PlayerColumns> orderBy = null, Database db = null)
			{
				return Player.Where(where, orderBy, db);
			}

			public Player OneWhere(WhereDelegate<PlayerColumns> where, Database db = null)
			{
				return Player.OneWhere(where, db);
			}
		
			public Player FirstOneWhere(WhereDelegate<PlayerColumns> where, Database db = null)
			{
				return Player.FirstOneWhere(where, db);
			}

			public PlayerCollection Top(int count, WhereDelegate<PlayerColumns> where, Database db = null)
			{
				return Player.Top(count, where, db);
			}

			public PlayerCollection Top(int count, WhereDelegate<PlayerColumns> where, OrderBy<PlayerColumns> orderBy, Database db = null)
			{
				return Player.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerColumns> where, Database db = null)
			{
				return Player.Count(where, db);
			}
	}

	static PlayerQueryContext _players;
	static object _playersLock = new object();
	public static PlayerQueryContext Players
	{
		get
		{
			return _playersLock.DoubleCheckLock<PlayerQueryContext>(ref _players, () => new PlayerQueryContext());
		}
	}
	public class PlayerOneQueryContext
	{
			public PlayerOneCollection Where(WhereDelegate<PlayerOneColumns> where, Database db = null)
			{
				return PlayerOne.Where(where, db);
			}
		   
			public PlayerOneCollection Where(WhereDelegate<PlayerOneColumns> where, OrderBy<PlayerOneColumns> orderBy = null, Database db = null)
			{
				return PlayerOne.Where(where, orderBy, db);
			}

			public PlayerOne OneWhere(WhereDelegate<PlayerOneColumns> where, Database db = null)
			{
				return PlayerOne.OneWhere(where, db);
			}
		
			public PlayerOne FirstOneWhere(WhereDelegate<PlayerOneColumns> where, Database db = null)
			{
				return PlayerOne.FirstOneWhere(where, db);
			}

			public PlayerOneCollection Top(int count, WhereDelegate<PlayerOneColumns> where, Database db = null)
			{
				return PlayerOne.Top(count, where, db);
			}

			public PlayerOneCollection Top(int count, WhereDelegate<PlayerOneColumns> where, OrderBy<PlayerOneColumns> orderBy, Database db = null)
			{
				return PlayerOne.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneColumns> where, Database db = null)
			{
				return PlayerOne.Count(where, db);
			}
	}

	static PlayerOneQueryContext _playerOnes;
	static object _playerOnesLock = new object();
	public static PlayerOneQueryContext PlayerOnes
	{
		get
		{
			return _playerOnesLock.DoubleCheckLock<PlayerOneQueryContext>(ref _playerOnes, () => new PlayerOneQueryContext());
		}
	}
	public class PlayerTwoQueryContext
	{
			public PlayerTwoCollection Where(WhereDelegate<PlayerTwoColumns> where, Database db = null)
			{
				return PlayerTwo.Where(where, db);
			}
		   
			public PlayerTwoCollection Where(WhereDelegate<PlayerTwoColumns> where, OrderBy<PlayerTwoColumns> orderBy = null, Database db = null)
			{
				return PlayerTwo.Where(where, orderBy, db);
			}

			public PlayerTwo OneWhere(WhereDelegate<PlayerTwoColumns> where, Database db = null)
			{
				return PlayerTwo.OneWhere(where, db);
			}
		
			public PlayerTwo FirstOneWhere(WhereDelegate<PlayerTwoColumns> where, Database db = null)
			{
				return PlayerTwo.FirstOneWhere(where, db);
			}

			public PlayerTwoCollection Top(int count, WhereDelegate<PlayerTwoColumns> where, Database db = null)
			{
				return PlayerTwo.Top(count, where, db);
			}

			public PlayerTwoCollection Top(int count, WhereDelegate<PlayerTwoColumns> where, OrderBy<PlayerTwoColumns> orderBy, Database db = null)
			{
				return PlayerTwo.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoColumns> where, Database db = null)
			{
				return PlayerTwo.Count(where, db);
			}
	}

	static PlayerTwoQueryContext _playerTwos;
	static object _playerTwosLock = new object();
	public static PlayerTwoQueryContext PlayerTwos
	{
		get
		{
			return _playerTwosLock.DoubleCheckLock<PlayerTwoQueryContext>(ref _playerTwos, () => new PlayerTwoQueryContext());
		}
	}
	public class CharacterQueryContext
	{
			public CharacterCollection Where(WhereDelegate<CharacterColumns> where, Database db = null)
			{
				return Character.Where(where, db);
			}
		   
			public CharacterCollection Where(WhereDelegate<CharacterColumns> where, OrderBy<CharacterColumns> orderBy = null, Database db = null)
			{
				return Character.Where(where, orderBy, db);
			}

			public Character OneWhere(WhereDelegate<CharacterColumns> where, Database db = null)
			{
				return Character.OneWhere(where, db);
			}
		
			public Character FirstOneWhere(WhereDelegate<CharacterColumns> where, Database db = null)
			{
				return Character.FirstOneWhere(where, db);
			}

			public CharacterCollection Top(int count, WhereDelegate<CharacterColumns> where, Database db = null)
			{
				return Character.Top(count, where, db);
			}

			public CharacterCollection Top(int count, WhereDelegate<CharacterColumns> where, OrderBy<CharacterColumns> orderBy, Database db = null)
			{
				return Character.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CharacterColumns> where, Database db = null)
			{
				return Character.Count(where, db);
			}
	}

	static CharacterQueryContext _characters;
	static object _charactersLock = new object();
	public static CharacterQueryContext Characters
	{
		get
		{
			return _charactersLock.DoubleCheckLock<CharacterQueryContext>(ref _characters, () => new CharacterQueryContext());
		}
	}
	public class RequiredLevelQueryContext
	{
			public RequiredLevelCollection Where(WhereDelegate<RequiredLevelColumns> where, Database db = null)
			{
				return RequiredLevel.Where(where, db);
			}
		   
			public RequiredLevelCollection Where(WhereDelegate<RequiredLevelColumns> where, OrderBy<RequiredLevelColumns> orderBy = null, Database db = null)
			{
				return RequiredLevel.Where(where, orderBy, db);
			}

			public RequiredLevel OneWhere(WhereDelegate<RequiredLevelColumns> where, Database db = null)
			{
				return RequiredLevel.OneWhere(where, db);
			}
		
			public RequiredLevel FirstOneWhere(WhereDelegate<RequiredLevelColumns> where, Database db = null)
			{
				return RequiredLevel.FirstOneWhere(where, db);
			}

			public RequiredLevelCollection Top(int count, WhereDelegate<RequiredLevelColumns> where, Database db = null)
			{
				return RequiredLevel.Top(count, where, db);
			}

			public RequiredLevelCollection Top(int count, WhereDelegate<RequiredLevelColumns> where, OrderBy<RequiredLevelColumns> orderBy, Database db = null)
			{
				return RequiredLevel.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RequiredLevelColumns> where, Database db = null)
			{
				return RequiredLevel.Count(where, db);
			}
	}

	static RequiredLevelQueryContext _requiredLevels;
	static object _requiredLevelsLock = new object();
	public static RequiredLevelQueryContext RequiredLevels
	{
		get
		{
			return _requiredLevelsLock.DoubleCheckLock<RequiredLevelQueryContext>(ref _requiredLevels, () => new RequiredLevelQueryContext());
		}
	}
	public class PlayerTwoCharacterHealthQueryContext
	{
			public PlayerTwoCharacterHealthCollection Where(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
			{
				return PlayerTwoCharacterHealth.Where(where, db);
			}
		   
			public PlayerTwoCharacterHealthCollection Where(WhereDelegate<PlayerTwoCharacterHealthColumns> where, OrderBy<PlayerTwoCharacterHealthColumns> orderBy = null, Database db = null)
			{
				return PlayerTwoCharacterHealth.Where(where, orderBy, db);
			}

			public PlayerTwoCharacterHealth OneWhere(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
			{
				return PlayerTwoCharacterHealth.OneWhere(where, db);
			}
		
			public PlayerTwoCharacterHealth FirstOneWhere(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
			{
				return PlayerTwoCharacterHealth.FirstOneWhere(where, db);
			}

			public PlayerTwoCharacterHealthCollection Top(int count, WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
			{
				return PlayerTwoCharacterHealth.Top(count, where, db);
			}

			public PlayerTwoCharacterHealthCollection Top(int count, WhereDelegate<PlayerTwoCharacterHealthColumns> where, OrderBy<PlayerTwoCharacterHealthColumns> orderBy, Database db = null)
			{
				return PlayerTwoCharacterHealth.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
			{
				return PlayerTwoCharacterHealth.Count(where, db);
			}
	}

	static PlayerTwoCharacterHealthQueryContext _playerTwoCharacterHealths;
	static object _playerTwoCharacterHealthsLock = new object();
	public static PlayerTwoCharacterHealthQueryContext PlayerTwoCharacterHealths
	{
		get
		{
			return _playerTwoCharacterHealthsLock.DoubleCheckLock<PlayerTwoCharacterHealthQueryContext>(ref _playerTwoCharacterHealths, () => new PlayerTwoCharacterHealthQueryContext());
		}
	}
	public class PlayerOneCharacterHealthQueryContext
	{
			public PlayerOneCharacterHealthCollection Where(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
			{
				return PlayerOneCharacterHealth.Where(where, db);
			}
		   
			public PlayerOneCharacterHealthCollection Where(WhereDelegate<PlayerOneCharacterHealthColumns> where, OrderBy<PlayerOneCharacterHealthColumns> orderBy = null, Database db = null)
			{
				return PlayerOneCharacterHealth.Where(where, orderBy, db);
			}

			public PlayerOneCharacterHealth OneWhere(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
			{
				return PlayerOneCharacterHealth.OneWhere(where, db);
			}
		
			public PlayerOneCharacterHealth FirstOneWhere(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
			{
				return PlayerOneCharacterHealth.FirstOneWhere(where, db);
			}

			public PlayerOneCharacterHealthCollection Top(int count, WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
			{
				return PlayerOneCharacterHealth.Top(count, where, db);
			}

			public PlayerOneCharacterHealthCollection Top(int count, WhereDelegate<PlayerOneCharacterHealthColumns> where, OrderBy<PlayerOneCharacterHealthColumns> orderBy, Database db = null)
			{
				return PlayerOneCharacterHealth.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
			{
				return PlayerOneCharacterHealth.Count(where, db);
			}
	}

	static PlayerOneCharacterHealthQueryContext _playerOneCharacterHealths;
	static object _playerOneCharacterHealthsLock = new object();
	public static PlayerOneCharacterHealthQueryContext PlayerOneCharacterHealths
	{
		get
		{
			return _playerOneCharacterHealthsLock.DoubleCheckLock<PlayerOneCharacterHealthQueryContext>(ref _playerOneCharacterHealths, () => new PlayerOneCharacterHealthQueryContext());
		}
	}
	public class EffectOverTimeQueryContext
	{
			public EffectOverTimeCollection Where(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
			{
				return EffectOverTime.Where(where, db);
			}
		   
			public EffectOverTimeCollection Where(WhereDelegate<EffectOverTimeColumns> where, OrderBy<EffectOverTimeColumns> orderBy = null, Database db = null)
			{
				return EffectOverTime.Where(where, orderBy, db);
			}

			public EffectOverTime OneWhere(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
			{
				return EffectOverTime.OneWhere(where, db);
			}
		
			public EffectOverTime FirstOneWhere(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
			{
				return EffectOverTime.FirstOneWhere(where, db);
			}

			public EffectOverTimeCollection Top(int count, WhereDelegate<EffectOverTimeColumns> where, Database db = null)
			{
				return EffectOverTime.Top(count, where, db);
			}

			public EffectOverTimeCollection Top(int count, WhereDelegate<EffectOverTimeColumns> where, OrderBy<EffectOverTimeColumns> orderBy, Database db = null)
			{
				return EffectOverTime.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
			{
				return EffectOverTime.Count(where, db);
			}
	}

	static EffectOverTimeQueryContext _effectOverTimes;
	static object _effectOverTimesLock = new object();
	public static EffectOverTimeQueryContext EffectOverTimes
	{
		get
		{
			return _effectOverTimesLock.DoubleCheckLock<EffectOverTimeQueryContext>(ref _effectOverTimes, () => new EffectOverTimeQueryContext());
		}
	}
	public class WeaponQueryContext
	{
			public WeaponCollection Where(WhereDelegate<WeaponColumns> where, Database db = null)
			{
				return Weapon.Where(where, db);
			}
		   
			public WeaponCollection Where(WhereDelegate<WeaponColumns> where, OrderBy<WeaponColumns> orderBy = null, Database db = null)
			{
				return Weapon.Where(where, orderBy, db);
			}

			public Weapon OneWhere(WhereDelegate<WeaponColumns> where, Database db = null)
			{
				return Weapon.OneWhere(where, db);
			}
		
			public Weapon FirstOneWhere(WhereDelegate<WeaponColumns> where, Database db = null)
			{
				return Weapon.FirstOneWhere(where, db);
			}

			public WeaponCollection Top(int count, WhereDelegate<WeaponColumns> where, Database db = null)
			{
				return Weapon.Top(count, where, db);
			}

			public WeaponCollection Top(int count, WhereDelegate<WeaponColumns> where, OrderBy<WeaponColumns> orderBy, Database db = null)
			{
				return Weapon.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WeaponColumns> where, Database db = null)
			{
				return Weapon.Count(where, db);
			}
	}

	static WeaponQueryContext _weapons;
	static object _weaponsLock = new object();
	public static WeaponQueryContext Weapons
	{
		get
		{
			return _weaponsLock.DoubleCheckLock<WeaponQueryContext>(ref _weapons, () => new WeaponQueryContext());
		}
	}
	public class SpellQueryContext
	{
			public SpellCollection Where(WhereDelegate<SpellColumns> where, Database db = null)
			{
				return Spell.Where(where, db);
			}
		   
			public SpellCollection Where(WhereDelegate<SpellColumns> where, OrderBy<SpellColumns> orderBy = null, Database db = null)
			{
				return Spell.Where(where, orderBy, db);
			}

			public Spell OneWhere(WhereDelegate<SpellColumns> where, Database db = null)
			{
				return Spell.OneWhere(where, db);
			}
		
			public Spell FirstOneWhere(WhereDelegate<SpellColumns> where, Database db = null)
			{
				return Spell.FirstOneWhere(where, db);
			}

			public SpellCollection Top(int count, WhereDelegate<SpellColumns> where, Database db = null)
			{
				return Spell.Top(count, where, db);
			}

			public SpellCollection Top(int count, WhereDelegate<SpellColumns> where, OrderBy<SpellColumns> orderBy, Database db = null)
			{
				return Spell.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SpellColumns> where, Database db = null)
			{
				return Spell.Count(where, db);
			}
	}

	static SpellQueryContext _spells;
	static object _spellsLock = new object();
	public static SpellQueryContext Spells
	{
		get
		{
			return _spellsLock.DoubleCheckLock<SpellQueryContext>(ref _spells, () => new SpellQueryContext());
		}
	}
	public class SkillQueryContext
	{
			public SkillCollection Where(WhereDelegate<SkillColumns> where, Database db = null)
			{
				return Skill.Where(where, db);
			}
		   
			public SkillCollection Where(WhereDelegate<SkillColumns> where, OrderBy<SkillColumns> orderBy = null, Database db = null)
			{
				return Skill.Where(where, orderBy, db);
			}

			public Skill OneWhere(WhereDelegate<SkillColumns> where, Database db = null)
			{
				return Skill.OneWhere(where, db);
			}
		
			public Skill FirstOneWhere(WhereDelegate<SkillColumns> where, Database db = null)
			{
				return Skill.FirstOneWhere(where, db);
			}

			public SkillCollection Top(int count, WhereDelegate<SkillColumns> where, Database db = null)
			{
				return Skill.Top(count, where, db);
			}

			public SkillCollection Top(int count, WhereDelegate<SkillColumns> where, OrderBy<SkillColumns> orderBy, Database db = null)
			{
				return Skill.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SkillColumns> where, Database db = null)
			{
				return Skill.Count(where, db);
			}
	}

	static SkillQueryContext _skills;
	static object _skillsLock = new object();
	public static SkillQueryContext Skills
	{
		get
		{
			return _skillsLock.DoubleCheckLock<SkillQueryContext>(ref _skills, () => new SkillQueryContext());
		}
	}
	public class EquipmentQueryContext
	{
			public EquipmentCollection Where(WhereDelegate<EquipmentColumns> where, Database db = null)
			{
				return Equipment.Where(where, db);
			}
		   
			public EquipmentCollection Where(WhereDelegate<EquipmentColumns> where, OrderBy<EquipmentColumns> orderBy = null, Database db = null)
			{
				return Equipment.Where(where, orderBy, db);
			}

			public Equipment OneWhere(WhereDelegate<EquipmentColumns> where, Database db = null)
			{
				return Equipment.OneWhere(where, db);
			}
		
			public Equipment FirstOneWhere(WhereDelegate<EquipmentColumns> where, Database db = null)
			{
				return Equipment.FirstOneWhere(where, db);
			}

			public EquipmentCollection Top(int count, WhereDelegate<EquipmentColumns> where, Database db = null)
			{
				return Equipment.Top(count, where, db);
			}

			public EquipmentCollection Top(int count, WhereDelegate<EquipmentColumns> where, OrderBy<EquipmentColumns> orderBy, Database db = null)
			{
				return Equipment.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EquipmentColumns> where, Database db = null)
			{
				return Equipment.Count(where, db);
			}
	}

	static EquipmentQueryContext _equipments;
	static object _equipmentsLock = new object();
	public static EquipmentQueryContext Equipments
	{
		get
		{
			return _equipmentsLock.DoubleCheckLock<EquipmentQueryContext>(ref _equipments, () => new EquipmentQueryContext());
		}
	}
	public class EffectQueryContext
	{
			public EffectCollection Where(WhereDelegate<EffectColumns> where, Database db = null)
			{
				return Effect.Where(where, db);
			}
		   
			public EffectCollection Where(WhereDelegate<EffectColumns> where, OrderBy<EffectColumns> orderBy = null, Database db = null)
			{
				return Effect.Where(where, orderBy, db);
			}

			public Effect OneWhere(WhereDelegate<EffectColumns> where, Database db = null)
			{
				return Effect.OneWhere(where, db);
			}
		
			public Effect FirstOneWhere(WhereDelegate<EffectColumns> where, Database db = null)
			{
				return Effect.FirstOneWhere(where, db);
			}

			public EffectCollection Top(int count, WhereDelegate<EffectColumns> where, Database db = null)
			{
				return Effect.Top(count, where, db);
			}

			public EffectCollection Top(int count, WhereDelegate<EffectColumns> where, OrderBy<EffectColumns> orderBy, Database db = null)
			{
				return Effect.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<EffectColumns> where, Database db = null)
			{
				return Effect.Count(where, db);
			}
	}

	static EffectQueryContext _effects;
	static object _effectsLock = new object();
	public static EffectQueryContext Effects
	{
		get
		{
			return _effectsLock.DoubleCheckLock<EffectQueryContext>(ref _effects, () => new EffectQueryContext());
		}
	}
	public class PlayerCharacterQueryContext
	{
			public PlayerCharacterCollection Where(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
			{
				return PlayerCharacter.Where(where, db);
			}
		   
			public PlayerCharacterCollection Where(WhereDelegate<PlayerCharacterColumns> where, OrderBy<PlayerCharacterColumns> orderBy = null, Database db = null)
			{
				return PlayerCharacter.Where(where, orderBy, db);
			}

			public PlayerCharacter OneWhere(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
			{
				return PlayerCharacter.OneWhere(where, db);
			}
		
			public PlayerCharacter FirstOneWhere(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
			{
				return PlayerCharacter.FirstOneWhere(where, db);
			}

			public PlayerCharacterCollection Top(int count, WhereDelegate<PlayerCharacterColumns> where, Database db = null)
			{
				return PlayerCharacter.Top(count, where, db);
			}

			public PlayerCharacterCollection Top(int count, WhereDelegate<PlayerCharacterColumns> where, OrderBy<PlayerCharacterColumns> orderBy, Database db = null)
			{
				return PlayerCharacter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
			{
				return PlayerCharacter.Count(where, db);
			}
	}

	static PlayerCharacterQueryContext _playerCharacters;
	static object _playerCharactersLock = new object();
	public static PlayerCharacterQueryContext PlayerCharacters
	{
		get
		{
			return _playerCharactersLock.DoubleCheckLock<PlayerCharacterQueryContext>(ref _playerCharacters, () => new PlayerCharacterQueryContext());
		}
	}
	public class PlayerWeaponQueryContext
	{
			public PlayerWeaponCollection Where(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
			{
				return PlayerWeapon.Where(where, db);
			}
		   
			public PlayerWeaponCollection Where(WhereDelegate<PlayerWeaponColumns> where, OrderBy<PlayerWeaponColumns> orderBy = null, Database db = null)
			{
				return PlayerWeapon.Where(where, orderBy, db);
			}

			public PlayerWeapon OneWhere(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
			{
				return PlayerWeapon.OneWhere(where, db);
			}
		
			public PlayerWeapon FirstOneWhere(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
			{
				return PlayerWeapon.FirstOneWhere(where, db);
			}

			public PlayerWeaponCollection Top(int count, WhereDelegate<PlayerWeaponColumns> where, Database db = null)
			{
				return PlayerWeapon.Top(count, where, db);
			}

			public PlayerWeaponCollection Top(int count, WhereDelegate<PlayerWeaponColumns> where, OrderBy<PlayerWeaponColumns> orderBy, Database db = null)
			{
				return PlayerWeapon.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
			{
				return PlayerWeapon.Count(where, db);
			}
	}

	static PlayerWeaponQueryContext _playerWeapons;
	static object _playerWeaponsLock = new object();
	public static PlayerWeaponQueryContext PlayerWeapons
	{
		get
		{
			return _playerWeaponsLock.DoubleCheckLock<PlayerWeaponQueryContext>(ref _playerWeapons, () => new PlayerWeaponQueryContext());
		}
	}
	public class PlayerSpellQueryContext
	{
			public PlayerSpellCollection Where(WhereDelegate<PlayerSpellColumns> where, Database db = null)
			{
				return PlayerSpell.Where(where, db);
			}
		   
			public PlayerSpellCollection Where(WhereDelegate<PlayerSpellColumns> where, OrderBy<PlayerSpellColumns> orderBy = null, Database db = null)
			{
				return PlayerSpell.Where(where, orderBy, db);
			}

			public PlayerSpell OneWhere(WhereDelegate<PlayerSpellColumns> where, Database db = null)
			{
				return PlayerSpell.OneWhere(where, db);
			}
		
			public PlayerSpell FirstOneWhere(WhereDelegate<PlayerSpellColumns> where, Database db = null)
			{
				return PlayerSpell.FirstOneWhere(where, db);
			}

			public PlayerSpellCollection Top(int count, WhereDelegate<PlayerSpellColumns> where, Database db = null)
			{
				return PlayerSpell.Top(count, where, db);
			}

			public PlayerSpellCollection Top(int count, WhereDelegate<PlayerSpellColumns> where, OrderBy<PlayerSpellColumns> orderBy, Database db = null)
			{
				return PlayerSpell.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerSpellColumns> where, Database db = null)
			{
				return PlayerSpell.Count(where, db);
			}
	}

	static PlayerSpellQueryContext _playerSpells;
	static object _playerSpellsLock = new object();
	public static PlayerSpellQueryContext PlayerSpells
	{
		get
		{
			return _playerSpellsLock.DoubleCheckLock<PlayerSpellQueryContext>(ref _playerSpells, () => new PlayerSpellQueryContext());
		}
	}
	public class PlayerSkillQueryContext
	{
			public PlayerSkillCollection Where(WhereDelegate<PlayerSkillColumns> where, Database db = null)
			{
				return PlayerSkill.Where(where, db);
			}
		   
			public PlayerSkillCollection Where(WhereDelegate<PlayerSkillColumns> where, OrderBy<PlayerSkillColumns> orderBy = null, Database db = null)
			{
				return PlayerSkill.Where(where, orderBy, db);
			}

			public PlayerSkill OneWhere(WhereDelegate<PlayerSkillColumns> where, Database db = null)
			{
				return PlayerSkill.OneWhere(where, db);
			}
		
			public PlayerSkill FirstOneWhere(WhereDelegate<PlayerSkillColumns> where, Database db = null)
			{
				return PlayerSkill.FirstOneWhere(where, db);
			}

			public PlayerSkillCollection Top(int count, WhereDelegate<PlayerSkillColumns> where, Database db = null)
			{
				return PlayerSkill.Top(count, where, db);
			}

			public PlayerSkillCollection Top(int count, WhereDelegate<PlayerSkillColumns> where, OrderBy<PlayerSkillColumns> orderBy, Database db = null)
			{
				return PlayerSkill.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerSkillColumns> where, Database db = null)
			{
				return PlayerSkill.Count(where, db);
			}
	}

	static PlayerSkillQueryContext _playerSkills;
	static object _playerSkillsLock = new object();
	public static PlayerSkillQueryContext PlayerSkills
	{
		get
		{
			return _playerSkillsLock.DoubleCheckLock<PlayerSkillQueryContext>(ref _playerSkills, () => new PlayerSkillQueryContext());
		}
	}
	public class PlayerEquipmentQueryContext
	{
			public PlayerEquipmentCollection Where(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
			{
				return PlayerEquipment.Where(where, db);
			}
		   
			public PlayerEquipmentCollection Where(WhereDelegate<PlayerEquipmentColumns> where, OrderBy<PlayerEquipmentColumns> orderBy = null, Database db = null)
			{
				return PlayerEquipment.Where(where, orderBy, db);
			}

			public PlayerEquipment OneWhere(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
			{
				return PlayerEquipment.OneWhere(where, db);
			}
		
			public PlayerEquipment FirstOneWhere(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
			{
				return PlayerEquipment.FirstOneWhere(where, db);
			}

			public PlayerEquipmentCollection Top(int count, WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
			{
				return PlayerEquipment.Top(count, where, db);
			}

			public PlayerEquipmentCollection Top(int count, WhereDelegate<PlayerEquipmentColumns> where, OrderBy<PlayerEquipmentColumns> orderBy, Database db = null)
			{
				return PlayerEquipment.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerEquipmentColumns> where, Database db = null)
			{
				return PlayerEquipment.Count(where, db);
			}
	}

	static PlayerEquipmentQueryContext _playerEquipments;
	static object _playerEquipmentsLock = new object();
	public static PlayerEquipmentQueryContext PlayerEquipments
	{
		get
		{
			return _playerEquipmentsLock.DoubleCheckLock<PlayerEquipmentQueryContext>(ref _playerEquipments, () => new PlayerEquipmentQueryContext());
		}
	}
	public class PlayerOneCharacterQueryContext
	{
			public PlayerOneCharacterCollection Where(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
			{
				return PlayerOneCharacter.Where(where, db);
			}
		   
			public PlayerOneCharacterCollection Where(WhereDelegate<PlayerOneCharacterColumns> where, OrderBy<PlayerOneCharacterColumns> orderBy = null, Database db = null)
			{
				return PlayerOneCharacter.Where(where, orderBy, db);
			}

			public PlayerOneCharacter OneWhere(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
			{
				return PlayerOneCharacter.OneWhere(where, db);
			}
		
			public PlayerOneCharacter FirstOneWhere(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
			{
				return PlayerOneCharacter.FirstOneWhere(where, db);
			}

			public PlayerOneCharacterCollection Top(int count, WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
			{
				return PlayerOneCharacter.Top(count, where, db);
			}

			public PlayerOneCharacterCollection Top(int count, WhereDelegate<PlayerOneCharacterColumns> where, OrderBy<PlayerOneCharacterColumns> orderBy, Database db = null)
			{
				return PlayerOneCharacter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
			{
				return PlayerOneCharacter.Count(where, db);
			}
	}

	static PlayerOneCharacterQueryContext _playerOneCharacters;
	static object _playerOneCharactersLock = new object();
	public static PlayerOneCharacterQueryContext PlayerOneCharacters
	{
		get
		{
			return _playerOneCharactersLock.DoubleCheckLock<PlayerOneCharacterQueryContext>(ref _playerOneCharacters, () => new PlayerOneCharacterQueryContext());
		}
	}
	public class PlayerOneWeaponQueryContext
	{
			public PlayerOneWeaponCollection Where(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
			{
				return PlayerOneWeapon.Where(where, db);
			}
		   
			public PlayerOneWeaponCollection Where(WhereDelegate<PlayerOneWeaponColumns> where, OrderBy<PlayerOneWeaponColumns> orderBy = null, Database db = null)
			{
				return PlayerOneWeapon.Where(where, orderBy, db);
			}

			public PlayerOneWeapon OneWhere(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
			{
				return PlayerOneWeapon.OneWhere(where, db);
			}
		
			public PlayerOneWeapon FirstOneWhere(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
			{
				return PlayerOneWeapon.FirstOneWhere(where, db);
			}

			public PlayerOneWeaponCollection Top(int count, WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
			{
				return PlayerOneWeapon.Top(count, where, db);
			}

			public PlayerOneWeaponCollection Top(int count, WhereDelegate<PlayerOneWeaponColumns> where, OrderBy<PlayerOneWeaponColumns> orderBy, Database db = null)
			{
				return PlayerOneWeapon.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
			{
				return PlayerOneWeapon.Count(where, db);
			}
	}

	static PlayerOneWeaponQueryContext _playerOneWeapons;
	static object _playerOneWeaponsLock = new object();
	public static PlayerOneWeaponQueryContext PlayerOneWeapons
	{
		get
		{
			return _playerOneWeaponsLock.DoubleCheckLock<PlayerOneWeaponQueryContext>(ref _playerOneWeapons, () => new PlayerOneWeaponQueryContext());
		}
	}
	public class PlayerOneSpellQueryContext
	{
			public PlayerOneSpellCollection Where(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
			{
				return PlayerOneSpell.Where(where, db);
			}
		   
			public PlayerOneSpellCollection Where(WhereDelegate<PlayerOneSpellColumns> where, OrderBy<PlayerOneSpellColumns> orderBy = null, Database db = null)
			{
				return PlayerOneSpell.Where(where, orderBy, db);
			}

			public PlayerOneSpell OneWhere(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
			{
				return PlayerOneSpell.OneWhere(where, db);
			}
		
			public PlayerOneSpell FirstOneWhere(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
			{
				return PlayerOneSpell.FirstOneWhere(where, db);
			}

			public PlayerOneSpellCollection Top(int count, WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
			{
				return PlayerOneSpell.Top(count, where, db);
			}

			public PlayerOneSpellCollection Top(int count, WhereDelegate<PlayerOneSpellColumns> where, OrderBy<PlayerOneSpellColumns> orderBy, Database db = null)
			{
				return PlayerOneSpell.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
			{
				return PlayerOneSpell.Count(where, db);
			}
	}

	static PlayerOneSpellQueryContext _playerOneSpells;
	static object _playerOneSpellsLock = new object();
	public static PlayerOneSpellQueryContext PlayerOneSpells
	{
		get
		{
			return _playerOneSpellsLock.DoubleCheckLock<PlayerOneSpellQueryContext>(ref _playerOneSpells, () => new PlayerOneSpellQueryContext());
		}
	}
	public class PlayerOneSkillQueryContext
	{
			public PlayerOneSkillCollection Where(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
			{
				return PlayerOneSkill.Where(where, db);
			}
		   
			public PlayerOneSkillCollection Where(WhereDelegate<PlayerOneSkillColumns> where, OrderBy<PlayerOneSkillColumns> orderBy = null, Database db = null)
			{
				return PlayerOneSkill.Where(where, orderBy, db);
			}

			public PlayerOneSkill OneWhere(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
			{
				return PlayerOneSkill.OneWhere(where, db);
			}
		
			public PlayerOneSkill FirstOneWhere(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
			{
				return PlayerOneSkill.FirstOneWhere(where, db);
			}

			public PlayerOneSkillCollection Top(int count, WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
			{
				return PlayerOneSkill.Top(count, where, db);
			}

			public PlayerOneSkillCollection Top(int count, WhereDelegate<PlayerOneSkillColumns> where, OrderBy<PlayerOneSkillColumns> orderBy, Database db = null)
			{
				return PlayerOneSkill.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
			{
				return PlayerOneSkill.Count(where, db);
			}
	}

	static PlayerOneSkillQueryContext _playerOneSkills;
	static object _playerOneSkillsLock = new object();
	public static PlayerOneSkillQueryContext PlayerOneSkills
	{
		get
		{
			return _playerOneSkillsLock.DoubleCheckLock<PlayerOneSkillQueryContext>(ref _playerOneSkills, () => new PlayerOneSkillQueryContext());
		}
	}
	public class PlayerOneEquipmentQueryContext
	{
			public PlayerOneEquipmentCollection Where(WhereDelegate<PlayerOneEquipmentColumns> where, Database db = null)
			{
				return PlayerOneEquipment.Where(where, db);
			}
		   
			public PlayerOneEquipmentCollection Where(WhereDelegate<PlayerOneEquipmentColumns> where, OrderBy<PlayerOneEquipmentColumns> orderBy = null, Database db = null)
			{
				return PlayerOneEquipment.Where(where, orderBy, db);
			}

			public PlayerOneEquipment OneWhere(WhereDelegate<PlayerOneEquipmentColumns> where, Database db = null)
			{
				return PlayerOneEquipment.OneWhere(where, db);
			}
		
			public PlayerOneEquipment FirstOneWhere(WhereDelegate<PlayerOneEquipmentColumns> where, Database db = null)
			{
				return PlayerOneEquipment.FirstOneWhere(where, db);
			}

			public PlayerOneEquipmentCollection Top(int count, WhereDelegate<PlayerOneEquipmentColumns> where, Database db = null)
			{
				return PlayerOneEquipment.Top(count, where, db);
			}

			public PlayerOneEquipmentCollection Top(int count, WhereDelegate<PlayerOneEquipmentColumns> where, OrderBy<PlayerOneEquipmentColumns> orderBy, Database db = null)
			{
				return PlayerOneEquipment.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerOneEquipmentColumns> where, Database db = null)
			{
				return PlayerOneEquipment.Count(where, db);
			}
	}

	static PlayerOneEquipmentQueryContext _playerOneEquipments;
	static object _playerOneEquipmentsLock = new object();
	public static PlayerOneEquipmentQueryContext PlayerOneEquipments
	{
		get
		{
			return _playerOneEquipmentsLock.DoubleCheckLock<PlayerOneEquipmentQueryContext>(ref _playerOneEquipments, () => new PlayerOneEquipmentQueryContext());
		}
	}
	public class PlayerTwoCharacterQueryContext
	{
			public PlayerTwoCharacterCollection Where(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
			{
				return PlayerTwoCharacter.Where(where, db);
			}
		   
			public PlayerTwoCharacterCollection Where(WhereDelegate<PlayerTwoCharacterColumns> where, OrderBy<PlayerTwoCharacterColumns> orderBy = null, Database db = null)
			{
				return PlayerTwoCharacter.Where(where, orderBy, db);
			}

			public PlayerTwoCharacter OneWhere(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
			{
				return PlayerTwoCharacter.OneWhere(where, db);
			}
		
			public PlayerTwoCharacter FirstOneWhere(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
			{
				return PlayerTwoCharacter.FirstOneWhere(where, db);
			}

			public PlayerTwoCharacterCollection Top(int count, WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
			{
				return PlayerTwoCharacter.Top(count, where, db);
			}

			public PlayerTwoCharacterCollection Top(int count, WhereDelegate<PlayerTwoCharacterColumns> where, OrderBy<PlayerTwoCharacterColumns> orderBy, Database db = null)
			{
				return PlayerTwoCharacter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
			{
				return PlayerTwoCharacter.Count(where, db);
			}
	}

	static PlayerTwoCharacterQueryContext _playerTwoCharacters;
	static object _playerTwoCharactersLock = new object();
	public static PlayerTwoCharacterQueryContext PlayerTwoCharacters
	{
		get
		{
			return _playerTwoCharactersLock.DoubleCheckLock<PlayerTwoCharacterQueryContext>(ref _playerTwoCharacters, () => new PlayerTwoCharacterQueryContext());
		}
	}
	public class PlayerTwoWeaponQueryContext
	{
			public PlayerTwoWeaponCollection Where(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
			{
				return PlayerTwoWeapon.Where(where, db);
			}
		   
			public PlayerTwoWeaponCollection Where(WhereDelegate<PlayerTwoWeaponColumns> where, OrderBy<PlayerTwoWeaponColumns> orderBy = null, Database db = null)
			{
				return PlayerTwoWeapon.Where(where, orderBy, db);
			}

			public PlayerTwoWeapon OneWhere(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
			{
				return PlayerTwoWeapon.OneWhere(where, db);
			}
		
			public PlayerTwoWeapon FirstOneWhere(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
			{
				return PlayerTwoWeapon.FirstOneWhere(where, db);
			}

			public PlayerTwoWeaponCollection Top(int count, WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
			{
				return PlayerTwoWeapon.Top(count, where, db);
			}

			public PlayerTwoWeaponCollection Top(int count, WhereDelegate<PlayerTwoWeaponColumns> where, OrderBy<PlayerTwoWeaponColumns> orderBy, Database db = null)
			{
				return PlayerTwoWeapon.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
			{
				return PlayerTwoWeapon.Count(where, db);
			}
	}

	static PlayerTwoWeaponQueryContext _playerTwoWeapons;
	static object _playerTwoWeaponsLock = new object();
	public static PlayerTwoWeaponQueryContext PlayerTwoWeapons
	{
		get
		{
			return _playerTwoWeaponsLock.DoubleCheckLock<PlayerTwoWeaponQueryContext>(ref _playerTwoWeapons, () => new PlayerTwoWeaponQueryContext());
		}
	}
	public class PlayerTwoSpellQueryContext
	{
			public PlayerTwoSpellCollection Where(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
			{
				return PlayerTwoSpell.Where(where, db);
			}
		   
			public PlayerTwoSpellCollection Where(WhereDelegate<PlayerTwoSpellColumns> where, OrderBy<PlayerTwoSpellColumns> orderBy = null, Database db = null)
			{
				return PlayerTwoSpell.Where(where, orderBy, db);
			}

			public PlayerTwoSpell OneWhere(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
			{
				return PlayerTwoSpell.OneWhere(where, db);
			}
		
			public PlayerTwoSpell FirstOneWhere(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
			{
				return PlayerTwoSpell.FirstOneWhere(where, db);
			}

			public PlayerTwoSpellCollection Top(int count, WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
			{
				return PlayerTwoSpell.Top(count, where, db);
			}

			public PlayerTwoSpellCollection Top(int count, WhereDelegate<PlayerTwoSpellColumns> where, OrderBy<PlayerTwoSpellColumns> orderBy, Database db = null)
			{
				return PlayerTwoSpell.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
			{
				return PlayerTwoSpell.Count(where, db);
			}
	}

	static PlayerTwoSpellQueryContext _playerTwoSpells;
	static object _playerTwoSpellsLock = new object();
	public static PlayerTwoSpellQueryContext PlayerTwoSpells
	{
		get
		{
			return _playerTwoSpellsLock.DoubleCheckLock<PlayerTwoSpellQueryContext>(ref _playerTwoSpells, () => new PlayerTwoSpellQueryContext());
		}
	}
	public class PlayerTwoSkillQueryContext
	{
			public PlayerTwoSkillCollection Where(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
			{
				return PlayerTwoSkill.Where(where, db);
			}
		   
			public PlayerTwoSkillCollection Where(WhereDelegate<PlayerTwoSkillColumns> where, OrderBy<PlayerTwoSkillColumns> orderBy = null, Database db = null)
			{
				return PlayerTwoSkill.Where(where, orderBy, db);
			}

			public PlayerTwoSkill OneWhere(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
			{
				return PlayerTwoSkill.OneWhere(where, db);
			}
		
			public PlayerTwoSkill FirstOneWhere(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
			{
				return PlayerTwoSkill.FirstOneWhere(where, db);
			}

			public PlayerTwoSkillCollection Top(int count, WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
			{
				return PlayerTwoSkill.Top(count, where, db);
			}

			public PlayerTwoSkillCollection Top(int count, WhereDelegate<PlayerTwoSkillColumns> where, OrderBy<PlayerTwoSkillColumns> orderBy, Database db = null)
			{
				return PlayerTwoSkill.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
			{
				return PlayerTwoSkill.Count(where, db);
			}
	}

	static PlayerTwoSkillQueryContext _playerTwoSkills;
	static object _playerTwoSkillsLock = new object();
	public static PlayerTwoSkillQueryContext PlayerTwoSkills
	{
		get
		{
			return _playerTwoSkillsLock.DoubleCheckLock<PlayerTwoSkillQueryContext>(ref _playerTwoSkills, () => new PlayerTwoSkillQueryContext());
		}
	}
	public class PlayerTwoEquipmentQueryContext
	{
			public PlayerTwoEquipmentCollection Where(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
			{
				return PlayerTwoEquipment.Where(where, db);
			}
		   
			public PlayerTwoEquipmentCollection Where(WhereDelegate<PlayerTwoEquipmentColumns> where, OrderBy<PlayerTwoEquipmentColumns> orderBy = null, Database db = null)
			{
				return PlayerTwoEquipment.Where(where, orderBy, db);
			}

			public PlayerTwoEquipment OneWhere(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
			{
				return PlayerTwoEquipment.OneWhere(where, db);
			}
		
			public PlayerTwoEquipment FirstOneWhere(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
			{
				return PlayerTwoEquipment.FirstOneWhere(where, db);
			}

			public PlayerTwoEquipmentCollection Top(int count, WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
			{
				return PlayerTwoEquipment.Top(count, where, db);
			}

			public PlayerTwoEquipmentCollection Top(int count, WhereDelegate<PlayerTwoEquipmentColumns> where, OrderBy<PlayerTwoEquipmentColumns> orderBy, Database db = null)
			{
				return PlayerTwoEquipment.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PlayerTwoEquipmentColumns> where, Database db = null)
			{
				return PlayerTwoEquipment.Count(where, db);
			}
	}

	static PlayerTwoEquipmentQueryContext _playerTwoEquipments;
	static object _playerTwoEquipmentsLock = new object();
	public static PlayerTwoEquipmentQueryContext PlayerTwoEquipments
	{
		get
		{
			return _playerTwoEquipmentsLock.DoubleCheckLock<PlayerTwoEquipmentQueryContext>(ref _playerTwoEquipments, () => new PlayerTwoEquipmentQueryContext());
		}
	}
	public class RequiredLevelCharacterQueryContext
	{
			public RequiredLevelCharacterCollection Where(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
			{
				return RequiredLevelCharacter.Where(where, db);
			}
		   
			public RequiredLevelCharacterCollection Where(WhereDelegate<RequiredLevelCharacterColumns> where, OrderBy<RequiredLevelCharacterColumns> orderBy = null, Database db = null)
			{
				return RequiredLevelCharacter.Where(where, orderBy, db);
			}

			public RequiredLevelCharacter OneWhere(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
			{
				return RequiredLevelCharacter.OneWhere(where, db);
			}
		
			public RequiredLevelCharacter FirstOneWhere(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
			{
				return RequiredLevelCharacter.FirstOneWhere(where, db);
			}

			public RequiredLevelCharacterCollection Top(int count, WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
			{
				return RequiredLevelCharacter.Top(count, where, db);
			}

			public RequiredLevelCharacterCollection Top(int count, WhereDelegate<RequiredLevelCharacterColumns> where, OrderBy<RequiredLevelCharacterColumns> orderBy, Database db = null)
			{
				return RequiredLevelCharacter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
			{
				return RequiredLevelCharacter.Count(where, db);
			}
	}

	static RequiredLevelCharacterQueryContext _requiredLevelCharacters;
	static object _requiredLevelCharactersLock = new object();
	public static RequiredLevelCharacterQueryContext RequiredLevelCharacters
	{
		get
		{
			return _requiredLevelCharactersLock.DoubleCheckLock<RequiredLevelCharacterQueryContext>(ref _requiredLevelCharacters, () => new RequiredLevelCharacterQueryContext());
		}
	}
	public class RequiredLevelWeaponQueryContext
	{
			public RequiredLevelWeaponCollection Where(WhereDelegate<RequiredLevelWeaponColumns> where, Database db = null)
			{
				return RequiredLevelWeapon.Where(where, db);
			}
		   
			public RequiredLevelWeaponCollection Where(WhereDelegate<RequiredLevelWeaponColumns> where, OrderBy<RequiredLevelWeaponColumns> orderBy = null, Database db = null)
			{
				return RequiredLevelWeapon.Where(where, orderBy, db);
			}

			public RequiredLevelWeapon OneWhere(WhereDelegate<RequiredLevelWeaponColumns> where, Database db = null)
			{
				return RequiredLevelWeapon.OneWhere(where, db);
			}
		
			public RequiredLevelWeapon FirstOneWhere(WhereDelegate<RequiredLevelWeaponColumns> where, Database db = null)
			{
				return RequiredLevelWeapon.FirstOneWhere(where, db);
			}

			public RequiredLevelWeaponCollection Top(int count, WhereDelegate<RequiredLevelWeaponColumns> where, Database db = null)
			{
				return RequiredLevelWeapon.Top(count, where, db);
			}

			public RequiredLevelWeaponCollection Top(int count, WhereDelegate<RequiredLevelWeaponColumns> where, OrderBy<RequiredLevelWeaponColumns> orderBy, Database db = null)
			{
				return RequiredLevelWeapon.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RequiredLevelWeaponColumns> where, Database db = null)
			{
				return RequiredLevelWeapon.Count(where, db);
			}
	}

	static RequiredLevelWeaponQueryContext _requiredLevelWeapons;
	static object _requiredLevelWeaponsLock = new object();
	public static RequiredLevelWeaponQueryContext RequiredLevelWeapons
	{
		get
		{
			return _requiredLevelWeaponsLock.DoubleCheckLock<RequiredLevelWeaponQueryContext>(ref _requiredLevelWeapons, () => new RequiredLevelWeaponQueryContext());
		}
	}
	public class RequiredLevelSpellQueryContext
	{
			public RequiredLevelSpellCollection Where(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
			{
				return RequiredLevelSpell.Where(where, db);
			}
		   
			public RequiredLevelSpellCollection Where(WhereDelegate<RequiredLevelSpellColumns> where, OrderBy<RequiredLevelSpellColumns> orderBy = null, Database db = null)
			{
				return RequiredLevelSpell.Where(where, orderBy, db);
			}

			public RequiredLevelSpell OneWhere(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
			{
				return RequiredLevelSpell.OneWhere(where, db);
			}
		
			public RequiredLevelSpell FirstOneWhere(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
			{
				return RequiredLevelSpell.FirstOneWhere(where, db);
			}

			public RequiredLevelSpellCollection Top(int count, WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
			{
				return RequiredLevelSpell.Top(count, where, db);
			}

			public RequiredLevelSpellCollection Top(int count, WhereDelegate<RequiredLevelSpellColumns> where, OrderBy<RequiredLevelSpellColumns> orderBy, Database db = null)
			{
				return RequiredLevelSpell.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
			{
				return RequiredLevelSpell.Count(where, db);
			}
	}

	static RequiredLevelSpellQueryContext _requiredLevelSpells;
	static object _requiredLevelSpellsLock = new object();
	public static RequiredLevelSpellQueryContext RequiredLevelSpells
	{
		get
		{
			return _requiredLevelSpellsLock.DoubleCheckLock<RequiredLevelSpellQueryContext>(ref _requiredLevelSpells, () => new RequiredLevelSpellQueryContext());
		}
	}
	public class RequiredLevelSkillQueryContext
	{
			public RequiredLevelSkillCollection Where(WhereDelegate<RequiredLevelSkillColumns> where, Database db = null)
			{
				return RequiredLevelSkill.Where(where, db);
			}
		   
			public RequiredLevelSkillCollection Where(WhereDelegate<RequiredLevelSkillColumns> where, OrderBy<RequiredLevelSkillColumns> orderBy = null, Database db = null)
			{
				return RequiredLevelSkill.Where(where, orderBy, db);
			}

			public RequiredLevelSkill OneWhere(WhereDelegate<RequiredLevelSkillColumns> where, Database db = null)
			{
				return RequiredLevelSkill.OneWhere(where, db);
			}
		
			public RequiredLevelSkill FirstOneWhere(WhereDelegate<RequiredLevelSkillColumns> where, Database db = null)
			{
				return RequiredLevelSkill.FirstOneWhere(where, db);
			}

			public RequiredLevelSkillCollection Top(int count, WhereDelegate<RequiredLevelSkillColumns> where, Database db = null)
			{
				return RequiredLevelSkill.Top(count, where, db);
			}

			public RequiredLevelSkillCollection Top(int count, WhereDelegate<RequiredLevelSkillColumns> where, OrderBy<RequiredLevelSkillColumns> orderBy, Database db = null)
			{
				return RequiredLevelSkill.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RequiredLevelSkillColumns> where, Database db = null)
			{
				return RequiredLevelSkill.Count(where, db);
			}
	}

	static RequiredLevelSkillQueryContext _requiredLevelSkills;
	static object _requiredLevelSkillsLock = new object();
	public static RequiredLevelSkillQueryContext RequiredLevelSkills
	{
		get
		{
			return _requiredLevelSkillsLock.DoubleCheckLock<RequiredLevelSkillQueryContext>(ref _requiredLevelSkills, () => new RequiredLevelSkillQueryContext());
		}
	}    }
}																								
