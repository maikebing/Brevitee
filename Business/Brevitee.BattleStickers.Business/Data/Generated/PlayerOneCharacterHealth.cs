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
	[Brevitee.Data.Table("PlayerOneCharacterHealth", "BattleStickers")]
	public partial class PlayerOneCharacterHealth: Dao
	{
		public PlayerOneCharacterHealth():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerOneCharacterHealth(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerOneCharacterHealth(DataRow data)
		{
			return new PlayerOneCharacterHealth(data);
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="Int", MaxLength="4", AllowNull=false)]
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



	// start PlayerOneId -> PlayerOneId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneCharacterHealth",
		Name="PlayerOneId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
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
	
	// start CharacterId -> CharacterId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneCharacterHealth",
		Name="CharacterId", 
		DbDataType="BigInt", 
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
			var colFilter = new PlayerOneCharacterHealthColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerOneCharacterHealth table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerOneCharacterHealthCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerOneCharacterHealth>();
			Database db = database == null ? Db.For<PlayerOneCharacterHealth>(): database;
			var results = new PlayerOneCharacterHealthCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealthCollection Where(Func<PlayerOneCharacterHealthColumns, QueryFilter<PlayerOneCharacterHealthColumns>> where, OrderBy<PlayerOneCharacterHealthColumns> orderBy = null)
		{
			return new PlayerOneCharacterHealthCollection(new Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealthCollection Where(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
		{
			var results = new PlayerOneCharacterHealthCollection(db, new Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealthCollection Where(WhereDelegate<PlayerOneCharacterHealthColumns> where, OrderBy<PlayerOneCharacterHealthColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerOneCharacterHealthCollection(db, new Query<PlayerOneCharacterHealthColumns, PlayerOneCharacterHealth>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneCharacterHealthColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealthCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerOneCharacterHealthCollection(db, Select<PlayerOneCharacterHealthColumns>.From<PlayerOneCharacterHealth>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerOneCharacterHealth instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealth OneWhere(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
		{
			var results = new PlayerOneCharacterHealthCollection(db, Select<PlayerOneCharacterHealthColumns>.From<PlayerOneCharacterHealth>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneCharacterHealthColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealth OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerOneCharacterHealthCollection(db, Select<PlayerOneCharacterHealthColumns>.From<PlayerOneCharacterHealth>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerOneCharacterHealth OneOrThrow(PlayerOneCharacterHealthCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealth FirstOneWhere(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
		{
			var results = new PlayerOneCharacterHealthCollection(db, Select<PlayerOneCharacterHealthColumns>.From<PlayerOneCharacterHealth>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealthCollection Top(int count, WhereDelegate<PlayerOneCharacterHealthColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneCharacterHealthCollection Top(int count, WhereDelegate<PlayerOneCharacterHealthColumns> where, OrderBy<PlayerOneCharacterHealthColumns> orderBy, Database database = null)
		{
			PlayerOneCharacterHealthColumns c = new PlayerOneCharacterHealthColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerOneCharacterHealth>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerOneCharacterHealth>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerOneCharacterHealthColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerOneCharacterHealthCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneCharacterHealthColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneCharacterHealthColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerOneCharacterHealthColumns> where, Database database = null)
		{
			PlayerOneCharacterHealthColumns c = new PlayerOneCharacterHealthColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerOneCharacterHealth>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerOneCharacterHealth>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
