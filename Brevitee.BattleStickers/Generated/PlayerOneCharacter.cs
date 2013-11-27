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
	[Brevitee.Data.Table("PlayerOneCharacter", "BattleStickers")]
	public partial class PlayerOneCharacter: Dao
	{
		public PlayerOneCharacter():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerOneCharacter(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerOneCharacter(DataRow data)
		{
			return new PlayerOneCharacter(data);
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



	// start PlayerOneId -> PlayerOneId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneCharacter",
		Name="PlayerOneId", 
		ExtractedType="", 
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
				_playerOneOfPlayerOneId = Brevitee.BattleStickers.PlayerOne.OneWhere(f => f.Id == this.PlayerOneId);
			}
			return _playerOneOfPlayerOneId;
		}
	}
	
	// start CharacterId -> CharacterId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneCharacter",
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
			var colFilter = new PlayerOneCharacterColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerOneCharacterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterCollection Where(Func<PlayerOneCharacterColumns, QueryFilter<PlayerOneCharacterColumns>> where, OrderBy<PlayerOneCharacterColumns> orderBy = null)
		{
			return new PlayerOneCharacterCollection(new Query<PlayerOneCharacterColumns, PlayerOneCharacter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterCollection Where(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
		{
			return new PlayerOneCharacterCollection(new Query<PlayerOneCharacterColumns, PlayerOneCharacter>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterCollection Where(WhereDelegate<PlayerOneCharacterColumns> where, OrderBy<PlayerOneCharacterColumns> orderBy = null, Database db = null)
		{
			return new PlayerOneCharacterCollection(new Query<PlayerOneCharacterColumns, PlayerOneCharacter>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneCharacterCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerOneCharacterCollection(Select<PlayerOneCharacterColumns>.From<PlayerOneCharacter>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerOneCharacter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacter OneWhere(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
		{
			var results = new PlayerOneCharacterCollection(Select<PlayerOneCharacterColumns>.From<PlayerOneCharacter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneCharacter OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerOneCharacterCollection(Select<PlayerOneCharacterColumns>.From<PlayerOneCharacter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerOneCharacter OneOrThrow(PlayerOneCharacterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacter FirstOneWhere(WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
		{
			var results = new PlayerOneCharacterCollection(Select<PlayerOneCharacterColumns>.From<PlayerOneCharacter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterCollection Top(int count, WhereDelegate<PlayerOneCharacterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterCollection Top(int count, WhereDelegate<PlayerOneCharacterColumns> where, OrderBy<PlayerOneCharacterColumns> orderBy, Database database = null)
		{
			PlayerOneCharacterColumns c = new PlayerOneCharacterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerOneCharacter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerOneCharacter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerOneCharacterColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerOneCharacterCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerOneCharacterColumns> where, Database database = null)
		{
			PlayerOneCharacterColumns c = new PlayerOneCharacterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerOneCharacter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerOneCharacter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
