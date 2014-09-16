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
	[Brevitee.Data.Table("PlayerOneSpell", "BattleStickers")]
	public partial class PlayerOneSpell: Dao
	{
		public PlayerOneSpell():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerOneSpell(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerOneSpell(DataRow data)
		{
			return new PlayerOneSpell(data);
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
        Table="PlayerOneSpell",
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
	
	// start SpellId -> SpellId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneSpell",
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
			var colFilter = new PlayerOneSpellColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PlayerOneSpell table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PlayerOneSpellCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PlayerOneSpell>();
			Database db = database == null ? Db.For<PlayerOneSpell>(): database;
			var results = new PlayerOneSpellCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerOneSpellColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpellCollection Where(Func<PlayerOneSpellColumns, QueryFilter<PlayerOneSpellColumns>> where, OrderBy<PlayerOneSpellColumns> orderBy = null)
		{
			return new PlayerOneSpellCollection(new Query<PlayerOneSpellColumns, PlayerOneSpell>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpellCollection Where(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
		{
			var results = new PlayerOneSpellCollection(db, new Query<PlayerOneSpellColumns, PlayerOneSpell>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpellCollection Where(WhereDelegate<PlayerOneSpellColumns> where, OrderBy<PlayerOneSpellColumns> orderBy = null, Database db = null)
		{
			var results = new PlayerOneSpellCollection(db, new Query<PlayerOneSpellColumns, PlayerOneSpell>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneSpellCollection Where(QiQuery where, Database db = null)
		{
			var results = new PlayerOneSpellCollection(db, Select<PlayerOneSpellColumns>.From<PlayerOneSpell>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerOneSpell instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpell OneWhere(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
		{
			var results = new PlayerOneSpellCollection(db, Select<PlayerOneSpellColumns>.From<PlayerOneSpell>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneSpell OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerOneSpellCollection(db, Select<PlayerOneSpellColumns>.From<PlayerOneSpell>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerOneSpell OneOrThrow(PlayerOneSpellCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpell FirstOneWhere(WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
		{
			var results = new PlayerOneSpellCollection(db, Select<PlayerOneSpellColumns>.From<PlayerOneSpell>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpellCollection Top(int count, WhereDelegate<PlayerOneSpellColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSpellCollection Top(int count, WhereDelegate<PlayerOneSpellColumns> where, OrderBy<PlayerOneSpellColumns> orderBy, Database database = null)
		{
			PlayerOneSpellColumns c = new PlayerOneSpellColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PlayerOneSpell>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerOneSpell>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerOneSpellColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PlayerOneSpellCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerOneSpellColumns> where, Database database = null)
		{
			PlayerOneSpellColumns c = new PlayerOneSpellColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PlayerOneSpell>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerOneSpell>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
