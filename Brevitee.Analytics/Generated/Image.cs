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
	[Brevitee.Data.Table("Image", "Analytics")]
	public partial class Image: Dao
	{
		public Image():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Image(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Image(DataRow data)
		{
			return new Image(data);
		}

		private void SetChildren()
		{

			this.ChildCollections.Add("ImageRating_ImageId", new ImageRatingCollection(new Query<ImageRatingColumns, ImageRating>((c) => c.ImageId == this.Id), this, "ImageId"));	
			this.ChildCollections.Add("ImageTag_ImageId", new ImageTagCollection(new Query<ImageTagColumns, ImageTag>((c) => c.ImageId == this.Id), this, "ImageId"));				
			this.ChildCollections.Add("Image_ImageTag_Tag",  new XrefDaoCollection<ImageTag, Tag>(this, false));
							
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

	// property:Date, columnName:Date	
	[Brevitee.Data.Column(Name="Date", ExtractedType="", MaxLength="", AllowNull=false)]
	public DateTime Date
	{
		get
		{
			return GetDateTimeValue("Date");
		}
		set
		{
			SetValue("Date", value);
		}
	}

	// property:CrawlerId, columnName:CrawlerId	
	[Brevitee.Data.Column(Name="CrawlerId", ExtractedType="", MaxLength="", AllowNull=true)]
	public long? CrawlerId
	{
		get
		{
			return GetLongValue("CrawlerId");
		}
		set
		{
			SetValue("CrawlerId", value);
		}
	}



	// start UrlId -> UrlId
	[Brevitee.Data.ForeignKey(
		Table="Image",
		Name="UrlId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Url",
		Suffix="1")]
	public long? UrlId
	{
		get
		{
			return GetLongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	Url _urlOfUrlId;
	public Url UrlOfUrlId
	{
		get
		{
			if(_urlOfUrlId == null)
			{
				_urlOfUrlId = Brevitee.Analytics.Data.Url.OneWhere(f => f.Id == this.UrlId);
			}
			return _urlOfUrlId;
		}
	}
	
				
	[Exclude]	
	public ImageRatingCollection ImageRatingsByImageId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ImageRating_ImageId"))
			{
				SetChildren();
			}

			var c = (ImageRatingCollection)this.ChildCollections["ImageRating_ImageId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public ImageTagCollection ImageTagsByImageId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ImageTag_ImageId"))
			{
				SetChildren();
			}

			var c = (ImageTagCollection)this.ChildCollections["ImageTag_ImageId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		// Xref       
		public XrefDaoCollection<ImageTag, Tag> Tags
		{
			get
			{
				if(!this.ChildCollections.ContainsKey("Image_ImageTag_Tag"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<ImageTag, Tag>)this.ChildCollections["Image_ImageTag_Tag"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
			}
		}

		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ImageColumns();
			return (colFilter.Id == IdValue);
		}

		public static ImageCollection Where(Func<ImageColumns, QueryFilter<ImageColumns>> where, OrderBy<ImageColumns> orderBy = null)
		{
			return new ImageCollection(new Query<ImageColumns, Image>(where, orderBy), true);
		}
		
		public static ImageCollection Where(WhereDelegate<ImageColumns> where, Database db = null)
		{
			return new ImageCollection(new Query<ImageColumns, Image>(where, db), true);
		}
		   
		public static ImageCollection Where(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database db = null)
		{
			return new ImageCollection(new Query<ImageColumns, Image>(where, orderBy, db), true);
		}

		public static ImageCollection Where(QiQuery where, Database db = null)
		{
			return new ImageCollection(Select<ImageColumns>.From<Image>().Where(where, db));
		}

		public static Image OneWhere(WhereDelegate<ImageColumns> where, Database db = null)
		{
			var results = new ImageCollection(Select<ImageColumns>.From<Image>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Image OneWhere(QiQuery where, Database db = null)
		{
			var results = new ImageCollection(Select<ImageColumns>.From<Image>().Where(where, db));
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

		public static Image FirstOneWhere(WhereDelegate<ImageColumns> where, Database db = null)
		{
			var results = new ImageCollection(Select<ImageColumns>.From<Image>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ImageCollection Top(int count, WhereDelegate<ImageColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ImageCollection Top(int count, WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy, Database database = null)
		{
			ImageColumns c = new ImageColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Image>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Image>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<ImageCollection>(0);
		}

		public static long Count(WhereDelegate<ImageColumns> where, Database database = null)
		{
			ImageColumns c = new ImageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Image>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Image>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
