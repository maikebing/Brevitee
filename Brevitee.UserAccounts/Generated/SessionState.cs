// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.UserAccounts.Data
{
	// schema = UserAccounts
	// connection Name = UserAccounts
	[Brevitee.Data.Table("SessionState", "UserAccounts")]
	public partial class SessionState: Dao
	{
		public SessionState():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public SessionState(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator SessionState(DataRow data)
		{
			return new SessionState(data);
		}

		private void SetChildren()
		{
						
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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

	// property:ValueType, columnName:ValueType	
	[Brevitee.Data.Column(Name="ValueType", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ValueType
	{
		get
		{
			return GetStringValue("ValueType");
		}
		set
		{
			SetValue("ValueType", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="VarBinary", MaxLength="8000", AllowNull=true)]
	public byte[] Value
	{
		get
		{
			return GetByteValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



	// start SessionId -> SessionId
	[Brevitee.Data.ForeignKey(
        Table="SessionState",
		Name="SessionId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Session",
		Suffix="1")]
	public long? SessionId
	{
		get
		{
			return GetLongValue("SessionId");
		}
		set
		{
			SetValue("SessionId", value);
		}
	}

	Session _sessionOfSessionId;
	public Session SessionOfSessionId
	{
		get
		{
			if(_sessionOfSessionId == null)
			{
				_sessionOfSessionId = Brevitee.UserAccounts.Data.Session.OneWhere(f => f.Id == this.SessionId);
			}
			return _sessionOfSessionId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new SessionStateColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the SessionState table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SessionStateCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<SessionState>();
			Database db = database == null ? Db.For<SessionState>(): database;
			var results = new SessionStateCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SessionStateColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SessionStateCollection Where(Func<SessionStateColumns, QueryFilter<SessionStateColumns>> where, OrderBy<SessionStateColumns> orderBy = null)
		{
			return new SessionStateCollection(new Query<SessionStateColumns, SessionState>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SessionStateCollection Where(WhereDelegate<SessionStateColumns> where, Database db = null)
		{
			var results = new SessionStateCollection(db, new Query<SessionStateColumns, SessionState>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SessionStateCollection Where(WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy = null, Database db = null)
		{
			var results = new SessionStateCollection(db, new Query<SessionStateColumns, SessionState>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SessionStateColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SessionStateCollection Where(QiQuery where, Database db = null)
		{
			var results = new SessionStateCollection(db, Select<SessionStateColumns>.From<SessionState>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SessionState instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SessionState OneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
		{
			var results = new SessionStateCollection(db, Select<SessionStateColumns>.From<SessionState>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SessionStateColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SessionState OneWhere(QiQuery where, Database db = null)
		{
			var results = new SessionStateCollection(db, Select<SessionStateColumns>.From<SessionState>().Where(where, db));
			return OneOrThrow(results);
		}

		private static SessionState OneOrThrow(SessionStateCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SessionState FirstOneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
		{
			var results = new SessionStateCollection(db, Select<SessionStateColumns>.From<SessionState>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SessionStateCollection Top(int count, WhereDelegate<SessionStateColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SessionStateCollection Top(int count, WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy, Database database = null)
		{
			SessionStateColumns c = new SessionStateColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<SessionState>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<SessionState>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SessionStateColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SessionStateCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SessionStateColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SessionStateColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SessionStateColumns> where, Database database = null)
		{
			SessionStateColumns c = new SessionStateColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<SessionState>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<SessionState>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
