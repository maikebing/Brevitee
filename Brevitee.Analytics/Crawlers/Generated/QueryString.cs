// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Data
{
	// schema = Analytics
	// connection Name = Analytics
	[Brevitee.Data.Table("QueryString", "Analytics")]
	public partial class QueryString: Dao
	{
		public QueryString():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public QueryString(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator QueryString(DataRow data)
		{
			return new QueryString(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Url_QueryStringId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.QueryStringId == this.Id), this, "QueryStringId"));							
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
	[Brevitee.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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
	public UrlCollection UrlsByQueryStringId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_QueryStringId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_QueryStringId"];
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
			var colFilter = new QueryStringColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the QueryString table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static QueryStringCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<QueryString>();
			Database db = database ?? Db.For<QueryString>();
			var results = new QueryStringCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a QueryStringColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static QueryStringCollection Where(Func<QueryStringColumns, QueryFilter<QueryStringColumns>> where, OrderBy<QueryStringColumns> orderBy = null)
		{
			return new QueryStringCollection(new Query<QueryStringColumns, QueryString>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			var results = new QueryStringCollection(db, new Query<QueryStringColumns, QueryString>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null)
		{
			var results = new QueryStringCollection(db, new Query<QueryStringColumns, QueryString>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<QueryStringColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static QueryStringCollection Where(QiQuery where, Database db = null)
		{
			var results = new QueryStringCollection(db, Select<QueryStringColumns>.From<QueryString>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single QueryString instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static QueryString OneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			var results = new QueryStringCollection(db, Select<QueryStringColumns>.From<QueryString>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<QueryStringColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static QueryString OneWhere(QiQuery where, Database db = null)
		{
			var results = new QueryStringCollection(db, Select<QueryStringColumns>.From<QueryString>().Where(where, db));
			return OneOrThrow(results);
		}

		private static QueryString OneOrThrow(QueryStringCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static QueryString FirstOneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
		{
			var results = new QueryStringCollection(db, Select<QueryStringColumns>.From<QueryString>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy, Database database = null)
		{
			QueryStringColumns c = new QueryStringColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<QueryString>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<QueryString>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<QueryStringColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<QueryStringCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a QueryStringColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between QueryStringColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<QueryStringColumns> where, Database database = null)
		{
			QueryStringColumns c = new QueryStringColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<QueryString>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<QueryString>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
