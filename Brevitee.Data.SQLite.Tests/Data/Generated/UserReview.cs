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
    [Brevitee.Data.Table("UserReview", "Test")]
    public partial class UserReview: Dao
    {
        public UserReview():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserReview(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

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


	// start ReviewerId -> ReviewerId
	[Brevitee.Data.ForeignKey(
        Table="UserReview",
		Name="ReviewerId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? ReviewerId
	{
		get
		{
			return GetLongValue("ReviewerId");
		}
		set
		{
			SetValue("ReviewerId", value);
		}
	}

	User _userOfReviewerId;
	public User UserOfReviewerId
	{
		get
		{
			if(_userOfReviewerId == null)
			{
				_userOfReviewerId = SampleData.User.OneWhere(f => f.Id == this.ReviewerId);
			}
			return _userOfReviewerId;
		}
	}
	
	// start RevieweeId -> RevieweeId
	[Brevitee.Data.ForeignKey(
        Table="UserReview",
		Name="RevieweeId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="2")]
	public long? RevieweeId
	{
		get
		{
			return GetLongValue("RevieweeId");
		}
		set
		{
			SetValue("RevieweeId", value);
		}
	}

	User _userOfRevieweeId;
	public User UserOfRevieweeId
	{
		get
		{
			if(_userOfRevieweeId == null)
			{
				_userOfRevieweeId = SampleData.User.OneWhere(f => f.Id == this.RevieweeId);
			}
			return _userOfRevieweeId;
		}
	}
	
	// start ReviewId -> ReviewId
	[Brevitee.Data.ForeignKey(
        Table="UserReview",
		Name="ReviewId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Review",
		Suffix="3")]
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
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserReviewColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserReviewCollection Where(Func<UserReviewColumns, QueryFilter<UserReviewColumns>> where, OrderBy<UserReviewColumns> orderBy = null)
		{
			return new UserReviewCollection(new Query<UserReviewColumns, UserReview>(where, orderBy), true);
		}
		
		public static UserReviewCollection Where(WhereDelegate<UserReviewColumns> where, Database db = null)
		{
			return new UserReviewCollection(new Query<UserReviewColumns, UserReview>(where, db), true);
		}
		   
		public static UserReviewCollection Where(WhereDelegate<UserReviewColumns> where, OrderBy<UserReviewColumns> orderBy = null, Database db = null)
		{
			return new UserReviewCollection(new Query<UserReviewColumns, UserReview>(where, orderBy, db), true);
		}

		public static UserReviewCollection Where(QiQuery where, Database db = null)
		{
			return new UserReviewCollection(Select<UserReviewColumns>.From<UserReview>().Where(where, db));
		}

		public static UserReview OneWhere(WhereDelegate<UserReviewColumns> where, Database db = null)
		{
			var results = new UserReviewCollection(Select<UserReviewColumns>.From<UserReview>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserReview OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserReviewCollection(Select<UserReviewColumns>.From<UserReview>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserReview OneOrThrow(UserReviewCollection c)
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

		public static UserReview FirstOneWhere(WhereDelegate<UserReviewColumns> where, Database db = null)
		{
			var results = new UserReviewCollection(Select<UserReviewColumns>.From<UserReview>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserReviewCollection Top(int count, WhereDelegate<UserReviewColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserReviewCollection Top(int count, WhereDelegate<UserReviewColumns> where, OrderBy<UserReviewColumns> orderBy, Database database = null)
        {
            UserReviewColumns c = new UserReviewColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<UserReview>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserReview>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserReviewColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserReviewCollection>(0);
        }

		public static long Count(WhereDelegate<UserReviewColumns> where, Database database = null)
		{
			UserReviewColumns c = new UserReviewColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UserReview>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserReview>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
