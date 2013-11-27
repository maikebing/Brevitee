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
    [Brevitee.Data.Table("ItemTag", "Test")]
    public partial class ItemTag: Dao
    {
        public ItemTag():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ItemTag(DataRow data): base(data)
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
        Table="ItemTag",
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
	
	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
        Table="ItemTag",
		Name="TagId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public long? TagId
	{
		get
		{
			return GetLongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = SampleData.Tag.OneWhere(f => f.Id == this.TagId);
			}
			return _tagOfTagId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ItemTagColumns();
			return (colFilter.Id == IdValue);
		}

		public static ItemTagCollection Where(Func<ItemTagColumns, QueryFilter<ItemTagColumns>> where, OrderBy<ItemTagColumns> orderBy = null)
		{
			return new ItemTagCollection(new Query<ItemTagColumns, ItemTag>(where, orderBy), true);
		}
		
		public static ItemTagCollection Where(WhereDelegate<ItemTagColumns> where, Database db = null)
		{
			return new ItemTagCollection(new Query<ItemTagColumns, ItemTag>(where, db), true);
		}
		   
		public static ItemTagCollection Where(WhereDelegate<ItemTagColumns> where, OrderBy<ItemTagColumns> orderBy = null, Database db = null)
		{
			return new ItemTagCollection(new Query<ItemTagColumns, ItemTag>(where, orderBy, db), true);
		}

		public static ItemTagCollection Where(QiQuery where, Database db = null)
		{
			return new ItemTagCollection(Select<ItemTagColumns>.From<ItemTag>().Where(where, db));
		}

		public static ItemTag OneWhere(WhereDelegate<ItemTagColumns> where, Database db = null)
		{
			var results = new ItemTagCollection(Select<ItemTagColumns>.From<ItemTag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ItemTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new ItemTagCollection(Select<ItemTagColumns>.From<ItemTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ItemTag OneOrThrow(ItemTagCollection c)
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

		public static ItemTag FirstOneWhere(WhereDelegate<ItemTagColumns> where, Database db = null)
		{
			var results = new ItemTagCollection(Select<ItemTagColumns>.From<ItemTag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ItemTagCollection Top(int count, WhereDelegate<ItemTagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ItemTagCollection Top(int count, WhereDelegate<ItemTagColumns> where, OrderBy<ItemTagColumns> orderBy, Database database = null)
        {
            ItemTagColumns c = new ItemTagColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<ItemTag>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ItemTag>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ItemTagColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ItemTagCollection>(0);
        }

		public static long Count(WhereDelegate<ItemTagColumns> where, Database database = null)
		{
			ItemTagColumns c = new ItemTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ItemTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ItemTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
