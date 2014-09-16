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
    [Brevitee.Data.Table("Role", "Test")]
    public partial class Role: Dao
    {
        public Role():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Role(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("UserRole_RoleId", new UserRoleCollection(new Query<UserRoleColumns, UserRole>((c) => c.RoleId == this.Id), this, "RoleId"));	
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


				
	
	public UserRoleCollection UserRoleCollectionByRoleId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserRole_RoleId"))
			{
				SetChildren();
			}

			var c = (UserRoleCollection)this.ChildCollections["UserRole_RoleId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new RoleColumns();
			return (colFilter.Id == IdValue);
		}

		public static RoleCollection Where(Func<RoleColumns, QueryFilter<RoleColumns>> where, OrderBy<RoleColumns> orderBy = null)
		{
			return new RoleCollection(new Query<RoleColumns, Role>(where, orderBy), true);
		}
		
		public static RoleCollection Where(WhereDelegate<RoleColumns> where, Database db = null)
		{
			return new RoleCollection(new Query<RoleColumns, Role>(where, db), true);
		}
		   
		public static RoleCollection Where(WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy = null, Database db = null)
		{
			return new RoleCollection(new Query<RoleColumns, Role>(where, orderBy, db), true);
		}

		public static RoleCollection Where(QiQuery where, Database db = null)
		{
			return new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
		}

		public static Role OneWhere(WhereDelegate<RoleColumns> where, Database db = null)
		{
			var results = new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Role OneWhere(QiQuery where, Database db = null)
		{
			var results = new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Role OneOrThrow(RoleCollection c)
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

		public static Role FirstOneWhere(WhereDelegate<RoleColumns> where, Database db = null)
		{
			var results = new RoleCollection(Select<RoleColumns>.From<Role>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static RoleCollection Top(int count, WhereDelegate<RoleColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static RoleCollection Top(int count, WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy, Database database = null)
        {
            RoleColumns c = new RoleColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Role>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Role>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<RoleColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<RoleCollection>(0);
        }

		public static long Count(WhereDelegate<RoleColumns> where, Database database = null)
		{
			RoleColumns c = new RoleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Role>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Role>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
