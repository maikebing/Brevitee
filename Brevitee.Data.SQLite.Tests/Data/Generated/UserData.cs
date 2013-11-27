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
    [Brevitee.Data.Table("UserData", "Test")]
    public partial class UserData: Dao
    {
        public UserData():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public UserData(DataRow data): base(data)
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
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
	[Brevitee.Data.Column(Name="Value", ExtractedType="VarBinary", MaxLength="4000", AllowNull=false)]
	public byte[] Value
	{
		get
		{
			return GetByteValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}

	// property:DataType, columnName:DataType	
	[Brevitee.Data.Column(Name="DataType", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string DataType
	{
		get
		{
			return GetStringValue("DataType");
		}
		set
		{
			SetValue("DataType", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="UserData",
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
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserDataColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserDataCollection Where(Func<UserDataColumns, QueryFilter<UserDataColumns>> where, OrderBy<UserDataColumns> orderBy = null)
		{
			return new UserDataCollection(new Query<UserDataColumns, UserData>(where, orderBy), true);
		}
		
		public static UserDataCollection Where(WhereDelegate<UserDataColumns> where, Database db = null)
		{
			return new UserDataCollection(new Query<UserDataColumns, UserData>(where, db), true);
		}
		   
		public static UserDataCollection Where(WhereDelegate<UserDataColumns> where, OrderBy<UserDataColumns> orderBy = null, Database db = null)
		{
			return new UserDataCollection(new Query<UserDataColumns, UserData>(where, orderBy, db), true);
		}

		public static UserDataCollection Where(QiQuery where, Database db = null)
		{
			return new UserDataCollection(Select<UserDataColumns>.From<UserData>().Where(where, db));
		}

		public static UserData OneWhere(WhereDelegate<UserDataColumns> where, Database db = null)
		{
			var results = new UserDataCollection(Select<UserDataColumns>.From<UserData>().Where(where, db));
			return OneOrThrow(results);
		}

		public static UserData OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserDataCollection(Select<UserDataColumns>.From<UserData>().Where(where, db));
			return OneOrThrow(results);
		}

		private static UserData OneOrThrow(UserDataCollection c)
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

		public static UserData FirstOneWhere(WhereDelegate<UserDataColumns> where, Database db = null)
		{
			var results = new UserDataCollection(Select<UserDataColumns>.From<UserData>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserDataCollection Top(int count, WhereDelegate<UserDataColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserDataCollection Top(int count, WhereDelegate<UserDataColumns> where, OrderBy<UserDataColumns> orderBy, Database database = null)
        {
            UserDataColumns c = new UserDataColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<UserData>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<UserData>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserDataColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserDataCollection>(0);
        }

		public static long Count(WhereDelegate<UserDataColumns> where, Database database = null)
		{
			UserDataColumns c = new UserDataColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<UserData>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<UserData>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
