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
    [Brevitee.Data.Table("WishList", "Test")]
    public partial class WishList: Dao
    {
        public WishList():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public WishList(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("WantWishList_WishListId", new WantWishListCollection(new Query<WantWishListColumns, WantWishList>((c) => c.WishListId == this.Id), this, "WishListId"));	
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="NVarChar", MaxLength="255", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}

	// property:Description, columnName:Description	
	[Brevitee.Data.Column(Name="Description", DbDataType="NVarChar", MaxLength="4000", AllowNull=true)]
	public string Description
	{
		get
		{
			return GetStringValue("Description");
		}
		set
		{
			SetValue("Description", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="WishList",
		Name="UserId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
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
	
				
	
	public WantWishListCollection WantWishListCollectionByWishListId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("WantWishList_WishListId"))
			{
				SetChildren();
			}

			var c = (WantWishListCollection)this.ChildCollections["WantWishList_WishListId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new WishListColumns();
			return (colFilter.Id == IdValue);
		}

		public static WishListCollection Where(Func<WishListColumns, QueryFilter<WishListColumns>> where, OrderBy<WishListColumns> orderBy = null)
		{
			return new WishListCollection(new Query<WishListColumns, WishList>(where, orderBy), true);
		}
		
		public static WishListCollection Where(WhereDelegate<WishListColumns> where, Database db = null)
		{
			return new WishListCollection(new Query<WishListColumns, WishList>(where, db), true);
		}
		   
		public static WishListCollection Where(WhereDelegate<WishListColumns> where, OrderBy<WishListColumns> orderBy = null, Database db = null)
		{
			return new WishListCollection(new Query<WishListColumns, WishList>(where, orderBy, db), true);
		}

		public static WishListCollection Where(QiQuery where, Database db = null)
		{
			return new WishListCollection(Select<WishListColumns>.From<WishList>().Where(where, db));
		}

		public static WishList OneWhere(WhereDelegate<WishListColumns> where, Database db = null)
		{
			var results = new WishListCollection(Select<WishListColumns>.From<WishList>().Where(where, db));
			return OneOrThrow(results);
		}

		public static WishList OneWhere(QiQuery where, Database db = null)
		{
			var results = new WishListCollection(Select<WishListColumns>.From<WishList>().Where(where, db));
			return OneOrThrow(results);
		}

		private static WishList OneOrThrow(WishListCollection c)
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

		public static WishList FirstOneWhere(WhereDelegate<WishListColumns> where, Database db = null)
		{
			var results = new WishListCollection(Select<WishListColumns>.From<WishList>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static WishListCollection Top(int count, WhereDelegate<WishListColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static WishListCollection Top(int count, WhereDelegate<WishListColumns> where, OrderBy<WishListColumns> orderBy, Database database = null)
        {
            WishListColumns c = new WishListColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<WishList>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<WishList>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WishListColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<WishListCollection>(0);
        }

		public static long Count(WhereDelegate<WishListColumns> where, Database database = null)
		{
			WishListColumns c = new WishListColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<WishList>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<WishList>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
