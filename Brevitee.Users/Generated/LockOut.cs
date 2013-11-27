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
	[Brevitee.Data.Table("LockOut", "Users")]
	public partial class LockOut: Dao
	{
		public LockOut():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public LockOut(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator LockOut(DataRow data)
		{
			return new LockOut(data);
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

	// property:Unlocked, columnName:Unlocked	
	[Brevitee.Data.Column(Name="Unlocked", ExtractedType="", MaxLength="", AllowNull=true)]
	public bool? Unlocked
	{
		get
		{
			return GetBooleanValue("Unlocked");
		}
		set
		{
			SetValue("Unlocked", value);
		}
	}

	// property:UnlockedDate, columnName:UnlockedDate	
	[Brevitee.Data.Column(Name="UnlockedDate", ExtractedType="", MaxLength="", AllowNull=true)]
	public DateTime? UnlockedDate
	{
		get
		{
			return GetDateTimeValue("UnlockedDate");
		}
		set
		{
			SetValue("UnlockedDate", value);
		}
	}



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="LockOut",
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
			var colFilter = new LockOutColumns();
			return (colFilter.Id == IdValue);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a LockOutColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LockOutCollection Where(Func<LockOutColumns, QueryFilter<LockOutColumns>> where, OrderBy<LockOutColumns> orderBy = null)
		{
			return new LockOutCollection(new Query<LockOutColumns, LockOut>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LockOutCollection Where(WhereDelegate<LockOutColumns> where, Database db = null)
		{
			return new LockOutCollection(new Query<LockOutColumns, LockOut>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LockOutCollection Where(WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy = null, Database db = null)
		{
			return new LockOutCollection(new Query<LockOutColumns, LockOut>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LockOutColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LockOutCollection Where(QiQuery where, Database db = null)
		{
			return new LockOutCollection(Select<LockOutColumns>.From<LockOut>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single LockOut instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LockOut OneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
		{
			var results = new LockOutCollection(Select<LockOutColumns>.From<LockOut>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<LockOutColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static LockOut OneWhere(QiQuery where, Database db = null)
		{
			var results = new LockOutCollection(Select<LockOutColumns>.From<LockOut>().Where(where, db));
			return OneOrThrow(results);
		}

		private static LockOut OneOrThrow(LockOutCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LockOut FirstOneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
		{
			var results = new LockOutCollection(Select<LockOutColumns>.From<LockOut>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static LockOutCollection Top(int count, WhereDelegate<LockOutColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static LockOutCollection Top(int count, WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy, Database database = null)
		{
			LockOutColumns c = new LockOutColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<LockOut>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<LockOut>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<LockOutColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<LockOutCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a LockOutColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between LockOutColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<LockOutColumns> where, Database database = null)
		{
			LockOutColumns c = new LockOutColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<LockOut>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<LockOut>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
