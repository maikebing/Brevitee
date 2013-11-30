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
	[Brevitee.Data.Table("RequiredLevelCharacter", "BattleStickers")]
	public partial class RequiredLevelCharacter: Dao
	{
		public RequiredLevelCharacter():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public RequiredLevelCharacter(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator RequiredLevelCharacter(DataRow data)
		{
			return new RequiredLevelCharacter(data);
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



	// start RequiredLevelId -> RequiredLevelId
	[Brevitee.Data.ForeignKey(
        Table="RequiredLevelCharacter",
		Name="RequiredLevelId", 
		ExtractedType="", 
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
				_requiredLevelOfRequiredLevelId = Brevitee.StickerHeroes.RequiredLevel.OneWhere(f => f.Id == this.RequiredLevelId);
			}
			return _requiredLevelOfRequiredLevelId;
		}
	}
	
	// start CharacterId -> CharacterId
	[Brevitee.Data.ForeignKey(
        Table="RequiredLevelCharacter",
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
			var colFilter = new RequiredLevelCharacterColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacterCollection Where(Func<RequiredLevelCharacterColumns, QueryFilter<RequiredLevelCharacterColumns>> where, OrderBy<RequiredLevelCharacterColumns> orderBy = null)
		{
			return new RequiredLevelCharacterCollection(new Query<RequiredLevelCharacterColumns, RequiredLevelCharacter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacterCollection Where(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
		{
			return new RequiredLevelCharacterCollection(new Query<RequiredLevelCharacterColumns, RequiredLevelCharacter>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacterCollection Where(WhereDelegate<RequiredLevelCharacterColumns> where, OrderBy<RequiredLevelCharacterColumns> orderBy = null, Database db = null)
		{
			return new RequiredLevelCharacterCollection(new Query<RequiredLevelCharacterColumns, RequiredLevelCharacter>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequiredLevelCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RequiredLevelCharacterCollection Where(QiQuery where, Database db = null)
		{
			return new RequiredLevelCharacterCollection(Select<RequiredLevelCharacterColumns>.From<RequiredLevelCharacter>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single RequiredLevelCharacter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacter OneWhere(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
		{
			var results = new RequiredLevelCharacterCollection(Select<RequiredLevelCharacterColumns>.From<RequiredLevelCharacter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequiredLevelCharacterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RequiredLevelCharacter OneWhere(QiQuery where, Database db = null)
		{
			var results = new RequiredLevelCharacterCollection(Select<RequiredLevelCharacterColumns>.From<RequiredLevelCharacter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static RequiredLevelCharacter OneOrThrow(RequiredLevelCharacterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacter FirstOneWhere(WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
		{
			var results = new RequiredLevelCharacterCollection(Select<RequiredLevelCharacterColumns>.From<RequiredLevelCharacter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacterCollection Top(int count, WhereDelegate<RequiredLevelCharacterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCharacterCollection Top(int count, WhereDelegate<RequiredLevelCharacterColumns> where, OrderBy<RequiredLevelCharacterColumns> orderBy, Database database = null)
		{
			RequiredLevelCharacterColumns c = new RequiredLevelCharacterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<RequiredLevelCharacter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<RequiredLevelCharacter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RequiredLevelCharacterColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<RequiredLevelCharacterCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelCharacterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelCharacterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<RequiredLevelCharacterColumns> where, Database database = null)
		{
			RequiredLevelCharacterColumns c = new RequiredLevelCharacterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<RequiredLevelCharacter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<RequiredLevelCharacter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
