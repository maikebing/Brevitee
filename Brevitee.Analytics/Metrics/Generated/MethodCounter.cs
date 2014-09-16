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
	[Brevitee.Data.Table("MethodCounter", "Metrics")]
	public partial class MethodCounter: Dao
	{
		public MethodCounter():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public MethodCounter(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator MethodCounter(DataRow data)
		{
			return new MethodCounter(data);
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

﻿	// property:MethodName, columnName:MethodName	
	[Brevitee.Data.Column(Name="MethodName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string MethodName
	{
		get
		{
			return GetStringValue("MethodName");
		}
		set
		{
			SetValue("MethodName", value);
		}
	}



﻿	// start CounterId -> CounterId
	[Brevitee.Data.ForeignKey(
        Table="MethodCounter",
		Name="CounterId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Counter",
		Suffix="1")]
	public long? CounterId
	{
		get
		{
			return GetLongValue("CounterId");
		}
		set
		{
			SetValue("CounterId", value);
		}
	}

	Counter _counterOfCounterId;
	public Counter CounterOfCounterId
	{
		get
		{
			if(_counterOfCounterId == null)
			{
				_counterOfCounterId = Brevitee.Analytics.Metrics.Counter.OneWhere(c => c.KeyColumn == this.CounterId);
			}
			return _counterOfCounterId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new MethodCounterColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the MethodCounter table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static MethodCounterCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<MethodCounter>();
			Database db = database ?? Db.For<MethodCounter>();
			var results = new MethodCounterCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a MethodCounterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MethodCounterCollection Where(Func<MethodCounterColumns, QueryFilter<MethodCounterColumns>> where, OrderBy<MethodCounterColumns> orderBy = null)
		{
			return new MethodCounterCollection(new Query<MethodCounterColumns, MethodCounter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MethodCounterCollection Where(WhereDelegate<MethodCounterColumns> where, Database db = null)
		{
			var results = new MethodCounterCollection(db, new Query<MethodCounterColumns, MethodCounter>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static MethodCounterCollection Where(WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy = null, Database db = null)
		{
			var results = new MethodCounterCollection(db, new Query<MethodCounterColumns, MethodCounter>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<MethodCounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static MethodCounterCollection Where(QiQuery where, Database db = null)
		{
			var results = new MethodCounterCollection(db, Select<MethodCounterColumns>.From<MethodCounter>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single MethodCounter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MethodCounter OneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
		{
			var results = new MethodCounterCollection(db, Select<MethodCounterColumns>.From<MethodCounter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<MethodCounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static MethodCounter OneWhere(QiQuery where, Database db = null)
		{
			var results = new MethodCounterCollection(db, Select<MethodCounterColumns>.From<MethodCounter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static MethodCounter OneOrThrow(MethodCounterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MethodCounter FirstOneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
		{
			var results = new MethodCounterCollection(db, Select<MethodCounterColumns>.From<MethodCounter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static MethodCounterCollection Top(int count, WhereDelegate<MethodCounterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static MethodCounterCollection Top(int count, WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy, Database database = null)
		{
			MethodCounterColumns c = new MethodCounterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<MethodCounter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<MethodCounter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<MethodCounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<MethodCounterCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a MethodCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between MethodCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<MethodCounterColumns> where, Database database = null)
		{
			MethodCounterColumns c = new MethodCounterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<MethodCounter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<MethodCounter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
