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
	[Brevitee.Data.Table("UserRole", "Users")]
	public partial class UserRole: Dao
	{
		public UserRole():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public UserRole(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator UserRole(DataRow data)
		{
			return new UserRole(data);
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



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="UserRole",
		Name="UserId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
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
	
	// start RoleId -> RoleId
	[Brevitee.Data.ForeignKey(
        Table="UserRole",
		Name="RoleId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Role",
		Suffix="2")]
	public long? RoleId
	{
		get
		{
			return GetLongValue("RoleId");
		}
		set
		{
			SetValue("RoleId", value);
		}
	}

	Role _roleOfRoleId;
	public Role RoleOfRoleId
	{
		get
		{
			if(_roleOfRoleId == null)
			{
				_roleOfRoleId = Brevitee.Users.Data.Role.OneWhere(f => f.Id == this.RoleId);
			}
			return _roleOfRoleId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that will should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserRoleColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UserRoleColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserRoleCollection Where(Func<UserRoleColumns, QueryFilter<UserRoleColumns>> where, OrderBy<UserRoleColumns> orderBy = null)
		{
			return new UserRoleCollection(new Query<UserRoleColumns, UserRole>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			return new UserRoleCollection(new Query<UserRoleColumns, UserRole>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null)
		{
			return new UserRoleCollection(new Query<UserRoleColumns, UserRole>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserRoleColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserRoleCollection Where(QiQuery where, Database db = null)
		{
			return new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UserRole instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserRole OneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			var results = new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserRoleColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserRole OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserRole OneOrThrow(UserRoleCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserRole FirstOneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			var results = new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy, Database database = null)
		{
			UserRoleColumns c = new UserRoleColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<UserRole>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<UserRole>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserRoleColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<UserRoleCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserRoleColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserRoleColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UserRoleColumns> where, Database database = null)
		{
			UserRoleColumns c = new UserRoleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UserRole>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserRole>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
