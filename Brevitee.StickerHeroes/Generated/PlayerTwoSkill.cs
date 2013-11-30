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
	[Brevitee.Data.Table("PlayerTwoSkill", "BattleStickers")]
	public partial class PlayerTwoSkill: Dao
	{
		public PlayerTwoSkill():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerTwoSkill(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerTwoSkill(DataRow data)
		{
			return new PlayerTwoSkill(data);
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
        Table="PlayerTwoSkill",
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
	
	// start SkillId -> SkillId
	[Brevitee.Data.ForeignKey(
        Table="PlayerTwoSkill",
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
			var colFilter = new PlayerTwoSkillColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerTwoSkillColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkillCollection Where(Func<PlayerTwoSkillColumns, QueryFilter<PlayerTwoSkillColumns>> where, OrderBy<PlayerTwoSkillColumns> orderBy = null)
		{
			return new PlayerTwoSkillCollection(new Query<PlayerTwoSkillColumns, PlayerTwoSkill>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkillCollection Where(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
		{
			return new PlayerTwoSkillCollection(new Query<PlayerTwoSkillColumns, PlayerTwoSkill>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkillCollection Where(WhereDelegate<PlayerTwoSkillColumns> where, OrderBy<PlayerTwoSkillColumns> orderBy = null, Database db = null)
		{
			return new PlayerTwoSkillCollection(new Query<PlayerTwoSkillColumns, PlayerTwoSkill>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoSkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoSkillCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerTwoSkillCollection(Select<PlayerTwoSkillColumns>.From<PlayerTwoSkill>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerTwoSkill instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkill OneWhere(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
		{
			var results = new PlayerTwoSkillCollection(Select<PlayerTwoSkillColumns>.From<PlayerTwoSkill>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerTwoSkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerTwoSkill OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerTwoSkillCollection(Select<PlayerTwoSkillColumns>.From<PlayerTwoSkill>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerTwoSkill OneOrThrow(PlayerTwoSkillCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkill FirstOneWhere(WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
		{
			var results = new PlayerTwoSkillCollection(Select<PlayerTwoSkillColumns>.From<PlayerTwoSkill>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkillCollection Top(int count, WhereDelegate<PlayerTwoSkillColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerTwoSkillCollection Top(int count, WhereDelegate<PlayerTwoSkillColumns> where, OrderBy<PlayerTwoSkillColumns> orderBy, Database database = null)
		{
			PlayerTwoSkillColumns c = new PlayerTwoSkillColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerTwoSkill>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerTwoSkill>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerTwoSkillColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerTwoSkillCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerTwoSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerTwoSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerTwoSkillColumns> where, Database database = null)
		{
			PlayerTwoSkillColumns c = new PlayerTwoSkillColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerTwoSkill>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerTwoSkill>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
