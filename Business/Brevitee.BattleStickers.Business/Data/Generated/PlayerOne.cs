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
	[Brevitee.Data.Table("PlayerOne", "BattleStickers")]
	public partial class PlayerOne: Dao
	{
		public PlayerOne():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerOne(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerOne(DataRow data)
		{
			return new PlayerOne(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("PlayerOneCharacterHealth_PlayerOneId", new PlayerOneCharacterHealthCollection(new Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth>((c) => c.PlayerOneId == this.Id), this, "PlayerOneId"));	
            this.ChildCollections.Add("PlayerOneCharacter_PlayerOneId", new PlayerOneCharacterCollection(new Query<PlayerOneCharacterColumns, PlayerOneCharacter>((c) => c.PlayerOneId == this.Id), this, "PlayerOneId"));	
            this.ChildCollections.Add("PlayerOneWeapon_PlayerOneId", new PlayerOneWeaponCollection(new Query<PlayerOneWeaponColumns, PlayerOneWeapon>((c) => c.PlayerOneId == this.Id), this, "PlayerOneId"));	
            this.ChildCollections.Add("PlayerOneSpell_PlayerOneId", new PlayerOneSpellCollection(new Query<PlayerOneSpellColumns, PlayerOneSpell>((c) => c.PlayerOneId == this.Id), this, "PlayerOneId"));	
            this.ChildCollections.Add("PlayerOneSkill_PlayerOneId", new PlayerOneSkillCollection(new Query<PlayerOneSkillColumns, PlayerOneSkill>((c) => c.PlayerOneId == this.Id), this, "PlayerOneId"));	
            this.ChildCollections.Add("PlayerOneEquipment_PlayerOneId", new PlayerOneEquipmentCollection(new Query<PlayerOneEquipmentColumns, PlayerOneEquipment>((c) => c.PlayerOneId == this.Id), this, "PlayerOneId"));				
            this.ChildCollections.Add("PlayerOne_PlayerOneCharacter_Character",  new XrefDaoCollection<PlayerOneCharacter, Character>(this, false));
				
            this.ChildCollections.Add("PlayerOne_PlayerOneWeapon_Weapon",  new XrefDaoCollection<PlayerOneWeapon, Weapon>(this, false));
				
            this.ChildCollections.Add("PlayerOne_PlayerOneSpell_Spell",  new XrefDaoCollection<PlayerOneSpell, Spell>(this, false));
				
            this.ChildCollections.Add("PlayerOne_PlayerOneSkill_Skill",  new XrefDaoCollection<PlayerOneSkill, Skill>(this, false));
				
            this.ChildCollections.Add("PlayerOne_PlayerOneEquipment_Equipment",  new XrefDaoCollection<PlayerOneEquipment, Equipment>(this, false));
							
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



	// start BattleId -> BattleId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOne",
		Name="BattleId", 
		DbDataType="BigInt", 
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
				_battleOfBattleId = Brevitee.BattleStickers.Business.Data.Battle.OneWhere(f => f.Id == this.BattleId);
			}
			return _battleOfBattleId;
		}
	}
	
	// start PlayerId -> PlayerId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOne",
		Name="PlayerId", 
		DbDataType="BigInt", 
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
				_playerOfPlayerId = Brevitee.BattleStickers.Business.Data.Player.OneWhere(f => f.Id == this.PlayerId);
			}
			return _playerOfPlayerId;
		}
	}
	
				

	[Exclude]	
	public PlayerOneCharacterHealthCollection PlayerOneCharacterHealthsByPlayerOneId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneCharacterHealth_PlayerOneId"))
			{
				SetChildren();
			}

			var c = (PlayerOneCharacterHealthCollection)this.ChildCollections["PlayerOneCharacterHealth_PlayerOneId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneCharacterCollection PlayerOneCharactersByPlayerOneId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneCharacter_PlayerOneId"))
			{
				SetChildren();
			}

			var c = (PlayerOneCharacterCollection)this.ChildCollections["PlayerOneCharacter_PlayerOneId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneWeaponCollection PlayerOneWeaponsByPlayerOneId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneWeapon_PlayerOneId"))
			{
				SetChildren();
			}

			var c = (PlayerOneWeaponCollection)this.ChildCollections["PlayerOneWeapon_PlayerOneId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneSpellCollection PlayerOneSpellsByPlayerOneId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneSpell_PlayerOneId"))
			{
				SetChildren();
			}

			var c = (PlayerOneSpellCollection)this.ChildCollections["PlayerOneSpell_PlayerOneId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneSkillCollection PlayerOneSkillsByPlayerOneId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneSkill_PlayerOneId"))
			{
				SetChildren();
			}

			var c = (PlayerOneSkillCollection)this.ChildCollections["PlayerOneSkill_PlayerOneId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneEquipmentCollection PlayerOneEquipmentsByPlayerOneId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneEquipment_PlayerOneId"))
			{
				SetChildren();
			}

			var c = (PlayerOneEquipmentCollection)this.ChildCollections["PlayerOneEquipment_PlayerOneId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<PlayerOneCharacter, Character> Characters
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerOne_PlayerOneCharacter_Character"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneCharacter, Character>)this.ChildCollections["PlayerOne_PlayerOneCharacter_Character"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneWeapon, Weapon> Weapons
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerOne_PlayerOneWeapon_Weapon"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneWeapon, Weapon>)this.ChildCollections["PlayerOne_PlayerOneWeapon_Weapon"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneSpell, Spell> Spells
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerOne_PlayerOneSpell_Spell"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneSpell, Spell>)this.ChildCollections["PlayerOne_PlayerOneSpell_Spell"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneSkill, Skill> Skills
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerOne_PlayerOneSkill_Skill"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneSkill, Skill>)this.ChildCollections["PlayerOne_PlayerOneSkill_Skill"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneEquipment, Equipment> Equipments
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("PlayerOne_PlayerOneEquipment_Equipment"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneEquipment, Equipment>)this.ChildCollections["PlayerOne_PlayerOneEquipment_Equipment"];
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
			var colFilter = new PlayerOneColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerOne table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerOneCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerOne>();
			Database db = database == null ? Db.For<PlayerOne>(): database;
			var results = new PlayerOneCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerOneColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCollection Where(Func<PlayerOneColumns, QueryFilter<PlayerOneColumns>> where, OrderBy<PlayerOneColumns> orderBy = null)
		{
			return new PlayerOneCollection(new Query<PlayerOneColumns, PlayerOne>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCollection Where(WhereDelegate<PlayerOneColumns> where, Database db = null)
		{
			var results = new PlayerOneCollection(db, new Query<PlayerOneColumns, PlayerOne>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCollection Where(WhereDelegate<PlayerOneColumns> where, OrderBy<PlayerOneColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerOneCollection(db, new Query<PlayerOneColumns, PlayerOne>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerOneCollection(db, Select<PlayerOneColumns>.From<PlayerOne>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerOne instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOne OneWhere(WhereDelegate<PlayerOneColumns> where, Database db = null)
		{
			var results = new PlayerOneCollection(db, Select<PlayerOneColumns>.From<PlayerOne>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOne OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerOneCollection(db, Select<PlayerOneColumns>.From<PlayerOne>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerOne OneOrThrow(PlayerOneCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOne FirstOneWhere(WhereDelegate<PlayerOneColumns> where, Database db = null)
		{
			var results = new PlayerOneCollection(db, Select<PlayerOneColumns>.From<PlayerOne>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCollection Top(int count, WhereDelegate<PlayerOneColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCollection Top(int count, WhereDelegate<PlayerOneColumns> where, OrderBy<PlayerOneColumns> orderBy, Database database = null)
		{
			PlayerOneColumns c = new PlayerOneColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerOne>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerOne>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerOneColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerOneCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerOneColumns> where, Database database = null)
		{
			PlayerOneColumns c = new PlayerOneColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerOne>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerOne>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
