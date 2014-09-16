// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Automation.ContinuousIntegration.Data
{
	// schema = BuildAutomation
	// connection Name = BuildAutomation
	[Brevitee.Data.Table("BuildJob", "BuildAutomation")]
	public partial class BuildJob: Dao
	{
		public BuildJob():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public BuildJob(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator BuildJob(DataRow data)
		{
			return new BuildJob(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("BuildResult_BuildJobId", new BuildResultCollection(new Query<BuildResultColumns, BuildResult>((c) => c.BuildJobId == this.Id), this, "BuildJobId"));							
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
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

	// property:HostName, columnName:HostName	
	[Brevitee.Data.Column(Name="HostName", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string HostName
	{
		get
		{
			return GetStringValue("HostName");
		}
		set
		{
			SetValue("HostName", value);
		}
	}

	// property:OutputPath, columnName:OutputPath	
	[Brevitee.Data.Column(Name="OutputPath", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string OutputPath
	{
		get
		{
			return GetStringValue("OutputPath");
		}
		set
		{
			SetValue("OutputPath", value);
		}
	}



				

	[Exclude]	
	public BuildResultCollection BuildResultsByBuildJobId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("BuildResult_BuildJobId"))
			{
				SetChildren();
			}

			var c = (BuildResultCollection)this.ChildCollections["BuildResult_BuildJobId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new BuildJobColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the BuildJob table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static BuildJobCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<BuildJob>();
			Database db = database == null ? Db.For<BuildJob>(): database;
			return new BuildJobCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a BuildJobColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BuildJobCollection Where(Func<BuildJobColumns, QueryFilter<BuildJobColumns>> where, OrderBy<BuildJobColumns> orderBy = null)
		{
			return new BuildJobCollection(new Query<BuildJobColumns, BuildJob>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BuildJobCollection Where(WhereDelegate<BuildJobColumns> where, Database db = null)
		{
			return new BuildJobCollection(new Query<BuildJobColumns, BuildJob>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static BuildJobCollection Where(WhereDelegate<BuildJobColumns> where, OrderBy<BuildJobColumns> orderBy = null, Database db = null)
		{
			return new BuildJobCollection(new Query<BuildJobColumns, BuildJob>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<BuildJobColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static BuildJobCollection Where(QiQuery where, Database db = null)
		{
			return new BuildJobCollection(Select<BuildJobColumns>.From<BuildJob>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single BuildJob instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BuildJob OneWhere(WhereDelegate<BuildJobColumns> where, Database db = null)
		{
			var results = new BuildJobCollection(Select<BuildJobColumns>.From<BuildJob>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<BuildJobColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static BuildJob OneWhere(QiQuery where, Database db = null)
		{
			var results = new BuildJobCollection(Select<BuildJobColumns>.From<BuildJob>().Where(where, db));
			return OneOrThrow(results);
		}

		private static BuildJob OneOrThrow(BuildJobCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BuildJob FirstOneWhere(WhereDelegate<BuildJobColumns> where, Database db = null)
		{
			var results = new BuildJobCollection(Select<BuildJobColumns>.From<BuildJob>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static BuildJobCollection Top(int count, WhereDelegate<BuildJobColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static BuildJobCollection Top(int count, WhereDelegate<BuildJobColumns> where, OrderBy<BuildJobColumns> orderBy, Database database = null)
		{
			BuildJobColumns c = new BuildJobColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<BuildJob>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<BuildJob>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<BuildJobColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<BuildJobCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a BuildJobColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BuildJobColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<BuildJobColumns> where, Database database = null)
		{
			BuildJobColumns c = new BuildJobColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<BuildJob>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<BuildJob>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
