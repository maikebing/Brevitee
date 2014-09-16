// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Stickerize.Business.Data
{
	// schema = Stickerize
	// connection Name = Stickerize
	[Brevitee.Data.Table("StickerizerStickerizee", "Stickerize")]
	public partial class StickerizerStickerizee: Dao
	{
		public StickerizerStickerizee():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public StickerizerStickerizee(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator StickerizerStickerizee(DataRow data)
		{
			return new StickerizerStickerizee(data);
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



﻿	// start StickerizerId -> StickerizerId
	[Brevitee.Data.ForeignKey(
        Table="StickerizerStickerizee",
		Name="StickerizerId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Stickerizer",
		Suffix="1")]
	public long? StickerizerId
	{
		get
		{
			return GetLongValue("StickerizerId");
		}
		set
		{
			SetValue("StickerizerId", value);
		}
	}

	Stickerizer _stickerizerOfStickerizerId;
	public Stickerizer StickerizerOfStickerizerId
	{
		get
		{
			if(_stickerizerOfStickerizerId == null)
			{
				_stickerizerOfStickerizerId = Brevitee.Stickerize.Business.Data.Stickerizer.OneWhere(c => c.KeyColumn == this.StickerizerId);
			}
			return _stickerizerOfStickerizerId;
		}
	}
	
﻿	// start StickerizeeId -> StickerizeeId
	[Brevitee.Data.ForeignKey(
        Table="StickerizerStickerizee",
		Name="StickerizeeId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Stickerizee",
		Suffix="2")]
	public long? StickerizeeId
	{
		get
		{
			return GetLongValue("StickerizeeId");
		}
		set
		{
			SetValue("StickerizeeId", value);
		}
	}

	Stickerizee _stickerizeeOfStickerizeeId;
	public Stickerizee StickerizeeOfStickerizeeId
	{
		get
		{
			if(_stickerizeeOfStickerizeeId == null)
			{
				_stickerizeeOfStickerizeeId = Brevitee.Stickerize.Business.Data.Stickerizee.OneWhere(c => c.KeyColumn == this.StickerizeeId);
			}
			return _stickerizeeOfStickerizeeId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new StickerizerStickerizeeColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the StickerizerStickerizee table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerizerStickerizeeCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<StickerizerStickerizee>();
			Database db = database ?? Db.For<StickerizerStickerizee>();
			var results = new StickerizerStickerizeeCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizeeCollection Where(Func<StickerizerStickerizeeColumns, QueryFilter<StickerizerStickerizeeColumns>> where, OrderBy<StickerizerStickerizeeColumns> orderBy = null)
		{
			return new StickerizerStickerizeeCollection(new Query<StickerizerStickerizeeColumns, StickerizerStickerizee>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizeeCollection Where(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
		{
			var results = new StickerizerStickerizeeCollection(db, new Query<StickerizerStickerizeeColumns, StickerizerStickerizee>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizeeCollection Where(WhereDelegate<StickerizerStickerizeeColumns> where, OrderBy<StickerizerStickerizeeColumns> orderBy = null, Database db = null)
		{
			var results = new StickerizerStickerizeeCollection(db, new Query<StickerizerStickerizeeColumns, StickerizerStickerizee>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizerStickerizeeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizerStickerizeeCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerizerStickerizeeCollection(db, Select<StickerizerStickerizeeColumns>.From<StickerizerStickerizee>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single StickerizerStickerizee instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizee OneWhere(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
		{
			var results = new StickerizerStickerizeeCollection(db, Select<StickerizerStickerizeeColumns>.From<StickerizerStickerizee>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizerStickerizeeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizerStickerizee OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerizerStickerizeeCollection(db, Select<StickerizerStickerizeeColumns>.From<StickerizerStickerizee>().Where(where, db));
			return OneOrThrow(results);
		}

		private static StickerizerStickerizee OneOrThrow(StickerizerStickerizeeCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizee FirstOneWhere(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
		{
			var results = new StickerizerStickerizeeCollection(db, Select<StickerizerStickerizeeColumns>.From<StickerizerStickerizee>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizeeCollection Top(int count, WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizerStickerizeeCollection Top(int count, WhereDelegate<StickerizerStickerizeeColumns> where, OrderBy<StickerizerStickerizeeColumns> orderBy, Database database = null)
		{
			StickerizerStickerizeeColumns c = new StickerizerStickerizeeColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<StickerizerStickerizee>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<StickerizerStickerizee>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerizerStickerizeeColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerizerStickerizeeCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerStickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerStickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerizerStickerizeeColumns> where, Database database = null)
		{
			StickerizerStickerizeeColumns c = new StickerizerStickerizeeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<StickerizerStickerizee>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<StickerizerStickerizee>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
