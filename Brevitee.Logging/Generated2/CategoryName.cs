// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Logging
{
	// schema = DaoLogger2
	// connection Name = DaoLogger2
	[Brevitee.Data.Table("CategoryName", "DaoLogger2")]
	public partial class CategoryName: Dao
	{
		public CategoryName():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public CategoryName(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator CategoryName(DataRow data)
		{
			return new CategoryName(data);
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



				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CategoryNameColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the CategoryName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static CategoryNameCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<CategoryName>();
			Database db = database == null ? Db.For<CategoryName>(): database;
			return new CategoryNameCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a CategoryNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Where(Func<CategoryNameColumns, QueryFilter<CategoryNameColumns>> where, OrderBy<CategoryNameColumns> orderBy = null)
		{
			return new CategoryNameCollection(new Query<CategoryNameColumns, CategoryName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, Database db = null)
		{
			return new CategoryNameCollection(new Query<CategoryNameColumns, CategoryName>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Where(WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy = null, Database db = null)
		{
			return new CategoryNameCollection(new Query<CategoryNameColumns, CategoryName>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CategoryNameCollection Where(QiQuery where, Database db = null)
		{
			return new CategoryNameCollection(Select<CategoryNameColumns>.From<CategoryName>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single CategoryName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryName OneWhere(WhereDelegate<CategoryNameColumns> where, Database db = null)
		{
			var results = new CategoryNameCollection(Select<CategoryNameColumns>.From<CategoryName>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<CategoryNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static CategoryName OneWhere(QiQuery where, Database db = null)
		{
			var results = new CategoryNameCollection(Select<CategoryNameColumns>.From<CategoryName>().Where(where, db));
			return OneOrThrow(results);
		}

		private static CategoryName OneOrThrow(CategoryNameCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryName FirstOneWhere(WhereDelegate<CategoryNameColumns> where, Database db = null)
		{
			var results = new CategoryNameCollection(Select<CategoryNameColumns>.From<CategoryName>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static CategoryNameCollection Top(int count, WhereDelegate<CategoryNameColumns> where, OrderBy<CategoryNameColumns> orderBy, Database database = null)
		{
			CategoryNameColumns c = new CategoryNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<CategoryName>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<CategoryName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryNameColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<CategoryNameCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a CategoryNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between CategoryNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<CategoryNameColumns> where, Database database = null)
		{
			CategoryNameColumns c = new CategoryNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<CategoryName>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<CategoryName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
