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
    [Brevitee.Data.Table("WantWishList", "Test")]
    public partial class WantWishList: Dao
    {
        public WantWishList():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public WantWishList(DataRow data): base(data)
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


	// start WantId -> WantId
	[Brevitee.Data.ForeignKey(
        Table="WantWishList",
		Name="WantId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Want",
		Suffix="1")]
	public long? WantId
	{
		get
		{
			return GetLongValue("WantId");
		}
		set
		{
			SetValue("WantId", value);
		}
	}

	Want _wantOfWantId;
	public Want WantOfWantId
	{
		get
		{
			if(_wantOfWantId == null)
			{
				_wantOfWantId = SampleData.Want.OneWhere(f => f.Id == this.WantId);
			}
			return _wantOfWantId;
		}
	}
	
	// start WishListId -> WishListId
	[Brevitee.Data.ForeignKey(
        Table="WantWishList",
		Name="WishListId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="WishList",
		Suffix="2")]
	public long? WishListId
	{
		get
		{
			return GetLongValue("WishListId");
		}
		set
		{
			SetValue("WishListId", value);
		}
	}

	WishList _wishListOfWishListId;
	public WishList WishListOfWishListId
	{
		get
		{
			if(_wishListOfWishListId == null)
			{
				_wishListOfWishListId = SampleData.WishList.OneWhere(f => f.Id == this.WishListId);
			}
			return _wishListOfWishListId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new WantWishListColumns();
			return (colFilter.Id == IdValue);
		}

		public static WantWishListCollection Where(Func<WantWishListColumns, QueryFilter<WantWishListColumns>> where, OrderBy<WantWishListColumns> orderBy = null)
		{
			return new WantWishListCollection(new Query<WantWishListColumns, WantWishList>(where, orderBy), true);
		}
		
		public static WantWishListCollection Where(WhereDelegate<WantWishListColumns> where, Database db = null)
		{
			return new WantWishListCollection(new Query<WantWishListColumns, WantWishList>(where, db), true);
		}
		   
		public static WantWishListCollection Where(WhereDelegate<WantWishListColumns> where, OrderBy<WantWishListColumns> orderBy = null, Database db = null)
		{
			return new WantWishListCollection(new Query<WantWishListColumns, WantWishList>(where, orderBy, db), true);
		}

		public static WantWishListCollection Where(QiQuery where, Database db = null)
		{
			return new WantWishListCollection(Select<WantWishListColumns>.From<WantWishList>().Where(where, db));
		}

		public static WantWishList OneWhere(WhereDelegate<WantWishListColumns> where, Database db = null)
		{
			var results = new WantWishListCollection(Select<WantWishListColumns>.From<WantWishList>().Where(where, db));
			return OneOrThrow(results);
		}

		public static WantWishList OneWhere(QiQuery where, Database db = null)
		{
			var results = new WantWishListCollection(Select<WantWishListColumns>.From<WantWishList>().Where(where, db));
			return OneOrThrow(results);
		}

		private static WantWishList OneOrThrow(WantWishListCollection c)
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

		public static WantWishList FirstOneWhere(WhereDelegate<WantWishListColumns> where, Database db = null)
		{
			var results = new WantWishListCollection(Select<WantWishListColumns>.From<WantWishList>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static WantWishListCollection Top(int count, WhereDelegate<WantWishListColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static WantWishListCollection Top(int count, WhereDelegate<WantWishListColumns> where, OrderBy<WantWishListColumns> orderBy, Database database = null)
        {
            WantWishListColumns c = new WantWishListColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<WantWishList>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<WantWishList>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WantWishListColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<WantWishListCollection>(0);
        }

		public static long Count(WhereDelegate<WantWishListColumns> where, Database database = null)
		{
			WantWishListColumns c = new WantWishListColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<WantWishList>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<WantWishList>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
