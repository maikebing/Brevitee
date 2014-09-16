// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Stickerize.Business.Data
{
	// schema = Stickerize
	// connection Name = Stickerize
	[Brevitee.Data.Table("LoginTime", "Stickerize")]
	public partial class LoginTime: Dao
	{
		public LoginTime():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public LoginTime(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator LoginTime(DataRow data)
		{
			return new LoginTime(data);
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

﻿	// property:DateTime, columnName:DateTime	
	[Brevitee.Data.Column(Name="DateTime", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? DateTime
	{
		get
		{
			return GetDateTimeValue("DateTime");
		}
		set
		{
			SetValue("DateTime", value);
		}
	}

﻿	// property:UserName, columnName:UserName	
	[Brevitee.Data.Column(Name="UserName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserName
	{
		get
		{
			return GetStringValue("UserName");
		}
		set
		{
			SetValue("UserName", value);
		}
	}



				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new LoginTimeColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the LoginTime table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static LoginTimeCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<LoginTime>();
			Database db = database ?? Db.For<LoginTime>();
			var results = new LoginTimeCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LoginTimeColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginTimeCollection Where(Func<LoginTimeColumns, QueryFilter<LoginTimeColumns>> where, OrderBy<LoginTimeColumns> orderBy = null)
		{
			return new LoginTimeCollection(new Query<LoginTimeColumns, LoginTime>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginTimeCollection Where(WhereDelegate<LoginTimeColumns> where, Database db = null)
		{
			var results = new LoginTimeCollection(db, new Query<LoginTimeColumns, LoginTime>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LoginTimeCollection Where(WhereDelegate<LoginTimeColumns> where, OrderBy<LoginTimeColumns> orderBy = null, Database db = null)
		{
			var results = new LoginTimeCollection(db, new Query<LoginTimeColumns, LoginTime>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoginTimeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LoginTimeCollection Where(QiQuery where, Database db = null)
		{
			var results = new LoginTimeCollection(db, Select<LoginTimeColumns>.From<LoginTime>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single LoginTime instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginTime OneWhere(WhereDelegate<LoginTimeColumns> where, Database db = null)
		{
			var results = new LoginTimeCollection(db, Select<LoginTimeColumns>.From<LoginTime>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoginTimeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LoginTime OneWhere(QiQuery where, Database db = null)
		{
			var results = new LoginTimeCollection(db, Select<LoginTimeColumns>.From<LoginTime>().Where(where, db));
			return OneOrThrow(results);
		}

		private static LoginTime OneOrThrow(LoginTimeCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginTime FirstOneWhere(WhereDelegate<LoginTimeColumns> where, Database db = null)
		{
			var results = new LoginTimeCollection(db, Select<LoginTimeColumns>.From<LoginTime>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginTimeCollection Top(int count, WhereDelegate<LoginTimeColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LoginTimeCollection Top(int count, WhereDelegate<LoginTimeColumns> where, OrderBy<LoginTimeColumns> orderBy, Database database = null)
		{
			LoginTimeColumns c = new LoginTimeColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<LoginTime>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<LoginTime>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LoginTimeColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<LoginTimeCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginTimeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginTimeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LoginTimeColumns> where, Database database = null)
		{
			LoginTimeColumns c = new LoginTimeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<LoginTime>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<LoginTime>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
