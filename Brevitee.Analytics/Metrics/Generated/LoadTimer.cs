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
	[Brevitee.Data.Table("LoadTimer", "Metrics")]
	public partial class LoadTimer: Dao
	{
		public LoadTimer():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LoadTimer(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator LoadTimer(DataRow data)
		{
			return new LoadTimer(data);
		}

		private void SetChildren()
		{
						
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

﻿	// property:UrlId, columnName:UrlId	
	[Brevitee.Data.Column(Name="UrlId", DbDataType="Int", MaxLength="4", AllowNull=true)]
	public int? UrlId
	{
		get
		{
			return GetIntValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}



﻿	// start TimerId -> TimerId
	[Brevitee.Data.ForeignKey(
        Table="LoadTimer",
		Name="TimerId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Timer",
		Suffix="1")]
	public long? TimerId
	{
		get
		{
			return GetLongValue("TimerId");
		}
		set
		{
			SetValue("TimerId", value);
		}
	}

	Timer _timerOfTimerId;
	public Timer TimerOfTimerId
	{
		get
		{
			if(_timerOfTimerId == null)
			{
				_timerOfTimerId = Brevitee.Analytics.Metrics.Timer.OneWhere(c => c.KeyColumn == this.TimerId);
			}
			return _timerOfTimerId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new LoadTimerColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the LoadTimer table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static LoadTimerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<LoadTimer>();
			Database db = database ?? Db.For<LoadTimer>();
			var results = new LoadTimerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LoadTimerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoadTimerCollection Where(Func<LoadTimerColumns, QueryFilter<LoadTimerColumns>> where, OrderBy<LoadTimerColumns> orderBy = null)
		{
			return new LoadTimerCollection(new Query<LoadTimerColumns, LoadTimer>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoadTimerCollection Where(WhereDelegate<LoadTimerColumns> where, Database db = null)
		{
			var results = new LoadTimerCollection(db, new Query<LoadTimerColumns, LoadTimer>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LoadTimerCollection Where(WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy = null, Database db = null)
		{
			var results = new LoadTimerCollection(db, new Query<LoadTimerColumns, LoadTimer>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoadTimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LoadTimerCollection Where(QiQuery where, Database db = null)
		{
			var results = new LoadTimerCollection(db, Select<LoadTimerColumns>.From<LoadTimer>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single LoadTimer instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoadTimer OneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
		{
			var results = new LoadTimerCollection(db, Select<LoadTimerColumns>.From<LoadTimer>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoadTimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LoadTimer OneWhere(QiQuery where, Database db = null)
		{
			var results = new LoadTimerCollection(db, Select<LoadTimerColumns>.From<LoadTimer>().Where(where, db));
			return OneOrThrow(results);
		}

		private static LoadTimer OneOrThrow(LoadTimerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoadTimer FirstOneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
		{
			var results = new LoadTimerCollection(db, Select<LoadTimerColumns>.From<LoadTimer>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoadTimerCollection Top(int count, WhereDelegate<LoadTimerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LoadTimerCollection Top(int count, WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy, Database database = null)
		{
			LoadTimerColumns c = new LoadTimerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<LoadTimer>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<LoadTimer>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LoadTimerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LoadTimerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoadTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoadTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LoadTimerColumns> where, Database database = null)
		{
			LoadTimerColumns c = new LoadTimerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<LoadTimer>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<LoadTimer>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
