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
	[Brevitee.Data.Table("Protocol", "Analytics")]
	public partial class Protocol: Dao
	{
		public Protocol():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Protocol(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Protocol(DataRow data)
		{
			return new Protocol(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Url_ProtocolId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.ProtocolId == this.Id), this, "ProtocolId"));							
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
	public UrlCollection UrlsByProtocolId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_ProtocolId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_ProtocolId"];
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
			var colFilter = new ProtocolColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Protocol table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ProtocolCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Protocol>();
			Database db = database ?? Db.For<Protocol>();
			var results = new ProtocolCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ProtocolColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ProtocolCollection Where(Func<ProtocolColumns, QueryFilter<ProtocolColumns>> where, OrderBy<ProtocolColumns> orderBy = null)
		{
			return new ProtocolCollection(new Query<ProtocolColumns, Protocol>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			var results = new ProtocolCollection(db, new Query<ProtocolColumns, Protocol>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy = null, Database db = null)
		{
			var results = new ProtocolCollection(db, new Query<ProtocolColumns, Protocol>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ProtocolColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ProtocolCollection Where(QiQuery where, Database db = null)
		{
			var results = new ProtocolCollection(db, Select<ProtocolColumns>.From<Protocol>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Protocol instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Protocol OneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			var results = new ProtocolCollection(db, Select<ProtocolColumns>.From<Protocol>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ProtocolColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Protocol OneWhere(QiQuery where, Database db = null)
		{
			var results = new ProtocolCollection(db, Select<ProtocolColumns>.From<Protocol>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Protocol OneOrThrow(ProtocolCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Protocol FirstOneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
		{
			var results = new ProtocolCollection(db, Select<ProtocolColumns>.From<Protocol>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy, Database database = null)
		{
			ProtocolColumns c = new ProtocolColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Protocol>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Protocol>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ProtocolColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ProtocolCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ProtocolColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ProtocolColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ProtocolColumns> where, Database database = null)
		{
			ProtocolColumns c = new ProtocolColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Protocol>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Protocol>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
