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
	[Brevitee.Data.Table("Fragment", "Analytics")]
	public partial class Fragment: Dao
	{
		public Fragment():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Fragment(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Fragment(DataRow data)
		{
			return new Fragment(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Url_FragmentId", new UrlCollection(new Query<UrlColumns, Url>((c) => c.FragmentId == this.Id), this, "FragmentId"));							
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
	[Brevitee.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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



				
﻿
	[Exclude]	
	public UrlCollection UrlsByFragmentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Url_FragmentId"))
			{
				SetChildren();
			}

			var c = (UrlCollection)this.ChildCollections["Url_FragmentId"];
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
			var colFilter = new FragmentColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Fragment table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static FragmentCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Fragment>();
			Database db = database ?? Db.For<Fragment>();
			var results = new FragmentCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a FragmentColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static FragmentCollection Where(Func<FragmentColumns, QueryFilter<FragmentColumns>> where, OrderBy<FragmentColumns> orderBy = null)
		{
			return new FragmentCollection(new Query<FragmentColumns, Fragment>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static FragmentCollection Where(WhereDelegate<FragmentColumns> where, Database db = null)
		{
			var results = new FragmentCollection(db, new Query<FragmentColumns, Fragment>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static FragmentCollection Where(WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy = null, Database db = null)
		{
			var results = new FragmentCollection(db, new Query<FragmentColumns, Fragment>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<FragmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static FragmentCollection Where(QiQuery where, Database db = null)
		{
			var results = new FragmentCollection(db, Select<FragmentColumns>.From<Fragment>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Fragment instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Fragment OneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
		{
			var results = new FragmentCollection(db, Select<FragmentColumns>.From<Fragment>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<FragmentColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Fragment OneWhere(QiQuery where, Database db = null)
		{
			var results = new FragmentCollection(db, Select<FragmentColumns>.From<Fragment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Fragment OneOrThrow(FragmentCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Fragment FirstOneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
		{
			var results = new FragmentCollection(db, Select<FragmentColumns>.From<Fragment>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static FragmentCollection Top(int count, WhereDelegate<FragmentColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static FragmentCollection Top(int count, WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy, Database database = null)
		{
			FragmentColumns c = new FragmentColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Fragment>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Fragment>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<FragmentColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<FragmentCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a FragmentColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between FragmentColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<FragmentColumns> where, Database database = null)
		{
			FragmentColumns c = new FragmentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Fragment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Fragment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
