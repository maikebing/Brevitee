// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Data
{
	// schema = Analytics
	// connection Name = Analytics
	[Brevitee.Data.Table("Port", "Analytics")]
	public partial class Port: Dao
	{
		public Port():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Port(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Port(DataRow data)
		{
			return new Port(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Url_PortId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.PortId == this.Id), this, "PortId"));							
		}

﻿	// property:Id, columnName:Id	
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

﻿	// property:Uuid, columnName:Uuid	
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

﻿	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="Int", MaxLength="4", AllowNull=false)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				
﻿
	[Exclude]	
	public UrlCollection UrlsByPortId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_PortId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_PortId"];
			if(!c.Loaded)
			{
				c.Load(Database);
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
			var colFilter = new PortColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Port table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static PortCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Port>();
			Database db = database ?? Db.For<Port>();
			var results = new PortCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a PortColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PortCollection Where(Func<PortColumns, QueryFilter<PortColumns>> where, OrderBy<PortColumns> orderBy = null)
		{
			return new PortCollection(new Query<PortColumns, Port>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PortCollection Where(WhereDelegate<PortColumns> where, Database db = null)
		{
			var results = new PortCollection(db, new Query<PortColumns, Port>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PortCollection Where(WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy = null, Database db = null)
		{
			var results = new PortCollection(db, new Query<PortColumns, Port>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PortColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static PortCollection Where(QiQuery where, Database db = null)
		{
			var results = new PortCollection(db, Select<PortColumns>.From<Port>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Port instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Port OneWhere(WhereDelegate<PortColumns> where, Database db = null)
		{
			var results = new PortCollection(db, Select<PortColumns>.From<Port>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<PortColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Port OneWhere(QiQuery where, Database db = null)
		{
			var results = new PortCollection(db, Select<PortColumns>.From<Port>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Port OneOrThrow(PortCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Port FirstOneWhere(WhereDelegate<PortColumns> where, Database db = null)
		{
			var results = new PortCollection(db, Select<PortColumns>.From<Port>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static PortCollection Top(int count, WhereDelegate<PortColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static PortCollection Top(int count, WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy, Database database = null)
		{
			PortColumns c = new PortColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Port>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Port>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PortColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<PortCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a PortColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between PortColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<PortColumns> where, Database database = null)
		{
			PortColumns c = new PortColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Port>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Port>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
