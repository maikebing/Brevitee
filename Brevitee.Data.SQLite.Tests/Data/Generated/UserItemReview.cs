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
    [Brevitee.Data.Table("UserItemReview", "Test")]
    public partial class UserItemReview: Dao
    {
        public UserItemReview():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserItemReview(DataRow data): base(data)
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


	// start ReviewerId -> ReviewerId
	[Brevitee.Data.ForeignKey(
        Table="UserItemReview",
		Name="ReviewerId", 
		DbDataType="BigInt", 
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
        Table="UserItemReview",
		Name="RevieweeId", 
		DbDataType="BigInt", 
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
	
	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="UserItemReview",
		Name="ItemId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Item",
		Suffix="3")]
	public long? ItemId
	{
		get
		{
			return GetLongValue("ItemId");
		}
		set
		{
			SetValue("ItemId", value);
		}
	}

	Item _itemOfItemId;
	public Item ItemOfItemId
	{
		get
		{
			if(_itemOfItemId == null)
			{
				_itemOfItemId = SampleData.Item.OneWhere(f => f.Id == this.ItemId);
			}
			return _itemOfItemId;
		}
	}
	
	// start ReviewId -> ReviewId
	[Brevitee.Data.ForeignKey(
        Table="UserItemReview",
		Name="ReviewId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Review",
		Suffix="4")]
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
			var colFilter = new UserItemReviewColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserItemReviewCollection Where(Func<UserItemReviewColumns, QueryFilter<UserItemReviewColumns>> where, OrderBy<UserItemReviewColumns> orderBy = null)
		{
			return new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>(where, orderBy), true);
		}
		
		public static UserItemReviewCollection Where(WhereDelegate<UserItemReviewColumns> where, Database db = null)
		{
			return new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>(where, db), true);
		}
		   
		public static UserItemReviewCollection Where(WhereDelegate<UserItemReviewColumns> where, OrderBy<UserItemReviewColumns> orderBy = null, Database db = null)
		{
			return new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>(where, orderBy, db), true);
		}

		public static UserItemReviewCollection Where(QiQuery where, Database db = null)
		{
			return new UserItemReviewCollection(Select<UserItemReviewColumns>.From<UserItemReview>().Where(where, db));
		}

		public static UserItemReview OneWhere(WhereDelegate<UserItemReviewColumns> where, Database db = null)
		{
			var results = new UserItemReviewCollection(Select<UserItemReviewColumns>.From<UserItemReview>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserItemReview OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserItemReviewCollection(Select<UserItemReviewColumns>.From<UserItemReview>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserItemReview OneOrThrow(UserItemReviewCollection c)
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

		public static UserItemReview FirstOneWhere(WhereDelegate<UserItemReviewColumns> where, Database db = null)
		{
			var results = new UserItemReviewCollection(Select<UserItemReviewColumns>.From<UserItemReview>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserItemReviewCollection Top(int count, WhereDelegate<UserItemReviewColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserItemReviewCollection Top(int count, WhereDelegate<UserItemReviewColumns> where, OrderBy<UserItemReviewColumns> orderBy, Database database = null)
        {
            UserItemReviewColumns c = new UserItemReviewColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<UserItemReview>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserItemReview>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserItemReviewColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserItemReviewCollection>(0);
        }

		public static long Count(WhereDelegate<UserItemReviewColumns> where, Database database = null)
		{
			UserItemReviewColumns c = new UserItemReviewColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UserItemReview>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserItemReview>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
