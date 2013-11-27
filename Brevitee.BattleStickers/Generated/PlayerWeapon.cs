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
	[Brevitee.Data.Table("PlayerWeapon", "BattleStickers")]
	public partial class PlayerWeapon: Dao
	{
		public PlayerWeapon():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerWeapon(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerWeapon(DataRow data)
		{
			return new PlayerWeapon(data);
		}

		private void SetChildren()
		{
						
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



	// start PlayerId -> PlayerId
	[Brevitee.Data.ForeignKey(
        Table="PlayerWeapon",
		Name="PlayerId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Player",
		Suffix="1")]
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
	
	// start WeaponId -> WeaponId
	[Brevitee.Data.ForeignKey(
        Table="PlayerWeapon",
		Name="WeaponId", 
		ExtractedType="", 
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
				_weaponOfWeaponId = Brevitee.BattleStickers.Weapon.OneWhere(f => f.Id == this.WeaponId);
			}
			return _weaponOfWeaponId;
		}
	}
	
				
		


		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerWeaponColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerWeaponColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeaponCollection Where(Func<PlayerWeaponColumns, QueryFilter<PlayerWeaponColumns>> where, OrderBy<PlayerWeaponColumns> orderBy = null)
		{
			return new PlayerWeaponCollection(new Query<PlayerWeaponColumns, PlayerWeapon>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeaponCollection Where(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
		{
			return new PlayerWeaponCollection(new Query<PlayerWeaponColumns, PlayerWeapon>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeaponCollection Where(WhereDelegate<PlayerWeaponColumns> where, OrderBy<PlayerWeaponColumns> orderBy = null, Database db = null)
		{
			return new PlayerWeaponCollection(new Query<PlayerWeaponColumns, PlayerWeapon>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerWeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerWeaponCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerWeaponCollection(Select<PlayerWeaponColumns>.From<PlayerWeapon>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerWeapon instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeapon OneWhere(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
		{
			var results = new PlayerWeaponCollection(Select<PlayerWeaponColumns>.From<PlayerWeapon>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerWeaponColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerWeapon OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerWeaponCollection(Select<PlayerWeaponColumns>.From<PlayerWeapon>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerWeapon OneOrThrow(PlayerWeaponCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeapon FirstOneWhere(WhereDelegate<PlayerWeaponColumns> where, Database db = null)
		{
			var results = new PlayerWeaponCollection(Select<PlayerWeaponColumns>.From<PlayerWeapon>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeaponCollection Top(int count, WhereDelegate<PlayerWeaponColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerWeaponCollection Top(int count, WhereDelegate<PlayerWeaponColumns> where, OrderBy<PlayerWeaponColumns> orderBy, Database database = null)
		{
			PlayerWeaponColumns c = new PlayerWeaponColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerWeapon>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerWeapon>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerWeaponColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerWeaponCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerWeaponColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerWeaponColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerWeaponColumns> where, Database database = null)
		{
			PlayerWeaponColumns c = new PlayerWeaponColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerWeapon>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerWeapon>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
