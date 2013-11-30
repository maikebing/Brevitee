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
	[Brevitee.Data.Table("EffectOverTime", "BattleStickers")]
	public partial class EffectOverTime: Dao
	{
		public EffectOverTime():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public EffectOverTime(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator EffectOverTime(DataRow data)
		{
			return new EffectOverTime(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Weapon_EffectOverTimeId", new WeaponCollection(new Query<WeaponColumns, Weapon>((c) => c.EffectOverTimeId == this.Id), this, "EffectOverTimeId"));	
            this.ChildCollections.Add("Spell_EffectOverTimeId", new SpellCollection(new Query<SpellColumns, Spell>((c) => c.EffectOverTimeId == this.Id), this, "EffectOverTimeId"));	
            this.ChildCollections.Add("Skill_EffectOverTimeId", new SkillCollection(new Query<SkillColumns, Skill>((c) => c.EffectOverTimeId == this.Id), this, "EffectOverTimeId"));							
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

	// property:Speed, columnName:Speed	
	[Brevitee.Data.Column(Name="Speed", ExtractedType="", MaxLength="", AllowNull=false)]
	public int? Speed
	{
		get
		{
			return GetIntValue("Speed");
		}
		set
		{
			SetValue("Speed", value);
		}
	}



				
	[Exclude]	
	public WeaponCollection WeaponsByEffectOverTimeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Weapon_EffectOverTimeId"))
			{
				SetChildren();
			}

			var c = (WeaponCollection)this.ChildCollections["Weapon_EffectOverTimeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public SpellCollection SpellsByEffectOverTimeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Spell_EffectOverTimeId"))
			{
				SetChildren();
			}

			var c = (SpellCollection)this.ChildCollections["Spell_EffectOverTimeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public SkillCollection SkillsByEffectOverTimeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Skill_EffectOverTimeId"))
			{
				SetChildren();
			}

			var c = (SkillCollection)this.ChildCollections["Skill_EffectOverTimeId"];
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
			var colFilter = new EffectOverTimeColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EffectOverTimeColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTimeCollection Where(Func<EffectOverTimeColumns, QueryFilter<EffectOverTimeColumns>> where, OrderBy<EffectOverTimeColumns> orderBy = null)
		{
			return new EffectOverTimeCollection(new Query<EffectOverTimeColumns, EffectOverTime>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTimeCollection Where(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
		{
			return new EffectOverTimeCollection(new Query<EffectOverTimeColumns, EffectOverTime>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTimeCollection Where(WhereDelegate<EffectOverTimeColumns> where, OrderBy<EffectOverTimeColumns> orderBy = null, Database db = null)
		{
			return new EffectOverTimeCollection(new Query<EffectOverTimeColumns, EffectOverTime>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EffectOverTimeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EffectOverTimeCollection Where(QiQuery where, Database db = null)
		{
			return new EffectOverTimeCollection(Select<EffectOverTimeColumns>.From<EffectOverTime>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single EffectOverTime instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTime OneWhere(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
		{
			var results = new EffectOverTimeCollection(Select<EffectOverTimeColumns>.From<EffectOverTime>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EffectOverTimeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EffectOverTime OneWhere(QiQuery where, Database db = null)
		{
			var results = new EffectOverTimeCollection(Select<EffectOverTimeColumns>.From<EffectOverTime>().Where(where, db));
			return OneOrThrow(results);
		}

		private static EffectOverTime OneOrThrow(EffectOverTimeCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTime FirstOneWhere(WhereDelegate<EffectOverTimeColumns> where, Database db = null)
		{
			var results = new EffectOverTimeCollection(Select<EffectOverTimeColumns>.From<EffectOverTime>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTimeCollection Top(int count, WhereDelegate<EffectOverTimeColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EffectOverTimeCollection Top(int count, WhereDelegate<EffectOverTimeColumns> where, OrderBy<EffectOverTimeColumns> orderBy, Database database = null)
		{
			EffectOverTimeColumns c = new EffectOverTimeColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<EffectOverTime>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<EffectOverTime>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EffectOverTimeColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<EffectOverTimeCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EffectOverTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EffectOverTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<EffectOverTimeColumns> where, Database database = null)
		{
			EffectOverTimeColumns c = new EffectOverTimeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<EffectOverTime>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<EffectOverTime>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
