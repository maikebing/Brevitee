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
	[Brevitee.Data.Table("ImageTag", "Analytics")]
	public partial class ImageTag: Dao
	{
		public ImageTag():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ImageTag(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator ImageTag(DataRow data)
		{
			return new ImageTag(data);
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



﻿	// start ImageId -> ImageId
	[Brevitee.Data.ForeignKey(
        Table="ImageTag",
		Name="ImageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Image",
		Suffix="1")]
	public long? ImageId
	{
		get
		{
			return GetLongValue("ImageId");
		}
		set
		{
			SetValue("ImageId", value);
		}
	}

	Image _imageOfImageId;
	public Image ImageOfImageId
	{
		get
		{
			if(_imageOfImageId == null)
			{
				_imageOfImageId = Brevitee.Analytics.Data.Image.OneWhere(c => c.KeyColumn == this.ImageId);
			}
			return _imageOfImageId;
		}
	}
	
﻿	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
        Table="ImageTag",
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
			var colFilter = new ImageTagColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the ImageTag table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ImageTagCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ImageTag>();
			Database db = database ?? Db.For<ImageTag>();
			var results = new ImageTagCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ImageTagColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageTagCollection Where(Func<ImageTagColumns, QueryFilter<ImageTagColumns>> where, OrderBy<ImageTagColumns> orderBy = null)
		{
			return new ImageTagCollection(new Query<ImageTagColumns, ImageTag>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			var results = new ImageTagCollection(db, new Query<ImageTagColumns, ImageTag>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null)
		{
			var results = new ImageTagCollection(db, new Query<ImageTagColumns, ImageTag>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ImageTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ImageTagCollection Where(QiQuery where, Database db = null)
		{
			var results = new ImageTagCollection(db, Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ImageTag instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageTag OneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			var results = new ImageTagCollection(db, Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ImageTagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ImageTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new ImageTagCollection(db, Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ImageTag OneOrThrow(ImageTagCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageTag FirstOneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			var results = new ImageTagCollection(db, Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy, Database database = null)
		{
			ImageTagColumns c = new ImageTagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<ImageTag>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<ImageTag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageTagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ImageTagCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageTagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageTagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ImageTagColumns> where, Database database = null)
		{
			ImageTagColumns c = new ImageTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ImageTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ImageTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
