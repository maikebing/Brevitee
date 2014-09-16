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
	[Brevitee.Data.Table("Sticker", "Stickerize")]
	public partial class Sticker: Dao
	{
		public Sticker():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Sticker(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Sticker(DataRow data)
		{
			return new Sticker(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Stickerization_StickerId", new StickerizationCollection(new Query<StickerizationColumns, Stickerization>((c) => c.StickerId == this.Id), this, "StickerId"));							
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

﻿	// property:Created, columnName:Created	
	[Brevitee.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime? Created
	{
		get
		{
			return GetDateTimeValue("Created");
		}
		set
		{
			SetValue("Created", value);
		}
	}

﻿	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}

﻿	// property:Description, columnName:Description	
	[Brevitee.Data.Column(Name="Description", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}



﻿	// start ImageId -> ImageId
	[Brevitee.Data.ForeignKey(
        Table="Sticker",
		Name="ImageId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
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
				_imageOfImageId = Brevitee.Stickerize.Business.Data.Image.OneWhere(c => c.KeyColumn == this.ImageId);
			}
			return _imageOfImageId;
		}
	}
	
				
﻿
	[Exclude]	
	public StickerizationCollection StickerizationsByStickerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerization_StickerId"))
			{
				SetChildren();
			}

			var c = (StickerizationCollection)this.ChildCollections["Stickerization_StickerId"];
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
			var colFilter = new StickerColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Sticker table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Sticker>();
			Database db = database ?? Db.For<Sticker>();
			var results = new StickerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerCollection Where(Func<StickerColumns, QueryFilter<StickerColumns>> where, OrderBy<StickerColumns> orderBy = null)
		{
			return new StickerCollection(new Query<StickerColumns, Sticker>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerCollection Where(WhereDelegate<StickerColumns> where, Database db = null)
		{
			var results = new StickerCollection(db, new Query<StickerColumns, Sticker>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerCollection Where(WhereDelegate<StickerColumns> where, OrderBy<StickerColumns> orderBy = null, Database db = null)
		{
			var results = new StickerCollection(db, new Query<StickerColumns, Sticker>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerCollection(db, Select<StickerColumns>.From<Sticker>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Sticker instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Sticker OneWhere(WhereDelegate<StickerColumns> where, Database db = null)
		{
			var results = new StickerCollection(db, Select<StickerColumns>.From<Sticker>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Sticker OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerCollection(db, Select<StickerColumns>.From<Sticker>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Sticker OneOrThrow(StickerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Sticker FirstOneWhere(WhereDelegate<StickerColumns> where, Database db = null)
		{
			var results = new StickerCollection(db, Select<StickerColumns>.From<Sticker>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerCollection Top(int count, WhereDelegate<StickerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerCollection Top(int count, WhereDelegate<StickerColumns> where, OrderBy<StickerColumns> orderBy, Database database = null)
		{
			StickerColumns c = new StickerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Sticker>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Sticker>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerColumns> where, Database database = null)
		{
			StickerColumns c = new StickerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Sticker>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Sticker>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
