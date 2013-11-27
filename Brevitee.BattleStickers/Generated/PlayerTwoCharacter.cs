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
	[Brevitee.Data.Table("PlayerTwoCharacter", "BattleStickers")]
	public partial class PlayerTwoCharacter: Dao
	{
		public PlayerTwoCharacter():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwoCharacter(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwoCharacter(DataRow data)
		{
			return new PlayerTwoCharacter(data);
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



	// start PlayerTwoId -> PlayerTwoId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoCharacter",
		Name="PlayerTwoId", 
		ExtractedType="", 
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
				_playerTwoOfPlayerTwoId = Brevitee.BattleStickers.PlayerTwo.OneWhere(f => f.Id == this.PlayerTwoId);
			}
			return _playerTwoOfPlayerTwoId;
		}
	}
	
	// start CharacterId -> CharacterId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoCharacter",
		Name="CharacterId", 
		ExtractedType="", 
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
				_characterOfCharacterId = Brevitee.BattleStickers.Character.OneWhere(f => f.Id == this.CharacterId);
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
			var colFilter = new PlayerTwoCharacterColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterCollection Where(Func<PlayerTwoCharacterColumns, QueryFilter<PlayerTwoCharacterColumns>> where, OrderBy<PlayerTwoCharacterColumns> orderBy = null)
		{
			return new PlayerTwoCharacterCollection(new Query<PlayerTwoCharacterColumns, PlayerTwoCharacter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterCollection Where(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
		{
			return new PlayerTwoCharacterCollection(new Query<PlayerTwoCharacterColumns, PlayerTwoCharacter>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterCollection Where(WhereDelegate<PlayerTwoCharacterColumns> where, OrderBy<PlayerTwoCharacterColumns> orderBy = null, Database db = null)
		{
			return new PlayerTwoCharacterCollection(new Query<PlayerTwoCharacterColumns, PlayerTwoCharacter>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerTwoCharacterCollection(Select<PlayerTwoCharacterColumns>.From<PlayerTwoCharacter>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwoCharacter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacter OneWhere(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
		{
			var results = new PlayerTwoCharacterCollection(Select<PlayerTwoCharacterColumns>.From<PlayerTwoCharacter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoCharacter OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoCharacterCollection(Select<PlayerTwoCharacterColumns>.From<PlayerTwoCharacter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwoCharacter OneOrThrow(PlayerTwoCharacterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacter FirstOneWhere(WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
		{
			var results = new PlayerTwoCharacterCollection(Select<PlayerTwoCharacterColumns>.From<PlayerTwoCharacter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterCollection Top(int count, WhereDelegate<PlayerTwoCharacterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoCharacterCollection Top(int count, WhereDelegate<PlayerTwoCharacterColumns> where, OrderBy<PlayerTwoCharacterColumns> orderBy, Database database = null)
		{
			PlayerTwoCharacterColumns c = new PlayerTwoCharacterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerTwoCharacter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwoCharacter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoCharacterColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerTwoCharacterCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoCharacterColumns> where, Database database = null)
		{
			PlayerTwoCharacterColumns c = new PlayerTwoCharacterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerTwoCharacter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwoCharacter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
