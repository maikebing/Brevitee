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
	[Brevitee.Data.Table("CustomTimer", "Metrics")]
	public partial class CustomTimer: Dao
	{
		public CustomTimer():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public CustomTimer(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator CustomTimer(DataRow data)
		{
			return new CustomTimer(data);
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

﻿	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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

﻿	// property:Description, columnName:Description	
	[Brevitee.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}



﻿	// start TimerId -> TimerId
	[Brevitee.Data.ForeignKey(
        Table="CustomTimer",
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
			var colFilter = new CustomTimerColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the CustomTimer table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CustomTimerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<CustomTimer>();
			Database db = database ?? Db.For<CustomTimer>();
			var results = new CustomTimerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CustomTimerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Where(Func<CustomTimerColumns, QueryFilter<CustomTimerColumns>> where, OrderBy<CustomTimerColumns> orderBy = null)
		{
			return new CustomTimerCollection(new Query<CustomTimerColumns, CustomTimer>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, Database db = null)
		{
			var results = new CustomTimerCollection(db, new Query<CustomTimerColumns, CustomTimer>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy = null, Database db = null)
		{
			var results = new CustomTimerCollection(db, new Query<CustomTimerColumns, CustomTimer>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CustomTimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CustomTimerCollection Where(QiQuery where, Database db = null)
		{
			var results = new CustomTimerCollection(db, Select<CustomTimerColumns>.From<CustomTimer>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CustomTimer instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimer OneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
		{
			var results = new CustomTimerCollection(db, Select<CustomTimerColumns>.From<CustomTimer>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CustomTimerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CustomTimer OneWhere(QiQuery where, Database db = null)
		{
			var results = new CustomTimerCollection(db, Select<CustomTimerColumns>.From<CustomTimer>().Where(where, db));
			return OneOrThrow(results);
		}

		private static CustomTimer OneOrThrow(CustomTimerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimer FirstOneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
		{
			var results = new CustomTimerCollection(db, Select<CustomTimerColumns>.From<CustomTimer>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy, Database database = null)
		{
			CustomTimerColumns c = new CustomTimerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<CustomTimer>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<CustomTimer>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CustomTimerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CustomTimerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CustomTimerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CustomTimerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CustomTimerColumns> where, Database database = null)
		{
			CustomTimerColumns c = new CustomTimerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<CustomTimer>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<CustomTimer>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
