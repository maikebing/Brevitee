// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Classification
{
	// schema = Classification
	// connection Name = Classification
	[Brevitee.Data.Table("Category", "Classification")]
	public partial class Category: Dao
	{
		public Category():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Category(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Category(DataRow data)
		{
			return new Category(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Feature_CategoryId", new FeatureCollection(new Query<FeatureColumns, Feature>((c) => c.CategoryId == this.Id), this, "CategoryId"));							
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

﻿	// property:DocumentCount, columnName:DocumentCount	
	[Brevitee.Data.Column(Name="DocumentCount", DbDataType="BigInt", MaxLength="8", AllowNull=false)]
	public long? DocumentCount
	{
		get
		{
			return GetLongValue("DocumentCount");
		}
		set
		{
			SetValue("DocumentCount", value);
		}
	}



				
﻿
	[Exclude]	
	public FeatureCollection FeaturesByCategoryId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Feature_CategoryId"))
			{
				SetChildren();
			}

			var c = (FeatureCollection)this.ChildCollections["Feature_CategoryId"];
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
			var colFilter = new CategoryColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Category table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CategoryCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Category>();
			Database db = database ?? Db.For<Category>();
			var results = new CategoryCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CategoryColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryCollection Where(Func<CategoryColumns, QueryFilter<CategoryColumns>> where, OrderBy<CategoryColumns> orderBy = null)
		{
			return new CategoryCollection(new Query<CategoryColumns, Category>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryCollection Where(WhereDelegate<CategoryColumns> where, Database db = null)
		{
			var results = new CategoryCollection(db, new Query<CategoryColumns, Category>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CategoryCollection Where(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database db = null)
		{
			var results = new CategoryCollection(db, new Query<CategoryColumns, Category>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CategoryCollection Where(QiQuery where, Database db = null)
		{
			var results = new CategoryCollection(db, Select<CategoryColumns>.From<Category>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Category instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Category OneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
		{
			var results = new CategoryCollection(db, Select<CategoryColumns>.From<Category>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Category OneWhere(QiQuery where, Database db = null)
		{
			var results = new CategoryCollection(db, Select<CategoryColumns>.From<Category>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Category OneOrThrow(CategoryCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Category FirstOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
		{
			var results = new CategoryCollection(db, Select<CategoryColumns>.From<Category>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database database = null)
		{
			CategoryColumns c = new CategoryColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Category>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Category>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<CategoryCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CategoryColumns> where, Database database = null)
		{
			CategoryColumns c = new CategoryColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Category>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Category>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
