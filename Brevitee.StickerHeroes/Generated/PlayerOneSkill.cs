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
	[Brevitee.Data.Table("PlayerOneSkill", "BattleStickers")]
	public partial class PlayerOneSkill: Dao
	{
		public PlayerOneSkill():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerOneSkill(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerOneSkill(DataRow data)
		{
			return new PlayerOneSkill(data);
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
        Table="PlayerOneSkill",
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
				_playerOneOfPlayerOneId = Brevitee.StickerHeroes.PlayerOne.OneWhere(f => f.Id == this.PlayerOneId);
			}
			return _playerOneOfPlayerOneId;
		}
	}
	
	// start SkillId -> SkillId
	[Brevitee.Data.ForeignKey(
        Table="PlayerOneSkill",
		Name="SkillId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Skill",
		Suffix="2")]
	public long? SkillId
	{
		get
		{
			return GetLongValue("SkillId");
		}
		set
		{
			SetValue("SkillId", value);
		}
	}

	Skill _skillOfSkillId;
	public Skill SkillOfSkillId
	{
		get
		{
			if(_skillOfSkillId == null)
			{
				_skillOfSkillId = Brevitee.StickerHeroes.Skill.OneWhere(f => f.Id == this.SkillId);
			}
			return _skillOfSkillId;
		}
	}
	
				
		


		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PlayerOneSkillColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerOneSkillColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkillCollection Where(Func<PlayerOneSkillColumns, QueryFilter<PlayerOneSkillColumns>> where, OrderBy<PlayerOneSkillColumns> orderBy = null)
		{
			return new PlayerOneSkillCollection(new Query<PlayerOneSkillColumns, PlayerOneSkill>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkillCollection Where(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
		{
			return new PlayerOneSkillCollection(new Query<PlayerOneSkillColumns, PlayerOneSkill>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkillCollection Where(WhereDelegate<PlayerOneSkillColumns> where, OrderBy<PlayerOneSkillColumns> orderBy = null, Database db = null)
		{
			return new PlayerOneSkillCollection(new Query<PlayerOneSkillColumns, PlayerOneSkill>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneSkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneSkillCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerOneSkillCollection(Select<PlayerOneSkillColumns>.From<PlayerOneSkill>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerOneSkill instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkill OneWhere(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
		{
			var results = new PlayerOneSkillCollection(Select<PlayerOneSkillColumns>.From<PlayerOneSkill>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerOneSkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerOneSkill OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerOneSkillCollection(Select<PlayerOneSkillColumns>.From<PlayerOneSkill>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerOneSkill OneOrThrow(PlayerOneSkillCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkill FirstOneWhere(WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
		{
			var results = new PlayerOneSkillCollection(Select<PlayerOneSkillColumns>.From<PlayerOneSkill>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkillCollection Top(int count, WhereDelegate<PlayerOneSkillColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerOneSkillCollection Top(int count, WhereDelegate<PlayerOneSkillColumns> where, OrderBy<PlayerOneSkillColumns> orderBy, Database database = null)
		{
			PlayerOneSkillColumns c = new PlayerOneSkillColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerOneSkill>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerOneSkill>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerOneSkillColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerOneSkillCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerOneSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerOneSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerOneSkillColumns> where, Database database = null)
		{
			PlayerOneSkillColumns c = new PlayerOneSkillColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerOneSkill>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerOneSkill>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
