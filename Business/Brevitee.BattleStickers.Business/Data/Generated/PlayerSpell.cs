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
	[Brevitee.Data.Table("PlayerSpell", "BattleStickers")]
	public partial class PlayerSpell: Dao
	{
		public PlayerSpell():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerSpell(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerSpell(DataRow data)
		{
			return new PlayerSpell(data);
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
        Table="PlayerSpell",
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
	
	// start SpellId -> SpellId
	[Brevitee.Data.ForeignKey(
        Table="PlayerSpell",
		Name="SpellId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Spell",
		Suffix="2")]
	public long? SpellId
	{
		get
		{
			return GetLongValue("SpellId");
		}
		set
		{
			SetValue("SpellId", value);
		}
	}

	Spell _spellOfSpellId;
	public Spell SpellOfSpellId
	{
		get
		{
			if(_spellOfSpellId == null)
			{
				_spellOfSpellId = Brevitee.BattleStickers.Business.Data.Spell.OneWhere(f => f.Id == this.SpellId);
			}
			return _spellOfSpellId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerSpellColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerSpell table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerSpellCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerSpell>();
			Database db = database == null ? Db.For<PlayerSpell>(): database;
			var results = new PlayerSpellCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerSpellColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpellCollection Where(Func<PlayerSpellColumns, QueryFilter<PlayerSpellColumns>> where, OrderBy<PlayerSpellColumns> orderBy = null)
		{
			return new PlayerSpellCollection(new Query<PlayerSpellColumns, PlayerSpell>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpellCollection Where(WhereDelegate<PlayerSpellColumns> where, Database db = null)
		{
			var results = new PlayerSpellCollection(db, new Query<PlayerSpellColumns, PlayerSpell>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpellCollection Where(WhereDelegate<PlayerSpellColumns> where, OrderBy<PlayerSpellColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerSpellCollection(db, new Query<PlayerSpellColumns, PlayerSpell>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerSpellCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerSpellCollection(db, Select<PlayerSpellColumns>.From<PlayerSpell>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerSpell instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpell OneWhere(WhereDelegate<PlayerSpellColumns> where, Database db = null)
		{
			var results = new PlayerSpellCollection(db, Select<PlayerSpellColumns>.From<PlayerSpell>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerSpell OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerSpellCollection(db, Select<PlayerSpellColumns>.From<PlayerSpell>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerSpell OneOrThrow(PlayerSpellCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpell FirstOneWhere(WhereDelegate<PlayerSpellColumns> where, Database db = null)
		{
			var results = new PlayerSpellCollection(db, Select<PlayerSpellColumns>.From<PlayerSpell>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpellCollection Top(int count, WhereDelegate<PlayerSpellColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerSpellCollection Top(int count, WhereDelegate<PlayerSpellColumns> where, OrderBy<PlayerSpellColumns> orderBy, Database database = null)
		{
			PlayerSpellColumns c = new PlayerSpellColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerSpell>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerSpell>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerSpellColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerSpellCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerSpellColumns> where, Database database = null)
		{
			PlayerSpellColumns c = new PlayerSpellColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerSpell>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerSpell>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
