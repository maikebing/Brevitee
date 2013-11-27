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
    [Brevitee.Data.Table("UserSetting", "Test")]
    public partial class UserSetting: Dao
    {
        public UserSetting():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserSetting(DataRow data): base(data)
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


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="UserSetting",
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
	
	// start SettingId -> SettingId
	[Brevitee.Data.ForeignKey(
        Table="UserSetting",
		Name="SettingId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Setting",
		Suffix="2")]
	public long? SettingId
	{
		get
		{
			return GetLongValue("SettingId");
		}
		set
		{
			SetValue("SettingId", value);
		}
	}

	Setting _settingOfSettingId;
	public Setting SettingOfSettingId
	{
		get
		{
			if(_settingOfSettingId == null)
			{
				_settingOfSettingId = SampleData.Setting.OneWhere(f => f.Id == this.SettingId);
			}
			return _settingOfSettingId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserSettingColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserSettingCollection Where(Func<UserSettingColumns, QueryFilter<UserSettingColumns>> where, OrderBy<UserSettingColumns> orderBy = null)
		{
			return new UserSettingCollection(new Query<UserSettingColumns, UserSetting>(where, orderBy), true);
		}
		
		public static UserSettingCollection Where(WhereDelegate<UserSettingColumns> where, Database db = null)
		{
			return new UserSettingCollection(new Query<UserSettingColumns, UserSetting>(where, db), true);
		}
		   
		public static UserSettingCollection Where(WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy = null, Database db = null)
		{
			return new UserSettingCollection(new Query<UserSettingColumns, UserSetting>(where, orderBy, db), true);
		}

		public static UserSettingCollection Where(QiQuery where, Database db = null)
		{
			return new UserSettingCollection(Select<UserSettingColumns>.From<UserSetting>().Where(where, db));
		}

		public static UserSetting OneWhere(WhereDelegate<UserSettingColumns> where, Database db = null)
		{
			var results = new UserSettingCollection(Select<UserSettingColumns>.From<UserSetting>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserSetting OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserSettingCollection(Select<UserSettingColumns>.From<UserSetting>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserSetting OneOrThrow(UserSettingCollection c)
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

		public static UserSetting FirstOneWhere(WhereDelegate<UserSettingColumns> where, Database db = null)
		{
			var results = new UserSettingCollection(Select<UserSettingColumns>.From<UserSetting>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserSettingCollection Top(int count, WhereDelegate<UserSettingColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserSettingCollection Top(int count, WhereDelegate<UserSettingColumns> where, OrderBy<UserSettingColumns> orderBy, Database database = null)
        {
            UserSettingColumns c = new UserSettingColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<UserSetting>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserSetting>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserSettingColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserSettingCollection>(0);
        }

		public static long Count(WhereDelegate<UserSettingColumns> where, Database database = null)
		{
			UserSettingColumns c = new UserSettingColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UserSetting>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserSetting>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
