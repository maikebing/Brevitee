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
	[Brevitee.Data.Table("Battle", "BattleStickers")]
	public partial class Battle: Dao
	{
		public Battle():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Battle(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Battle(DataRow data)
		{
			return new Battle(data);
		}

		private void SetChildren()
		{

			this.ChildCollections.Add("PlayerOne_BattleId", new PlayerOneCollection(new Query<PlayerOneColumns, PlayerOne>((c) => c.BattleId == this.Id), this, "BattleId"));	
			this.ChildCollections.Add("PlayerTwo_BattleId", new PlayerTwoCollection(new Query<PlayerTwoColumns, PlayerTwo>((c) => c.BattleId == this.Id), this, "BattleId"));							
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

	// property:MaxActiveCharacters, columnName:MaxActiveCharacters	
	[Brevitee.Data.Column(Name="MaxActiveCharacters", ExtractedType="", MaxLength="", AllowNull=false)]
	public int? MaxActiveCharacters
	{
		get
		{
			return GetIntValue("MaxActiveCharacters");
		}
		set
		{
			SetValue("MaxActiveCharacters", value);
		}
	}



	// start RockPaperScissorsWinnerId -> RockPaperScissorsWinnerId
	[Brevitee.Data.ForeignKey(
		Table="Battle",
		Name="RockPaperScissorsWinnerId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Player",
		Suffix="1")]
	public long? RockPaperScissorsWinnerId
	{
		get
		{
			return GetLongValue("RockPaperScissorsWinnerId");
		}
		set
		{
			SetValue("RockPaperScissorsWinnerId", value);
		}
	}

	Player _playerOfRockPaperScissorsWinnerId;
	public Player PlayerOfRockPaperScissorsWinnerId
	{
		get
		{
			if(_playerOfRockPaperScissorsWinnerId == null)
			{
				_playerOfRockPaperScissorsWinnerId = Brevitee.StickerHeroes.Player.OneWhere(f => f.Id == this.RockPaperScissorsWinnerId);
			}
			return _playerOfRockPaperScissorsWinnerId;
		}
	}
	
				
	[Exclude]	
	public PlayerOneCollection PlayerOnesByBattleId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOne_BattleId"))
			{
				SetChildren();
			}

			var c = (PlayerOneCollection)this.ChildCollections["PlayerOne_BattleId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoCollection PlayerTwosByBattleId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwo_BattleId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoCollection)this.ChildCollections["PlayerTwo_BattleId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new BattleColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a BattleColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BattleCollection Where(Func<BattleColumns, QueryFilter<BattleColumns>> where, OrderBy<BattleColumns> orderBy = null)
		{
			return new BattleCollection(new Query<BattleColumns, Battle>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BattleCollection Where(WhereDelegate<BattleColumns> where, Database db = null)
		{
			return new BattleCollection(new Query<BattleColumns, Battle>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static BattleCollection Where(WhereDelegate<BattleColumns> where, OrderBy<BattleColumns> orderBy = null, Database db = null)
		{
			return new BattleCollection(new Query<BattleColumns, Battle>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<BattleColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static BattleCollection Where(QiQuery where, Database db = null)
		{
			return new BattleCollection(Select<BattleColumns>.From<Battle>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Battle instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Battle OneWhere(WhereDelegate<BattleColumns> where, Database db = null)
		{
			var results = new BattleCollection(Select<BattleColumns>.From<Battle>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<BattleColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Battle OneWhere(QiQuery where, Database db = null)
		{
			var results = new BattleCollection(Select<BattleColumns>.From<Battle>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Battle OneOrThrow(BattleCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Battle FirstOneWhere(WhereDelegate<BattleColumns> where, Database db = null)
		{
			var results = new BattleCollection(Select<BattleColumns>.From<Battle>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BattleCollection Top(int count, WhereDelegate<BattleColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static BattleCollection Top(int count, WhereDelegate<BattleColumns> where, OrderBy<BattleColumns> orderBy, Database database = null)
		{
			BattleColumns c = new BattleColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Battle>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Battle>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<BattleColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<BattleCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BattleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BattleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<BattleColumns> where, Database database = null)
		{
			BattleColumns c = new BattleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Battle>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Battle>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
