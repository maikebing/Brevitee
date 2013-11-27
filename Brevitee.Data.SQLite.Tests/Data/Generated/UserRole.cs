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
    [Brevitee.Data.Table("UserRole", "Test")]
    public partial class UserRole: Dao
    {
        public UserRole():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserRole(DataRow data): base(data)
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
        Table="UserRole",
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
	
	// start RoleId -> RoleId
	[Brevitee.Data.ForeignKey(
        Table="UserRole",
		Name="RoleId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Role",
		Suffix="2")]
	public long? RoleId
	{
		get
		{
			return GetLongValue("RoleId");
		}
		set
		{
			SetValue("RoleId", value);
		}
	}

	Role _roleOfRoleId;
	public Role RoleOfRoleId
	{
		get
		{
			if(_roleOfRoleId == null)
			{
				_roleOfRoleId = SampleData.Role.OneWhere(f => f.Id == this.RoleId);
			}
			return _roleOfRoleId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserRoleColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserRoleCollection Where(Func<UserRoleColumns, QueryFilter<UserRoleColumns>> where, OrderBy<UserRoleColumns> orderBy = null)
		{
			return new UserRoleCollection(new Query<UserRoleColumns, UserRole>(where, orderBy), true);
		}
		
		public static UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			return new UserRoleCollection(new Query<UserRoleColumns, UserRole>(where, db), true);
		}
		   
		public static UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null)
		{
			return new UserRoleCollection(new Query<UserRoleColumns, UserRole>(where, orderBy, db), true);
		}

		public static UserRoleCollection Where(QiQuery where, Database db = null)
		{
			return new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
		}

		public static UserRole OneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			var results = new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserRole OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserRole OneOrThrow(UserRoleCollection c)
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

		public static UserRole FirstOneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			var results = new UserRoleCollection(Select<UserRoleColumns>.From<UserRole>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy, Database database = null)
        {
            UserRoleColumns c = new UserRoleColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<UserRole>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserRole>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserRoleColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserRoleCollection>(0);
        }

		public static long Count(WhereDelegate<UserRoleColumns> where, Database database = null)
		{
			UserRoleColumns c = new UserRoleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UserRole>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserRole>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
