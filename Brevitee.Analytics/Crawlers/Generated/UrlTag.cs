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
	[Brevitee.Data.Table("UrlTag", "Analytics")]
	public partial class UrlTag: Dao
	{
		public UrlTag():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public UrlTag(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator UrlTag(DataRow data)
		{
			return new UrlTag(data);
		}

		private void SetChildren()
		{
						
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



﻿	// start UrlId -> UrlId
	[Brevitee.Data.ForeignKey(
        Table="UrlTag",
		Name="UrlId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Url",
		Suffix="1")]
	public long? UrlId
	{
		get
		{
			return GetLongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	Url _urlOfUrlId;
	public Url UrlOfUrlId
	{
		get
		{
			if(_urlOfUrlId == null)
			{
				_urlOfUrlId = Brevitee.Analytics.Data.Url.OneWhere(c => c.KeyColumn == this.UrlId);
			}
			return _urlOfUrlId;
		}
	}
	
﻿	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
        Table="UrlTag",
		Name="TagId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public long? TagId
	{
		get
		{
			return GetLongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = Brevitee.Analytics.Data.Tag.OneWhere(c => c.KeyColumn == this.TagId);
			}
			return _tagOfTagId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UrlTagColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the UrlTag table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static UrlTagCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<UrlTag>();
			Database db = database ?? Db.For<UrlTag>();
			var results = new UrlTagCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a UrlTagColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Where(Func<UrlTagColumns, QueryFilter<UrlTagColumns>> where, OrderBy<UrlTagColumns> orderBy = null)
		{
			return new UrlTagCollection(new Query<UrlTagColumns, UrlTag>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			var results = new UrlTagCollection(db, new Query<UrlTagColumns, UrlTag>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy = null, Database db = null)
		{
			var results = new UrlTagCollection(db, new Query<UrlTagColumns, UrlTag>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UrlTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UrlTagCollection Where(QiQuery where, Database db = null)
		{
			var results = new UrlTagCollection(db, Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single UrlTag instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTag OneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			var results = new UrlTagCollection(db, Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<UrlTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static UrlTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new UrlTagCollection(db, Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UrlTag OneOrThrow(UrlTagCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTag FirstOneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
		{
			var results = new UrlTagCollection(db, Select<UrlTagColumns>.From<UrlTag>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy, Database database = null)
		{
			UrlTagColumns c = new UrlTagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<UrlTag>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<UrlTag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UrlTagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<UrlTagCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a UrlTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between UrlTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<UrlTagColumns> where, Database database = null)
		{
			UrlTagColumns c = new UrlTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UrlTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UrlTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
