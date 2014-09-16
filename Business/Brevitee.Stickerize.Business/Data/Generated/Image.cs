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
	[Brevitee.Data.Table("Image", "Stickerize")]
	public partial class Image: Dao
	{
		public Image():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Image(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Image(DataRow data)
		{
			return new Image(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Stickerizer_ImageId", new StickerizerCollection(new Query<StickerizerColumns, Stickerizer>((c) => c.ImageId == this.Id), this, "ImageId"));	﻿
            this.ChildCollections.Add("Stickerizee_ImageId", new StickerizeeCollection(new Query<StickerizeeColumns, Stickerizee>((c) => c.ImageId == this.Id), this, "ImageId"));	﻿
            this.ChildCollections.Add("Sticker_ImageId", new StickerCollection(new Query<StickerColumns, Sticker>((c) => c.ImageId == this.Id), this, "ImageId"));							
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

﻿	// property:Url, columnName:Url	
	[Brevitee.Data.Column(Name="Url", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Url
	{
		get
		{
			return GetStringValue("Url");
		}
		set
		{
			SetValue("Url", value);
		}
	}



				
﻿
	[Exclude]	
	public StickerizerCollection StickerizersByImageId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerizer_ImageId"))
			{
				SetChildren();
			}

			var c = (StickerizerCollection)this.ChildCollections["Stickerizer_ImageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizeeCollection StickerizeesByImageId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerizee_ImageId"))
			{
				SetChildren();
			}

			var c = (StickerizeeCollection)this.ChildCollections["Stickerizee_ImageId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerCollection StickersByImageId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Sticker_ImageId"))
			{
				SetChildren();
			}

			var c = (StickerCollection)this.ChildCollections["Sticker_ImageId"];
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
			var colFilter = new ImageColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Image table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ImageCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Image>();
			Database db = database ?? Db.For<Image>();
			var results = new ImageCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ImageColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageCollection Where(Func<ImageColumns, QueryFilter<ImageColumns>> where, OrderBy<ImageColumns> orderBy = null)
		{
			return new ImageCollection(new Query<ImageColumns, Image>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageCollection Where(WhereDelegate<ImageColumns> where, Database db = null)
		{
			var results = new ImageCollection(db, new Query<ImageColumns, Image>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ImageCollection Where(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database db = null)
		{
			var results = new ImageCollection(db, new Query<ImageColumns, Image>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ImageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static ImageCollection Where(QiQuery where, Database db = null)
		{
			var results = new ImageCollection(db, Select<ImageColumns>.From<Image>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Image instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Image OneWhere(WhereDelegate<ImageColumns> where, Database db = null)
		{
			var results = new ImageCollection(db, Select<ImageColumns>.From<Image>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ImageColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Image OneWhere(QiQuery where, Database db = null)
		{
			var results = new ImageCollection(db, Select<ImageColumns>.From<Image>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Image OneOrThrow(ImageCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Image FirstOneWhere(WhereDelegate<ImageColumns> where, Database db = null)
		{
			var results = new ImageCollection(db, Select<ImageColumns>.From<Image>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static ImageCollection Top(int count, WhereDelegate<ImageColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static ImageCollection Top(int count, WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy, Database database = null)
		{
			ImageColumns c = new ImageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Image>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Image>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ImageCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ImageColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ImageColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<ImageColumns> where, Database database = null)
		{
			ImageColumns c = new ImageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Image>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Image>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
