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
    [Brevitee.Data.Table("ItemProperty", "Test")]
    public partial class ItemProperty: Dao
    {
        public ItemProperty():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ItemProperty(DataRow data): base(data)
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}


	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="ItemProperty",
		Name="ItemId", 
		DbDataType="BigInt", 
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
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ItemPropertyColumns();
			return (colFilter.Id == IdValue);
		}

		public static ItemPropertyCollection Where(Func<ItemPropertyColumns, QueryFilter<ItemPropertyColumns>> where, OrderBy<ItemPropertyColumns> orderBy = null)
		{
			return new ItemPropertyCollection(new Query<ItemPropertyColumns, ItemProperty>(where, orderBy), true);
		}
		
		public static ItemPropertyCollection Where(WhereDelegate<ItemPropertyColumns> where, Database db = null)
		{
			return new ItemPropertyCollection(new Query<ItemPropertyColumns, ItemProperty>(where, db), true);
		}
		   
		public static ItemPropertyCollection Where(WhereDelegate<ItemPropertyColumns> where, OrderBy<ItemPropertyColumns> orderBy = null, Database db = null)
		{
			return new ItemPropertyCollection(new Query<ItemPropertyColumns, ItemProperty>(where, orderBy, db), true);
		}

		public static ItemPropertyCollection Where(QiQuery where, Database db = null)
		{
			return new ItemPropertyCollection(Select<ItemPropertyColumns>.From<ItemProperty>().Where(where, db));
		}

		public static ItemProperty OneWhere(WhereDelegate<ItemPropertyColumns> where, Database db = null)
		{
			var results = new ItemPropertyCollection(Select<ItemPropertyColumns>.From<ItemProperty>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ItemProperty OneWhere(QiQuery where, Database db = null)
		{
			var results = new ItemPropertyCollection(Select<ItemPropertyColumns>.From<ItemProperty>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ItemProperty OneOrThrow(ItemPropertyCollection c)
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

		public static ItemProperty FirstOneWhere(WhereDelegate<ItemPropertyColumns> where, Database db = null)
		{
			var results = new ItemPropertyCollection(Select<ItemPropertyColumns>.From<ItemProperty>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ItemPropertyCollection Top(int count, WhereDelegate<ItemPropertyColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ItemPropertyCollection Top(int count, WhereDelegate<ItemPropertyColumns> where, OrderBy<ItemPropertyColumns> orderBy, Database database = null)
        {
            ItemPropertyColumns c = new ItemPropertyColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<ItemProperty>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ItemProperty>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ItemPropertyColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ItemPropertyCollection>(0);
        }

		public static long Count(WhereDelegate<ItemPropertyColumns> where, Database database = null)
		{
			ItemPropertyColumns c = new ItemPropertyColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ItemProperty>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ItemProperty>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
