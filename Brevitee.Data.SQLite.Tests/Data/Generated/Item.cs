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
    [Brevitee.Data.Table("Item", "Test")]
    public partial class Item: Dao
    {
        public Item():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Item(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Activity_ItemId", new ActivityCollection(new Query<ActivityColumns, Activity>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("Have_ItemId", new HaveCollection(new Query<HaveColumns, Have>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("ItemData_ItemId", new ItemDataCollection(new Query<ItemDataColumns, ItemData>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("ItemProperty_ItemId", new ItemPropertyCollection(new Query<ItemPropertyColumns, ItemProperty>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("ItemReview_ItemId", new ItemReviewCollection(new Query<ItemReviewColumns, ItemReview>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("ItemTag_ItemId", new ItemTagCollection(new Query<ItemTagColumns, ItemTag>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("Need_ItemId", new NeedCollection(new Query<NeedColumns, Need>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("UserItemReview_ItemId", new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>((c) => c.ItemId == this.Id), this, "ItemId"));	
            this.ChildCollections.Add("Want_ItemId", new WantCollection(new Query<WantColumns, Want>((c) => c.ItemId == this.Id), this, "ItemId"));	
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
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


				
	
	public ActivityCollection ActivityCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Activity_ItemId"))
			{
				SetChildren();
			}

			var c = (ActivityCollection)this.ChildCollections["Activity_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public HaveCollection HaveCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Have_ItemId"))
			{
				SetChildren();
			}

			var c = (HaveCollection)this.ChildCollections["Have_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ItemDataCollection ItemDataCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemData_ItemId"))
			{
				SetChildren();
			}

			var c = (ItemDataCollection)this.ChildCollections["ItemData_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ItemPropertyCollection ItemPropertyCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemProperty_ItemId"))
			{
				SetChildren();
			}

			var c = (ItemPropertyCollection)this.ChildCollections["ItemProperty_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ItemReviewCollection ItemReviewCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemReview_ItemId"))
			{
				SetChildren();
			}

			var c = (ItemReviewCollection)this.ChildCollections["ItemReview_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ItemTagCollection ItemTagCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemTag_ItemId"))
			{
				SetChildren();
			}

			var c = (ItemTagCollection)this.ChildCollections["ItemTag_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public NeedCollection NeedCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Need_ItemId"))
			{
				SetChildren();
			}

			var c = (NeedCollection)this.ChildCollections["Need_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserItemReviewCollection UserItemReviewCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserItemReview_ItemId"))
			{
				SetChildren();
			}

			var c = (UserItemReviewCollection)this.ChildCollections["UserItemReview_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public WantCollection WantCollectionByItemId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Want_ItemId"))
			{
				SetChildren();
			}

			var c = (WantCollection)this.ChildCollections["Want_ItemId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ItemColumns();
			return (colFilter.Id == IdValue);
		}

		public static ItemCollection Where(Func<ItemColumns, QueryFilter<ItemColumns>> where, OrderBy<ItemColumns> orderBy = null)
		{
			return new ItemCollection(new Query<ItemColumns, Item>(where, orderBy), true);
		}
		
		public static ItemCollection Where(WhereDelegate<ItemColumns> where, Database db = null)
		{
			return new ItemCollection(new Query<ItemColumns, Item>(where, db), true);
		}
		   
		public static ItemCollection Where(WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy = null, Database db = null)
		{
			return new ItemCollection(new Query<ItemColumns, Item>(where, orderBy, db), true);
		}

		public static ItemCollection Where(QiQuery where, Database db = null)
		{
			return new ItemCollection(Select<ItemColumns>.From<Item>().Where(where, db));
		}

		public static Item OneWhere(WhereDelegate<ItemColumns> where, Database db = null)
		{
			var results = new ItemCollection(Select<ItemColumns>.From<Item>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Item OneWhere(QiQuery where, Database db = null)
		{
			var results = new ItemCollection(Select<ItemColumns>.From<Item>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Item OneOrThrow(ItemCollection c)
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

		public static Item FirstOneWhere(WhereDelegate<ItemColumns> where, Database db = null)
		{
			var results = new ItemCollection(Select<ItemColumns>.From<Item>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ItemCollection Top(int count, WhereDelegate<ItemColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ItemCollection Top(int count, WhereDelegate<ItemColumns> where, OrderBy<ItemColumns> orderBy, Database database = null)
        {
            ItemColumns c = new ItemColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Item>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Item>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ItemColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ItemCollection>(0);
        }

		public static long Count(WhereDelegate<ItemColumns> where, Database database = null)
		{
			ItemColumns c = new ItemColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Item>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Item>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
