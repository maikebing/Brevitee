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
	[Brevitee.Data.Table("ApiKey", "SecureServiceProxy")]
	public partial class ApiKey: Dao
	{
		public ApiKey():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public ApiKey(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator ApiKey(DataRow data)
		{
			return new ApiKey(data);
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

	// property:ClientId, columnName:ClientId	
	[Brevitee.Data.Column(Name="ClientId", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ClientId
	{
		get
		{
			return GetStringValue("ClientId");
		}
		set
		{
			SetValue("ClientId", value);
		}
	}

	// property:SharedSecret, columnName:SharedSecret	
	[Brevitee.Data.Column(Name="SharedSecret", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SharedSecret
	{
		get
		{
			return GetStringValue("SharedSecret");
		}
		set
		{
			SetValue("SharedSecret", value);
		}
	}

	// property:CreatedBy, columnName:CreatedBy	
	[Brevitee.Data.Column(Name="CreatedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string CreatedBy
	{
		get
		{
			return GetStringValue("CreatedBy");
		}
		set
		{
			SetValue("CreatedBy", value);
		}
	}

	// property:CreatedAt, columnName:CreatedAt	
	[Brevitee.Data.Column(Name="CreatedAt", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? CreatedAt
	{
		get
		{
			return GetDateTimeValue("CreatedAt");
		}
		set
		{
			SetValue("CreatedAt", value);
		}
	}

	// property:Confirmed, columnName:Confirmed	
	[Brevitee.Data.Column(Name="Confirmed", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Confirmed
	{
		get
		{
			return GetDateTimeValue("Confirmed");
		}
		set
		{
			SetValue("Confirmed", value);
		}
	}

	// property:Disabled, columnName:Disabled	
	[Brevitee.Data.Column(Name="Disabled", DbDataType="Bit", MaxLength="1", AllowNull=false)]
	public bool? Disabled
	{
		get
		{
			return GetBooleanValue("Disabled");
		}
		set
		{
			SetValue("Disabled", value);
		}
	}

	// property:DisabledBy, columnName:DisabledBy	
	[Brevitee.Data.Column(Name="DisabledBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DisabledBy
	{
		get
		{
			return GetStringValue("DisabledBy");
		}
		set
		{
			SetValue("DisabledBy", value);
		}
	}



	// start ApplicationId -> ApplicationId
	[Brevitee.Data.ForeignKey(
        Table="ApiKey",
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
			var colFilter = new ApiKeyColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the ApiKey table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ApiKeyCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ApiKey>();
			Database db = database == null ? Db.For<ApiKey>(): database;
			var results = new ApiKeyCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ApiKeyColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ApiKeyCollection Where(Func<ApiKeyColumns, QueryFilter<ApiKeyColumns>> where, OrderBy<ApiKeyColumns> orderBy = null)
		{
			return new ApiKeyCollection(new Query<ApiKeyColumns, ApiKey>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, Database db = null)
		{
			var results = new ApiKeyCollection(db, new Query<ApiKeyColumns, ApiKey>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ApiKeyCollection Where(WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy = null, Database db = null)
		{
			var results = new ApiKeyCollection(db, new Query<ApiKeyColumns, ApiKey>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ApiKeyColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ApiKeyCollection Where(QiQuery where, Database db = null)
		{
			var results = new ApiKeyCollection(db, Select<ApiKeyColumns>.From<ApiKey>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ApiKey instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ApiKey OneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
		{
			var results = new ApiKeyCollection(db, Select<ApiKeyColumns>.From<ApiKey>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ApiKeyColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ApiKey OneWhere(QiQuery where, Database db = null)
		{
			var results = new ApiKeyCollection(db, Select<ApiKeyColumns>.From<ApiKey>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ApiKey OneOrThrow(ApiKeyCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ApiKey FirstOneWhere(WhereDelegate<ApiKeyColumns> where, Database db = null)
		{
			var results = new ApiKeyCollection(db, Select<ApiKeyColumns>.From<ApiKey>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ApiKeyCollection Top(int count, WhereDelegate<ApiKeyColumns> where, OrderBy<ApiKeyColumns> orderBy, Database database = null)
		{
			ApiKeyColumns c = new ApiKeyColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<ApiKey>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<ApiKey>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ApiKeyColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ApiKeyCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ApiKeyColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ApiKeyColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ApiKeyColumns> where, Database database = null)
		{
			ApiKeyColumns c = new ApiKeyColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ApiKey>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ApiKey>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
