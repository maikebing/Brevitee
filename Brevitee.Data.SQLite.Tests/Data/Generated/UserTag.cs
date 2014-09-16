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
    [Brevitee.Data.Table("UserTag", "Test")]
    public partial class UserTag: Dao
    {
        public UserTag():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserTag(DataRow data): base(data)
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


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="UserTag",
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
	
	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
        Table="UserTag",
		Name="TagId", 
		DbDataType="BigInt", 
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
			var colFilter = new UserTagColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserTagCollection Where(Func<UserTagColumns, QueryFilter<UserTagColumns>> where, OrderBy<UserTagColumns> orderBy = null)
		{
			return new UserTagCollection(new Query<UserTagColumns, UserTag>(where, orderBy), true);
		}
		
		public static UserTagCollection Where(WhereDelegate<UserTagColumns> where, Database db = null)
		{
			return new UserTagCollection(new Query<UserTagColumns, UserTag>(where, db), true);
		}
		   
		public static UserTagCollection Where(WhereDelegate<UserTagColumns> where, OrderBy<UserTagColumns> orderBy = null, Database db = null)
		{
			return new UserTagCollection(new Query<UserTagColumns, UserTag>(where, orderBy, db), true);
		}

		public static UserTagCollection Where(QiQuery where, Database db = null)
		{
			return new UserTagCollection(Select<UserTagColumns>.From<UserTag>().Where(where, db));
		}

		public static UserTag OneWhere(WhereDelegate<UserTagColumns> where, Database db = null)
		{
			var results = new UserTagCollection(Select<UserTagColumns>.From<UserTag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserTagCollection(Select<UserTagColumns>.From<UserTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserTag OneOrThrow(UserTagCollection c)
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

		public static UserTag FirstOneWhere(WhereDelegate<UserTagColumns> where, Database db = null)
		{
			var results = new UserTagCollection(Select<UserTagColumns>.From<UserTag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserTagCollection Top(int count, WhereDelegate<UserTagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserTagCollection Top(int count, WhereDelegate<UserTagColumns> where, OrderBy<UserTagColumns> orderBy, Database database = null)
        {
            UserTagColumns c = new UserTagColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<UserTag>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserTag>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserTagColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserTagCollection>(0);
        }

		public static long Count(WhereDelegate<UserTagColumns> where, Database database = null)
		{
			UserTagColumns c = new UserTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UserTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
