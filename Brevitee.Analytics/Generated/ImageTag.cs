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
	[Brevitee.Data.Table("ImageTag", "Analytics")]
	public partial class ImageTag: Dao
	{
		public ImageTag():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public ImageTag(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator ImageTag(DataRow data)
		{
			return new ImageTag(data);
		}

		private void SetChildren()
		{
						
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



	// start ImageId -> ImageId
	[Brevitee.Data.ForeignKey(
		Table="ImageTag",
		Name="ImageId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
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
				_imageOfImageId = Brevitee.Analytics.Data.Image.OneWhere(f => f.Id == this.ImageId);
			}
			return _imageOfImageId;
		}
	}
	
	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
		Table="ImageTag",
		Name="TagId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public long? TagId
	{
		get
		{
			return GetLongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = Brevitee.Analytics.Data.Tag.OneWhere(f => f.Id == this.TagId);
			}
			return _tagOfTagId;
		}
	}
	
				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ImageTagColumns();
			return (colFilter.Id == IdValue);
		}

		public static ImageTagCollection Where(Func<ImageTagColumns, QueryFilter<ImageTagColumns>> where, OrderBy<ImageTagColumns> orderBy = null)
		{
			return new ImageTagCollection(new Query<ImageTagColumns, ImageTag>(where, orderBy), true);
		}
		
		public static ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			return new ImageTagCollection(new Query<ImageTagColumns, ImageTag>(where, db), true);
		}
		   
		public static ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null)
		{
			return new ImageTagCollection(new Query<ImageTagColumns, ImageTag>(where, orderBy, db), true);
		}

		public static ImageTagCollection Where(QiQuery where, Database db = null)
		{
			return new ImageTagCollection(Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
		}

		public static ImageTag OneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			var results = new ImageTagCollection(Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ImageTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new ImageTagCollection(Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ImageTag OneOrThrow(ImageTagCollection c)
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

		public static ImageTag FirstOneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			var results = new ImageTagCollection(Select<ImageTagColumns>.From<ImageTag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy, Database database = null)
		{
			ImageTagColumns c = new ImageTagColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<ImageTag>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<ImageTag>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageTagColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<ImageTagCollection>(0);
		}

		public static long Count(WhereDelegate<ImageTagColumns> where, Database database = null)
		{
			ImageTagColumns c = new ImageTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ImageTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ImageTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
