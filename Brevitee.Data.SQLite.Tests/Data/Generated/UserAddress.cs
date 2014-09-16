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
    [Brevitee.Data.Table("UserAddress", "Test")]
    public partial class UserAddress: Dao
    {
        public UserAddress():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserAddress(DataRow data): base(data)
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
        Table="UserAddress",
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
	
	// start AddressId -> AddressId
	[Brevitee.Data.ForeignKey(
        Table="UserAddress",
		Name="AddressId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Address",
		Suffix="2")]
	public long? AddressId
	{
		get
		{
			return GetLongValue("AddressId");
		}
		set
		{
			SetValue("AddressId", value);
		}
	}

	Address _addressOfAddressId;
	public Address AddressOfAddressId
	{
		get
		{
			if(_addressOfAddressId == null)
			{
				_addressOfAddressId = SampleData.Address.OneWhere(f => f.Id == this.AddressId);
			}
			return _addressOfAddressId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserAddressColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserAddressCollection Where(Func<UserAddressColumns, QueryFilter<UserAddressColumns>> where, OrderBy<UserAddressColumns> orderBy = null)
		{
			return new UserAddressCollection(new Query<UserAddressColumns, UserAddress>(where, orderBy), true);
		}
		
		public static UserAddressCollection Where(WhereDelegate<UserAddressColumns> where, Database db = null)
		{
			return new UserAddressCollection(new Query<UserAddressColumns, UserAddress>(where, db), true);
		}
		   
		public static UserAddressCollection Where(WhereDelegate<UserAddressColumns> where, OrderBy<UserAddressColumns> orderBy = null, Database db = null)
		{
			return new UserAddressCollection(new Query<UserAddressColumns, UserAddress>(where, orderBy, db), true);
		}

		public static UserAddressCollection Where(QiQuery where, Database db = null)
		{
			return new UserAddressCollection(Select<UserAddressColumns>.From<UserAddress>().Where(where, db));
		}

		public static UserAddress OneWhere(WhereDelegate<UserAddressColumns> where, Database db = null)
		{
			var results = new UserAddressCollection(Select<UserAddressColumns>.From<UserAddress>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserAddress OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserAddressCollection(Select<UserAddressColumns>.From<UserAddress>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserAddress OneOrThrow(UserAddressCollection c)
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

		public static UserAddress FirstOneWhere(WhereDelegate<UserAddressColumns> where, Database db = null)
		{
			var results = new UserAddressCollection(Select<UserAddressColumns>.From<UserAddress>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserAddressCollection Top(int count, WhereDelegate<UserAddressColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserAddressCollection Top(int count, WhereDelegate<UserAddressColumns> where, OrderBy<UserAddressColumns> orderBy, Database database = null)
        {
            UserAddressColumns c = new UserAddressColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<UserAddress>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserAddress>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserAddressColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserAddressCollection>(0);
        }

		public static long Count(WhereDelegate<UserAddressColumns> where, Database database = null)
		{
			UserAddressColumns c = new UserAddressColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<UserAddress>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserAddress>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
