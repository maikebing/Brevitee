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
	[Brevitee.Data.Table("SubSectionStickerizable", "Stickerize")]
	public partial class SubSectionStickerizable: Dao
	{
		public SubSectionStickerizable():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SubSectionStickerizable(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator SubSectionStickerizable(DataRow data)
		{
			return new SubSectionStickerizable(data);
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



﻿	// start SubSectionId -> SubSectionId
	[Brevitee.Data.ForeignKey(
        Table="SubSectionStickerizable",
		Name="SubSectionId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="SubSection",
		Suffix="1")]
	public long? SubSectionId
	{
		get
		{
			return GetLongValue("SubSectionId");
		}
		set
		{
			SetValue("SubSectionId", value);
		}
	}

	SubSection _subSectionOfSubSectionId;
	public SubSection SubSectionOfSubSectionId
	{
		get
		{
			if(_subSectionOfSubSectionId == null)
			{
				_subSectionOfSubSectionId = Brevitee.Stickerize.Business.Data.SubSection.OneWhere(c => c.KeyColumn == this.SubSectionId);
			}
			return _subSectionOfSubSectionId;
		}
	}
	
﻿	// start StickerizableId -> StickerizableId
	[Brevitee.Data.ForeignKey(
        Table="SubSectionStickerizable",
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
			var colFilter = new SubSectionStickerizableColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the SubSectionStickerizable table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SubSectionStickerizableCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<SubSectionStickerizable>();
			Database db = database ?? Db.For<SubSectionStickerizable>();
			var results = new SubSectionStickerizableCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SubSectionStickerizableColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizableCollection Where(Func<SubSectionStickerizableColumns, QueryFilter<SubSectionStickerizableColumns>> where, OrderBy<SubSectionStickerizableColumns> orderBy = null)
		{
			return new SubSectionStickerizableCollection(new Query<SubSectionStickerizableColumns, SubSectionStickerizable>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizableCollection Where(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
		{
			var results = new SubSectionStickerizableCollection(db, new Query<SubSectionStickerizableColumns, SubSectionStickerizable>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizableCollection Where(WhereDelegate<SubSectionStickerizableColumns> where, OrderBy<SubSectionStickerizableColumns> orderBy = null, Database db = null)
		{
			var results = new SubSectionStickerizableCollection(db, new Query<SubSectionStickerizableColumns, SubSectionStickerizable>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SubSectionStickerizableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SubSectionStickerizableCollection Where(QiQuery where, Database db = null)
		{
			var results = new SubSectionStickerizableCollection(db, Select<SubSectionStickerizableColumns>.From<SubSectionStickerizable>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SubSectionStickerizable instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizable OneWhere(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
		{
			var results = new SubSectionStickerizableCollection(db, Select<SubSectionStickerizableColumns>.From<SubSectionStickerizable>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SubSectionStickerizableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SubSectionStickerizable OneWhere(QiQuery where, Database db = null)
		{
			var results = new SubSectionStickerizableCollection(db, Select<SubSectionStickerizableColumns>.From<SubSectionStickerizable>().Where(where, db));
			return OneOrThrow(results);
		}

		private static SubSectionStickerizable OneOrThrow(SubSectionStickerizableCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizable FirstOneWhere(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
		{
			var results = new SubSectionStickerizableCollection(db, Select<SubSectionStickerizableColumns>.From<SubSectionStickerizable>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizableCollection Top(int count, WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SubSectionStickerizableCollection Top(int count, WhereDelegate<SubSectionStickerizableColumns> where, OrderBy<SubSectionStickerizableColumns> orderBy, Database database = null)
		{
			SubSectionStickerizableColumns c = new SubSectionStickerizableColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<SubSectionStickerizable>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<SubSectionStickerizable>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SubSectionStickerizableColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SubSectionStickerizableCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionStickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionStickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SubSectionStickerizableColumns> where, Database database = null)
		{
			SubSectionStickerizableColumns c = new SubSectionStickerizableColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<SubSectionStickerizable>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<SubSectionStickerizable>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
