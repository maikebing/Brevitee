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
	[Brevitee.Data.Table("PasswordFailure", "Users")]
	public partial class PasswordFailure: Dao
	{
		public PasswordFailure():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PasswordFailure(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PasswordFailure(DataRow data)
		{
			return new PasswordFailure(data);
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
        Table="PasswordFailure",
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
			var colFilter = new PasswordFailureColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PasswordFailureColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailureCollection Where(Func<PasswordFailureColumns, QueryFilter<PasswordFailureColumns>> where, OrderBy<PasswordFailureColumns> orderBy = null)
		{
			return new PasswordFailureCollection(new Query<PasswordFailureColumns, PasswordFailure>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailureCollection Where(WhereDelegate<PasswordFailureColumns> where, Database db = null)
		{
			return new PasswordFailureCollection(new Query<PasswordFailureColumns, PasswordFailure>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailureCollection Where(WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy = null, Database db = null)
		{
			return new PasswordFailureCollection(new Query<PasswordFailureColumns, PasswordFailure>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordFailureColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordFailureCollection Where(QiQuery where, Database db = null)
		{
			return new PasswordFailureCollection(Select<PasswordFailureColumns>.From<PasswordFailure>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PasswordFailure instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailure OneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
		{
			var results = new PasswordFailureCollection(Select<PasswordFailureColumns>.From<PasswordFailure>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordFailureColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordFailure OneWhere(QiQuery where, Database db = null)
		{
			var results = new PasswordFailureCollection(Select<PasswordFailureColumns>.From<PasswordFailure>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PasswordFailure OneOrThrow(PasswordFailureCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailure FirstOneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
		{
			var results = new PasswordFailureCollection(Select<PasswordFailureColumns>.From<PasswordFailure>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailureCollection Top(int count, WhereDelegate<PasswordFailureColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordFailureCollection Top(int count, WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy, Database database = null)
		{
			PasswordFailureColumns c = new PasswordFailureColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<PasswordFailure>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PasswordFailure>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PasswordFailureColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<PasswordFailureCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordFailureColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordFailureColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PasswordFailureColumns> where, Database database = null)
		{
			PasswordFailureColumns c = new PasswordFailureColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PasswordFailure>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PasswordFailure>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
