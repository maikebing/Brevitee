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
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Tag(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Tag(DataRow data)
		{
			return new Tag(data);
		}

		private void SetChildren()
		{

			this.ChildCollections.Add("UrlTag_TagId", new UrlTagCollection(new Query<UrlTagColumns, UrlTag>((c) => c.TagId == this.Id), this, "TagId"));	
			this.ChildCollections.Add("ImageTag_TagId", new ImageTagCollection(new Query<ImageTagColumns, ImageTag>((c) => c.TagId == this.Id), this, "TagId"));							
			this.ChildCollections.Add("Tag_UrlTag_Url",  new XrefDaoCollection<UrlTag, Url>(this, false));
				
			this.ChildCollections.Add("Tag_ImageTag_Image",  new XrefDaoCollection<ImageTag, Image>(this, false));
				
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="", MaxLength="")]
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=false)]
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
				c.Load();
			}
			return c;
		}
	}
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
				c.Load();
			}
			return c;
		}
	}
			

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
		}		// Xref       
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
		}
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new TagColumns();
			return (colFilter.Id == IdValue);
		}

		public static TagCollection Where(Func<TagColumns, QueryFilter<TagColumns>> where, OrderBy<TagColumns> orderBy = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, orderBy), true);
		}
		
		public static TagCollection Where(WhereDelegate<TagColumns> where, Database db = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, db), true);
		}
		   
		public static TagCollection Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, orderBy, db), true);
		}

		public static TagCollection Where(QiQuery where, Database db = null)
		{
			return new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
		}

		public static Tag OneWhere(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Tag OneWhere(QiQuery where, Database db = null)
		{
			var results = new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
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

		public static Tag FirstOneWhere(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Tag>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Tag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TagColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<TagCollection>(0);
		}

		public static long Count(WhereDelegate<TagColumns> where, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Tag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Tag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
