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
	[Brevitee.Data.Table("Tag", "Analytics")]
	public partial class Tag: Dao
	{
		public Tag():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Tag(DataRow data): base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Tag(DataRow data)
		{
			return new Tag(data);
		}

		private void SetChildren()
		{
﻿
            this.ChildCollections.Add("UrlTag_TagId", new UrlTagCollection(new Query<UrlTagColumns, UrlTag>((c) => c.TagId == this.Id), this, "TagId"));	﻿
            this.ChildCollections.Add("ImageTag_TagId", new ImageTagCollection(new Query<ImageTagColumns, ImageTag>((c) => c.TagId == this.Id), this, "TagId"));							﻿
            this.ChildCollections.Add("Tag_UrlTag_Url",  new XrefDaoCollection<UrlTag, Url>(this, false));
				﻿
            this.ChildCollections.Add("Tag_ImageTag_Image",  new XrefDaoCollection<ImageTag, Image>(this, false));
				
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

﻿	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				
﻿
	[Exclude]	
	public UrlTagCollection UrlTagsByTagId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UrlTag_TagId"))
			{
				SetChildren();
			}

			var c = (UrlTagCollection)this.ChildCollections["UrlTag_TagId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
	﻿
	[Exclude]	
	public ImageTagCollection ImageTagsByTagId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ImageTag_TagId"))
			{
				SetChildren();
			}

			var c = (ImageTagCollection)this.ChildCollections["ImageTag_TagId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

﻿
		// Xref       
        public XrefDaoCollection<UrlTag, Url> Urls
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Tag_UrlTag_Url"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UrlTag, Url>)this.ChildCollections["Tag_UrlTag_Url"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }﻿
		// Xref       
        public XrefDaoCollection<ImageTag, Image> Images
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Tag_ImageTag_Image"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ImageTag, Image>)this.ChildCollections["Tag_ImageTag_Image"];
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
			var colFilter = new TagColumns();
			return (colFilter.KeyColumn == IdValue);
		}
		/// <summary>
		/// Return every record in the Tag table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static TagCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Tag>();
			Database db = database ?? Db.For<Tag>();
			var results = new TagCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a TagColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TagCollection Where(Func<TagColumns, QueryFilter<TagColumns>> where, OrderBy<TagColumns> orderBy = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TagCollection Where(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(db, new Query<TagColumns, Tag>(where, db), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static TagCollection Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null)
		{
			var results = new TagCollection(db, new Query<TagColumns, Tag>(where, orderBy, db), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static TagCollection Where(QiQuery where, Database db = null)
		{
			var results = new TagCollection(db, Select<TagColumns>.From<Tag>().Where(where, db));
			return results;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Tag instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Tag OneWhere(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(db, Select<TagColumns>.From<Tag>().Where(where, db));
			return OneOrThrow(results);
		}
			 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<TagColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="db"></param>
		public static Tag OneWhere(QiQuery where, Database db = null)
		{
			var results = new TagCollection(db, Select<TagColumns>.From<Tag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Tag OneOrThrow(TagCollection c)
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static Tag FirstOneWhere(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(db, Select<TagColumns>.From<Tag>().Where(where, db));
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, Database db = null)
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
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? Db.For<Tag>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Tag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TagColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<TagCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a TagColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between TagColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<TagColumns> where, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Tag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Tag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
