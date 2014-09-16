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
	[Brevitee.Data.Table("Timer", "Metrics")]
	public partial class Timer: Dao
	{
		public Timer():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Timer(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Timer(DataRow data)
		{
			return new Timer(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("MethodTimer_TimerId", new MethodTimerCollection(new Query<MethodTimerColumns, MethodTimer>((c) => c.TimerId == this.Id), this, "TimerId"));	﻿
            this.ChildCollections.Add("LoadTimer_TimerId", new LoadTimerCollection(new Query<LoadTimerColumns, LoadTimer>((c) => c.TimerId == this.Id), this, "TimerId"));	﻿
            this.ChildCollections.Add("CustomTimer_TimerId", new CustomTimerCollection(new Query<CustomTimerColumns, CustomTimer>((c) => c.TimerId == this.Id), this, "TimerId"));							
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
	[Brevitee.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				
﻿
	[Exclude]	
	public MethodTimerCollection MethodTimersByTimerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("MethodTimer_TimerId"))
			{
				SetChildren();
			}

			var c = (MethodTimerCollection)this.ChildCollections["MethodTimer_TimerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public LoadTimerCollection LoadTimersByTimerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("LoadTimer_TimerId"))
			{
				SetChildren();
			}

			var c = (LoadTimerCollection)this.ChildCollections["LoadTimer_TimerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public CustomTimerCollection CustomTimersByTimerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("CustomTimer_TimerId"))
			{
				SetChildren();
			}

			var c = (CustomTimerCollection)this.ChildCollections["CustomTimer_TimerId"];
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
			var colFilter = new TimerColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Timer table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TimerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Timer>();
			Database db = database ?? Db.For<Timer>();
			var results = new TimerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TimerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TimerCollection Where(Func<TimerColumns, QueryFilter<TimerColumns>> where, OrderBy<TimerColumns> orderBy = null)
		{
			return new TimerCollection(new Query<TimerColumns, Timer>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TimerCollection Where(WhereDelegate<TimerColumns> where, Database db = null)
		{
			var results = new TimerCollection(db, new Query<TimerColumns, Timer>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static TimerCollection Where(WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy = null, Database db = null)
		{
			var results = new TimerCollection(db, new Query<TimerColumns, Timer>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static TimerCollection Where(QiQuery where, Database db = null)
		{
			var results = new TimerCollection(db, Select<TimerColumns>.From<Timer>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Timer instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Timer OneWhere(WhereDelegate<TimerColumns> where, Database db = null)
		{
			var results = new TimerCollection(db, Select<TimerColumns>.From<Timer>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Timer OneWhere(QiQuery where, Database db = null)
		{
			var results = new TimerCollection(db, Select<TimerColumns>.From<Timer>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Timer OneOrThrow(TimerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Timer FirstOneWhere(WhereDelegate<TimerColumns> where, Database db = null)
		{
			var results = new TimerCollection(db, Select<TimerColumns>.From<Timer>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TimerCollection Top(int count, WhereDelegate<TimerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static TimerCollection Top(int count, WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy, Database database = null)
		{
			TimerColumns c = new TimerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Timer>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Timer>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TimerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TimerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<TimerColumns> where, Database database = null)
		{
			TimerColumns c = new TimerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Timer>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Timer>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
