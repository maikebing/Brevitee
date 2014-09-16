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
	[Brevitee.Data.Table("PlayerCharacter", "BattleStickers")]
	public partial class PlayerCharacter: Dao
	{
		public PlayerCharacter():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerCharacter(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerCharacter(DataRow data)
		{
			return new PlayerCharacter(data);
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



	// start PlayerId -> PlayerId
	[Brevitee.Data.ForeignKey(
        Table="PlayerCharacter",
		Name="PlayerId", 
		DbDataType="BigInt", 
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
				_playerOfPlayerId = Brevitee.BattleStickers.Business.Data.Player.OneWhere(f => f.Id == this.PlayerId);
			}
			return _playerOfPlayerId;
		}
	}
	
	// start CharacterId -> CharacterId
	[Brevitee.Data.ForeignKey(
        Table="PlayerCharacter",
		Name="CharacterId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Character",
		Suffix="2")]
	public long? CharacterId
	{
		get
		{
			return GetLongValue("CharacterId");
		}
		set
		{
			SetValue("CharacterId", value);
		}
	}

	Character _characterOfCharacterId;
	public Character CharacterOfCharacterId
	{
		get
		{
			if(_characterOfCharacterId == null)
			{
				_characterOfCharacterId = Brevitee.BattleStickers.Business.Data.Character.OneWhere(f => f.Id == this.CharacterId);
			}
			return _characterOfCharacterId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerCharacterColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerCharacter table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerCharacterCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerCharacter>();
			Database db = database == null ? Db.For<PlayerCharacter>(): database;
			var results = new PlayerCharacterCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerCharacterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacterCollection Where(Func<PlayerCharacterColumns, QueryFilter<PlayerCharacterColumns>> where, OrderBy<PlayerCharacterColumns> orderBy = null)
		{
			return new PlayerCharacterCollection(new Query<PlayerCharacterColumns, PlayerCharacter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacterCollection Where(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
		{
			var results = new PlayerCharacterCollection(db, new Query<PlayerCharacterColumns, PlayerCharacter>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacterCollection Where(WhereDelegate<PlayerCharacterColumns> where, OrderBy<PlayerCharacterColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerCharacterCollection(db, new Query<PlayerCharacterColumns, PlayerCharacter>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerCharacterCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerCharacterCollection(db, Select<PlayerCharacterColumns>.From<PlayerCharacter>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerCharacter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacter OneWhere(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
		{
			var results = new PlayerCharacterCollection(db, Select<PlayerCharacterColumns>.From<PlayerCharacter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerCharacter OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerCharacterCollection(db, Select<PlayerCharacterColumns>.From<PlayerCharacter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerCharacter OneOrThrow(PlayerCharacterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacter FirstOneWhere(WhereDelegate<PlayerCharacterColumns> where, Database db = null)
		{
			var results = new PlayerCharacterCollection(db, Select<PlayerCharacterColumns>.From<PlayerCharacter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacterCollection Top(int count, WhereDelegate<PlayerCharacterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerCharacterCollection Top(int count, WhereDelegate<PlayerCharacterColumns> where, OrderBy<PlayerCharacterColumns> orderBy, Database database = null)
		{
			PlayerCharacterColumns c = new PlayerCharacterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerCharacter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerCharacter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerCharacterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerCharacterCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerCharacterColumns> where, Database database = null)
		{
			PlayerCharacterColumns c = new PlayerCharacterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerCharacter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerCharacter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
