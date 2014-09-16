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
	[Brevitee.Data.Table("PasswordReset", "UserAccounts")]
	public partial class PasswordReset: Dao
	{
		public PasswordReset():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public PasswordReset(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator PasswordReset(DataRow data)
		{
			return new PasswordReset(data);
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

	// property:Token, columnName:Token	
	[Brevitee.Data.Column(Name="Token", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Token
	{
		get
		{
			return GetStringValue("Token");
		}
		set
		{
			SetValue("Token", value);
		}
	}

	// property:DateTime, columnName:DateTime	
	[Brevitee.Data.Column(Name="DateTime", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
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

	// property:WasReset, columnName:WasReset	
	[Brevitee.Data.Column(Name="WasReset", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? WasReset
	{
		get
		{
			return GetBooleanValue("WasReset");
		}
		set
		{
			SetValue("WasReset", value);
		}
	}

	// property:ExpiresInMinutes, columnName:ExpiresInMinutes	
	[Brevitee.Data.Column(Name="ExpiresInMinutes", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? ExpiresInMinutes
	{
		get
		{
			return GetIntValue("ExpiresInMinutes");
		}
		set
		{
			SetValue("ExpiresInMinutes", value);
		}
	}



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="PasswordReset",
		Name="UserId", 
		DbDataType="BigInt", 
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
				_userOfUserId = Brevitee.UserAccounts.Data.User.OneWhere(f => f.Id == this.UserId);
			}
			return _userOfUserId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new PasswordResetColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the PasswordReset table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PasswordResetCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<PasswordReset>();
			Database db = database == null ? Db.For<PasswordReset>(): database;
			var results = new PasswordResetCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PasswordResetColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordResetCollection Where(Func<PasswordResetColumns, QueryFilter<PasswordResetColumns>> where, OrderBy<PasswordResetColumns> orderBy = null)
		{
			return new PasswordResetCollection(new Query<PasswordResetColumns, PasswordReset>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, Database db = null)
		{
			var results = new PasswordResetCollection(db, new Query<PasswordResetColumns, PasswordReset>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy = null, Database db = null)
		{
			var results = new PasswordResetCollection(db, new Query<PasswordResetColumns, PasswordReset>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordResetColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordResetCollection Where(QiQuery where, Database db = null)
		{
			var results = new PasswordResetCollection(db, Select<PasswordResetColumns>.From<PasswordReset>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single PasswordReset instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordReset OneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
		{
			var results = new PasswordResetCollection(db, Select<PasswordResetColumns>.From<PasswordReset>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordResetColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordReset OneWhere(QiQuery where, Database db = null)
		{
			var results = new PasswordResetCollection(db, Select<PasswordResetColumns>.From<PasswordReset>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PasswordReset OneOrThrow(PasswordResetCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordReset FirstOneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
		{
			var results = new PasswordResetCollection(db, Select<PasswordResetColumns>.From<PasswordReset>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy, Database database = null)
		{
			PasswordResetColumns c = new PasswordResetColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<PasswordReset>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<PasswordReset>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PasswordResetColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PasswordResetCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordResetColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordResetColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PasswordResetColumns> where, Database database = null)
		{
			PasswordResetColumns c = new PasswordResetColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<PasswordReset>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PasswordReset>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
