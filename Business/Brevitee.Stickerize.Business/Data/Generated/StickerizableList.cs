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
	[Brevitee.Data.Table("StickerizableList", "Stickerize")]
	public partial class StickerizableList: Dao
	{
		public StickerizableList():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public StickerizableList(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator StickerizableList(DataRow data)
		{
			return new StickerizableList(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("SubSection_StickerizableListId", new SubSectionCollection(new Query<SubSectionColumns, SubSection>((c) => c.StickerizableListId == this.Id), this, "StickerizableListId"));	﻿
            this.ChildCollections.Add("StickerizableListStickerizable_StickerizableListId", new StickerizableListStickerizableCollection(new Query<StickerizableListStickerizableColumns, StickerizableListStickerizable>((c) => c.StickerizableListId == this.Id), this, "StickerizableListId"));				﻿
            this.ChildCollections.Add("StickerizableList_StickerizableListStickerizable_Stickerizable",  new XrefDaoCollection<StickerizableListStickerizable, Stickerizable>(this, false));
							
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

﻿	// property:Public, columnName:Public	
	[Brevitee.Data.Column(Name="Public", DbDataType="Bit", MaxLength="1", AllowNull=false)]
	public bool? Public
	{
		get
		{
			return GetBooleanValue("Public");
		}
		set
		{
			SetValue("Public", value);
		}
	}



﻿	// start CreatorId -> CreatorId
	[Brevitee.Data.ForeignKey(
        Table="StickerizableList",
		Name="CreatorId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Stickerizer",
		Suffix="1")]
	public long? CreatorId
	{
		get
		{
			return GetLongValue("CreatorId");
		}
		set
		{
			SetValue("CreatorId", value);
		}
	}

	Stickerizer _stickerizerOfCreatorId;
	public Stickerizer StickerizerOfCreatorId
	{
		get
		{
			if(_stickerizerOfCreatorId == null)
			{
				_stickerizerOfCreatorId = Brevitee.Stickerize.Business.Data.Stickerizer.OneWhere(c => c.KeyColumn == this.CreatorId);
			}
			return _stickerizerOfCreatorId;
		}
	}
	
				
﻿
	[Exclude]	
	public SubSectionCollection SubSectionsByStickerizableListId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("SubSection_StickerizableListId"))
			{
				SetChildren();
			}

			var c = (SubSectionCollection)this.ChildCollections["SubSection_StickerizableListId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizableListStickerizableCollection StickerizableListStickerizablesByStickerizableListId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("StickerizableListStickerizable_StickerizableListId"))
			{
				SetChildren();
			}

			var c = (StickerizableListStickerizableCollection)this.ChildCollections["StickerizableListStickerizable_StickerizableListId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			
﻿
		// Xref       
        public XrefDaoCollection<StickerizableListStickerizable, Stickerizable> Stickerizables
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("StickerizableList_StickerizableListStickerizable_Stickerizable"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<StickerizableListStickerizable, Stickerizable>)this.ChildCollections["StickerizableList_StickerizableListStickerizable_Stickerizable"];
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
			var colFilter = new StickerizableListColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the StickerizableList table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerizableListCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<StickerizableList>();
			Database db = database ?? Db.For<StickerizableList>();
			var results = new StickerizableListCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerizableListColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListCollection Where(Func<StickerizableListColumns, QueryFilter<StickerizableListColumns>> where, OrderBy<StickerizableListColumns> orderBy = null)
		{
			return new StickerizableListCollection(new Query<StickerizableListColumns, StickerizableList>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListCollection Where(WhereDelegate<StickerizableListColumns> where, Database db = null)
		{
			var results = new StickerizableListCollection(db, new Query<StickerizableListColumns, StickerizableList>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListCollection Where(WhereDelegate<StickerizableListColumns> where, OrderBy<StickerizableListColumns> orderBy = null, Database db = null)
		{
			var results = new StickerizableListCollection(db, new Query<StickerizableListColumns, StickerizableList>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizableListColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizableListCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerizableListCollection(db, Select<StickerizableListColumns>.From<StickerizableList>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single StickerizableList instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableList OneWhere(WhereDelegate<StickerizableListColumns> where, Database db = null)
		{
			var results = new StickerizableListCollection(db, Select<StickerizableListColumns>.From<StickerizableList>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizableListColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizableList OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerizableListCollection(db, Select<StickerizableListColumns>.From<StickerizableList>().Where(where, db));
			return OneOrThrow(results);
		}

		private static StickerizableList OneOrThrow(StickerizableListCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableList FirstOneWhere(WhereDelegate<StickerizableListColumns> where, Database db = null)
		{
			var results = new StickerizableListCollection(db, Select<StickerizableListColumns>.From<StickerizableList>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListCollection Top(int count, WhereDelegate<StickerizableListColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizableListCollection Top(int count, WhereDelegate<StickerizableListColumns> where, OrderBy<StickerizableListColumns> orderBy, Database database = null)
		{
			StickerizableListColumns c = new StickerizableListColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<StickerizableList>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<StickerizableList>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerizableListColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerizableListCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableListColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableListColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerizableListColumns> where, Database database = null)
		{
			StickerizableListColumns c = new StickerizableListColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<StickerizableList>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<StickerizableList>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
