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
    [Brevitee.Data.Table("Invitation", "Test")]
    public partial class Invitation: Dao
    {
        public Invitation():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Invitation(DataRow data): base(data)
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

	// property:LastModifiedBy, columnName:LastModifiedBy	
	[Brevitee.Data.Column(Name="LastModifiedBy", DbDataType="VarChar", MaxLength="50", AllowNull=false)]
	public string LastModifiedBy
	{
		get
		{
			return GetStringValue("LastModifiedBy");
		}
		set
		{
			SetValue("LastModifiedBy", value);
		}
	}

	// property:LastModifiedDate, columnName:LastModifiedDate	
	[Brevitee.Data.Column(Name="LastModifiedDate", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime LastModifiedDate
	{
		get
		{
			return GetDateTimeValue("LastModifiedDate");
		}
		set
		{
			SetValue("LastModifiedDate", value);
		}
	}

	// property:Sent, columnName:Sent	
	[Brevitee.Data.Column(Name="Sent", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime Sent
	{
		get
		{
			return GetDateTimeValue("Sent");
		}
		set
		{
			SetValue("Sent", value);
		}
	}


	// start InviterId -> InviterId
	[Brevitee.Data.ForeignKey(
        Table="Invitation",
		Name="InviterId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? InviterId
	{
		get
		{
			return GetLongValue("InviterId");
		}
		set
		{
			SetValue("InviterId", value);
		}
	}

	User _userOfInviterId;
	public User UserOfInviterId
	{
		get
		{
			if(_userOfInviterId == null)
			{
				_userOfInviterId = SampleData.User.OneWhere(f => f.Id == this.InviterId);
			}
			return _userOfInviterId;
		}
	}
	
	// start InviteeId -> InviteeId
	[Brevitee.Data.ForeignKey(
        Table="Invitation",
		Name="InviteeId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="2")]
	public long? InviteeId
	{
		get
		{
			return GetLongValue("InviteeId");
		}
		set
		{
			SetValue("InviteeId", value);
		}
	}

	User _userOfInviteeId;
	public User UserOfInviteeId
	{
		get
		{
			if(_userOfInviteeId == null)
			{
				_userOfInviteeId = SampleData.User.OneWhere(f => f.Id == this.InviteeId);
			}
			return _userOfInviteeId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new InvitationColumns();
			return (colFilter.Id == IdValue);
		}

		public static InvitationCollection Where(Func<InvitationColumns, QueryFilter<InvitationColumns>> where, OrderBy<InvitationColumns> orderBy = null)
		{
			return new InvitationCollection(new Query<InvitationColumns, Invitation>(where, orderBy), true);
		}
		
		public static InvitationCollection Where(WhereDelegate<InvitationColumns> where, Database db = null)
		{
			return new InvitationCollection(new Query<InvitationColumns, Invitation>(where, db), true);
		}
		   
		public static InvitationCollection Where(WhereDelegate<InvitationColumns> where, OrderBy<InvitationColumns> orderBy = null, Database db = null)
		{
			return new InvitationCollection(new Query<InvitationColumns, Invitation>(where, orderBy, db), true);
		}

		public static InvitationCollection Where(QiQuery where, Database db = null)
		{
			return new InvitationCollection(Select<InvitationColumns>.From<Invitation>().Where(where, db));
		}

		public static Invitation OneWhere(WhereDelegate<InvitationColumns> where, Database db = null)
		{
			var results = new InvitationCollection(Select<InvitationColumns>.From<Invitation>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Invitation OneWhere(QiQuery where, Database db = null)
		{
			var results = new InvitationCollection(Select<InvitationColumns>.From<Invitation>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Invitation OneOrThrow(InvitationCollection c)
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

		public static Invitation FirstOneWhere(WhereDelegate<InvitationColumns> where, Database db = null)
		{
			var results = new InvitationCollection(Select<InvitationColumns>.From<Invitation>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static InvitationCollection Top(int count, WhereDelegate<InvitationColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static InvitationCollection Top(int count, WhereDelegate<InvitationColumns> where, OrderBy<InvitationColumns> orderBy, Database database = null)
        {
            InvitationColumns c = new InvitationColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Invitation>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Invitation>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<InvitationColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<InvitationCollection>(0);
        }

		public static long Count(WhereDelegate<InvitationColumns> where, Database database = null)
		{
			InvitationColumns c = new InvitationColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Invitation>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Invitation>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
