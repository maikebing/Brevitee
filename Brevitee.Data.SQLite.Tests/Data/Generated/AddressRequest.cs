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
    [Brevitee.Data.Table("AddressRequest", "Test")]
    public partial class AddressRequest: Dao
    {
        public AddressRequest():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public AddressRequest(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("AddressRequestComment_AddressRequestId", new AddressRequestCommentCollection(new Query<AddressRequestCommentColumns, AddressRequestComment>((c) => c.AddressRequestId == this.Id), this, "AddressRequestId"));	
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

	// property:Approved, columnName:Approved	
	[Brevitee.Data.Column(Name="Approved", DbDataType="Bit", MaxLength="1", AllowNull=false)]
	public bool? Approved
	{
		get
		{
			return GetBooleanValue("Approved");
		}
		set
		{
			SetValue("Approved", value);
		}
	}


	// start RequesterId -> RequesterId
	[Brevitee.Data.ForeignKey(
        Table="AddressRequest",
		Name="RequesterId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? RequesterId
	{
		get
		{
			return GetLongValue("RequesterId");
		}
		set
		{
			SetValue("RequesterId", value);
		}
	}

	User _userOfRequesterId;
	public User UserOfRequesterId
	{
		get
		{
			if(_userOfRequesterId == null)
			{
				_userOfRequesterId = SampleData.User.OneWhere(f => f.Id == this.RequesterId);
			}
			return _userOfRequesterId;
		}
	}
	
	// start RequesteeId -> RequesteeId
	[Brevitee.Data.ForeignKey(
        Table="AddressRequest",
		Name="RequesteeId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="2")]
	public long? RequesteeId
	{
		get
		{
			return GetLongValue("RequesteeId");
		}
		set
		{
			SetValue("RequesteeId", value);
		}
	}

	User _userOfRequesteeId;
	public User UserOfRequesteeId
	{
		get
		{
			if(_userOfRequesteeId == null)
			{
				_userOfRequesteeId = SampleData.User.OneWhere(f => f.Id == this.RequesteeId);
			}
			return _userOfRequesteeId;
		}
	}
	
				
	
	public AddressRequestCommentCollection AddressRequestCommentCollectionByAddressRequestId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("AddressRequestComment_AddressRequestId"))
			{
				SetChildren();
			}

			var c = (AddressRequestCommentCollection)this.ChildCollections["AddressRequestComment_AddressRequestId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new AddressRequestColumns();
			return (colFilter.Id == IdValue);
		}

		public static AddressRequestCollection Where(Func<AddressRequestColumns, QueryFilter<AddressRequestColumns>> where, OrderBy<AddressRequestColumns> orderBy = null)
		{
			return new AddressRequestCollection(new Query<AddressRequestColumns, AddressRequest>(where, orderBy), true);
		}
		
		public static AddressRequestCollection Where(WhereDelegate<AddressRequestColumns> where, Database db = null)
		{
			return new AddressRequestCollection(new Query<AddressRequestColumns, AddressRequest>(where, db), true);
		}
		   
		public static AddressRequestCollection Where(WhereDelegate<AddressRequestColumns> where, OrderBy<AddressRequestColumns> orderBy = null, Database db = null)
		{
			return new AddressRequestCollection(new Query<AddressRequestColumns, AddressRequest>(where, orderBy, db), true);
		}

		public static AddressRequestCollection Where(QiQuery where, Database db = null)
		{
			return new AddressRequestCollection(Select<AddressRequestColumns>.From<AddressRequest>().Where(where, db));
		}

		public static AddressRequest OneWhere(WhereDelegate<AddressRequestColumns> where, Database db = null)
		{
			var results = new AddressRequestCollection(Select<AddressRequestColumns>.From<AddressRequest>().Where(where, db));
			return OneOrThrow(results);
		}

		public static AddressRequest OneWhere(QiQuery where, Database db = null)
		{
			var results = new AddressRequestCollection(Select<AddressRequestColumns>.From<AddressRequest>().Where(where, db));
			return OneOrThrow(results);
		}

		private static AddressRequest OneOrThrow(AddressRequestCollection c)
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

		public static AddressRequest FirstOneWhere(WhereDelegate<AddressRequestColumns> where, Database db = null)
		{
			var results = new AddressRequestCollection(Select<AddressRequestColumns>.From<AddressRequest>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static AddressRequestCollection Top(int count, WhereDelegate<AddressRequestColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static AddressRequestCollection Top(int count, WhereDelegate<AddressRequestColumns> where, OrderBy<AddressRequestColumns> orderBy, Database database = null)
        {
            AddressRequestColumns c = new AddressRequestColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<AddressRequest>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<AddressRequest>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AddressRequestColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<AddressRequestCollection>(0);
        }

		public static long Count(WhereDelegate<AddressRequestColumns> where, Database database = null)
		{
			AddressRequestColumns c = new AddressRequestColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<AddressRequest>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<AddressRequest>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
