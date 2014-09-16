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
    [Brevitee.Data.Table("ReviewComment", "Test")]
    public partial class ReviewComment: Dao
    {
        public ReviewComment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ReviewComment(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

		}

	// property:Id, columnName:Id	
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


	// start ReviewId -> ReviewId
	[Brevitee.Data.ForeignKey(
        Table="ReviewComment",
		Name="ReviewId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Review",
		Suffix="1")]
	public long? ReviewId
	{
		get
		{
			return GetLongValue("ReviewId");
		}
		set
		{
			SetValue("ReviewId", value);
		}
	}

	Review _reviewOfReviewId;
	public Review ReviewOfReviewId
	{
		get
		{
			if(_reviewOfReviewId == null)
			{
				_reviewOfReviewId = SampleData.Review.OneWhere(f => f.Id == this.ReviewId);
			}
			return _reviewOfReviewId;
		}
	}
	
	// start CommentId -> CommentId
	[Brevitee.Data.ForeignKey(
        Table="ReviewComment",
		Name="CommentId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Comment",
		Suffix="2")]
	public long? CommentId
	{
		get
		{
			return GetLongValue("CommentId");
		}
		set
		{
			SetValue("CommentId", value);
		}
	}

	Comment _commentOfCommentId;
	public Comment CommentOfCommentId
	{
		get
		{
			if(_commentOfCommentId == null)
			{
				_commentOfCommentId = SampleData.Comment.OneWhere(f => f.Id == this.CommentId);
			}
			return _commentOfCommentId;
		}
	}
	
	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="ReviewComment",
		Name="UserId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="3")]
	public long? UserId
	{
		get
		{
			return GetLongValue("UserId");
		}
		set
		{
			SetValue("UserId", value);
		}
	}

	User _userOfUserId;
	public User UserOfUserId
	{
		get
		{
			if(_userOfUserId == null)
			{
				_userOfUserId = SampleData.User.OneWhere(f => f.Id == this.UserId);
			}
			return _userOfUserId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ReviewCommentColumns();
			return (colFilter.Id == IdValue);
		}

		public static ReviewCommentCollection Where(Func<ReviewCommentColumns, QueryFilter<ReviewCommentColumns>> where, OrderBy<ReviewCommentColumns> orderBy = null)
		{
			return new ReviewCommentCollection(new Query<ReviewCommentColumns, ReviewComment>(where, orderBy), true);
		}
		
		public static ReviewCommentCollection Where(WhereDelegate<ReviewCommentColumns> where, Database db = null)
		{
			return new ReviewCommentCollection(new Query<ReviewCommentColumns, ReviewComment>(where, db), true);
		}
		   
		public static ReviewCommentCollection Where(WhereDelegate<ReviewCommentColumns> where, OrderBy<ReviewCommentColumns> orderBy = null, Database db = null)
		{
			return new ReviewCommentCollection(new Query<ReviewCommentColumns, ReviewComment>(where, orderBy, db), true);
		}

		public static ReviewCommentCollection Where(QiQuery where, Database db = null)
		{
			return new ReviewCommentCollection(Select<ReviewCommentColumns>.From<ReviewComment>().Where(where, db));
		}

		public static ReviewComment OneWhere(WhereDelegate<ReviewCommentColumns> where, Database db = null)
		{
			var results = new ReviewCommentCollection(Select<ReviewCommentColumns>.From<ReviewComment>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ReviewComment OneWhere(QiQuery where, Database db = null)
		{
			var results = new ReviewCommentCollection(Select<ReviewCommentColumns>.From<ReviewComment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ReviewComment OneOrThrow(ReviewCommentCollection c)
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

		public static ReviewComment FirstOneWhere(WhereDelegate<ReviewCommentColumns> where, Database db = null)
		{
			var results = new ReviewCommentCollection(Select<ReviewCommentColumns>.From<ReviewComment>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ReviewCommentCollection Top(int count, WhereDelegate<ReviewCommentColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ReviewCommentCollection Top(int count, WhereDelegate<ReviewCommentColumns> where, OrderBy<ReviewCommentColumns> orderBy, Database database = null)
        {
            ReviewCommentColumns c = new ReviewCommentColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<ReviewComment>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ReviewComment>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ReviewCommentColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ReviewCommentCollection>(0);
        }

		public static long Count(WhereDelegate<ReviewCommentColumns> where, Database database = null)
		{
			ReviewCommentColumns c = new ReviewCommentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ReviewComment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ReviewComment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
