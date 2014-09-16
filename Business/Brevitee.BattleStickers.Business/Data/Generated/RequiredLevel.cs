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
	[Brevitee.Data.Table("RequiredLevel", "BattleStickers")]
	public partial class RequiredLevel: Dao
	{
		public RequiredLevel():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public RequiredLevel(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator RequiredLevel(DataRow data)
		{
			return new RequiredLevel(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("RequiredLevelCharacter_RequiredLevelId", new RequiredLevelCharacterCollection(new Query<RequiredLevelCharacterColumns, RequiredLevelCharacter>((c) => c.RequiredLevelId == this.Id), this, "RequiredLevelId"));	
            this.ChildCollections.Add("RequiredLevelWeapon_RequiredLevelId", new RequiredLevelWeaponCollection(new Query<RequiredLevelWeaponColumns, RequiredLevelWeapon>((c) => c.RequiredLevelId == this.Id), this, "RequiredLevelId"));	
            this.ChildCollections.Add("RequiredLevelSpell_RequiredLevelId", new RequiredLevelSpellCollection(new Query<RequiredLevelSpellColumns, RequiredLevelSpell>((c) => c.RequiredLevelId == this.Id), this, "RequiredLevelId"));	
            this.ChildCollections.Add("RequiredLevelSkill_RequiredLevelId", new RequiredLevelSkillCollection(new Query<RequiredLevelSkillColumns, RequiredLevelSkill>((c) => c.RequiredLevelId == this.Id), this, "RequiredLevelId"));				
            this.ChildCollections.Add("RequiredLevel_RequiredLevelCharacter_Character",  new XrefDaoCollection<RequiredLevelCharacter, Character>(this, false));
				
            this.ChildCollections.Add("RequiredLevel_RequiredLevelWeapon_Weapon",  new XrefDaoCollection<RequiredLevelWeapon, Weapon>(this, false));
				
            this.ChildCollections.Add("RequiredLevel_RequiredLevelSpell_Spell",  new XrefDaoCollection<RequiredLevelSpell, Spell>(this, false));
				
            this.ChildCollections.Add("RequiredLevel_RequiredLevelSkill_Skill",  new XrefDaoCollection<RequiredLevelSkill, Skill>(this, false));
							
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

	// property:Uuid, columnName:Uuid	
	[Brevitee.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				

	[Exclude]	
	public RequiredLevelCharacterCollection RequiredLevelCharactersByRequiredLevelId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelCharacter_RequiredLevelId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelCharacterCollection)this.ChildCollections["RequiredLevelCharacter_RequiredLevelId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public RequiredLevelWeaponCollection RequiredLevelWeaponsByRequiredLevelId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelWeapon_RequiredLevelId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelWeaponCollection)this.ChildCollections["RequiredLevelWeapon_RequiredLevelId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public RequiredLevelSpellCollection RequiredLevelSpellsByRequiredLevelId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelSpell_RequiredLevelId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelSpellCollection)this.ChildCollections["RequiredLevelSpell_RequiredLevelId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public RequiredLevelSkillCollection RequiredLevelSkillsByRequiredLevelId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RequiredLevelSkill_RequiredLevelId"))
			{
				SetChildren();
			}

			var c = (RequiredLevelSkillCollection)this.ChildCollections["RequiredLevelSkill_RequiredLevelId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<RequiredLevelCharacter, Character> Characters
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("RequiredLevel_RequiredLevelCharacter_Character"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelCharacter, Character>)this.ChildCollections["RequiredLevel_RequiredLevelCharacter_Character"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<RequiredLevelWeapon, Weapon> Weapons
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("RequiredLevel_RequiredLevelWeapon_Weapon"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelWeapon, Weapon>)this.ChildCollections["RequiredLevel_RequiredLevelWeapon_Weapon"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<RequiredLevelSpell, Spell> Spells
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("RequiredLevel_RequiredLevelSpell_Spell"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelSpell, Spell>)this.ChildCollections["RequiredLevel_RequiredLevelSpell_Spell"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<RequiredLevelSkill, Skill> Skills
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("RequiredLevel_RequiredLevelSkill_Skill"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<RequiredLevelSkill, Skill>)this.ChildCollections["RequiredLevel_RequiredLevelSkill_Skill"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new RequiredLevelColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the RequiredLevel table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static RequiredLevelCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<RequiredLevel>();
			Database db = database == null ? Db.For<RequiredLevel>(): database;
			var results = new RequiredLevelCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a RequiredLevelColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCollection Where(Func<RequiredLevelColumns, QueryFilter<RequiredLevelColumns>> where, OrderBy<RequiredLevelColumns> orderBy = null)
		{
			return new RequiredLevelCollection(new Query<RequiredLevelColumns, RequiredLevel>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCollection Where(WhereDelegate<RequiredLevelColumns> where, Database db = null)
		{
			var results = new RequiredLevelCollection(db, new Query<RequiredLevelColumns, RequiredLevel>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCollection Where(WhereDelegate<RequiredLevelColumns> where, OrderBy<RequiredLevelColumns> orderBy = null, Database db = null)
		{
			var results = new RequiredLevelCollection(db, new Query<RequiredLevelColumns, RequiredLevel>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequiredLevelColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RequiredLevelCollection Where(QiQuery where, Database db = null)
		{
			var results = new RequiredLevelCollection(db, Select<RequiredLevelColumns>.From<RequiredLevel>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single RequiredLevel instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevel OneWhere(WhereDelegate<RequiredLevelColumns> where, Database db = null)
		{
			var results = new RequiredLevelCollection(db, Select<RequiredLevelColumns>.From<RequiredLevel>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<RequiredLevelColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static RequiredLevel OneWhere(QiQuery where, Database db = null)
		{
			var results = new RequiredLevelCollection(db, Select<RequiredLevelColumns>.From<RequiredLevel>().Where(where, db));
			return OneOrThrow(results);
		}

		private static RequiredLevel OneOrThrow(RequiredLevelCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevel FirstOneWhere(WhereDelegate<RequiredLevelColumns> where, Database db = null)
		{
			var results = new RequiredLevelCollection(db, Select<RequiredLevelColumns>.From<RequiredLevel>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCollection Top(int count, WhereDelegate<RequiredLevelColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static RequiredLevelCollection Top(int count, WhereDelegate<RequiredLevelColumns> where, OrderBy<RequiredLevelColumns> orderBy, Database database = null)
		{
			RequiredLevelColumns c = new RequiredLevelColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<RequiredLevel>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<RequiredLevel>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RequiredLevelColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<RequiredLevelCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a RequiredLevelColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between RequiredLevelColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<RequiredLevelColumns> where, Database database = null)
		{
			RequiredLevelColumns c = new RequiredLevelColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<RequiredLevel>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<RequiredLevel>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
