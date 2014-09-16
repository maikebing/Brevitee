// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.ServiceProxy.Secure
{
	// schema = SecureServiceProxy
	// connection Name = SecureServiceProxy
	[Brevitee.Data.Table("SecureSession", "SecureServiceProxy")]
	public partial class SecureSession: Dao
	{
		public SecureSession():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public SecureSession(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator SecureSession(DataRow data)
		{
			return new SecureSession(data);
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

	// property:Identifier, columnName:Identifier	
	[Brevitee.Data.Column(Name="Identifier", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Identifier
	{
		get
		{
			return GetStringValue("Identifier");
		}
		set
		{
			SetValue("Identifier", value);
		}
	}

	// property:AsymmetricKey, columnName:AsymmetricKey	
	[Brevitee.Data.Column(Name="AsymmetricKey", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string AsymmetricKey
	{
		get
		{
			return GetStringValue("AsymmetricKey");
		}
		set
		{
			SetValue("AsymmetricKey", value);
		}
	}

	// property:SymmetricKey, columnName:SymmetricKey	
	[Brevitee.Data.Column(Name="SymmetricKey", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SymmetricKey
	{
		get
		{
			return GetStringValue("SymmetricKey");
		}
		set
		{
			SetValue("SymmetricKey", value);
		}
	}

	// property:SymmetricIV, columnName:SymmetricIV	
	[Brevitee.Data.Column(Name="SymmetricIV", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SymmetricIV
	{
		get
		{
			return GetStringValue("SymmetricIV");
		}
		set
		{
			SetValue("SymmetricIV", value);
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

	// property:TimeOffset, columnName:TimeOffset	
	[Brevitee.Data.Column(Name="TimeOffset", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? TimeOffset
	{
		get
		{
			return GetIntValue("TimeOffset");
		}
		set
		{
			SetValue("TimeOffset", value);
		}
	}

	// property:LastActivity, columnName:LastActivity	
	[Brevitee.Data.Column(Name="LastActivity", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? LastActivity
	{
		get
		{
			return GetDateTimeValue("LastActivity");
		}
		set
		{
			SetValue("LastActivity", value);
		}
	}

	// property:IsActive, columnName:IsActive	
	[Brevitee.Data.Column(Name="IsActive", DbDataType="Bit", MaxLength="1", AllowNull=true)]
	public bool? IsActive
	{
		get
		{
			return GetBooleanValue("IsActive");
		}
		set
		{
			SetValue("IsActive", value);
		}
	}



	// start ApplicationId -> ApplicationId
	[Brevitee.Data.ForeignKey(
        Table="SecureSession",
		Name="ApplicationId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Application",
		Suffix="1")]
	public long? ApplicationId
	{
		get
		{
			return GetLongValue("ApplicationId");
		}
		set
		{
			SetValue("ApplicationId", value);
		}
	}

	Application _applicationOfApplicationId;
	public Application ApplicationOfApplicationId
	{
		get
		{
			if(_applicationOfApplicationId == null)
			{
				_applicationOfApplicationId = Brevitee.ServiceProxy.Secure.Application.OneWhere(f => f.Id == this.ApplicationId);
			}
			return _applicationOfApplicationId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new SecureSessionColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the SecureSession table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SecureSessionCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<SecureSession>();
			Database db = database == null ? Db.For<SecureSession>(): database;
			var results = new SecureSessionCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SecureSessionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SecureSessionCollection Where(Func<SecureSessionColumns, QueryFilter<SecureSessionColumns>> where, OrderBy<SecureSessionColumns> orderBy = null)
		{
			return new SecureSessionCollection(new Query<SecureSessionColumns, SecureSession>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, Database db = null)
		{
			var results = new SecureSessionCollection(db, new Query<SecureSessionColumns, SecureSession>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SecureSessionCollection Where(WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy = null, Database db = null)
		{
			var results = new SecureSessionCollection(db, new Query<SecureSessionColumns, SecureSession>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SecureSessionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SecureSessionCollection Where(QiQuery where, Database db = null)
		{
			var results = new SecureSessionCollection(db, Select<SecureSessionColumns>.From<SecureSession>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SecureSession instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SecureSession OneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
		{
			var results = new SecureSessionCollection(db, Select<SecureSessionColumns>.From<SecureSession>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SecureSessionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SecureSession OneWhere(QiQuery where, Database db = null)
		{
			var results = new SecureSessionCollection(db, Select<SecureSessionColumns>.From<SecureSession>().Where(where, db));
			return OneOrThrow(results);
		}

		private static SecureSession OneOrThrow(SecureSessionCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SecureSession FirstOneWhere(WhereDelegate<SecureSessionColumns> where, Database db = null)
		{
			var results = new SecureSessionCollection(db, Select<SecureSessionColumns>.From<SecureSession>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SecureSessionCollection Top(int count, WhereDelegate<SecureSessionColumns> where, OrderBy<SecureSessionColumns> orderBy, Database database = null)
		{
			SecureSessionColumns c = new SecureSessionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<SecureSession>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<SecureSession>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SecureSessionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SecureSessionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SecureSessionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SecureSessionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SecureSessionColumns> where, Database database = null)
		{
			SecureSessionColumns c = new SecureSessionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<SecureSession>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<SecureSession>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
