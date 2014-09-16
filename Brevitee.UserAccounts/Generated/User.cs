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
	[Brevitee.Data.Table("User", "UserAccounts")]
	public partial class User: Dao
	{
		public User():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public User(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator User(DataRow data)
		{
			return new User(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Account_UserId", new AccountCollection(new Query<AccountColumns, Account>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Password_UserId", new PasswordCollection(new Query<PasswordColumns, Password>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("PasswordReset_UserId", new PasswordResetCollection(new Query<PasswordResetColumns, PasswordReset>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("PasswordFailure_UserId", new PasswordFailureCollection(new Query<PasswordFailureColumns, PasswordFailure>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("LockOut_UserId", new LockOutCollection(new Query<LockOutColumns, LockOut>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Login_UserId", new LoginCollection(new Query<LoginColumns, Login>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("PasswordQuestion_UserId", new PasswordQuestionCollection(new Query<PasswordQuestionColumns, PasswordQuestion>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Setting_UserId", new SettingCollection(new Query<SettingColumns, Setting>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Session_UserId", new SessionCollection(new Query<SessionColumns, Session>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("UserRole_UserId", new UserRoleCollection(new Query<UserRoleColumns, UserRole>((c) => c.UserId == this.Id), this, "UserId"));				
            this.ChildCollections.Add("User_UserRole_Role",  new XrefDaoCollection<UserRole, Role>(this, false));
							
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

	// property:IsDeleted, columnName:IsDeleted	
	[Brevitee.Data.Column(Name="IsDeleted", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsDeleted
	{
		get
		{
			return GetBooleanValue("IsDeleted");
		}
		set
		{
			SetValue("IsDeleted", value);
		}
	}

	// property:UserName, columnName:UserName	
	[Brevitee.Data.Column(Name="UserName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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

	// property:IsApproved, columnName:IsApproved	
	[Brevitee.Data.Column(Name="IsApproved", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsApproved
	{
		get
		{
			return GetBooleanValue("IsApproved");
		}
		set
		{
			SetValue("IsApproved", value);
		}
	}

	// property:Email, columnName:Email	
	[Brevitee.Data.Column(Name="Email", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Email
	{
		get
		{
			return GetStringValue("Email");
		}
		set
		{
			SetValue("Email", value);
		}
	}



				

	[Exclude]	
	public AccountCollection AccountsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Account_UserId"))
			{
				SetChildren();
			}

			var c = (AccountCollection)this.ChildCollections["Account_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PasswordCollection PasswordsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Password_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordCollection)this.ChildCollections["Password_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PasswordResetCollection PasswordResetsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PasswordReset_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordResetCollection)this.ChildCollections["PasswordReset_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PasswordFailureCollection PasswordFailuresByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PasswordFailure_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordFailureCollection)this.ChildCollections["PasswordFailure_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public LockOutCollection LockOutsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("LockOut_UserId"))
			{
				SetChildren();
			}

			var c = (LockOutCollection)this.ChildCollections["LockOut_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public LoginCollection LoginsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Login_UserId"))
			{
				SetChildren();
			}

			var c = (LoginCollection)this.ChildCollections["Login_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public PasswordQuestionCollection PasswordQuestionsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PasswordQuestion_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordQuestionCollection)this.ChildCollections["PasswordQuestion_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public SettingCollection SettingsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Setting_UserId"))
			{
				SetChildren();
			}

			var c = (SettingCollection)this.ChildCollections["Setting_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public SessionCollection SessionsByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Session_UserId"))
			{
				SetChildren();
			}

			var c = (SessionCollection)this.ChildCollections["Session_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	
	[Exclude]	
	public UserRoleCollection UserRolesByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserRole_UserId"))
			{
				SetChildren();
			}

			var c = (UserRoleCollection)this.ChildCollections["UserRole_UserId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<UserRole, Role> Roles
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("User_UserRole_Role"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UserRole, Role>)this.ChildCollections["User_UserRole_Role"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the User table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UserCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<User>();
			Database db = database == null ? Db.For<User>(): database;
			var results = new UserCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UserColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserCollection Where(Func<UserColumns, QueryFilter<UserColumns>> where, OrderBy<UserColumns> orderBy = null)
		{
			return new UserCollection(new Query<UserColumns, User>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserCollection Where(WhereDelegate<UserColumns> where, Database db = null)
		{
			var results = new UserCollection(db, new Query<UserColumns, User>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
		{
			var results = new UserCollection(db, new Query<UserColumns, User>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UserCollection Where(QiQuery where, Database db = null)
		{
			var results = new UserCollection(db, Select<UserColumns>.From<User>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single User instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static User OneWhere(WhereDelegate<UserColumns> where, Database db = null)
		{
			var results = new UserCollection(db, Select<UserColumns>.From<User>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UserColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static User OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserCollection(db, Select<UserColumns>.From<User>().Where(where, db));
			return OneOrThrow(results);
		}

		private static User OneOrThrow(UserCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static User FirstOneWhere(WhereDelegate<UserColumns> where, Database db = null)
		{
			var results = new UserCollection(db, Select<UserColumns>.From<User>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UserCollection Top(int count, WhereDelegate<UserColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database database = null)
		{
			UserColumns c = new UserColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<User>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<User>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UserCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UserColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UserColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UserColumns> where, Database database = null)
		{
			UserColumns c = new UserColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<User>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<User>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
