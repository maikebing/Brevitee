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
	[Brevitee.Data.Table("SourceName", "DaoLogger2")]
	public partial class SourceName: Dao
	{
		public SourceName():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public SourceName(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator SourceName(DataRow data)
		{
			return new SourceName(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="BigInt", MaxLength="8")]
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
	[Brevitee.Data.Column(Name="Value", ExtractedType="VarChar", MaxLength="4000", AllowNull=true)]
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
			var colFilter = new SourceNameColumns();
			return (colFilter.Id == IdValue);
		}
		/// <summary>
		/// Return every record in the SourceName table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SourceNameCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<SourceName>();
			Database db = database == null ? _.Db.For<SourceName>(): database;
			return new SourceNameCollection(sql.GetDataTable(db));
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SourceNameColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SourceNameCollection Where(Func<SourceNameColumns, QueryFilter<SourceNameColumns>> where, OrderBy<SourceNameColumns> orderBy = null)
		{
			return new SourceNameCollection(new Query<SourceNameColumns, SourceName>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SourceNameCollection Where(WhereDelegate<SourceNameColumns> where, Database db = null)
		{
			return new SourceNameCollection(new Query<SourceNameColumns, SourceName>(where, db), true);
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SourceNameCollection Where(WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy = null, Database db = null)
		{
			return new SourceNameCollection(new Query<SourceNameColumns, SourceName>(where, orderBy, db), true);
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SourceNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SourceNameCollection Where(QiQuery where, Database db = null)
		{
			return new SourceNameCollection(Select<SourceNameColumns>.From<SourceName>().Where(where, db));
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SourceName instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SourceName OneWhere(WhereDelegate<SourceNameColumns> where, Database db = null)
		{
			var results = new SourceNameCollection(Select<SourceNameColumns>.From<SourceName>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SourceNameColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SourceName OneWhere(QiQuery where, Database db = null)
		{
			var results = new SourceNameCollection(Select<SourceNameColumns>.From<SourceName>().Where(where, db));
			return OneOrThrow(results);
		}

		private static SourceName OneOrThrow(SourceNameCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SourceName FirstOneWhere(WhereDelegate<SourceNameColumns> where, Database db = null)
		{
			var results = new SourceNameCollection(Select<SourceNameColumns>.From<SourceName>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SourceNameCollection Top(int count, WhereDelegate<SourceNameColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SourceNameCollection Top(int count, WhereDelegate<SourceNameColumns> where, OrderBy<SourceNameColumns> orderBy, Database database = null)
		{
			SourceNameColumns c = new SourceNameColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<SourceName>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<SourceName>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SourceNameColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<SourceNameCollection>(0);
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SourceNameColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SourceNameColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SourceNameColumns> where, Database database = null)
		{
			SourceNameColumns c = new SourceNameColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<SourceName>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<SourceName>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
