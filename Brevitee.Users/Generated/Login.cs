// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Users.Data
{
	// schema = Users
	// connection Name = Users
	[Brevitee.Data.Table("Login", "Users")]
	public partial class Login: Dao
	{
		public Login():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Login(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Login(DataRow data)
		{
			return new Login(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="", MaxLength="")]
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

	// property:DateTime, columnName:DateTime	
	[Brevitee.Data.Column(Name="DateTime", ExtractedType="", MaxLength="", AllowNull=true)]
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



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Login",
		Name="UserId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? UserId
	{
		get
		{
			return GetLongValue("UserId");
		}
		set
		{
			SetValue("UserId", value);
		}
	}

	User _userOfUserId;
	public User UserOfUserId
	{
		get
		{
			if(_userOfUserId == null)
			{
				_userOfUserId = Brevitee.Users.Data.User.OneWhere(f => f.Id == this.UserId);
			}
			return _userOfUserId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new LoginColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LoginColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginCollection Where(Func<LoginColumns, QueryFilter<LoginColumns>> where, OrderBy<LoginColumns> orderBy = null)
		{
			return new LoginCollection(new Query<LoginColumns, Login>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginCollection Where(WhereDelegate<LoginColumns> where, Database db = null)
		{
			return new LoginCollection(new Query<LoginColumns, Login>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LoginCollection Where(WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy = null, Database db = null)
		{
			return new LoginCollection(new Query<LoginColumns, Login>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoginColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LoginCollection Where(QiQuery where, Database db = null)
		{
			return new LoginCollection(Select<LoginColumns>.From<Login>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Login instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Login OneWhere(WhereDelegate<LoginColumns> where, Database db = null)
		{
			var results = new LoginCollection(Select<LoginColumns>.From<Login>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LoginColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Login OneWhere(QiQuery where, Database db = null)
		{
			var results = new LoginCollection(Select<LoginColumns>.From<Login>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Login OneOrThrow(LoginCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Login FirstOneWhere(WhereDelegate<LoginColumns> where, Database db = null)
		{
			var results = new LoginCollection(Select<LoginColumns>.From<Login>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LoginCollection Top(int count, WhereDelegate<LoginColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LoginCollection Top(int count, WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy, Database database = null)
		{
			LoginColumns c = new LoginColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Login>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Login>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LoginColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<LoginCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LoginColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LoginColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LoginColumns> where, Database database = null)
		{
			LoginColumns c = new LoginColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Login>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Login>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
