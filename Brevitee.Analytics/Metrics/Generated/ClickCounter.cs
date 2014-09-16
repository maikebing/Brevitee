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
	[Brevitee.Data.Table("ClickCounter", "Metrics")]
	public partial class ClickCounter: Dao
	{
		public ClickCounter():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ClickCounter(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator ClickCounter(DataRow data)
		{
			return new ClickCounter(data);
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
	[Brevitee.Data.Column(Name="UrlId", DbDataType="Int", MaxLength="4", AllowNull=false)]
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



﻿	// start CounterId -> CounterId
	[Brevitee.Data.ForeignKey(
        Table="ClickCounter",
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
	
﻿	// start UserIdentifierId -> UserIdentifierId
	[Brevitee.Data.ForeignKey(
        Table="ClickCounter",
		Name="UserIdentifierId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="UserIdentifier",
		Suffix="2")]
	public long? UserIdentifierId
	{
		get
		{
			return GetLongValue("UserIdentifierId");
		}
		set
		{
			SetValue("UserIdentifierId", value);
		}
	}

	UserIdentifier _userIdentifierOfUserIdentifierId;
	public UserIdentifier UserIdentifierOfUserIdentifierId
	{
		get
		{
			if(_userIdentifierOfUserIdentifierId == null)
			{
				_userIdentifierOfUserIdentifierId = Brevitee.Analytics.Metrics.UserIdentifier.OneWhere(c => c.KeyColumn == this.UserIdentifierId);
			}
			return _userIdentifierOfUserIdentifierId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ClickCounterColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the ClickCounter table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ClickCounterCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ClickCounter>();
			Database db = database ?? Db.For<ClickCounter>();
			var results = new ClickCounterCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ClickCounterColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ClickCounterCollection Where(Func<ClickCounterColumns, QueryFilter<ClickCounterColumns>> where, OrderBy<ClickCounterColumns> orderBy = null)
		{
			return new ClickCounterCollection(new Query<ClickCounterColumns, ClickCounter>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ClickCounterCollection Where(WhereDelegate<ClickCounterColumns> where, Database db = null)
		{
			var results = new ClickCounterCollection(db, new Query<ClickCounterColumns, ClickCounter>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ClickCounterCollection Where(WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy = null, Database db = null)
		{
			var results = new ClickCounterCollection(db, new Query<ClickCounterColumns, ClickCounter>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ClickCounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ClickCounterCollection Where(QiQuery where, Database db = null)
		{
			var results = new ClickCounterCollection(db, Select<ClickCounterColumns>.From<ClickCounter>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ClickCounter instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ClickCounter OneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
		{
			var results = new ClickCounterCollection(db, Select<ClickCounterColumns>.From<ClickCounter>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ClickCounterColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ClickCounter OneWhere(QiQuery where, Database db = null)
		{
			var results = new ClickCounterCollection(db, Select<ClickCounterColumns>.From<ClickCounter>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ClickCounter OneOrThrow(ClickCounterCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ClickCounter FirstOneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
		{
			var results = new ClickCounterCollection(db, Select<ClickCounterColumns>.From<ClickCounter>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ClickCounterCollection Top(int count, WhereDelegate<ClickCounterColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ClickCounterCollection Top(int count, WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy, Database database = null)
		{
			ClickCounterColumns c = new ClickCounterColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<ClickCounter>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<ClickCounter>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ClickCounterColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ClickCounterCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClickCounterColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClickCounterColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ClickCounterColumns> where, Database database = null)
		{
			ClickCounterColumns c = new ClickCounterColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ClickCounter>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ClickCounter>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
