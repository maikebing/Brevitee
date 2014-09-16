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
	[Brevitee.Data.Table("Weapon", "BattleStickers")]
	public partial class Weapon: Dao
	{
		public Weapon():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Weapon(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Weapon(DataRow data)
		{
			return new Weapon(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("PlayerWeapon_WeaponId", new PlayerWeaponCollection(new Query<PlayerWeaponColumns, PlayerWeapon>((c) => c.WeaponId == this.Id), this, "WeaponId"));	
            this.ChildCollections.Add("PlayerOneWeapon_WeaponId", new PlayerOneWeaponCollection(new Query<PlayerOneWeaponColumns, PlayerOneWeapon>((c) => c.WeaponId == this.Id), this, "WeaponId"));	
            this.ChildCollections.Add("PlayerTwoWeapon_WeaponId", new PlayerTwoWeaponCollection(new Query<PlayerTwoWeaponColumns, PlayerTwoWeapon>((c) => c.WeaponId == this.Id), this, "WeaponId"));	
            this.ChildCollections.Add("RequiredLevelWeapon_WeaponId", new RequiredLevelWeaponCollection(new Query<RequiredLevelWeaponColumns, RequiredLevelWeapon>((c) => c.WeaponId == this.Id), this, "WeaponId"));							
            this.ChildCollections.Add("Weapon_PlayerWeapon_Player",  new XrefDaoCollection<PlayerWeapon, Player>(this, false));
				
            this.ChildCollections.Add("Weapon_PlayerOneWeapon_PlayerOne",  new XrefDaoCollection<PlayerOneWeapon, PlayerOne>(this, false));
				
            this.ChildCollections.Add("Weapon_PlayerTwoWeapon_PlayerTwo",  new XrefDaoCollection<PlayerTwoWeapon, PlayerTwo>(this, false));
				
            this.ChildCollections.Add("Weapon_RequiredLevelWeapon_RequiredLevel",  new XrefDaoCollection<RequiredLevelWeapon, RequiredLevel>(this, false));
				
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
        Table="Weapon",
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
	public PlayerWeaponCollection PlayerWeaponsByWeaponId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerWeapon_WeaponId"))
			{
				SetChildren();
			}

			var c = (PlayerWeaponCollection)this.ChildCollections["PlayerWeapon_WeaponId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerOneWeaponCollection PlayerOneWeaponsByWeaponId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneWeapon_WeaponId"))
			{
				SetChildren();
			}

			var c = (PlayerOneWeaponCollection)this.ChildCollections["PlayerOneWeapon_WeaponId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PlayerTwoWeaponCollection PlayerTwoWeaponsByWeaponId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoWeapon_WeaponId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoWeaponCollection)this.ChildCollections["PlayerTwoWeapon_WeaponId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public RequiredLevelWeaponCollection RequiredLevelWeaponsByWeaponId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelWeapon_WeaponId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelWeaponCollection)this.ChildCollections["RequiredLevelWeapon_WeaponId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<PlayerWeapon, Player> Players
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Weapon_PlayerWeapon_Player"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerWeapon, Player>)this.ChildCollections["Weapon_PlayerWeapon_Player"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneWeapon, PlayerOne> PlayerOnes
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Weapon_PlayerOneWeapon_PlayerOne"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneWeapon, PlayerOne>)this.ChildCollections["Weapon_PlayerOneWeapon_PlayerOne"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoWeapon, PlayerTwo> PlayerTwos
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Weapon_PlayerTwoWeapon_PlayerTwo"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoWeapon, PlayerTwo>)this.ChildCollections["Weapon_PlayerTwoWeapon_PlayerTwo"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<RequiredLevelWeapon, RequiredLevel> RequiredLevels
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Weapon_RequiredLevelWeapon_RequiredLevel"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelWeapon, RequiredLevel>)this.ChildCollections["Weapon_RequiredLevelWeapon_RequiredLevel"];
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
			var colFilter = new WeaponColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Weapon table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static WeaponCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Weapon>();
			Database db = database == null ? Db.For<Weapon>(): database;
			var results = new WeaponCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a WeaponColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static WeaponCollection Where(Func<WeaponColumns, QueryFilter<WeaponColumns>> where, OrderBy<WeaponColumns> orderBy = null)
		{
			return new WeaponCollection(new Query<WeaponColumns, Weapon>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static WeaponCollection Where(WhereDelegate<WeaponColumns> where, Database db = null)
		{
			var results = new WeaponCollection(db, new Query<WeaponColumns, Weapon>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static WeaponCollection Where(WhereDelegate<WeaponColumns> where, OrderBy<WeaponColumns> orderBy = null, Database db = null)
		{
			var results = new WeaponCollection(db, new Query<WeaponColumns, Weapon>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<WeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static WeaponCollection Where(QiQuery where, Database db = null)
		{
			var results = new WeaponCollection(db, Select<WeaponColumns>.From<Weapon>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Weapon instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Weapon OneWhere(WhereDelegate<WeaponColumns> where, Database db = null)
		{
			var results = new WeaponCollection(db, Select<WeaponColumns>.From<Weapon>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<WeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Weapon OneWhere(QiQuery where, Database db = null)
		{
			var results = new WeaponCollection(db, Select<WeaponColumns>.From<Weapon>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Weapon OneOrThrow(WeaponCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Weapon FirstOneWhere(WhereDelegate<WeaponColumns> where, Database db = null)
		{
			var results = new WeaponCollection(db, Select<WeaponColumns>.From<Weapon>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static WeaponCollection Top(int count, WhereDelegate<WeaponColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static WeaponCollection Top(int count, WhereDelegate<WeaponColumns> where, OrderBy<WeaponColumns> orderBy, Database database = null)
		{
			WeaponColumns c = new WeaponColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Weapon>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Weapon>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WeaponColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<WeaponCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a WeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between WeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<WeaponColumns> where, Database database = null)
		{
			WeaponColumns c = new WeaponColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Weapon>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Weapon>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
