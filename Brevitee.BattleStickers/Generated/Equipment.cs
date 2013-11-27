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
	[Brevitee.Data.Table("Equipment", "BattleStickers")]
	public partial class Equipment: Dao
	{
		public Equipment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Equipment(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Equipment(DataRow data)
		{
			return new Equipment(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Effect_EquipmentId", new EffectCollection(new Query<EffectColumns, Effect>((c) => c.EquipmentId == this.Id), this, "EquipmentId"));	
            this.ChildCollections.Add("PlayerEquipment_EquipmentId", new PlayerEquipmentCollection(new Query<PlayerEquipmentColumns, PlayerEquipment>((c) => c.EquipmentId == this.Id), this, "EquipmentId"));	
            this.ChildCollections.Add("PlayerOneEquipment_EquipmentId", new PlayerOneEquipmentCollection(new Query<PlayerOneEquipmentColumns, PlayerOneEquipment>((c) => c.EquipmentId == this.Id), this, "EquipmentId"));	
            this.ChildCollections.Add("PlayerTwoEquipment_EquipmentId", new PlayerTwoEquipmentCollection(new Query<PlayerTwoEquipmentColumns, PlayerTwoEquipment>((c) => c.EquipmentId == this.Id), this, "EquipmentId"));							
            this.ChildCollections.Add("Equipment_PlayerEquipment_Player",  new XrefDaoCollection<PlayerEquipment, Player>(this, false));
				
            this.ChildCollections.Add("Equipment_PlayerOneEquipment_PlayerOne",  new XrefDaoCollection<PlayerOneEquipment, PlayerOne>(this, false));
				
            this.ChildCollections.Add("Equipment_PlayerTwoEquipment_PlayerTwo",  new XrefDaoCollection<PlayerTwoEquipment, PlayerTwo>(this, false));
				
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



				
	[Exclude]	
	public EffectCollection EffectsByEquipmentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Effect_EquipmentId"))
			{
				SetChildren();
			}

			var c = (EffectCollection)this.ChildCollections["Effect_EquipmentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerEquipmentCollection PlayerEquipmentsByEquipmentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerEquipment_EquipmentId"))
			{
				SetChildren();
			}

			var c = (PlayerEquipmentCollection)this.ChildCollections["PlayerEquipment_EquipmentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerOneEquipmentCollection PlayerOneEquipmentsByEquipmentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerOneEquipment_EquipmentId"))
			{
				SetChildren();
			}

			var c = (PlayerOneEquipmentCollection)this.ChildCollections["PlayerOneEquipment_EquipmentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public PlayerTwoEquipmentCollection PlayerTwoEquipmentsByEquipmentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PlayerTwoEquipment_EquipmentId"))
			{
				SetChildren();
			}

			var c = (PlayerTwoEquipmentCollection)this.ChildCollections["PlayerTwoEquipment_EquipmentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			


		// Xref       
        public XrefDaoCollection<PlayerEquipment, Player> Players
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Equipment_PlayerEquipment_Player"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerEquipment, Player>)this.ChildCollections["Equipment_PlayerEquipment_Player"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerOneEquipment, PlayerOne> PlayerOnes
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Equipment_PlayerOneEquipment_PlayerOne"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerOneEquipment, PlayerOne>)this.ChildCollections["Equipment_PlayerOneEquipment_PlayerOne"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		// Xref       
        public XrefDaoCollection<PlayerTwoEquipment, PlayerTwo> PlayerTwos
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Equipment_PlayerTwoEquipment_PlayerTwo"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<PlayerTwoEquipment, PlayerTwo>)this.ChildCollections["Equipment_PlayerTwoEquipment_PlayerTwo"];
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
			var colFilter = new EquipmentColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a EquipmentColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EquipmentCollection Where(Func<EquipmentColumns, QueryFilter<EquipmentColumns>> where, OrderBy<EquipmentColumns> orderBy = null)
		{
			return new EquipmentCollection(new Query<EquipmentColumns, Equipment>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EquipmentCollection Where(WhereDelegate<EquipmentColumns> where, Database db = null)
		{
			return new EquipmentCollection(new Query<EquipmentColumns, Equipment>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EquipmentCollection Where(WhereDelegate<EquipmentColumns> where, OrderBy<EquipmentColumns> orderBy = null, Database db = null)
		{
			return new EquipmentCollection(new Query<EquipmentColumns, Equipment>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EquipmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static EquipmentCollection Where(QiQuery where, Database db = null)
		{
			return new EquipmentCollection(Select<EquipmentColumns>.From<Equipment>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Equipment instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Equipment OneWhere(WhereDelegate<EquipmentColumns> where, Database db = null)
		{
			var results = new EquipmentCollection(Select<EquipmentColumns>.From<Equipment>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<EquipmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Equipment OneWhere(QiQuery where, Database db = null)
		{
			var results = new EquipmentCollection(Select<EquipmentColumns>.From<Equipment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Equipment OneOrThrow(EquipmentCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Equipment FirstOneWhere(WhereDelegate<EquipmentColumns> where, Database db = null)
		{
			var results = new EquipmentCollection(Select<EquipmentColumns>.From<Equipment>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static EquipmentCollection Top(int count, WhereDelegate<EquipmentColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static EquipmentCollection Top(int count, WhereDelegate<EquipmentColumns> where, OrderBy<EquipmentColumns> orderBy, Database database = null)
		{
			EquipmentColumns c = new EquipmentColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Equipment>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Equipment>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<EquipmentColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<EquipmentCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a EquipmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between EquipmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<EquipmentColumns> where, Database database = null)
		{
			EquipmentColumns c = new EquipmentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Equipment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Equipment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
