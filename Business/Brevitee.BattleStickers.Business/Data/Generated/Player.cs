// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.BattleStickers.Business.Data
{
	// schema = BattleStickers
	// connection Name = BattleStickers
	[Brevitee.Data.Table("Player", "BattleStickers")]
	public partial class Player: Dao
	{
		public Player():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Player(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Player(DataRow data)
		{
			return new Player(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Battle_RockPaperScissorsWinnerId", new BattleCollection(new Query<BattleColumns, Battle>((c) => c.RockPaperScissorsWinnerId == this.Id), this, "RockPaperScissorsWinnerId"));	
            this.ChildCollections.Add("PlayerOne_PlayerId", new PlayerOneCollection(new Query<PlayerOneColumns, PlayerOne>((c) => c.PlayerId == this.Id), this, "PlayerId"));	
            this.ChildCollections.Add("PlayerTwo_PlayerId", new PlayerTwoCollection(new Query<PlayerTwoColumns, PlayerTwo>((c) => c.PlayerId == this.Id), this, "PlayerId"));	
            this.ChildCollections.Add("PlayerCharacter_PlayerId", new PlayerCharacterCollection(new Query<PlayerCharacterColumns, PlayerCharacter>((c) => c.PlayerId == this.Id), this, "PlayerId"));	
            this.ChildCollections.Add("PlayerWeapon_PlayerId", new PlayerWeaponCollection(new Query<PlayerWeaponColumns, PlayerWeapon>((c) => c.PlayerId == this.Id), this, "PlayerId"));	
            this.ChildCollections.Add("PlayerSpell_PlayerId", new PlayerSpellCollection(new Query<PlayerSpellColumns, PlayerSpell>((c) => c.PlayerId == this.Id), this, "PlayerId"));	
            this.ChildCollections.Add("PlayerSkill_PlayerId", new PlayerSkillCollection(new Query<PlayerSkillColumns, PlayerSkill>((c) => c.PlayerId == this.Id), this, "PlayerId"));	
            this.ChildCollections.Add("PlayerEquipment_PlayerId", new PlayerEquipmentCollection(new Query<PlayerEquipmentColumns, PlayerEquipment>((c) => c.PlayerId == this.Id), this, "PlayerId"));				
            this.ChildCollections.Add("Player_PlayerCharacter_Character",  new XrefDaoCollection<PlayerCharacter, Character>(this, false));
				
            this.ChildCollections.Add("Player_PlayerWeapon_Weapon",  new XrefDaoCollection<PlayerWeapon, Weapon>(this, false));
				
            this.ChildCollections.Add("Player_PlayerSpell_Spell",  new XrefDaoCollection<PlayerSpell, Spell>(this, false));
				
            this.ChildCollections.Add("Player_PlayerSkill_Skill",  new XrefDaoCollection<PlayerSkill, Skill>(this, false));
				
            this.ChildCollections.Add("Player_PlayerEquipment_Equipment",  new XrefDaoCollection<PlayerEquipment, Equipment>(this, false));
							
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="8")]
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

	// property:Uuid, columnName:Uuid	
	[Brevitee.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}

	// property:Level, columnName:Level	
	[Brevitee.Data.Column(Name="Level", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? Level
	{
		get
		{
			return GetIntValue("Level");
		}
		set
		{
			SetValue("Level", value);
		}
	}

	// property:ExperiencePoints, columnName:ExperiencePoints	
	[Brevitee.Data.Column(Name="ExperiencePoints", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? ExperiencePoints
	{
		get
		{
			return GetIntValue("ExperiencePoints");
		}
		set
		{
			SetValue("ExperiencePoints", value);
		}
	}



				

	[Exclude]	
	public BattleCollection BattlesByRockPaperScissorsWinnerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Battle_RockPaperScissorsWinnerId"))
			{
				SetChildren();
			}

			var c = (BattleCollection)this.ChildCollections["Battle_RockPaperScissorsWinnerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneCollection PlayerOnesByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOne_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerOneCollection)this.ChildCollections["PlayerOne_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerTwoCollection PlayerTwosByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwo_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoCollection)this.ChildCollections["PlayerTwo_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerCharacterCollection PlayerCharactersByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerCharacter_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerCharacterCollection)this.ChildCollections["PlayerCharacter_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerWeaponCollection PlayerWeaponsByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerWeapon_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerWeaponCollection)this.ChildCollections["PlayerWeapon_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerSpellCollection PlayerSpellsByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerSpell_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerSpellCollection)this.ChildCollections["PlayerSpell_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerSkillCollection PlayerSkillsByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerSkill_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerSkillCollection)this.ChildCollections["PlayerSkill_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerEquipmentCollection PlayerEquipmentsByPlayerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerEquipment_PlayerId"))
			{
				SetChildren();
			}

			var c = (PlayerEquipmentCollection)this.ChildCollections["PlayerEquipment_PlayerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<PlayerCharacter, Character> Characters
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Player_PlayerCharacter_Character"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerCharacter, Character>)this.ChildCollections["Player_PlayerCharacter_Character"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerWeapon, Weapon> Weapons
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Player_PlayerWeapon_Weapon"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerWeapon, Weapon>)this.ChildCollections["Player_PlayerWeapon_Weapon"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerSpell, Spell> Spells
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Player_PlayerSpell_Spell"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerSpell, Spell>)this.ChildCollections["Player_PlayerSpell_Spell"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerSkill, Skill> Skills
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Player_PlayerSkill_Skill"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerSkill, Skill>)this.ChildCollections["Player_PlayerSkill_Skill"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerEquipment, Equipment> Equipments
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Player_PlayerEquipment_Equipment"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerEquipment, Equipment>)this.ChildCollections["Player_PlayerEquipment_Equipment"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Player table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Player>();
			Database db = database == null ? Db.For<Player>(): database;
			var results = new PlayerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCollection Where(Func<PlayerColumns, QueryFilter<PlayerColumns>> where, OrderBy<PlayerColumns> orderBy = null)
		{
			return new PlayerCollection(new Query<PlayerColumns, Player>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCollection Where(WhereDelegate<PlayerColumns> where, Database db = null)
		{
			var results = new PlayerCollection(db, new Query<PlayerColumns, Player>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerCollection Where(WhereDelegate<PlayerColumns> where, OrderBy<PlayerColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerCollection(db, new Query<PlayerColumns, Player>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerCollection(db, Select<PlayerColumns>.From<Player>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Player instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Player OneWhere(WhereDelegate<PlayerColumns> where, Database db = null)
		{
			var results = new PlayerCollection(db, Select<PlayerColumns>.From<Player>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Player OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerCollection(db, Select<PlayerColumns>.From<Player>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Player OneOrThrow(PlayerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Player FirstOneWhere(WhereDelegate<PlayerColumns> where, Database db = null)
		{
			var results = new PlayerCollection(db, Select<PlayerColumns>.From<Player>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCollection Top(int count, WhereDelegate<PlayerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerCollection Top(int count, WhereDelegate<PlayerColumns> where, OrderBy<PlayerColumns> orderBy, Database database = null)
		{
			PlayerColumns c = new PlayerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Player>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Player>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerColumns> where, Database database = null)
		{
			PlayerColumns c = new PlayerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Player>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Player>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
