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
    [Brevitee.Data.Table("Need", "Test")]
    public partial class Need: Dao
    {
        public Need():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Need(DataRow data): base(data)
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

	// property:LastModified, columnName:LastModified	
	[Brevitee.Data.Column(Name="LastModified", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
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
        Table="Need",
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
	
	// start ItemId -> ItemId
	[Brevitee.Data.ForeignKey(
        Table="Need",
		Name="ItemId", 
		DbDataType="BigInt", 
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
	
	// start NeedStatusId -> NeedStatusId
	[Brevitee.Data.ForeignKey(
        Table="Need",
		Name="NeedStatusId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="NeedStatus",
		Suffix="3")]
	public long? NeedStatusId
	{
		get
		{
			return GetLongValue("NeedStatusId");
		}
		set
		{
			SetValue("NeedStatusId", value);
		}
	}

	NeedStatus _needStatusOfNeedStatusId;
	public NeedStatus NeedStatusOfNeedStatusId
	{
		get
		{
			if(_needStatusOfNeedStatusId == null)
			{
				_needStatusOfNeedStatusId = SampleData.NeedStatus.OneWhere(f => f.Id == this.NeedStatusId);
			}
			return _needStatusOfNeedStatusId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new NeedColumns();
			return (colFilter.Id == IdValue);
		}

		public static NeedCollection Where(Func<NeedColumns, QueryFilter<NeedColumns>> where, OrderBy<NeedColumns> orderBy = null)
		{
			return new NeedCollection(new Query<NeedColumns, Need>(where, orderBy), true);
		}
		
		public static NeedCollection Where(WhereDelegate<NeedColumns> where, Database db = null)
		{
			return new NeedCollection(new Query<NeedColumns, Need>(where, db), true);
		}
		   
		public static NeedCollection Where(WhereDelegate<NeedColumns> where, OrderBy<NeedColumns> orderBy = null, Database db = null)
		{
			return new NeedCollection(new Query<NeedColumns, Need>(where, orderBy, db), true);
		}

		public static NeedCollection Where(QiQuery where, Database db = null)
		{
			return new NeedCollection(Select<NeedColumns>.From<Need>().Where(where, db));
		}

		public static Need OneWhere(WhereDelegate<NeedColumns> where, Database db = null)
		{
			var results = new NeedCollection(Select<NeedColumns>.From<Need>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Need OneWhere(QiQuery where, Database db = null)
		{
			var results = new NeedCollection(Select<NeedColumns>.From<Need>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Need OneOrThrow(NeedCollection c)
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

		public static Need FirstOneWhere(WhereDelegate<NeedColumns> where, Database db = null)
		{
			var results = new NeedCollection(Select<NeedColumns>.From<Need>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static NeedCollection Top(int count, WhereDelegate<NeedColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static NeedCollection Top(int count, WhereDelegate<NeedColumns> where, OrderBy<NeedColumns> orderBy, Database database = null)
        {
            NeedColumns c = new NeedColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Need>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Need>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<NeedColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<NeedCollection>(0);
        }

		public static long Count(WhereDelegate<NeedColumns> where, Database database = null)
		{
			NeedColumns c = new NeedColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Need>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Need>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
