// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Metrics
{
	// schema = Metrics
	// connection Name = Metrics
	[Brevitee.Data.Table("Counter", "Metrics")]
	public partial class Counter: Dao
	{
		public Counter():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Counter(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Counter(DataRow data)
		{
			return new Counter(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("MethodCounter_CounterId", new MethodCounterCollection(new Query<MethodCounterColumns, MethodCounter>((c) => c.CounterId == this.Id), this, "CounterId"));	﻿
            this.ChildCollections.Add("LoadCounter_CounterId", new LoadCounterCollection(new Query<LoadCounterColumns, LoadCounter>((c) => c.CounterId == this.Id), this, "CounterId"));	﻿
            this.ChildCollections.Add("ClickCounter_CounterId", new ClickCounterCollection(new Query<ClickCounterColumns, ClickCounter>((c) => c.CounterId == this.Id), this, "CounterId"));	﻿
            this.ChildCollections.Add("LoginCounter_CounterId", new LoginCounterCollection(new Query<LoginCounterColumns, LoginCounter>((c) => c.CounterId == this.Id), this, "CounterId"));							
		}

﻿	// property:Id, columnName:Id	
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

﻿	// property:Uuid, columnName:Uuid	
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

﻿	// property:Value, columnName:Value	
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



				
﻿
	[Exclude]	
	public MethodCounterCollection MethodCountersByCounterId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("MethodCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (MethodCounterCollection)this.ChildCollections["MethodCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public LoadCounterCollection LoadCountersByCounterId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("LoadCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (LoadCounterCollection)this.ChildCollections["LoadCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public ClickCounterCollection ClickCountersByCounterId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ClickCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (ClickCounterCollection)this.ChildCollections["ClickCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public LoginCounterCollection LoginCountersByCounterId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("LoginCounter_CounterId"))
			{
				SetChildren();
			}

			var c = (LoginCounterCollection)this.ChildCollections["LoginCounter_CounterId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CounterColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Counter table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CounterCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Counter>();
			Database db = database ?? Db.For<Counter>();
			var results = new CounterCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CounterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CounterCollection Where(Func<CounterColumns, QueryFilter<CounterColumns>> where, OrderBy<CounterColumns> orderBy = null)
		{
			return new CounterCollection(new Query<CounterColumns, Counter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CounterCollection Where(WhereDelegate<CounterColumns> where, Database db = null)
		{
			var results = new CounterCollection(db, new Query<CounterColumns, Counter>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CounterCollection Where(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy = null, Database db = null)
		{
			var results = new CounterCollection(db, new Query<CounterColumns, Counter>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CounterCollection Where(QiQuery where, Database db = null)
		{
			var results = new CounterCollection(db, Select<CounterColumns>.From<Counter>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Counter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Counter OneWhere(WhereDelegate<CounterColumns> where, Database db = null)
		{
			var results = new CounterCollection(db, Select<CounterColumns>.From<Counter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Counter OneWhere(QiQuery where, Database db = null)
		{
			var results = new CounterCollection(db, Select<CounterColumns>.From<Counter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Counter OneOrThrow(CounterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Counter FirstOneWhere(WhereDelegate<CounterColumns> where, Database db = null)
		{
			var results = new CounterCollection(db, Select<CounterColumns>.From<Counter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CounterCollection Top(int count, WhereDelegate<CounterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CounterCollection Top(int count, WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy, Database database = null)
		{
			CounterColumns c = new CounterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Counter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Counter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CounterCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CounterColumns> where, Database database = null)
		{
			CounterColumns c = new CounterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Counter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Counter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
