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
	[Brevitee.Data.Table("Password", "UserAccounts")]
	public partial class Password: Dao
	{
		public Password():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Password(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Password(DataRow data)
		{
			return new Password(data);
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



	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Password",
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
			var colFilter = new PasswordColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the Password table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PasswordCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Password>();
			Database db = database == null ? Db.For<Password>(): database;
			var results = new PasswordCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PasswordColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordCollection Where(Func<PasswordColumns, QueryFilter<PasswordColumns>> where, OrderBy<PasswordColumns> orderBy = null)
		{
			return new PasswordCollection(new Query<PasswordColumns, Password>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordCollection Where(WhereDelegate<PasswordColumns> where, Database db = null)
		{
			var results = new PasswordCollection(db, new Query<PasswordColumns, Password>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordCollection Where(WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy = null, Database db = null)
		{
			var results = new PasswordCollection(db, new Query<PasswordColumns, Password>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PasswordCollection Where(QiQuery where, Database db = null)
		{
			var results = new PasswordCollection(db, Select<PasswordColumns>.From<Password>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Password instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Password OneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
		{
			var results = new PasswordCollection(db, Select<PasswordColumns>.From<Password>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PasswordColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Password OneWhere(QiQuery where, Database db = null)
		{
			var results = new PasswordCollection(db, Select<PasswordColumns>.From<Password>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Password OneOrThrow(PasswordCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Password FirstOneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
		{
			var results = new PasswordCollection(db, Select<PasswordColumns>.From<Password>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy, Database database = null)
		{
			PasswordColumns c = new PasswordColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Password>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Password>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PasswordColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PasswordCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PasswordColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PasswordColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PasswordColumns> where, Database database = null)
		{
			PasswordColumns c = new PasswordColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Password>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Password>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
