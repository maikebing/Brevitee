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
    [Brevitee.Data.Table("UserProperty", "Test")]
    public partial class UserProperty: Dao
    {
        public UserProperty():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserProperty(DataRow data): base(data)
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


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="UserProperty",
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
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserPropertyColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserPropertyCollection Where(Func<UserPropertyColumns, QueryFilter<UserPropertyColumns>> where, OrderBy<UserPropertyColumns> orderBy = null)
		{
			return new UserPropertyCollection(new Query<UserPropertyColumns, UserProperty>(where, orderBy), true);
		}
		
		public static UserPropertyCollection Where(WhereDelegate<UserPropertyColumns> where, Database db = null)
		{
			return new UserPropertyCollection(new Query<UserPropertyColumns, UserProperty>(where, db), true);
		}
		   
		public static UserPropertyCollection Where(WhereDelegate<UserPropertyColumns> where, OrderBy<UserPropertyColumns> orderBy = null, Database db = null)
		{
			return new UserPropertyCollection(new Query<UserPropertyColumns, UserProperty>(where, orderBy, db), true);
		}

		public static UserPropertyCollection Where(QiQuery where, Database db = null)
		{
			return new UserPropertyCollection(Select<UserPropertyColumns>.From<UserProperty>().Where(where, db));
		}

		public static UserProperty OneWhere(WhereDelegate<UserPropertyColumns> where, Database db = null)
		{
			var results = new UserPropertyCollection(Select<UserPropertyColumns>.From<UserProperty>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserProperty OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserPropertyCollection(Select<UserPropertyColumns>.From<UserProperty>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserProperty OneOrThrow(UserPropertyCollection c)
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

		public static UserProperty FirstOneWhere(WhereDelegate<UserPropertyColumns> where, Database db = null)
		{
			var results = new UserPropertyCollection(Select<UserPropertyColumns>.From<UserProperty>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserPropertyCollection Top(int count, WhereDelegate<UserPropertyColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserPropertyCollection Top(int count, WhereDelegate<UserPropertyColumns> where, OrderBy<UserPropertyColumns> orderBy, Database database = null)
        {
            UserPropertyColumns c = new UserPropertyColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<UserProperty>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserProperty>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserPropertyColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserPropertyCollection>(0);
        }

		public static long Count(WhereDelegate<UserPropertyColumns> where, Database database = null)
		{
			UserPropertyColumns c = new UserPropertyColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UserProperty>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserProperty>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
