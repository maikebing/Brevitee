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
    [Brevitee.Data.Table("ItemReview", "Test")]
    public partial class ItemReview: Dao
    {
        public ItemReview():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ItemReview(DataRow data): base(data)
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


	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="ItemReview",
		Name="ItemId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Item",
		Suffix="1")]
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
        Table="ItemReview",
		Name="ReviewId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Review",
		Suffix="2")]
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
	
	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="ItemReview",
		Name="UserId", 
		ExtractedType="BigInt", 
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
			var colFilter = new ItemReviewColumns();
			return (colFilter.Id == IdValue);
		}

		public static ItemReviewCollection Where(Func<ItemReviewColumns, QueryFilter<ItemReviewColumns>> where, OrderBy<ItemReviewColumns> orderBy = null)
		{
			return new ItemReviewCollection(new Query<ItemReviewColumns, ItemReview>(where, orderBy), true);
		}
		
		public static ItemReviewCollection Where(WhereDelegate<ItemReviewColumns> where, Database db = null)
		{
			return new ItemReviewCollection(new Query<ItemReviewColumns, ItemReview>(where, db), true);
		}
		   
		public static ItemReviewCollection Where(WhereDelegate<ItemReviewColumns> where, OrderBy<ItemReviewColumns> orderBy = null, Database db = null)
		{
			return new ItemReviewCollection(new Query<ItemReviewColumns, ItemReview>(where, orderBy, db), true);
		}

		public static ItemReviewCollection Where(QiQuery where, Database db = null)
		{
			return new ItemReviewCollection(Select<ItemReviewColumns>.From<ItemReview>().Where(where, db));
		}

		public static ItemReview OneWhere(WhereDelegate<ItemReviewColumns> where, Database db = null)
		{
			var results = new ItemReviewCollection(Select<ItemReviewColumns>.From<ItemReview>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ItemReview OneWhere(QiQuery where, Database db = null)
		{
			var results = new ItemReviewCollection(Select<ItemReviewColumns>.From<ItemReview>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ItemReview OneOrThrow(ItemReviewCollection c)
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

		public static ItemReview FirstOneWhere(WhereDelegate<ItemReviewColumns> where, Database db = null)
		{
			var results = new ItemReviewCollection(Select<ItemReviewColumns>.From<ItemReview>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ItemReviewCollection Top(int count, WhereDelegate<ItemReviewColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ItemReviewCollection Top(int count, WhereDelegate<ItemReviewColumns> where, OrderBy<ItemReviewColumns> orderBy, Database database = null)
        {
            ItemReviewColumns c = new ItemReviewColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<ItemReview>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ItemReview>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ItemReviewColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ItemReviewCollection>(0);
        }

		public static long Count(WhereDelegate<ItemReviewColumns> where, Database database = null)
		{
			ItemReviewColumns c = new ItemReviewColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ItemReview>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ItemReview>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
