// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.BattleStickers
{
	// schema = BattleStickers
	// connection Name = BattleStickers
	[Brevitee.Data.Table("PlayerTwo", "BattleStickers")]
	public partial class PlayerTwo: Dao
	{
		public PlayerTwo():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwo(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwo(DataRow data)
		{
			return new PlayerTwo(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("PlayerTwoCharacterHealth_PlayerTwoId", new PlayerTwoCharacterHealthCollection(new Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth>((c) => c.PlayerTwoId == this.Id), this, "PlayerTwoId"));	
            this.ChildCollections.Add("PlayerTwoCharacter_PlayerTwoId", new PlayerTwoCharacterCollection(new Query<PlayerTwoCharacterColumns, PlayerTwoCharacter>((c) => c.PlayerTwoId == this.Id), this, "PlayerTwoId"));	
            this.ChildCollections.Add("PlayerTwoWeapon_PlayerTwoId", new PlayerTwoWeaponCollection(new Query<PlayerTwoWeaponColumns, PlayerTwoWeapon>((c) => c.PlayerTwoId == this.Id), this, "PlayerTwoId"));	
            this.ChildCollections.Add("PlayerTwoSpell_PlayerTwoId", new PlayerTwoSpellCollection(new Query<PlayerTwoSpellColumns, PlayerTwoSpell>((c) => c.PlayerTwoId == this.Id), this, "PlayerTwoId"));	
            this.ChildCollections.Add("PlayerTwoSkill_PlayerTwoId", new PlayerTwoSkillCollection(new Query<PlayerTwoSkillColumns, PlayerTwoSkill>((c) => c.PlayerTwoId == this.Id), this, "PlayerTwoId"));	
            this.ChildCollections.Add("PlayerTwoEquipment_PlayerTwoId", new PlayerTwoEquipmentCollection(new Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment>((c) => c.PlayerTwoId == this.Id), this, "PlayerTwoId"));				
            this.ChildCollections.Add("PlayerTwo_PlayerTwoCharacter_Character",  new XrefDaoCollection<PlayerTwoCharacter, Character>(this, false));
				
            this.ChildCollections.Add("PlayerTwo_PlayerTwoWeapon_Weapon",  new XrefDaoCollection<PlayerTwoWeapon, Weapon>(this, false));
				
            this.ChildCollections.Add("PlayerTwo_PlayerTwoSpell_Spell",  new XrefDaoCollection<PlayerTwoSpell, Spell>(this, false));
				
            this.ChildCollections.Add("PlayerTwo_PlayerTwoSkill_Skill",  new XrefDaoCollection<PlayerTwoSkill, Skill>(this, false));
				
            this.ChildCollections.Add("PlayerTwo_PlayerTwoEquipment_Equipment",  new XrefDaoCollection<PlayerTwoEquipment, Equipment>(this, false));
							
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="", MaxLength="")]
	public long? Id
	{
		get
		{
			return GetLongValue("Id");
		}
		set
		{
			SetValue("Id", value);
		}
	}



	// start BattleId -> BattleId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwo",
		Name="BattleId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Battle",
		Suffix="1")]
	public long? BattleId
	{
		get
		{
			return GetLongValue("BattleId");
		}
		set
		{
			SetValue("BattleId", value);
		}
	}

	Battle _battleOfBattleId;
	public Battle BattleOfBattleId
	{
		get
		{
			if(_battleOfBattleId == null)
			{
				_battleOfBattleId = Brevitee.BattleStickers.Battle.OneWhere(f => f.Id == this.BattleId);
			}
			return _battleOfBattleId;
		}
	}
	
	// start PlayerId -> PlayerId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwo",
		Name="PlayerId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Player",
		Suffix="2")]
	public long? PlayerId
	{
		get
		{
			return GetLongValue("PlayerId");
		}
		set
		{
			SetValue("PlayerId", value);
		}
	}

	Player _playerOfPlayerId;
	public Player PlayerOfPlayerId
	{
		get
		{
			if(_playerOfPlayerId == null)
			{
				_playerOfPlayerId = Brevitee.BattleStickers.Player.OneWhere(f => f.Id == this.PlayerId);
			}
			return _playerOfPlayerId;
		}
	}
	
				
	[Exclude]	
	public PlayerTwoCharacterHealthCollection PlayerTwoCharacterHealthsByPlayerTwoId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoCharacterHealth_PlayerTwoId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoCharacterHealthCollection)this.ChildCollections["PlayerTwoCharacterHealth_PlayerTwoId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoCharacterCollection PlayerTwoCharactersByPlayerTwoId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoCharacter_PlayerTwoId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoCharacterCollection)this.ChildCollections["PlayerTwoCharacter_PlayerTwoId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoWeaponCollection PlayerTwoWeaponsByPlayerTwoId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoWeapon_PlayerTwoId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoWeaponCollection)this.ChildCollections["PlayerTwoWeapon_PlayerTwoId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoSpellCollection PlayerTwoSpellsByPlayerTwoId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoSpell_PlayerTwoId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoSpellCollection)this.ChildCollections["PlayerTwoSpell_PlayerTwoId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoSkillCollection PlayerTwoSkillsByPlayerTwoId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoSkill_PlayerTwoId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoSkillCollection)this.ChildCollections["PlayerTwoSkill_PlayerTwoId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoEquipmentCollection PlayerTwoEquipmentsByPlayerTwoId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoEquipment_PlayerTwoId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoEquipmentCollection)this.ChildCollections["PlayerTwoEquipment_PlayerTwoId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<PlayerTwoCharacter, Character> Characters
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerTwo_PlayerTwoCharacter_Character"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoCharacter, Character>)this.ChildCollections["PlayerTwo_PlayerTwoCharacter_Character"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoWeapon, Weapon> Weapons
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerTwo_PlayerTwoWeapon_Weapon"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoWeapon, Weapon>)this.ChildCollections["PlayerTwo_PlayerTwoWeapon_Weapon"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoSpell, Spell> Spells
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerTwo_PlayerTwoSpell_Spell"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoSpell, Spell>)this.ChildCollections["PlayerTwo_PlayerTwoSpell_Spell"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoSkill, Skill> Skills
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerTwo_PlayerTwoSkill_Skill"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoSkill, Skill>)this.ChildCollections["PlayerTwo_PlayerTwoSkill_Skill"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoEquipment, Equipment> Equipments
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerTwo_PlayerTwoEquipment_Equipment"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoEquipment, Equipment>)this.ChildCollections["PlayerTwo_PlayerTwoEquipment_Equipment"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }

		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerTwoColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCollection Where(Func<PlayerTwoColumns, QueryFilter<PlayerTwoColumns>> where, OrderBy<PlayerTwoColumns> orderBy = null)
		{
			return new PlayerTwoCollection(new Query<PlayerTwoColumns, PlayerTwo>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCollection Where(WhereDelegate<PlayerTwoColumns> where, Database db = null)
		{
			return new PlayerTwoCollection(new Query<PlayerTwoColumns, PlayerTwo>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCollection Where(WhereDelegate<PlayerTwoColumns> where, OrderBy<PlayerTwoColumns> orderBy = null, Database db = null)
		{
			return new PlayerTwoCollection(new Query<PlayerTwoColumns, PlayerTwo>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerTwoCollection(Select<PlayerTwoColumns>.From<PlayerTwo>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwo instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwo OneWhere(WhereDelegate<PlayerTwoColumns> where, Database db = null)
		{
			var results = new PlayerTwoCollection(Select<PlayerTwoColumns>.From<PlayerTwo>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwo OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoCollection(Select<PlayerTwoColumns>.From<PlayerTwo>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwo OneOrThrow(PlayerTwoCollection c)
		{
			if(c.Count == 1)
			{
				return c[0];
			}
			else if(c.Count > 1)
			{
				throw new MultipleEntriesFoundException();
			}

			return null;
		}

		/// <summary>
		/// Execute a query and return the first result
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwo FirstOneWhere(WhereDelegate<PlayerTwoColumns> where, Database db = null)
		{
			var results = new PlayerTwoCollection(Select<PlayerTwoColumns>.From<PlayerTwo>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Execute a query and return the specified number
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCollection Top(int count, WhereDelegate<PlayerTwoColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		/// <summary>
		/// Execute a query and return the specified count
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCollection Top(int count, WhereDelegate<PlayerTwoColumns> where, OrderBy<PlayerTwoColumns> orderBy, Database database = null)
		{
			PlayerTwoColumns c = new PlayerTwoColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerTwo>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwo>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerTwoCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoColumns> where, Database database = null)
		{
			PlayerTwoColumns c = new PlayerTwoColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerTwo>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwo>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
