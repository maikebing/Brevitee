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
	[Brevitee.Data.Table("StickerizableListStickerizable", "Stickerize")]
	public partial class StickerizableListStickerizable: Dao
	{
		public StickerizableListStickerizable():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public StickerizableListStickerizable(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator StickerizableListStickerizable(DataRow data)
		{
			return new StickerizableListStickerizable(data);
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



﻿	// start StickerizableListId -> StickerizableListId
	[Brevitee.Data.ForeignKey(
        Table="StickerizableListStickerizable",
		Name="StickerizableListId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="StickerizableList",
		Suffix="1")]
	public long? StickerizableListId
	{
		get
		{
			return GetLongValue("StickerizableListId");
		}
		set
		{
			SetValue("StickerizableListId", value);
		}
	}

	StickerizableList _stickerizableListOfStickerizableListId;
	public StickerizableList StickerizableListOfStickerizableListId
	{
		get
		{
			if(_stickerizableListOfStickerizableListId == null)
			{
				_stickerizableListOfStickerizableListId = Brevitee.Stickerize.Business.Data.StickerizableList.OneWhere(c => c.KeyColumn == this.StickerizableListId);
			}
			return _stickerizableListOfStickerizableListId;
		}
	}
	
﻿	// start StickerizableId -> StickerizableId
	[Brevitee.Data.ForeignKey(
        Table="StickerizableListStickerizable",
		Name="StickerizableId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Stickerizable",
		Suffix="2")]
	public long? StickerizableId
	{
		get
		{
			return GetLongValue("StickerizableId");
		}
		set
		{
			SetValue("StickerizableId", value);
		}
	}

	Stickerizable _stickerizableOfStickerizableId;
	public Stickerizable StickerizableOfStickerizableId
	{
		get
		{
			if(_stickerizableOfStickerizableId == null)
			{
				_stickerizableOfStickerizableId = Brevitee.Stickerize.Business.Data.Stickerizable.OneWhere(c => c.KeyColumn == this.StickerizableId);
			}
			return _stickerizableOfStickerizableId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new StickerizableListStickerizableColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the StickerizableListStickerizable table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerizableListStickerizableCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<StickerizableListStickerizable>();
			Database db = database ?? Db.For<StickerizableListStickerizable>();
			var results = new StickerizableListStickerizableCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizableCollection Where(Func<StickerizableListStickerizableColumns, QueryFilter<StickerizableListStickerizableColumns>> where, OrderBy<StickerizableListStickerizableColumns> orderBy = null)
		{
			return new StickerizableListStickerizableCollection(new Query<StickerizableListStickerizableColumns, StickerizableListStickerizable>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizableCollection Where(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
		{
			var results = new StickerizableListStickerizableCollection(db, new Query<StickerizableListStickerizableColumns, StickerizableListStickerizable>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizableCollection Where(WhereDelegate<StickerizableListStickerizableColumns> where, OrderBy<StickerizableListStickerizableColumns> orderBy = null, Database db = null)
		{
			var results = new StickerizableListStickerizableCollection(db, new Query<StickerizableListStickerizableColumns, StickerizableListStickerizable>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizableListStickerizableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizableListStickerizableCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerizableListStickerizableCollection(db, Select<StickerizableListStickerizableColumns>.From<StickerizableListStickerizable>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single StickerizableListStickerizable instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizable OneWhere(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
		{
			var results = new StickerizableListStickerizableCollection(db, Select<StickerizableListStickerizableColumns>.From<StickerizableListStickerizable>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizableListStickerizableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizableListStickerizable OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerizableListStickerizableCollection(db, Select<StickerizableListStickerizableColumns>.From<StickerizableListStickerizable>().Where(where, db));
			return OneOrThrow(results);
		}

		private static StickerizableListStickerizable OneOrThrow(StickerizableListStickerizableCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizable FirstOneWhere(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
		{
			var results = new StickerizableListStickerizableCollection(db, Select<StickerizableListStickerizableColumns>.From<StickerizableListStickerizable>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizableCollection Top(int count, WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListStickerizableCollection Top(int count, WhereDelegate<StickerizableListStickerizableColumns> where, OrderBy<StickerizableListStickerizableColumns> orderBy, Database database = null)
		{
			StickerizableListStickerizableColumns c = new StickerizableListStickerizableColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<StickerizableListStickerizable>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<StickerizableListStickerizable>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerizableListStickerizableColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerizableListStickerizableCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerizableListStickerizableColumns> where, Database database = null)
		{
			StickerizableListStickerizableColumns c = new StickerizableListStickerizableColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<StickerizableListStickerizable>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<StickerizableListStickerizable>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
