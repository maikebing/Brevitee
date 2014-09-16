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
	[Brevitee.Data.Table("PlayerOneWeapon", "BattleStickers")]
	public partial class PlayerOneWeapon: Dao
	{
		public PlayerOneWeapon():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerOneWeapon(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerOneWeapon(DataRow data)
		{
			return new PlayerOneWeapon(data);
		}

		private void SetChildren()
		{
						
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



	// start PlayerOneId -> PlayerOneId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneWeapon",
		Name="PlayerOneId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="PlayerOne",
		Suffix="1")]
	public long? PlayerOneId
	{
		get
		{
			return GetLongValue("PlayerOneId");
		}
		set
		{
			SetValue("PlayerOneId", value);
		}
	}

	PlayerOne _playerOneOfPlayerOneId;
	public PlayerOne PlayerOneOfPlayerOneId
	{
		get
		{
			if(_playerOneOfPlayerOneId == null)
			{
				_playerOneOfPlayerOneId = Brevitee.BattleStickers.Business.Data.PlayerOne.OneWhere(f => f.Id == this.PlayerOneId);
			}
			return _playerOneOfPlayerOneId;
		}
	}
	
	// start WeaponId -> WeaponId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneWeapon",
		Name="WeaponId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Weapon",
		Suffix="2")]
	public long? WeaponId
	{
		get
		{
			return GetLongValue("WeaponId");
		}
		set
		{
			SetValue("WeaponId", value);
		}
	}

	Weapon _weaponOfWeaponId;
	public Weapon WeaponOfWeaponId
	{
		get
		{
			if(_weaponOfWeaponId == null)
			{
				_weaponOfWeaponId = Brevitee.BattleStickers.Business.Data.Weapon.OneWhere(f => f.Id == this.WeaponId);
			}
			return _weaponOfWeaponId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerOneWeaponColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerOneWeapon table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerOneWeaponCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerOneWeapon>();
			Database db = database == null ? Db.For<PlayerOneWeapon>(): database;
			var results = new PlayerOneWeaponCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerOneWeaponColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeaponCollection Where(Func<PlayerOneWeaponColumns, QueryFilter<PlayerOneWeaponColumns>> where, OrderBy<PlayerOneWeaponColumns> orderBy = null)
		{
			return new PlayerOneWeaponCollection(new Query<PlayerOneWeaponColumns, PlayerOneWeapon>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeaponCollection Where(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
		{
			var results = new PlayerOneWeaponCollection(db, new Query<PlayerOneWeaponColumns, PlayerOneWeapon>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeaponCollection Where(WhereDelegate<PlayerOneWeaponColumns> where, OrderBy<PlayerOneWeaponColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerOneWeaponCollection(db, new Query<PlayerOneWeaponColumns, PlayerOneWeapon>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneWeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneWeaponCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerOneWeaponCollection(db, Select<PlayerOneWeaponColumns>.From<PlayerOneWeapon>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerOneWeapon instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeapon OneWhere(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
		{
			var results = new PlayerOneWeaponCollection(db, Select<PlayerOneWeaponColumns>.From<PlayerOneWeapon>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneWeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneWeapon OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerOneWeaponCollection(db, Select<PlayerOneWeaponColumns>.From<PlayerOneWeapon>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerOneWeapon OneOrThrow(PlayerOneWeaponCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeapon FirstOneWhere(WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
		{
			var results = new PlayerOneWeaponCollection(db, Select<PlayerOneWeaponColumns>.From<PlayerOneWeapon>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeaponCollection Top(int count, WhereDelegate<PlayerOneWeaponColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneWeaponCollection Top(int count, WhereDelegate<PlayerOneWeaponColumns> where, OrderBy<PlayerOneWeaponColumns> orderBy, Database database = null)
		{
			PlayerOneWeaponColumns c = new PlayerOneWeaponColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerOneWeapon>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerOneWeapon>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerOneWeaponColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerOneWeaponCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerOneWeaponColumns> where, Database database = null)
		{
			PlayerOneWeaponColumns c = new PlayerOneWeaponColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerOneWeapon>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerOneWeapon>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
