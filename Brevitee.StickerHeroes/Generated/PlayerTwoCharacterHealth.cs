// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.StickerHeroes
{
	// schema = BattleStickers
	// connection Name = BattleStickers
	[Brevitee.Data.Table("PlayerTwoCharacterHealth", "BattleStickers")]
	public partial class PlayerTwoCharacterHealth: Dao
	{
		public PlayerTwoCharacterHealth():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwoCharacterHealth(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwoCharacterHealth(DataRow data)
		{
			return new PlayerTwoCharacterHealth(data);
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=false)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



	// start PlayerTwoId -> PlayerTwoId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoCharacterHealth",
		Name="PlayerTwoId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
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
				_playerTwoOfPlayerTwoId = Brevitee.StickerHeroes.PlayerTwo.OneWhere(f => f.Id == this.PlayerTwoId);
			}
			return _playerTwoOfPlayerTwoId;
		}
	}
	
	// start CharacterId -> CharacterId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoCharacterHealth",
		Name="CharacterId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
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
				_characterOfCharacterId = Brevitee.StickerHeroes.Character.OneWhere(f => f.Id == this.CharacterId);
			}
			return _characterOfCharacterId;
		}
	}
	
				
		


		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerTwoCharacterHealthColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealthCollection Where(Func<PlayerTwoCharacterHealthColumns, QueryFilter<PlayerTwoCharacterHealthColumns>> where, OrderBy<PlayerTwoCharacterHealthColumns> orderBy = null)
		{
			return new PlayerTwoCharacterHealthCollection(new Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealthCollection Where(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
		{
			return new PlayerTwoCharacterHealthCollection(new Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealthCollection Where(WhereDelegate<PlayerTwoCharacterHealthColumns> where, OrderBy<PlayerTwoCharacterHealthColumns> orderBy = null, Database db = null)
		{
			return new PlayerTwoCharacterHealthCollection(new Query<PlayerTwoCharacterHealthColumns, PlayerTwoCharacterHealth>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoCharacterHealthColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealthCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerTwoCharacterHealthCollection(Select<PlayerTwoCharacterHealthColumns>.From<PlayerTwoCharacterHealth>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwoCharacterHealth instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealth OneWhere(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
		{
			var results = new PlayerTwoCharacterHealthCollection(Select<PlayerTwoCharacterHealthColumns>.From<PlayerTwoCharacterHealth>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoCharacterHealthColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealth OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoCharacterHealthCollection(Select<PlayerTwoCharacterHealthColumns>.From<PlayerTwoCharacterHealth>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwoCharacterHealth OneOrThrow(PlayerTwoCharacterHealthCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealth FirstOneWhere(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
		{
			var results = new PlayerTwoCharacterHealthCollection(Select<PlayerTwoCharacterHealthColumns>.From<PlayerTwoCharacterHealth>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealthCollection Top(int count, WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterHealthCollection Top(int count, WhereDelegate<PlayerTwoCharacterHealthColumns> where, OrderBy<PlayerTwoCharacterHealthColumns> orderBy, Database database = null)
		{
			PlayerTwoCharacterHealthColumns c = new PlayerTwoCharacterHealthColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerTwoCharacterHealth>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwoCharacterHealth>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoCharacterHealthColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerTwoCharacterHealthCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoCharacterHealthColumns> where, Database database = null)
		{
			PlayerTwoCharacterHealthColumns c = new PlayerTwoCharacterHealthColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerTwoCharacterHealth>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwoCharacterHealth>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
