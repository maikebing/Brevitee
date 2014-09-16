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
    [Brevitee.Data.Table("Setting", "Test")]
    public partial class Setting: Dao
    {
        public Setting():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Setting(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("UserSetting_SettingId", new UserSettingCollection(new Query<UserSettingColumns, UserSetting>((c) => c.SettingId == this.Id), this, "SettingId"));	
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
	[Brevitee.Data.Column(Name="Name", DbDataType="NVarChar", MaxLength="16", AllowNull=false)]
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
	[Brevitee.Data.Column(Name="Value", DbDataType="NVarChar", MaxLength="255", AllowNull=true)]
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


				
	
	public UserSettingCollection UserSettingCollectionBySettingId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserSetting_SettingId"))
			{
				SetChildren();
			}

			var c = (UserSettingCollection)this.ChildCollections["UserSetting_SettingId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new SettingColumns();
			return (colFilter.Id == IdValue);
		}

		public static SettingCollection Where(Func<SettingColumns, QueryFilter<SettingColumns>> where, OrderBy<SettingColumns> orderBy = null)
		{
			return new SettingCollection(new Query<SettingColumns, Setting>(where, orderBy), true);
		}
		
		public static SettingCollection Where(WhereDelegate<SettingColumns> where, Database db = null)
		{
			return new SettingCollection(new Query<SettingColumns, Setting>(where, db), true);
		}
		   
		public static SettingCollection Where(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database db = null)
		{
			return new SettingCollection(new Query<SettingColumns, Setting>(where, orderBy, db), true);
		}

		public static SettingCollection Where(QiQuery where, Database db = null)
		{
			return new SettingCollection(Select<SettingColumns>.From<Setting>().Where(where, db));
		}

		public static Setting OneWhere(WhereDelegate<SettingColumns> where, Database db = null)
		{
			var results = new SettingCollection(Select<SettingColumns>.From<Setting>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Setting OneWhere(QiQuery where, Database db = null)
		{
			var results = new SettingCollection(Select<SettingColumns>.From<Setting>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Setting OneOrThrow(SettingCollection c)
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

		public static Setting FirstOneWhere(WhereDelegate<SettingColumns> where, Database db = null)
		{
			var results = new SettingCollection(Select<SettingColumns>.From<Setting>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static SettingCollection Top(int count, WhereDelegate<SettingColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static SettingCollection Top(int count, WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy, Database database = null)
        {
            SettingColumns c = new SettingColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Setting>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Setting>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SettingColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<SettingCollection>(0);
        }

		public static long Count(WhereDelegate<SettingColumns> where, Database database = null)
		{
			SettingColumns c = new SettingColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Setting>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Setting>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
