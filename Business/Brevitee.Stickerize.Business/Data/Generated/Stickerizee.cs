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
	[Brevitee.Data.Table("Stickerizee", "Stickerize")]
	public partial class Stickerizee: Dao
	{
		public Stickerizee():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Stickerizee(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Stickerizee(DataRow data)
		{
			return new Stickerizee(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Stickerization_StickerizeeId", new StickerizationCollection(new Query<StickerizationColumns, Stickerization>((c) => c.StickerizeeId == this.Id), this, "StickerizeeId"));	﻿
            this.ChildCollections.Add("StickerizerStickerizee_StickerizeeId", new StickerizerStickerizeeCollection(new Query<StickerizerStickerizeeColumns, StickerizerStickerizee>((c) => c.StickerizeeId == this.Id), this, "StickerizeeId"));							﻿
            this.ChildCollections.Add("Stickerizee_StickerizerStickerizee_Stickerizer",  new XrefDaoCollection<StickerizerStickerizee, Stickerizer>(this, false));
				
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

﻿	// property:DisplayName, columnName:DisplayName	
	[Brevitee.Data.Column(Name="DisplayName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string DisplayName
	{
		get
		{
			return GetStringValue("DisplayName");
		}
		set
		{
			SetValue("DisplayName", value);
		}
	}

﻿	// property:Gender, columnName:Gender	
	[Brevitee.Data.Column(Name="Gender", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Gender
	{
		get
		{
			return GetStringValue("Gender");
		}
		set
		{
			SetValue("Gender", value);
		}
	}

﻿	// property:UserName, columnName:UserName	
	[Brevitee.Data.Column(Name="UserName", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string UserName
	{
		get
		{
			return GetStringValue("UserName");
		}
		set
		{
			SetValue("UserName", value);
		}
	}



﻿	// start ImageId -> ImageId
	[Brevitee.Data.ForeignKey(
        Table="Stickerizee",
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
	public StickerizationCollection StickerizationsByStickerizeeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerization_StickerizeeId"))
			{
				SetChildren();
			}

			var c = (StickerizationCollection)this.ChildCollections["Stickerization_StickerizeeId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizerStickerizeeCollection StickerizerStickerizeesByStickerizeeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("StickerizerStickerizee_StickerizeeId"))
			{
				SetChildren();
			}

			var c = (StickerizerStickerizeeCollection)this.ChildCollections["StickerizerStickerizee_StickerizeeId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

﻿
		// Xref       
        public XrefDaoCollection<StickerizerStickerizee, Stickerizer> Stickerizers
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Stickerizee_StickerizerStickerizee_Stickerizer"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<StickerizerStickerizee, Stickerizer>)this.ChildCollections["Stickerizee_StickerizerStickerizee_Stickerizer"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new StickerizeeColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Stickerizee table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerizeeCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Stickerizee>();
			Database db = database ?? Db.For<Stickerizee>();
			var results = new StickerizeeCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerizeeColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizeeCollection Where(Func<StickerizeeColumns, QueryFilter<StickerizeeColumns>> where, OrderBy<StickerizeeColumns> orderBy = null)
		{
			return new StickerizeeCollection(new Query<StickerizeeColumns, Stickerizee>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizeeCollection Where(WhereDelegate<StickerizeeColumns> where, Database db = null)
		{
			var results = new StickerizeeCollection(db, new Query<StickerizeeColumns, Stickerizee>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizeeCollection Where(WhereDelegate<StickerizeeColumns> where, OrderBy<StickerizeeColumns> orderBy = null, Database db = null)
		{
			var results = new StickerizeeCollection(db, new Query<StickerizeeColumns, Stickerizee>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizeeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizeeCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerizeeCollection(db, Select<StickerizeeColumns>.From<Stickerizee>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Stickerizee instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Stickerizee OneWhere(WhereDelegate<StickerizeeColumns> where, Database db = null)
		{
			var results = new StickerizeeCollection(db, Select<StickerizeeColumns>.From<Stickerizee>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizeeColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Stickerizee OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerizeeCollection(db, Select<StickerizeeColumns>.From<Stickerizee>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Stickerizee OneOrThrow(StickerizeeCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Stickerizee FirstOneWhere(WhereDelegate<StickerizeeColumns> where, Database db = null)
		{
			var results = new StickerizeeCollection(db, Select<StickerizeeColumns>.From<Stickerizee>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizeeCollection Top(int count, WhereDelegate<StickerizeeColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizeeCollection Top(int count, WhereDelegate<StickerizeeColumns> where, OrderBy<StickerizeeColumns> orderBy, Database database = null)
		{
			StickerizeeColumns c = new StickerizeeColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Stickerizee>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Stickerizee>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerizeeColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerizeeCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizeeColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizeeColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerizeeColumns> where, Database database = null)
		{
			StickerizeeColumns c = new StickerizeeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Stickerizee>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Stickerizee>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
