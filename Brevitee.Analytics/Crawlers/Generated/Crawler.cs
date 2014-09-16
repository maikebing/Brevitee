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
	[Brevitee.Data.Table("Crawler", "Analytics")]
	public partial class Crawler: Dao
	{
		public Crawler():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Crawler(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Crawler(DataRow data)
		{
			return new Crawler(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Image_CrawlerId", new ImageCollection(new Query<ImageColumns, Image>((c) => c.CrawlerId == this.Id), this, "CrawlerId"));							
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

﻿	// property:Name, columnName:Name	
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

﻿	// property:RootUrl, columnName:RootUrl	
	[Brevitee.Data.Column(Name="RootUrl", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string RootUrl
	{
		get
		{
			return GetStringValue("RootUrl");
		}
		set
		{
			SetValue("RootUrl", value);
		}
	}



				
﻿
	[Exclude]	
	public ImageCollection ImagesByCrawlerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Image_CrawlerId"))
			{
				SetChildren();
			}

			var c = (ImageCollection)this.ChildCollections["Image_CrawlerId"];
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
			var colFilter = new CrawlerColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Crawler table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CrawlerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Crawler>();
			Database db = database ?? Db.For<Crawler>();
			var results = new CrawlerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CrawlerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Where(Func<CrawlerColumns, QueryFilter<CrawlerColumns>> where, OrderBy<CrawlerColumns> orderBy = null)
		{
			return new CrawlerCollection(new Query<CrawlerColumns, Crawler>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, Database db = null)
		{
			var results = new CrawlerCollection(db, new Query<CrawlerColumns, Crawler>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy = null, Database db = null)
		{
			var results = new CrawlerCollection(db, new Query<CrawlerColumns, Crawler>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CrawlerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CrawlerCollection Where(QiQuery where, Database db = null)
		{
			var results = new CrawlerCollection(db, Select<CrawlerColumns>.From<Crawler>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Crawler instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Crawler OneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
		{
			var results = new CrawlerCollection(db, Select<CrawlerColumns>.From<Crawler>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CrawlerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Crawler OneWhere(QiQuery where, Database db = null)
		{
			var results = new CrawlerCollection(db, Select<CrawlerColumns>.From<Crawler>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Crawler OneOrThrow(CrawlerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Crawler FirstOneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
		{
			var results = new CrawlerCollection(db, Select<CrawlerColumns>.From<Crawler>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy, Database database = null)
		{
			CrawlerColumns c = new CrawlerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Crawler>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Crawler>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CrawlerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CrawlerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CrawlerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CrawlerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CrawlerColumns> where, Database database = null)
		{
			CrawlerColumns c = new CrawlerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Crawler>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Crawler>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
