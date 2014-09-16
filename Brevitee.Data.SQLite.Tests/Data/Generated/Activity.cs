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
    [Brevitee.Data.Table("Activity", "Test")]
    public partial class Activity: Dao
    {
        public Activity():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Activity(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ActivityComment_ActivityId", new ActivityCommentCollection(new Query<ActivityCommentColumns, ActivityComment>((c) => c.ActivityId == this.Id), this, "ActivityId"));	
            this.ChildCollections.Add("ActivitySystemComment_ActivityId", new ActivitySystemCommentCollection(new Query<ActivitySystemCommentColumns, ActivitySystemComment>((c) => c.ActivityId == this.Id), this, "ActivityId"));	
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

	// property:Action, columnName:Action	
	[Brevitee.Data.Column(Name="Action", DbDataType="NVarChar", MaxLength="255", AllowNull=false)]
	public string Action
	{
		get
		{
			return GetStringValue("Action");
		}
		set
		{
			SetValue("Action", value);
		}
	}

	// property:DateTime, columnName:DateTime	
	[Brevitee.Data.Column(Name="DateTime", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime DateTime
	{
		get
		{
			return GetDateTimeValue("DateTime");
		}
		set
		{
			SetValue("DateTime", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Activity",
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
        Table="Activity",
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
	
				
	
	public ActivityCommentCollection ActivityCommentCollectionByActivityId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ActivityComment_ActivityId"))
			{
				SetChildren();
			}

			var c = (ActivityCommentCollection)this.ChildCollections["ActivityComment_ActivityId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ActivitySystemCommentCollection ActivitySystemCommentCollectionByActivityId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ActivitySystemComment_ActivityId"))
			{
				SetChildren();
			}

			var c = (ActivitySystemCommentCollection)this.ChildCollections["ActivitySystemComment_ActivityId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ActivityColumns();
			return (colFilter.Id == IdValue);
		}

		public static ActivityCollection Where(Func<ActivityColumns, QueryFilter<ActivityColumns>> where, OrderBy<ActivityColumns> orderBy = null)
		{
			return new ActivityCollection(new Query<ActivityColumns, Activity>(where, orderBy), true);
		}
		
		public static ActivityCollection Where(WhereDelegate<ActivityColumns> where, Database db = null)
		{
			return new ActivityCollection(new Query<ActivityColumns, Activity>(where, db), true);
		}
		   
		public static ActivityCollection Where(WhereDelegate<ActivityColumns> where, OrderBy<ActivityColumns> orderBy = null, Database db = null)
		{
			return new ActivityCollection(new Query<ActivityColumns, Activity>(where, orderBy, db), true);
		}

		public static ActivityCollection Where(QiQuery where, Database db = null)
		{
			return new ActivityCollection(Select<ActivityColumns>.From<Activity>().Where(where, db));
		}

		public static Activity OneWhere(WhereDelegate<ActivityColumns> where, Database db = null)
		{
			var results = new ActivityCollection(Select<ActivityColumns>.From<Activity>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Activity OneWhere(QiQuery where, Database db = null)
		{
			var results = new ActivityCollection(Select<ActivityColumns>.From<Activity>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Activity OneOrThrow(ActivityCollection c)
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

		public static Activity FirstOneWhere(WhereDelegate<ActivityColumns> where, Database db = null)
		{
			var results = new ActivityCollection(Select<ActivityColumns>.From<Activity>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ActivityCollection Top(int count, WhereDelegate<ActivityColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ActivityCollection Top(int count, WhereDelegate<ActivityColumns> where, OrderBy<ActivityColumns> orderBy, Database database = null)
        {
            ActivityColumns c = new ActivityColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Activity>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Activity>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ActivityColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ActivityCollection>(0);
        }

		public static long Count(WhereDelegate<ActivityColumns> where, Database database = null)
		{
			ActivityColumns c = new ActivityColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Activity>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Activity>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
