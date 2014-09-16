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
    [Brevitee.Data.Table("Address", "Test")]
    public partial class Address: Dao
    {
        public Address():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Address(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("UserAddress_AddressId", new UserAddressCollection(new Query<UserAddressColumns, UserAddress>((c) => c.AddressId == this.Id), this, "AddressId"));	
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

	// property:Line1, columnName:Line1	
	[Brevitee.Data.Column(Name="Line1", DbDataType="VarChar", MaxLength="255", AllowNull=false)]
	public string Line1
	{
		get
		{
			return GetStringValue("Line1");
		}
		set
		{
			SetValue("Line1", value);
		}
	}

	// property:Line2, columnName:Line2	
	[Brevitee.Data.Column(Name="Line2", DbDataType="VarChar", MaxLength="255", AllowNull=true)]
	public string Line2
	{
		get
		{
			return GetStringValue("Line2");
		}
		set
		{
			SetValue("Line2", value);
		}
	}

	// property:City, columnName:City	
	[Brevitee.Data.Column(Name="City", DbDataType="VarChar", MaxLength="255", AllowNull=true)]
	public string City
	{
		get
		{
			return GetStringValue("City");
		}
		set
		{
			SetValue("City", value);
		}
	}

	// property:StateOrProvince, columnName:StateOrProvince	
	[Brevitee.Data.Column(Name="StateOrProvince", DbDataType="VarChar", MaxLength="255", AllowNull=true)]
	public string StateOrProvince
	{
		get
		{
			return GetStringValue("StateOrProvince");
		}
		set
		{
			SetValue("StateOrProvince", value);
		}
	}

	// property:PostalCode, columnName:PostalCode	
	[Brevitee.Data.Column(Name="PostalCode", DbDataType="VarChar", MaxLength="100", AllowNull=true)]
	public string PostalCode
	{
		get
		{
			return GetStringValue("PostalCode");
		}
		set
		{
			SetValue("PostalCode", value);
		}
	}


				
	
	public UserAddressCollection UserAddressCollectionByAddressId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserAddress_AddressId"))
			{
				SetChildren();
			}

			var c = (UserAddressCollection)this.ChildCollections["UserAddress_AddressId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new AddressColumns();
			return (colFilter.Id == IdValue);
		}

		public static AddressCollection Where(Func<AddressColumns, QueryFilter<AddressColumns>> where, OrderBy<AddressColumns> orderBy = null)
		{
			return new AddressCollection(new Query<AddressColumns, Address>(where, orderBy), true);
		}
		
		public static AddressCollection Where(WhereDelegate<AddressColumns> where, Database db = null)
		{
			return new AddressCollection(new Query<AddressColumns, Address>(where, db), true);
		}
		   
		public static AddressCollection Where(WhereDelegate<AddressColumns> where, OrderBy<AddressColumns> orderBy = null, Database db = null)
		{
			return new AddressCollection(new Query<AddressColumns, Address>(where, orderBy, db), true);
		}

		public static AddressCollection Where(QiQuery where, Database db = null)
		{
			return new AddressCollection(Select<AddressColumns>.From<Address>().Where(where, db));
		}

		public static Address OneWhere(WhereDelegate<AddressColumns> where, Database db = null)
		{
			var results = new AddressCollection(Select<AddressColumns>.From<Address>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Address OneWhere(QiQuery where, Database db = null)
		{
			var results = new AddressCollection(Select<AddressColumns>.From<Address>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Address OneOrThrow(AddressCollection c)
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

		public static Address FirstOneWhere(WhereDelegate<AddressColumns> where, Database db = null)
		{
			var results = new AddressCollection(Select<AddressColumns>.From<Address>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static AddressCollection Top(int count, WhereDelegate<AddressColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static AddressCollection Top(int count, WhereDelegate<AddressColumns> where, OrderBy<AddressColumns> orderBy, Database database = null)
        {
            AddressColumns c = new AddressColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Address>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Address>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AddressColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<AddressCollection>(0);
        }

		public static long Count(WhereDelegate<AddressColumns> where, Database database = null)
		{
			AddressColumns c = new AddressColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Address>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Address>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
