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
	[Brevitee.Data.Table("Stickerizer", "Stickerize")]
	public partial class Stickerizer: Dao
	{
		public Stickerizer():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Stickerizer(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Stickerizer(DataRow data)
		{
			return new Stickerizer(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Stickerizable_CreatorId", new StickerizableCollection(new Query<StickerizableColumns, Stickerizable>((c) => c.CreatorId == this.Id), this, "CreatorId"));	﻿
            this.ChildCollections.Add("StickerizableList_CreatorId", new StickerizableListCollection(new Query<StickerizableListColumns, StickerizableList>((c) => c.CreatorId == this.Id), this, "CreatorId"));	﻿
            this.ChildCollections.Add("Stickerization_StickerizerId", new StickerizationCollection(new Query<StickerizationColumns, Stickerization>((c) => c.StickerizerId == this.Id), this, "StickerizerId"));	﻿
            this.ChildCollections.Add("StickerizerStickerizee_StickerizerId", new StickerizerStickerizeeCollection(new Query<StickerizerStickerizeeColumns, StickerizerStickerizee>((c) => c.StickerizerId == this.Id), this, "StickerizerId"));				﻿
            this.ChildCollections.Add("Stickerizer_StickerizerStickerizee_Stickerizee",  new XrefDaoCollection<StickerizerStickerizee, Stickerizee>(this, false));
							
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
        Table="Stickerizer",
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
	public StickerizableCollection StickerizablesByCreatorId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerizable_CreatorId"))
			{
				SetChildren();
			}

			var c = (StickerizableCollection)this.ChildCollections["Stickerizable_CreatorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizableListCollection StickerizableListsByCreatorId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("StickerizableList_CreatorId"))
			{
				SetChildren();
			}

			var c = (StickerizableListCollection)this.ChildCollections["StickerizableList_CreatorId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizationCollection StickerizationsByStickerizerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerization_StickerizerId"))
			{
				SetChildren();
			}

			var c = (StickerizationCollection)this.ChildCollections["Stickerization_StickerizerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizerStickerizeeCollection StickerizerStickerizeesByStickerizerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("StickerizerStickerizee_StickerizerId"))
			{
				SetChildren();
			}

			var c = (StickerizerStickerizeeCollection)this.ChildCollections["StickerizerStickerizee_StickerizerId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			
﻿
		// Xref       
        public XrefDaoCollection<StickerizerStickerizee, Stickerizee> Stickerizees
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Stickerizer_StickerizerStickerizee_Stickerizee"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<StickerizerStickerizee, Stickerizee>)this.ChildCollections["Stickerizer_StickerizerStickerizee_Stickerizee"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new StickerizerColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Stickerizer table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerizerCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Stickerizer>();
			Database db = database ?? Db.For<Stickerizer>();
			var results = new StickerizerCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerizerColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerCollection Where(Func<StickerizerColumns, QueryFilter<StickerizerColumns>> where, OrderBy<StickerizerColumns> orderBy = null)
		{
			return new StickerizerCollection(new Query<StickerizerColumns, Stickerizer>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerCollection Where(WhereDelegate<StickerizerColumns> where, Database db = null)
		{
			var results = new StickerizerCollection(db, new Query<StickerizerColumns, Stickerizer>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizerCollection Where(WhereDelegate<StickerizerColumns> where, OrderBy<StickerizerColumns> orderBy = null, Database db = null)
		{
			var results = new StickerizerCollection(db, new Query<StickerizerColumns, Stickerizer>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizerCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerizerCollection(db, Select<StickerizerColumns>.From<Stickerizer>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Stickerizer instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Stickerizer OneWhere(WhereDelegate<StickerizerColumns> where, Database db = null)
		{
			var results = new StickerizerCollection(db, Select<StickerizerColumns>.From<Stickerizer>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizerColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Stickerizer OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerizerCollection(db, Select<StickerizerColumns>.From<Stickerizer>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Stickerizer OneOrThrow(StickerizerCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Stickerizer FirstOneWhere(WhereDelegate<StickerizerColumns> where, Database db = null)
		{
			var results = new StickerizerCollection(db, Select<StickerizerColumns>.From<Stickerizer>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizerCollection Top(int count, WhereDelegate<StickerizerColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizerCollection Top(int count, WhereDelegate<StickerizerColumns> where, OrderBy<StickerizerColumns> orderBy, Database database = null)
		{
			StickerizerColumns c = new StickerizerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Stickerizer>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Stickerizer>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerizerColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerizerCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizerColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizerColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerizerColumns> where, Database database = null)
		{
			StickerizerColumns c = new StickerizerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Stickerizer>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Stickerizer>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
