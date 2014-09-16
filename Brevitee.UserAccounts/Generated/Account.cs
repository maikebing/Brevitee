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
	[Brevitee.Data.Table("Account", "UserAccounts")]
	public partial class Account: Dao
	{
		public Account():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Account(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Account(DataRow data)
		{
			return new Account(data);
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

	// property:Provider, columnName:Provider	
	[Brevitee.Data.Column(Name="Provider", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Provider
	{
		get
		{
			return GetStringValue("Provider");
		}
		set
		{
			SetValue("Provider", value);
		}
	}

	// property:ProviderUserId, columnName:ProviderUserId	
	[Brevitee.Data.Column(Name="ProviderUserId", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ProviderUserId
	{
		get
		{
			return GetStringValue("ProviderUserId");
		}
		set
		{
			SetValue("ProviderUserId", value);
		}
	}

	// property:Comment, columnName:Comment	
	[Brevitee.Data.Column(Name="Comment", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Comment
	{
		get
		{
			return GetStringValue("Comment");
		}
		set
		{
			SetValue("Comment", value);
		}
	}

	// property:CreationDate, columnName:CreationDate	
	[Brevitee.Data.Column(Name="CreationDate", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? CreationDate
	{
		get
		{
			return GetDateTimeValue("CreationDate");
		}
		set
		{
			SetValue("CreationDate", value);
		}
	}

	// property:IsConfirmed, columnName:IsConfirmed	
	[Brevitee.Data.Column(Name="IsConfirmed", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsConfirmed
	{
		get
		{
			return GetBooleanValue("IsConfirmed");
		}
		set
		{
			SetValue("IsConfirmed", value);
		}
	}

	// property:ConfirmationDate, columnName:ConfirmationDate	
	[Brevitee.Data.Column(Name="ConfirmationDate", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? ConfirmationDate
	{
		get
		{
			return GetDateTimeValue("ConfirmationDate");
		}
		set
		{
			SetValue("ConfirmationDate", value);
		}
	}



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Account",
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
			var colFilter = new AccountColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Account table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static AccountCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Account>();
			Database db = database == null ? Db.For<Account>(): database;
			var results = new AccountCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a AccountColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static AccountCollection Where(Func<AccountColumns, QueryFilter<AccountColumns>> where, OrderBy<AccountColumns> orderBy = null)
		{
			return new AccountCollection(new Query<AccountColumns, Account>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static AccountCollection Where(WhereDelegate<AccountColumns> where, Database db = null)
		{
			var results = new AccountCollection(db, new Query<AccountColumns, Account>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static AccountCollection Where(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy = null, Database db = null)
		{
			var results = new AccountCollection(db, new Query<AccountColumns, Account>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AccountColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static AccountCollection Where(QiQuery where, Database db = null)
		{
			var results = new AccountCollection(db, Select<AccountColumns>.From<Account>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Account instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Account OneWhere(WhereDelegate<AccountColumns> where, Database db = null)
		{
			var results = new AccountCollection(db, Select<AccountColumns>.From<Account>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<AccountColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Account OneWhere(QiQuery where, Database db = null)
		{
			var results = new AccountCollection(db, Select<AccountColumns>.From<Account>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Account OneOrThrow(AccountCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Account FirstOneWhere(WhereDelegate<AccountColumns> where, Database db = null)
		{
			var results = new AccountCollection(db, Select<AccountColumns>.From<Account>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static AccountCollection Top(int count, WhereDelegate<AccountColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static AccountCollection Top(int count, WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy, Database database = null)
		{
			AccountColumns c = new AccountColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Account>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Account>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AccountColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<AccountCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a AccountColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between AccountColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<AccountColumns> where, Database database = null)
		{
			AccountColumns c = new AccountColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Account>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Account>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
