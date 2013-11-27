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
	[Brevitee.Data.Table("Skill", "BattleStickers")]
	public partial class Skill: Dao
	{
		public Skill():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Skill(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Skill(DataRow data)
		{
			return new Skill(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("PlayerSkill_SkillId", new PlayerSkillCollection(new Query<PlayerSkillColumns, PlayerSkill>((c) => c.SkillId == this.Id), this, "SkillId"));	
            this.ChildCollections.Add("PlayerOneSkill_SkillId", new PlayerOneSkillCollection(new Query<PlayerOneSkillColumns, PlayerOneSkill>((c) => c.SkillId == this.Id), this, "SkillId"));	
            this.ChildCollections.Add("PlayerTwoSkill_SkillId", new PlayerTwoSkillCollection(new Query<PlayerTwoSkillColumns, PlayerTwoSkill>((c) => c.SkillId == this.Id), this, "SkillId"));	
            this.ChildCollections.Add("RequiredLevelSkill_SkillId", new RequiredLevelSkillCollection(new Query<RequiredLevelSkillColumns, RequiredLevelSkill>((c) => c.SkillId == this.Id), this, "SkillId"));							
            this.ChildCollections.Add("Skill_PlayerSkill_Player",  new XrefDaoCollection<PlayerSkill, Player>(this, false));
				
            this.ChildCollections.Add("Skill_PlayerOneSkill_PlayerOne",  new XrefDaoCollection<PlayerOneSkill, PlayerOne>(this, false));
				
            this.ChildCollections.Add("Skill_PlayerTwoSkill_PlayerTwo",  new XrefDaoCollection<PlayerTwoSkill, PlayerTwo>(this, false));
				
            this.ChildCollections.Add("Skill_RequiredLevelSkill_RequiredLevel",  new XrefDaoCollection<RequiredLevelSkill, RequiredLevel>(this, false));
				
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", ExtractedType="", MaxLength="", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}

	// property:Strength, columnName:Strength	
	[Brevitee.Data.Column(Name="Strength", ExtractedType="", MaxLength="", AllowNull=false)]
	public int? Strength
	{
		get
		{
			return GetIntValue("Strength");
		}
		set
		{
			SetValue("Strength", value);
		}
	}

	// property:Element, columnName:Element	
	[Brevitee.Data.Column(Name="Element", ExtractedType="", MaxLength="", AllowNull=false)]
	public string Element
	{
		get
		{
			return GetStringValue("Element");
		}
		set
		{
			SetValue("Element", value);
		}
	}



	// start EffectOverTimeId -> EffectOverTimeId
	[Brevitee.Data.ForeignKey(
        Table="Skill",
		Name="EffectOverTimeId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="EffectOverTime",
		Suffix="1")]
	public long? EffectOverTimeId
	{
		get
		{
			return GetLongValue("EffectOverTimeId");
		}
		set
		{
			SetValue("EffectOverTimeId", value);
		}
	}

	EffectOverTime _effectOverTimeOfEffectOverTimeId;
	public EffectOverTime EffectOverTimeOfEffectOverTimeId
	{
		get
		{
			if(_effectOverTimeOfEffectOverTimeId == null)
			{
				_effectOverTimeOfEffectOverTimeId = Brevitee.BattleStickers.EffectOverTime.OneWhere(f => f.Id == this.EffectOverTimeId);
			}
			return _effectOverTimeOfEffectOverTimeId;
		}
	}
	
				
	[Exclude]	
	public PlayerSkillCollection PlayerSkillsBySkillId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerSkill_SkillId"))
			{
				SetChildren();
			}

			var c = (PlayerSkillCollection)this.ChildCollections["PlayerSkill_SkillId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerOneSkillCollection PlayerOneSkillsBySkillId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneSkill_SkillId"))
			{
				SetChildren();
			}

			var c = (PlayerOneSkillCollection)this.ChildCollections["PlayerOneSkill_SkillId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoSkillCollection PlayerTwoSkillsBySkillId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoSkill_SkillId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoSkillCollection)this.ChildCollections["PlayerTwoSkill_SkillId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public RequiredLevelSkillCollection RequiredLevelSkillsBySkillId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelSkill_SkillId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelSkillCollection)this.ChildCollections["RequiredLevelSkill_SkillId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<PlayerSkill, Player> Players
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Skill_PlayerSkill_Player"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerSkill, Player>)this.ChildCollections["Skill_PlayerSkill_Player"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneSkill, PlayerOne> PlayerOnes
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Skill_PlayerOneSkill_PlayerOne"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneSkill, PlayerOne>)this.ChildCollections["Skill_PlayerOneSkill_PlayerOne"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoSkill, PlayerTwo> PlayerTwos
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Skill_PlayerTwoSkill_PlayerTwo"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoSkill, PlayerTwo>)this.ChildCollections["Skill_PlayerTwoSkill_PlayerTwo"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<RequiredLevelSkill, RequiredLevel> RequiredLevels
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Skill_RequiredLevelSkill_RequiredLevel"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelSkill, RequiredLevel>)this.ChildCollections["Skill_RequiredLevelSkill_RequiredLevel"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new SkillColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SkillColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SkillCollection Where(Func<SkillColumns, QueryFilter<SkillColumns>> where, OrderBy<SkillColumns> orderBy = null)
		{
			return new SkillCollection(new Query<SkillColumns, Skill>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SkillCollection Where(WhereDelegate<SkillColumns> where, Database db = null)
		{
			return new SkillCollection(new Query<SkillColumns, Skill>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SkillCollection Where(WhereDelegate<SkillColumns> where, OrderBy<SkillColumns> orderBy = null, Database db = null)
		{
			return new SkillCollection(new Query<SkillColumns, Skill>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SkillCollection Where(QiQuery where, Database db = null)
		{
			return new SkillCollection(Select<SkillColumns>.From<Skill>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Skill instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Skill OneWhere(WhereDelegate<SkillColumns> where, Database db = null)
		{
			var results = new SkillCollection(Select<SkillColumns>.From<Skill>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SkillColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Skill OneWhere(QiQuery where, Database db = null)
		{
			var results = new SkillCollection(Select<SkillColumns>.From<Skill>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Skill OneOrThrow(SkillCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Skill FirstOneWhere(WhereDelegate<SkillColumns> where, Database db = null)
		{
			var results = new SkillCollection(Select<SkillColumns>.From<Skill>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SkillCollection Top(int count, WhereDelegate<SkillColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SkillCollection Top(int count, WhereDelegate<SkillColumns> where, OrderBy<SkillColumns> orderBy, Database database = null)
		{
			SkillColumns c = new SkillColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Skill>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Skill>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SkillColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<SkillCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SkillColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SkillColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SkillColumns> where, Database database = null)
		{
			SkillColumns c = new SkillColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Skill>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Skill>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
