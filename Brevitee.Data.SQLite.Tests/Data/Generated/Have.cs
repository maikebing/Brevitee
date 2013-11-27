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
    [Brevitee.Data.Table("Have", "Test")]
    public partial class Have: Dao
    {
        public Have():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Have(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Give_HaveId", new GiveCollection(new Query<GiveColumns, Give>((c) => c.HaveId == this.Id), this, "HaveId"));	
            this.ChildCollections.Add("HaveDescription_HaveId", new HaveDescriptionCollection(new Query<HaveDescriptionColumns, HaveDescription>((c) => c.HaveId == this.Id), this, "HaveId"));	
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

	// property:Quantity, columnName:Quantity	
	[Brevitee.Data.Column(Name="Quantity", ExtractedType="Int", MaxLength="4", AllowNull=true)]
	public int? Quantity
	{
		get
		{
			return GetIntValue("Quantity");
		}
		set
		{
			SetValue("Quantity", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Have",
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
	
	// start HaveStatusId -> HaveStatusId
	[Brevitee.Data.ForeignKey(
        Table="Have",
		Name="HaveStatusId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="HaveStatus",
		Suffix="2")]
	public long? HaveStatusId
	{
		get
		{
			return GetLongValue("HaveStatusId");
		}
		set
		{
			SetValue("HaveStatusId", value);
		}
	}

	HaveStatus _haveStatusOfHaveStatusId;
	public HaveStatus HaveStatusOfHaveStatusId
	{
		get
		{
			if(_haveStatusOfHaveStatusId == null)
			{
				_haveStatusOfHaveStatusId = SampleData.HaveStatus.OneWhere(f => f.Id == this.HaveStatusId);
			}
			return _haveStatusOfHaveStatusId;
		}
	}
	
	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="Have",
		Name="ItemId", 
		ExtractedType="BigInt", 
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
	
				
	
	public GiveCollection GiveCollectionByHaveId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Give_HaveId"))
			{
				SetChildren();
			}

			var c = (GiveCollection)this.ChildCollections["Give_HaveId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public HaveDescriptionCollection HaveDescriptionCollectionByHaveId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HaveDescription_HaveId"))
			{
				SetChildren();
			}

			var c = (HaveDescriptionCollection)this.ChildCollections["HaveDescription_HaveId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new HaveColumns();
			return (colFilter.Id == IdValue);
		}

		public static HaveCollection Where(Func<HaveColumns, QueryFilter<HaveColumns>> where, OrderBy<HaveColumns> orderBy = null)
		{
			return new HaveCollection(new Query<HaveColumns, Have>(where, orderBy), true);
		}
		
		public static HaveCollection Where(WhereDelegate<HaveColumns> where, Database db = null)
		{
			return new HaveCollection(new Query<HaveColumns, Have>(where, db), true);
		}
		   
		public static HaveCollection Where(WhereDelegate<HaveColumns> where, OrderBy<HaveColumns> orderBy = null, Database db = null)
		{
			return new HaveCollection(new Query<HaveColumns, Have>(where, orderBy, db), true);
		}

		public static HaveCollection Where(QiQuery where, Database db = null)
		{
			return new HaveCollection(Select<HaveColumns>.From<Have>().Where(where, db));
		}

		public static Have OneWhere(WhereDelegate<HaveColumns> where, Database db = null)
		{
			var results = new HaveCollection(Select<HaveColumns>.From<Have>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Have OneWhere(QiQuery where, Database db = null)
		{
			var results = new HaveCollection(Select<HaveColumns>.From<Have>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Have OneOrThrow(HaveCollection c)
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

		public static Have FirstOneWhere(WhereDelegate<HaveColumns> where, Database db = null)
		{
			var results = new HaveCollection(Select<HaveColumns>.From<Have>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static HaveCollection Top(int count, WhereDelegate<HaveColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static HaveCollection Top(int count, WhereDelegate<HaveColumns> where, OrderBy<HaveColumns> orderBy, Database database = null)
        {
            HaveColumns c = new HaveColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Have>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Have>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HaveColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<HaveCollection>(0);
        }

		public static long Count(WhereDelegate<HaveColumns> where, Database database = null)
		{
			HaveColumns c = new HaveColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Have>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Have>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
