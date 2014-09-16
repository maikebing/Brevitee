// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Logging
{
	// schema = DaoLogger2
	// connection Name = DaoLogger2
	[Brevitee.Data.Table("UserName", "DaoLogger2")]
	public partial class UserName: Dao
	{
		public UserName():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public UserName(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator UserName(DataRow data)
		{
			return new UserName(data);
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

	// property:Value, columnName:Value	
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



				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserNameColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the UserName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UserNameCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<UserName>();
			Database db = database == null ? Db.For<UserName>(): database;
			return new UserNameCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UserNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserNameCollection Where(Func<UserNameColumns, QueryFilter<UserNameColumns>> where, OrderBy<UserNameColumns> orderBy = null)
		{
			return new UserNameCollection(new Query<UserNameColumns, UserName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserNameCollection Where(WhereDelegate<UserNameColumns> where, Database db = null)
		{
			return new UserNameCollection(new Query<UserNameColumns, UserName>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserNameCollection Where(WhereDelegate<UserNameColumns> where, OrderBy<UserNameColumns> orderBy = null, Database db = null)
		{
			return new UserNameCollection(new Query<UserNameColumns, UserName>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserNameCollection Where(QiQuery where, Database db = null)
		{
			return new UserNameCollection(Select<UserNameColumns>.From<UserName>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UserName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserName OneWhere(WhereDelegate<UserNameColumns> where, Database db = null)
		{
			var results = new UserNameCollection(Select<UserNameColumns>.From<UserName>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserName OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserNameCollection(Select<UserNameColumns>.From<UserName>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserName OneOrThrow(UserNameCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserName FirstOneWhere(WhereDelegate<UserNameColumns> where, Database db = null)
		{
			var results = new UserNameCollection(Select<UserNameColumns>.From<UserName>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserNameCollection Top(int count, WhereDelegate<UserNameColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserNameCollection Top(int count, WhereDelegate<UserNameColumns> where, OrderBy<UserNameColumns> orderBy, Database database = null)
		{
			UserNameColumns c = new UserNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<UserName>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<UserName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserNameColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<UserNameCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UserNameColumns> where, Database database = null)
		{
			UserNameColumns c = new UserNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UserName>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
