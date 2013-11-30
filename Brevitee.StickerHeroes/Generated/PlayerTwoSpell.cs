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
	[Brevitee.Data.Table("PlayerTwoSpell", "BattleStickers")]
	public partial class PlayerTwoSpell: Dao
	{
		public PlayerTwoSpell():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwoSpell(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwoSpell(DataRow data)
		{
			return new PlayerTwoSpell(data);
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
        Table="PlayerTwoSpell",
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
				_playerTwoOfPlayerTwoId = Brevitee.StickerHeroes.PlayerTwo.OneWhere(f => f.Id == this.PlayerTwoId);
			}
			return _playerTwoOfPlayerTwoId;
		}
	}
	
	// start SpellId -> SpellId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoSpell",
		Name="SpellId", 
		ExtractedType="", 
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
				_spellOfSpellId = Brevitee.StickerHeroes.Spell.OneWhere(f => f.Id == this.SpellId);
			}
			return _spellOfSpellId;
		}
	}
	
				
		


		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerTwoSpellColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoSpellColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpellCollection Where(Func<PlayerTwoSpellColumns, QueryFilter<PlayerTwoSpellColumns>> where, OrderBy<PlayerTwoSpellColumns> orderBy = null)
		{
			return new PlayerTwoSpellCollection(new Query<PlayerTwoSpellColumns, PlayerTwoSpell>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpellCollection Where(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
		{
			return new PlayerTwoSpellCollection(new Query<PlayerTwoSpellColumns, PlayerTwoSpell>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpellCollection Where(WhereDelegate<PlayerTwoSpellColumns> where, OrderBy<PlayerTwoSpellColumns> orderBy = null, Database db = null)
		{
			return new PlayerTwoSpellCollection(new Query<PlayerTwoSpellColumns, PlayerTwoSpell>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoSpellCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerTwoSpellCollection(Select<PlayerTwoSpellColumns>.From<PlayerTwoSpell>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwoSpell instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpell OneWhere(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
		{
			var results = new PlayerTwoSpellCollection(Select<PlayerTwoSpellColumns>.From<PlayerTwoSpell>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoSpell OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoSpellCollection(Select<PlayerTwoSpellColumns>.From<PlayerTwoSpell>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwoSpell OneOrThrow(PlayerTwoSpellCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpell FirstOneWhere(WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
		{
			var results = new PlayerTwoSpellCollection(Select<PlayerTwoSpellColumns>.From<PlayerTwoSpell>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpellCollection Top(int count, WhereDelegate<PlayerTwoSpellColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSpellCollection Top(int count, WhereDelegate<PlayerTwoSpellColumns> where, OrderBy<PlayerTwoSpellColumns> orderBy, Database database = null)
		{
			PlayerTwoSpellColumns c = new PlayerTwoSpellColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerTwoSpell>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwoSpell>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoSpellColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerTwoSpellCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoSpellColumns> where, Database database = null)
		{
			PlayerTwoSpellColumns c = new PlayerTwoSpellColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerTwoSpell>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwoSpell>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
