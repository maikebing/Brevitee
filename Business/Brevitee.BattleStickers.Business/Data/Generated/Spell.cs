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
	[Brevitee.Data.Table("Spell", "BattleStickers")]
	public partial class Spell: Dao
	{
		public Spell():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Spell(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Spell(DataRow data)
		{
			return new Spell(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("PlayerSpell_SpellId", new PlayerSpellCollection(new Query<PlayerSpellColumns, PlayerSpell>((c) => c.SpellId == this.Id), this, "SpellId"));	
            this.ChildCollections.Add("PlayerOneSpell_SpellId", new PlayerOneSpellCollection(new Query<PlayerOneSpellColumns, PlayerOneSpell>((c) => c.SpellId == this.Id), this, "SpellId"));	
            this.ChildCollections.Add("PlayerTwoSpell_SpellId", new PlayerTwoSpellCollection(new Query<PlayerTwoSpellColumns, PlayerTwoSpell>((c) => c.SpellId == this.Id), this, "SpellId"));	
            this.ChildCollections.Add("RequiredLevelSpell_SpellId", new RequiredLevelSpellCollection(new Query<RequiredLevelSpellColumns, RequiredLevelSpell>((c) => c.SpellId == this.Id), this, "SpellId"));							
            this.ChildCollections.Add("Spell_PlayerSpell_Player",  new XrefDaoCollection<PlayerSpell, Player>(this, false));
				
            this.ChildCollections.Add("Spell_PlayerOneSpell_PlayerOne",  new XrefDaoCollection<PlayerOneSpell, PlayerOne>(this, false));
				
            this.ChildCollections.Add("Spell_PlayerTwoSpell_PlayerTwo",  new XrefDaoCollection<PlayerTwoSpell, PlayerTwo>(this, false));
				
            this.ChildCollections.Add("Spell_RequiredLevelSpell_RequiredLevel",  new XrefDaoCollection<RequiredLevelSpell, RequiredLevel>(this, false));
				
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

	// property:Strength, columnName:Strength	
	[Brevitee.Data.Column(Name="Strength", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? Strength
	{
		get
		{
			return GetIntValue("Strength");
		}
		set
		{
			SetValue("Strength", value);
		}
	}

	// property:Element, columnName:Element	
	[Brevitee.Data.Column(Name="Element", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Element
	{
		get
		{
			return GetStringValue("Element");
		}
		set
		{
			SetValue("Element", value);
		}
	}



	// start EffectOverTimeId -> EffectOverTimeId
	[Brevitee.Data.ForeignKey(
        Table="Spell",
		Name="EffectOverTimeId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="EffectOverTime",
		Suffix="1")]
	public long? EffectOverTimeId
	{
		get
		{
			return GetLongValue("EffectOverTimeId");
		}
		set
		{
			SetValue("EffectOverTimeId", value);
		}
	}

	EffectOverTime _effectOverTimeOfEffectOverTimeId;
	public EffectOverTime EffectOverTimeOfEffectOverTimeId
	{
		get
		{
			if(_effectOverTimeOfEffectOverTimeId == null)
			{
				_effectOverTimeOfEffectOverTimeId = Brevitee.BattleStickers.Business.Data.EffectOverTime.OneWhere(f => f.Id == this.EffectOverTimeId);
			}
			return _effectOverTimeOfEffectOverTimeId;
		}
	}
	
				

	[Exclude]	
	public PlayerSpellCollection PlayerSpellsBySpellId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerSpell_SpellId"))
			{
				SetChildren();
			}

			var c = (PlayerSpellCollection)this.ChildCollections["PlayerSpell_SpellId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneSpellCollection PlayerOneSpellsBySpellId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneSpell_SpellId"))
			{
				SetChildren();
			}

			var c = (PlayerOneSpellCollection)this.ChildCollections["PlayerOneSpell_SpellId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerTwoSpellCollection PlayerTwoSpellsBySpellId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoSpell_SpellId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoSpellCollection)this.ChildCollections["PlayerTwoSpell_SpellId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public RequiredLevelSpellCollection RequiredLevelSpellsBySpellId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelSpell_SpellId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelSpellCollection)this.ChildCollections["RequiredLevelSpell_SpellId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<PlayerSpell, Player> Players
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Spell_PlayerSpell_Player"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerSpell, Player>)this.ChildCollections["Spell_PlayerSpell_Player"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneSpell, PlayerOne> PlayerOnes
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Spell_PlayerOneSpell_PlayerOne"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneSpell, PlayerOne>)this.ChildCollections["Spell_PlayerOneSpell_PlayerOne"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoSpell, PlayerTwo> PlayerTwos
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Spell_PlayerTwoSpell_PlayerTwo"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoSpell, PlayerTwo>)this.ChildCollections["Spell_PlayerTwoSpell_PlayerTwo"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<RequiredLevelSpell, RequiredLevel> RequiredLevels
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Spell_RequiredLevelSpell_RequiredLevel"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelSpell, RequiredLevel>)this.ChildCollections["Spell_RequiredLevelSpell_RequiredLevel"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new SpellColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Spell table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SpellCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Spell>();
			Database db = database == null ? Db.For<Spell>(): database;
			var results = new SpellCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SpellColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SpellCollection Where(Func<SpellColumns, QueryFilter<SpellColumns>> where, OrderBy<SpellColumns> orderBy = null)
		{
			return new SpellCollection(new Query<SpellColumns, Spell>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SpellCollection Where(WhereDelegate<SpellColumns> where, Database db = null)
		{
			var results = new SpellCollection(db, new Query<SpellColumns, Spell>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SpellCollection Where(WhereDelegate<SpellColumns> where, OrderBy<SpellColumns> orderBy = null, Database db = null)
		{
			var results = new SpellCollection(db, new Query<SpellColumns, Spell>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SpellCollection Where(QiQuery where, Database db = null)
		{
			var results = new SpellCollection(db, Select<SpellColumns>.From<Spell>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Spell instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Spell OneWhere(WhereDelegate<SpellColumns> where, Database db = null)
		{
			var results = new SpellCollection(db, Select<SpellColumns>.From<Spell>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Spell OneWhere(QiQuery where, Database db = null)
		{
			var results = new SpellCollection(db, Select<SpellColumns>.From<Spell>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Spell OneOrThrow(SpellCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Spell FirstOneWhere(WhereDelegate<SpellColumns> where, Database db = null)
		{
			var results = new SpellCollection(db, Select<SpellColumns>.From<Spell>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SpellCollection Top(int count, WhereDelegate<SpellColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SpellCollection Top(int count, WhereDelegate<SpellColumns> where, OrderBy<SpellColumns> orderBy, Database database = null)
		{
			SpellColumns c = new SpellColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Spell>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Spell>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SpellColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SpellCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SpellColumns> where, Database database = null)
		{
			SpellColumns c = new SpellColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Spell>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Spell>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
