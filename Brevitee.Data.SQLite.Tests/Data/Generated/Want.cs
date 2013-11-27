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
    [Brevitee.Data.Table("Want", "Test")]
    public partial class Want: Dao
    {
        public Want():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Want(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Give_WantId", new GiveCollection(new Query<GiveColumns, Give>((c) => c.WantId == this.Id), this, "WantId"));	
            this.ChildCollections.Add("WantWishList_WantId", new WantWishListCollection(new Query<WantWishListColumns, WantWishList>((c) => c.WantId == this.Id), this, "WantId"));	
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

	// property:LastModified, columnName:LastModified	
	[Brevitee.Data.Column(Name="LastModified", ExtractedType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime LastModified
	{
		get
		{
			return GetDateTimeValue("LastModified");
		}
		set
		{
			SetValue("LastModified", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Want",
		Name="UserId", 
		ExtractedType="BigInt", 
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
	
	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="Want",
		Name="ItemId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Item",
		Suffix="2")]
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
	
	// start WantStatusId -> WantStatusId
	[Brevitee.Data.ForeignKey(
        Table="Want",
		Name="WantStatusId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="WantStatus",
		Suffix="3")]
	public long? WantStatusId
	{
		get
		{
			return GetLongValue("WantStatusId");
		}
		set
		{
			SetValue("WantStatusId", value);
		}
	}

	WantStatus _wantStatusOfWantStatusId;
	public WantStatus WantStatusOfWantStatusId
	{
		get
		{
			if(_wantStatusOfWantStatusId == null)
			{
				_wantStatusOfWantStatusId = SampleData.WantStatus.OneWhere(f => f.Id == this.WantStatusId);
			}
			return _wantStatusOfWantStatusId;
		}
	}
	
				
	
	public GiveCollection GiveCollectionByWantId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Give_WantId"))
			{
				SetChildren();
			}

			var c = (GiveCollection)this.ChildCollections["Give_WantId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public WantWishListCollection WantWishListCollectionByWantId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("WantWishList_WantId"))
			{
				SetChildren();
			}

			var c = (WantWishListCollection)this.ChildCollections["WantWishList_WantId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new WantColumns();
			return (colFilter.Id == IdValue);
		}

		public static WantCollection Where(Func<WantColumns, QueryFilter<WantColumns>> where, OrderBy<WantColumns> orderBy = null)
		{
			return new WantCollection(new Query<WantColumns, Want>(where, orderBy), true);
		}
		
		public static WantCollection Where(WhereDelegate<WantColumns> where, Database db = null)
		{
			return new WantCollection(new Query<WantColumns, Want>(where, db), true);
		}
		   
		public static WantCollection Where(WhereDelegate<WantColumns> where, OrderBy<WantColumns> orderBy = null, Database db = null)
		{
			return new WantCollection(new Query<WantColumns, Want>(where, orderBy, db), true);
		}

		public static WantCollection Where(QiQuery where, Database db = null)
		{
			return new WantCollection(Select<WantColumns>.From<Want>().Where(where, db));
		}

		public static Want OneWhere(WhereDelegate<WantColumns> where, Database db = null)
		{
			var results = new WantCollection(Select<WantColumns>.From<Want>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Want OneWhere(QiQuery where, Database db = null)
		{
			var results = new WantCollection(Select<WantColumns>.From<Want>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Want OneOrThrow(WantCollection c)
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

		public static Want FirstOneWhere(WhereDelegate<WantColumns> where, Database db = null)
		{
			var results = new WantCollection(Select<WantColumns>.From<Want>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static WantCollection Top(int count, WhereDelegate<WantColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static WantCollection Top(int count, WhereDelegate<WantColumns> where, OrderBy<WantColumns> orderBy, Database database = null)
        {
            WantColumns c = new WantColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Want>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Want>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WantColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<WantCollection>(0);
        }

		public static long Count(WhereDelegate<WantColumns> where, Database database = null)
		{
			WantColumns c = new WantColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Want>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Want>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
