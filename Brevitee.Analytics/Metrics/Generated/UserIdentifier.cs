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
	[Brevitee.Data.Table("UserIdentifier", "Metrics")]
	public partial class UserIdentifier: Dao
	{
		public UserIdentifier():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UserIdentifier(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator UserIdentifier(DataRow data)
		{
			return new UserIdentifier(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("ClickCounter_UserIdentifierId", new ClickCounterCollection(new Query<ClickCounterColumns, ClickCounter>((c) => c.UserIdentifierId == this.Id), this, "UserIdentifierId"));	﻿
            this.ChildCollections.Add("LoginCounter_UserIdentifierId", new LoginCounterCollection(new Query<LoginCounterColumns, LoginCounter>((c) => c.UserIdentifierId == this.Id), this, "UserIdentifierId"));							
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



				
﻿
	[Exclude]	
	public ClickCounterCollection ClickCountersByUserIdentifierId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ClickCounter_UserIdentifierId"))
			{
				SetChildren();
			}

			var c = (ClickCounterCollection)this.ChildCollections["ClickCounter_UserIdentifierId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public LoginCounterCollection LoginCountersByUserIdentifierId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("LoginCounter_UserIdentifierId"))
			{
				SetChildren();
			}

			var c = (LoginCounterCollection)this.ChildCollections["LoginCounter_UserIdentifierId"];
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
			var colFilter = new UserIdentifierColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the UserIdentifier table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UserIdentifierCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<UserIdentifier>();
			Database db = database ?? Db.For<UserIdentifier>();
			var results = new UserIdentifierCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UserIdentifierColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Where(Func<UserIdentifierColumns, QueryFilter<UserIdentifierColumns>> where, OrderBy<UserIdentifierColumns> orderBy = null)
		{
			return new UserIdentifierCollection(new Query<UserIdentifierColumns, UserIdentifier>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			var results = new UserIdentifierCollection(db, new Query<UserIdentifierColumns, UserIdentifier>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy = null, Database db = null)
		{
			var results = new UserIdentifierCollection(db, new Query<UserIdentifierColumns, UserIdentifier>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserIdentifierColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Where(QiQuery where, Database db = null)
		{
			var results = new UserIdentifierCollection(db, Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UserIdentifier instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifier OneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			var results = new UserIdentifierCollection(db, Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserIdentifierColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserIdentifier OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserIdentifierCollection(db, Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserIdentifier OneOrThrow(UserIdentifierCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifier FirstOneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
		{
			var results = new UserIdentifierCollection(db, Select<UserIdentifierColumns>.From<UserIdentifier>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy, Database database = null)
		{
			UserIdentifierColumns c = new UserIdentifierColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<UserIdentifier>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<UserIdentifier>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserIdentifierColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UserIdentifierCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserIdentifierColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserIdentifierColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UserIdentifierColumns> where, Database database = null)
		{
			UserIdentifierColumns c = new UserIdentifierColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UserIdentifier>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserIdentifier>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
