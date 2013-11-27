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
    [Brevitee.Data.Table("ItemData", "Test")]
    public partial class ItemData: Dao
    {
        public ItemData():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ItemData(DataRow data): base(data)
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="VarBinary", MaxLength="4000", AllowNull=false)]
	public byte[] Value
	{
		get
		{
			return GetByteValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}

	// property:DataType, columnName:DataType	
	[Brevitee.Data.Column(Name="DataType", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string DataType
	{
		get
		{
			return GetStringValue("DataType");
		}
		set
		{
			SetValue("DataType", value);
		}
	}


	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="ItemData",
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
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ItemDataColumns();
			return (colFilter.Id == IdValue);
		}

		public static ItemDataCollection Where(Func<ItemDataColumns, QueryFilter<ItemDataColumns>> where, OrderBy<ItemDataColumns> orderBy = null)
		{
			return new ItemDataCollection(new Query<ItemDataColumns, ItemData>(where, orderBy), true);
		}
		
		public static ItemDataCollection Where(WhereDelegate<ItemDataColumns> where, Database db = null)
		{
			return new ItemDataCollection(new Query<ItemDataColumns, ItemData>(where, db), true);
		}
		   
		public static ItemDataCollection Where(WhereDelegate<ItemDataColumns> where, OrderBy<ItemDataColumns> orderBy = null, Database db = null)
		{
			return new ItemDataCollection(new Query<ItemDataColumns, ItemData>(where, orderBy, db), true);
		}

		public static ItemDataCollection Where(QiQuery where, Database db = null)
		{
			return new ItemDataCollection(Select<ItemDataColumns>.From<ItemData>().Where(where, db));
		}

		public static ItemData OneWhere(WhereDelegate<ItemDataColumns> where, Database db = null)
		{
			var results = new ItemDataCollection(Select<ItemDataColumns>.From<ItemData>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ItemData OneWhere(QiQuery where, Database db = null)
		{
			var results = new ItemDataCollection(Select<ItemDataColumns>.From<ItemData>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ItemData OneOrThrow(ItemDataCollection c)
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

		public static ItemData FirstOneWhere(WhereDelegate<ItemDataColumns> where, Database db = null)
		{
			var results = new ItemDataCollection(Select<ItemDataColumns>.From<ItemData>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ItemDataCollection Top(int count, WhereDelegate<ItemDataColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ItemDataCollection Top(int count, WhereDelegate<ItemDataColumns> where, OrderBy<ItemDataColumns> orderBy, Database database = null)
        {
            ItemDataColumns c = new ItemDataColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<ItemData>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ItemData>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ItemDataColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ItemDataCollection>(0);
        }

		public static long Count(WhereDelegate<ItemDataColumns> where, Database database = null)
		{
			ItemDataColumns c = new ItemDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ItemData>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ItemData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
