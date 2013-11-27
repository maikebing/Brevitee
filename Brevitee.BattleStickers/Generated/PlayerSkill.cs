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
	[Brevitee.Data.Table("PlayerSkill", "BattleStickers")]
	public partial class PlayerSkill: Dao
	{
		public PlayerSkill():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PlayerSkill(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PlayerSkill(DataRow data)
		{
			return new PlayerSkill(data);
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



	// start PlayerId -> PlayerId
	[Brevitee.Data.ForeignKey(
        Table="PlayerSkill",
		Name="PlayerId", 
		ExtractedType="", 
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
				_playerOfPlayerId = Brevitee.BattleStickers.Player.OneWhere(f => f.Id == this.PlayerId);
			}
			return _playerOfPlayerId;
		}
	}
	
	// start SkillId -> SkillId
	[Brevitee.Data.ForeignKey(
        Table="PlayerSkill",
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
				_skillOfSkillId = Brevitee.BattleStickers.Skill.OneWhere(f => f.Id == this.SkillId);
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
			var colFilter = new PlayerSkillColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PlayerSkillColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkillCollection Where(Func<PlayerSkillColumns, QueryFilter<PlayerSkillColumns>> where, OrderBy<PlayerSkillColumns> orderBy = null)
		{
			return new PlayerSkillCollection(new Query<PlayerSkillColumns, PlayerSkill>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkillCollection Where(WhereDelegate<PlayerSkillColumns> where, Database db = null)
		{
			return new PlayerSkillCollection(new Query<PlayerSkillColumns, PlayerSkill>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkillCollection Where(WhereDelegate<PlayerSkillColumns> where, OrderBy<PlayerSkillColumns> orderBy = null, Database db = null)
		{
			return new PlayerSkillCollection(new Query<PlayerSkillColumns, PlayerSkill>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerSkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerSkillCollection Where(QiQuery where, Database db = null)
		{
			return new PlayerSkillCollection(Select<PlayerSkillColumns>.From<PlayerSkill>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PlayerSkill instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkill OneWhere(WhereDelegate<PlayerSkillColumns> where, Database db = null)
		{
			var results = new PlayerSkillCollection(Select<PlayerSkillColumns>.From<PlayerSkill>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PlayerSkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PlayerSkill OneWhere(QiQuery where, Database db = null)
		{
			var results = new PlayerSkillCollection(Select<PlayerSkillColumns>.From<PlayerSkill>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PlayerSkill OneOrThrow(PlayerSkillCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkill FirstOneWhere(WhereDelegate<PlayerSkillColumns> where, Database db = null)
		{
			var results = new PlayerSkillCollection(Select<PlayerSkillColumns>.From<PlayerSkill>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkillCollection Top(int count, WhereDelegate<PlayerSkillColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PlayerSkillCollection Top(int count, WhereDelegate<PlayerSkillColumns> where, OrderBy<PlayerSkillColumns> orderBy, Database database = null)
		{
			PlayerSkillColumns c = new PlayerSkillColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PlayerSkill>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PlayerSkill>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PlayerSkillColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PlayerSkillCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PlayerSkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PlayerSkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PlayerSkillColumns> where, Database database = null)
		{
			PlayerSkillColumns c = new PlayerSkillColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PlayerSkill>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PlayerSkill>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
