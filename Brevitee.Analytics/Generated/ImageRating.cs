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
    [Brevitee.Data.Table("ImageRating", "Analytics")]
    public partial class ImageRating: Dao
    {
        public ImageRating():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ImageRating(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator ImageRating(DataRow data)
        {
            return new ImageRating(data);
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=true)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



	// start ImageId -> ImageId
	[Brevitee.Data.ForeignKey(
        Table="ImageRating",
		Name="ImageId", 
		ExtractedType="", 
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
				_imageOfImageId = Brevitee.Analytics.Data.Image.OneWhere(f => f.Id == this.ImageId);
			}
			return _imageOfImageId;
		}
	}
	
	// start UserIdentifierId -> UserIdentifierId
	[Brevitee.Data.ForeignKey(
        Table="ImageRating",
		Name="UserIdentifierId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="UserIdentifier",
		Suffix="2")]
	public long? UserIdentifierId
	{
		get
		{
			return GetLongValue("UserIdentifierId");
		}
		set
		{
			SetValue("UserIdentifierId", value);
		}
	}

	UserIdentifier _userIdentifierOfUserIdentifierId;
	public UserIdentifier UserIdentifierOfUserIdentifierId
	{
		get
		{
			if(_userIdentifierOfUserIdentifierId == null)
			{
				_userIdentifierOfUserIdentifierId = Brevitee.Analytics.Data.UserIdentifier.OneWhere(f => f.Id == this.UserIdentifierId);
			}
			return _userIdentifierOfUserIdentifierId;
		}
	}
	
				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ImageRatingColumns();
			return (colFilter.Id == IdValue);
		}

		public static ImageRatingCollection Where(Func<ImageRatingColumns, QueryFilter<ImageRatingColumns>> where, OrderBy<ImageRatingColumns> orderBy = null)
		{
			return new ImageRatingCollection(new Query<ImageRatingColumns, ImageRating>(where, orderBy), true);
		}
		
		public static ImageRatingCollection Where(WhereDelegate<ImageRatingColumns> where, Database db = null)
		{
			return new ImageRatingCollection(new Query<ImageRatingColumns, ImageRating>(where, db), true);
		}
		   
		public static ImageRatingCollection Where(WhereDelegate<ImageRatingColumns> where, OrderBy<ImageRatingColumns> orderBy = null, Database db = null)
		{
			return new ImageRatingCollection(new Query<ImageRatingColumns, ImageRating>(where, orderBy, db), true);
		}

		public static ImageRatingCollection Where(QiQuery where, Database db = null)
		{
			return new ImageRatingCollection(Select<ImageRatingColumns>.From<ImageRating>().Where(where, db));
		}

		public static ImageRating OneWhere(WhereDelegate<ImageRatingColumns> where, Database db = null)
		{
			var results = new ImageRatingCollection(Select<ImageRatingColumns>.From<ImageRating>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ImageRating OneWhere(QiQuery where, Database db = null)
		{
			var results = new ImageRatingCollection(Select<ImageRatingColumns>.From<ImageRating>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ImageRating OneOrThrow(ImageRatingCollection c)
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

		public static ImageRating FirstOneWhere(WhereDelegate<ImageRatingColumns> where, Database db = null)
		{
			var results = new ImageRatingCollection(Select<ImageRatingColumns>.From<ImageRating>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ImageRatingCollection Top(int count, WhereDelegate<ImageRatingColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ImageRatingCollection Top(int count, WhereDelegate<ImageRatingColumns> where, OrderBy<ImageRatingColumns> orderBy, Database database = null)
        {
            ImageRatingColumns c = new ImageRatingColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<ImageRating>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ImageRating>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageRatingColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ImageRatingCollection>(0);
        }

		public static long Count(WhereDelegate<ImageRatingColumns> where, Database database = null)
		{
			ImageRatingColumns c = new ImageRatingColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ImageRating>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ImageRating>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
