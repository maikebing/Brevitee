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
	[Brevitee.Data.Table("PlayerTwoWeapon", "BattleStickers")]
	public partial class PlayerTwoWeapon: Dao
	{
		public PlayerTwoWeapon():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwoWeapon(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwoWeapon(DataRow data)
		{
			return new PlayerTwoWeapon(data);
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



	// start PlayerTwoId -> PlayerTwoId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoWeapon",
		Name="PlayerTwoId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="PlayerTwo",
		Suffix="1")]
	public long? PlayerTwoId
	{
		get
		{
			return GetLongValue("PlayerTwoId");
		}
		set
		{
			SetValue("PlayerTwoId", value);
		}
	}

	PlayerTwo _playerTwoOfPlayerTwoId;
	public PlayerTwo PlayerTwoOfPlayerTwoId
	{
		get
		{
			if(_playerTwoOfPlayerTwoId == null)
			{
				_playerTwoOfPlayerTwoId = Brevitee.BattleStickers.Business.Data.PlayerTwo.OneWhere(f => f.Id == this.PlayerTwoId);
			}
			return _playerTwoOfPlayerTwoId;
		}
	}
	
	// start WeaponId -> WeaponId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoWeapon",
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
			var colFilter = new PlayerTwoWeaponColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerTwoWeapon table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerTwoWeaponCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerTwoWeapon>();
			Database db = database == null ? Db.For<PlayerTwoWeapon>(): database;
			var results = new PlayerTwoWeaponCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeaponCollection Where(Func<PlayerTwoWeaponColumns, QueryFilter<PlayerTwoWeaponColumns>> where, OrderBy<PlayerTwoWeaponColumns> orderBy = null)
		{
			return new PlayerTwoWeaponCollection(new Query<PlayerTwoWeaponColumns, PlayerTwoWeapon>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeaponCollection Where(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
		{
			var results = new PlayerTwoWeaponCollection(db, new Query<PlayerTwoWeaponColumns, PlayerTwoWeapon>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeaponCollection Where(WhereDelegate<PlayerTwoWeaponColumns> where, OrderBy<PlayerTwoWeaponColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerTwoWeaponCollection(db, new Query<PlayerTwoWeaponColumns, PlayerTwoWeapon>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoWeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoWeaponCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoWeaponCollection(db, Select<PlayerTwoWeaponColumns>.From<PlayerTwoWeapon>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwoWeapon instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeapon OneWhere(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
		{
			var results = new PlayerTwoWeaponCollection(db, Select<PlayerTwoWeaponColumns>.From<PlayerTwoWeapon>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoWeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoWeapon OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoWeaponCollection(db, Select<PlayerTwoWeaponColumns>.From<PlayerTwoWeapon>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwoWeapon OneOrThrow(PlayerTwoWeaponCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeapon FirstOneWhere(WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
		{
			var results = new PlayerTwoWeaponCollection(db, Select<PlayerTwoWeaponColumns>.From<PlayerTwoWeapon>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeaponCollection Top(int count, WhereDelegate<PlayerTwoWeaponColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoWeaponCollection Top(int count, WhereDelegate<PlayerTwoWeaponColumns> where, OrderBy<PlayerTwoWeaponColumns> orderBy, Database database = null)
		{
			PlayerTwoWeaponColumns c = new PlayerTwoWeaponColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerTwoWeapon>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwoWeapon>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoWeaponColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerTwoWeaponCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoWeaponColumns> where, Database database = null)
		{
			PlayerTwoWeaponColumns c = new PlayerTwoWeaponColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerTwoWeapon>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwoWeapon>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
