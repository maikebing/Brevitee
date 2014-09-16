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
	[Brevitee.Data.Table("RequiredLevelSpell", "BattleStickers")]
	public partial class RequiredLevelSpell: Dao
	{
		public RequiredLevelSpell():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public RequiredLevelSpell(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator RequiredLevelSpell(DataRow data)
		{
			return new RequiredLevelSpell(data);
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



	// start RequiredLevelId -> RequiredLevelId
	[Brevitee.Data.ForeignKey(
        Table="RequiredLevelSpell",
		Name="RequiredLevelId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="RequiredLevel",
		Suffix="1")]
	public long? RequiredLevelId
	{
		get
		{
			return GetLongValue("RequiredLevelId");
		}
		set
		{
			SetValue("RequiredLevelId", value);
		}
	}

	RequiredLevel _requiredLevelOfRequiredLevelId;
	public RequiredLevel RequiredLevelOfRequiredLevelId
	{
		get
		{
			if(_requiredLevelOfRequiredLevelId == null)
			{
				_requiredLevelOfRequiredLevelId = Brevitee.BattleStickers.Business.Data.RequiredLevel.OneWhere(f => f.Id == this.RequiredLevelId);
			}
			return _requiredLevelOfRequiredLevelId;
		}
	}
	
	// start SpellId -> SpellId
	[Brevitee.Data.ForeignKey(
        Table="RequiredLevelSpell",
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
			var colFilter = new RequiredLevelSpellColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the RequiredLevelSpell table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static RequiredLevelSpellCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<RequiredLevelSpell>();
			Database db = database == null ? Db.For<RequiredLevelSpell>(): database;
			var results = new RequiredLevelSpellCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a RequiredLevelSpellColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpellCollection Where(Func<RequiredLevelSpellColumns, QueryFilter<RequiredLevelSpellColumns>> where, OrderBy<RequiredLevelSpellColumns> orderBy = null)
		{
			return new RequiredLevelSpellCollection(new Query<RequiredLevelSpellColumns, RequiredLevelSpell>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpellCollection Where(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
		{
			var results = new RequiredLevelSpellCollection(db, new Query<RequiredLevelSpellColumns, RequiredLevelSpell>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpellCollection Where(WhereDelegate<RequiredLevelSpellColumns> where, OrderBy<RequiredLevelSpellColumns> orderBy = null, Database db = null)
		{
			var results = new RequiredLevelSpellCollection(db, new Query<RequiredLevelSpellColumns, RequiredLevelSpell>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequiredLevelSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RequiredLevelSpellCollection Where(QiQuery where, Database db = null)
		{
			var results = new RequiredLevelSpellCollection(db, Select<RequiredLevelSpellColumns>.From<RequiredLevelSpell>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single RequiredLevelSpell instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpell OneWhere(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
		{
			var results = new RequiredLevelSpellCollection(db, Select<RequiredLevelSpellColumns>.From<RequiredLevelSpell>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequiredLevelSpellColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RequiredLevelSpell OneWhere(QiQuery where, Database db = null)
		{
			var results = new RequiredLevelSpellCollection(db, Select<RequiredLevelSpellColumns>.From<RequiredLevelSpell>().Where(where, db));
			return OneOrThrow(results);
		}

		private static RequiredLevelSpell OneOrThrow(RequiredLevelSpellCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpell FirstOneWhere(WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
		{
			var results = new RequiredLevelSpellCollection(db, Select<RequiredLevelSpellColumns>.From<RequiredLevelSpell>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpellCollection Top(int count, WhereDelegate<RequiredLevelSpellColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelSpellCollection Top(int count, WhereDelegate<RequiredLevelSpellColumns> where, OrderBy<RequiredLevelSpellColumns> orderBy, Database database = null)
		{
			RequiredLevelSpellColumns c = new RequiredLevelSpellColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<RequiredLevelSpell>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<RequiredLevelSpell>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RequiredLevelSpellColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<RequiredLevelSpellCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelSpellColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelSpellColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<RequiredLevelSpellColumns> where, Database database = null)
		{
			RequiredLevelSpellColumns c = new RequiredLevelSpellColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<RequiredLevelSpell>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<RequiredLevelSpell>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
