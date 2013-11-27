using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema; 
using Brevitee.Data.Qi;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace SampleData
{
    [Brevitee.Data.Table("Review", "Test")]
    public partial class Review: Dao
    {
        public Review():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Review(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ItemReview_ReviewId", new ItemReviewCollection(new Query<ItemReviewColumns, ItemReview>((c) => c.ReviewId == this.Id), this, "ReviewId"));	
            this.ChildCollections.Add("ReviewComment_ReviewId", new ReviewCommentCollection(new Query<ReviewCommentColumns, ReviewComment>((c) => c.ReviewId == this.Id), this, "ReviewId"));	
            this.ChildCollections.Add("UserItemReview_ReviewId", new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>((c) => c.ReviewId == this.Id), this, "ReviewId"));	
            this.ChildCollections.Add("UserReview_ReviewId", new UserReviewCollection(new Query<UserReviewColumns, UserReview>((c) => c.ReviewId == this.Id), this, "ReviewId"));	
		}

	// property:Id, columnName:Id	
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="BigInt", MaxLength="8")]
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

	// property:Title, columnName:Title	
	[Brevitee.Data.Column(Name="Title", ExtractedType="VarChar", MaxLength="50", AllowNull=false)]
	public string Title
	{
		get
		{
			return GetStringValue("Title");
		}
		set
		{
			SetValue("Title", value);
		}
	}

	// property:Text, columnName:Text	
	[Brevitee.Data.Column(Name="Text", ExtractedType="VarChar", MaxLength="MAX", AllowNull=false)]
	public string Text
	{
		get
		{
			return GetStringValue("Text");
		}
		set
		{
			SetValue("Text", value);
		}
	}

	// property:Rating, columnName:Rating	
	[Brevitee.Data.Column(Name="Rating", ExtractedType="Int", MaxLength="4", AllowNull=false)]
	public int? Rating
	{
		get
		{
			return GetIntValue("Rating");
		}
		set
		{
			SetValue("Rating", value);
		}
	}

	// property:Created, columnName:Created	
	[Brevitee.Data.Column(Name="Created", ExtractedType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime Created
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

	// property:Modified, columnName:Modified	
	[Brevitee.Data.Column(Name="Modified", ExtractedType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}


				
	
	public ItemReviewCollection ItemReviewCollectionByReviewId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemReview_ReviewId"))
			{
				SetChildren();
			}

			var c = (ItemReviewCollection)this.ChildCollections["ItemReview_ReviewId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ReviewCommentCollection ReviewCommentCollectionByReviewId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ReviewComment_ReviewId"))
			{
				SetChildren();
			}

			var c = (ReviewCommentCollection)this.ChildCollections["ReviewComment_ReviewId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserItemReviewCollection UserItemReviewCollectionByReviewId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserItemReview_ReviewId"))
			{
				SetChildren();
			}

			var c = (UserItemReviewCollection)this.ChildCollections["UserItemReview_ReviewId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserReviewCollection UserReviewCollectionByReviewId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserReview_ReviewId"))
			{
				SetChildren();
			}

			var c = (UserReviewCollection)this.ChildCollections["UserReview_ReviewId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ReviewColumns();
			return (colFilter.Id == IdValue);
		}

		public static ReviewCollection Where(Func<ReviewColumns, QueryFilter<ReviewColumns>> where, OrderBy<ReviewColumns> orderBy = null)
		{
			return new ReviewCollection(new Query<ReviewColumns, Review>(where, orderBy), true);
		}
		
		public static ReviewCollection Where(WhereDelegate<ReviewColumns> where, Database db = null)
		{
			return new ReviewCollection(new Query<ReviewColumns, Review>(where, db), true);
		}
		   
		public static ReviewCollection Where(WhereDelegate<ReviewColumns> where, OrderBy<ReviewColumns> orderBy = null, Database db = null)
		{
			return new ReviewCollection(new Query<ReviewColumns, Review>(where, orderBy, db), true);
		}

		public static ReviewCollection Where(QiQuery where, Database db = null)
		{
			return new ReviewCollection(Select<ReviewColumns>.From<Review>().Where(where, db));
		}

		public static Review OneWhere(WhereDelegate<ReviewColumns> where, Database db = null)
		{
			var results = new ReviewCollection(Select<ReviewColumns>.From<Review>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Review OneWhere(QiQuery where, Database db = null)
		{
			var results = new ReviewCollection(Select<ReviewColumns>.From<Review>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Review OneOrThrow(ReviewCollection c)
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

		public static Review FirstOneWhere(WhereDelegate<ReviewColumns> where, Database db = null)
		{
			var results = new ReviewCollection(Select<ReviewColumns>.From<Review>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ReviewCollection Top(int count, WhereDelegate<ReviewColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ReviewCollection Top(int count, WhereDelegate<ReviewColumns> where, OrderBy<ReviewColumns> orderBy, Database database = null)
        {
            ReviewColumns c = new ReviewColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Review>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Review>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ReviewColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ReviewCollection>(0);
        }

		public static long Count(WhereDelegate<ReviewColumns> where, Database database = null)
		{
			ReviewColumns c = new ReviewColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Review>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Review>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
