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
	[Brevitee.Data.Table("Stickerizable", "Stickerize")]
	public partial class Stickerizable: Dao
	{
		public Stickerizable():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Stickerizable(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Stickerizable(DataRow data)
		{
			return new Stickerizable(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("Stickerization_StickerizableId", new StickerizationCollection(new Query<StickerizationColumns, Stickerization>((c) => c.StickerizableId == this.Id), this, "StickerizableId"));	﻿
            this.ChildCollections.Add("StickerizableListStickerizable_StickerizableId", new StickerizableListStickerizableCollection(new Query<StickerizableListStickerizableColumns, StickerizableListStickerizable>((c) => c.StickerizableId == this.Id), this, "StickerizableId"));	﻿
            this.ChildCollections.Add("SubSectionStickerizable_StickerizableId", new SubSectionStickerizableCollection(new Query<SubSectionStickerizableColumns, SubSectionStickerizable>((c) => c.StickerizableId == this.Id), this, "StickerizableId"));							﻿
            this.ChildCollections.Add("Stickerizable_StickerizableListStickerizable_StickerizableList",  new XrefDaoCollection<StickerizableListStickerizable, StickerizableList>(this, false));
				﻿
            this.ChildCollections.Add("Stickerizable_SubSectionStickerizable_SubSection",  new XrefDaoCollection<SubSectionStickerizable, SubSection>(this, false));
				
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

﻿	// property:For, columnName:For	
	[Brevitee.Data.Column(Name="For", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string For
	{
		get
		{
			return GetStringValue("For");
		}
		set
		{
			SetValue("For", value);
		}
	}



﻿	// start CreatorId -> CreatorId
	[Brevitee.Data.ForeignKey(
        Table="Stickerizable",
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
	public StickerizationCollection StickerizationsByStickerizableId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Stickerization_StickerizableId"))
			{
				SetChildren();
			}

			var c = (StickerizationCollection)this.ChildCollections["Stickerization_StickerizableId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public StickerizableListStickerizableCollection StickerizableListStickerizablesByStickerizableId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("StickerizableListStickerizable_StickerizableId"))
			{
				SetChildren();
			}

			var c = (StickerizableListStickerizableCollection)this.ChildCollections["StickerizableListStickerizable_StickerizableId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public SubSectionStickerizableCollection SubSectionStickerizablesByStickerizableId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("SubSectionStickerizable_StickerizableId"))
			{
				SetChildren();
			}

			var c = (SubSectionStickerizableCollection)this.ChildCollections["SubSectionStickerizable_StickerizableId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

﻿
		// Xref       
        public XrefDaoCollection<StickerizableListStickerizable, StickerizableList> StickerizableLists
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Stickerizable_StickerizableListStickerizable_StickerizableList"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<StickerizableListStickerizable, StickerizableList>)this.ChildCollections["Stickerizable_StickerizableListStickerizable_StickerizableList"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }﻿
		// Xref       
        public XrefDaoCollection<SubSectionStickerizable, SubSection> SubSections
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Stickerizable_SubSectionStickerizable_SubSection"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<SubSectionStickerizable, SubSection>)this.ChildCollections["Stickerizable_SubSectionStickerizable_SubSection"];
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
			var colFilter = new StickerizableColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Stickerizable table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static StickerizableCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Stickerizable>();
			Database db = database ?? Db.For<Stickerizable>();
			var results = new StickerizableCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a StickerizableColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableCollection Where(Func<StickerizableColumns, QueryFilter<StickerizableColumns>> where, OrderBy<StickerizableColumns> orderBy = null)
		{
			return new StickerizableCollection(new Query<StickerizableColumns, Stickerizable>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableCollection Where(WhereDelegate<StickerizableColumns> where, Database db = null)
		{
			var results = new StickerizableCollection(db, new Query<StickerizableColumns, Stickerizable>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizableCollection Where(WhereDelegate<StickerizableColumns> where, OrderBy<StickerizableColumns> orderBy = null, Database db = null)
		{
			var results = new StickerizableCollection(db, new Query<StickerizableColumns, Stickerizable>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static StickerizableCollection Where(QiQuery where, Database db = null)
		{
			var results = new StickerizableCollection(db, Select<StickerizableColumns>.From<Stickerizable>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Stickerizable instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Stickerizable OneWhere(WhereDelegate<StickerizableColumns> where, Database db = null)
		{
			var results = new StickerizableCollection(db, Select<StickerizableColumns>.From<Stickerizable>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<StickerizableColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Stickerizable OneWhere(QiQuery where, Database db = null)
		{
			var results = new StickerizableCollection(db, Select<StickerizableColumns>.From<Stickerizable>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Stickerizable OneOrThrow(StickerizableCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Stickerizable FirstOneWhere(WhereDelegate<StickerizableColumns> where, Database db = null)
		{
			var results = new StickerizableCollection(db, Select<StickerizableColumns>.From<Stickerizable>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static StickerizableCollection Top(int count, WhereDelegate<StickerizableColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static StickerizableCollection Top(int count, WhereDelegate<StickerizableColumns> where, OrderBy<StickerizableColumns> orderBy, Database database = null)
		{
			StickerizableColumns c = new StickerizableColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Stickerizable>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Stickerizable>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StickerizableColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<StickerizableCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a StickerizableColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between StickerizableColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<StickerizableColumns> where, Database database = null)
		{
			StickerizableColumns c = new StickerizableColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Stickerizable>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Stickerizable>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
