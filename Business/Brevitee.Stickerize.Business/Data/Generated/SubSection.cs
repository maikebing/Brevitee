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
	[Brevitee.Data.Table("SubSection", "Stickerize")]
	public partial class SubSection: Dao
	{
		public SubSection():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public SubSection(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator SubSection(DataRow data)
		{
			return new SubSection(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("SubSectionStickerizable_SubSectionId", new SubSectionStickerizableCollection(new Query<SubSectionStickerizableColumns, SubSectionStickerizable>((c) => c.SubSectionId == this.Id), this, "SubSectionId"));				﻿
            this.ChildCollections.Add("SubSection_SubSectionStickerizable_Stickerizable",  new XrefDaoCollection<SubSectionStickerizable, Stickerizable>(this, false));
							
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
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
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



﻿	// start StickerizableListId -> StickerizableListId
	[Brevitee.Data.ForeignKey(
        Table="SubSection",
		Name="StickerizableListId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
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
	
				
﻿
	[Exclude]	
	public SubSectionStickerizableCollection SubSectionStickerizablesBySubSectionId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("SubSectionStickerizable_SubSectionId"))
			{
				SetChildren();
			}

			var c = (SubSectionStickerizableCollection)this.ChildCollections["SubSectionStickerizable_SubSectionId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			
﻿
		// Xref       
        public XrefDaoCollection<SubSectionStickerizable, Stickerizable> Stickerizables
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("SubSection_SubSectionStickerizable_Stickerizable"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<SubSectionStickerizable, Stickerizable>)this.ChildCollections["SubSection_SubSectionStickerizable_Stickerizable"];
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
			var colFilter = new SubSectionColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the SubSection table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SubSectionCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<SubSection>();
			Database db = database ?? Db.For<SubSection>();
			var results = new SubSectionCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SubSectionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionCollection Where(Func<SubSectionColumns, QueryFilter<SubSectionColumns>> where, OrderBy<SubSectionColumns> orderBy = null)
		{
			return new SubSectionCollection(new Query<SubSectionColumns, SubSection>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionCollection Where(WhereDelegate<SubSectionColumns> where, Database db = null)
		{
			var results = new SubSectionCollection(db, new Query<SubSectionColumns, SubSection>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SubSectionCollection Where(WhereDelegate<SubSectionColumns> where, OrderBy<SubSectionColumns> orderBy = null, Database db = null)
		{
			var results = new SubSectionCollection(db, new Query<SubSectionColumns, SubSection>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SubSectionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SubSectionCollection Where(QiQuery where, Database db = null)
		{
			var results = new SubSectionCollection(db, Select<SubSectionColumns>.From<SubSection>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single SubSection instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSection OneWhere(WhereDelegate<SubSectionColumns> where, Database db = null)
		{
			var results = new SubSectionCollection(db, Select<SubSectionColumns>.From<SubSection>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SubSectionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static SubSection OneWhere(QiQuery where, Database db = null)
		{
			var results = new SubSectionCollection(db, Select<SubSectionColumns>.From<SubSection>().Where(where, db));
			return OneOrThrow(results);
		}

		private static SubSection OneOrThrow(SubSectionCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSection FirstOneWhere(WhereDelegate<SubSectionColumns> where, Database db = null)
		{
			var results = new SubSectionCollection(db, Select<SubSectionColumns>.From<SubSection>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SubSectionCollection Top(int count, WhereDelegate<SubSectionColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SubSectionCollection Top(int count, WhereDelegate<SubSectionColumns> where, OrderBy<SubSectionColumns> orderBy, Database database = null)
		{
			SubSectionColumns c = new SubSectionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<SubSection>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<SubSection>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SubSectionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SubSectionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SubSectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SubSectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SubSectionColumns> where, Database database = null)
		{
			SubSectionColumns c = new SubSectionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<SubSection>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<SubSection>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
