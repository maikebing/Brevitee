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
    [Brevitee.Data.Table("Message", "Test")]
    public partial class Message: Dao
    {
        public Message():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Message(DataRow data): base(data)
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

	// property:To, columnName:To	
	[Brevitee.Data.Column(Name="To", DbDataType="NVarChar", MaxLength="4000", AllowNull=true)]
	public string To
	{
		get
		{
			return GetStringValue("To");
		}
		set
		{
			SetValue("To", value);
		}
	}

	// property:Cc, columnName:Cc	
	[Brevitee.Data.Column(Name="Cc", DbDataType="NVarChar", MaxLength="4000", AllowNull=true)]
	public string Cc
	{
		get
		{
			return GetStringValue("Cc");
		}
		set
		{
			SetValue("Cc", value);
		}
	}

	// property:Bcc, columnName:Bcc	
	[Brevitee.Data.Column(Name="Bcc", DbDataType="NVarChar", MaxLength="4000", AllowNull=true)]
	public string Bcc
	{
		get
		{
			return GetStringValue("Bcc");
		}
		set
		{
			SetValue("Bcc", value);
		}
	}

	// property:Subject, columnName:Subject	
	[Brevitee.Data.Column(Name="Subject", DbDataType="NVarChar", MaxLength="255", AllowNull=true)]
	public string Subject
	{
		get
		{
			return GetStringValue("Subject");
		}
		set
		{
			SetValue("Subject", value);
		}
	}

	// property:Body, columnName:Body	
	[Brevitee.Data.Column(Name="Body", DbDataType="NVarChar", MaxLength="4000", AllowNull=true)]
	public string Body
	{
		get
		{
			return GetStringValue("Body");
		}
		set
		{
			SetValue("Body", value);
		}
	}

	// property:Sent, columnName:Sent	
	[Brevitee.Data.Column(Name="Sent", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
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


	// start FromUserId -> FromUserId
	[Brevitee.Data.ForeignKey(
        Table="Message",
		Name="FromUserId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? FromUserId
	{
		get
		{
			return GetLongValue("FromUserId");
		}
		set
		{
			SetValue("FromUserId", value);
		}
	}

	User _userOfFromUserId;
	public User UserOfFromUserId
	{
		get
		{
			if(_userOfFromUserId == null)
			{
				_userOfFromUserId = SampleData.User.OneWhere(f => f.Id == this.FromUserId);
			}
			return _userOfFromUserId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new MessageColumns();
			return (colFilter.Id == IdValue);
		}

		public static MessageCollection Where(Func<MessageColumns, QueryFilter<MessageColumns>> where, OrderBy<MessageColumns> orderBy = null)
		{
			return new MessageCollection(new Query<MessageColumns, Message>(where, orderBy), true);
		}
		
		public static MessageCollection Where(WhereDelegate<MessageColumns> where, Database db = null)
		{
			return new MessageCollection(new Query<MessageColumns, Message>(where, db), true);
		}
		   
		public static MessageCollection Where(WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy = null, Database db = null)
		{
			return new MessageCollection(new Query<MessageColumns, Message>(where, orderBy, db), true);
		}

		public static MessageCollection Where(QiQuery where, Database db = null)
		{
			return new MessageCollection(Select<MessageColumns>.From<Message>().Where(where, db));
		}

		public static Message OneWhere(WhereDelegate<MessageColumns> where, Database db = null)
		{
			var results = new MessageCollection(Select<MessageColumns>.From<Message>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Message OneWhere(QiQuery where, Database db = null)
		{
			var results = new MessageCollection(Select<MessageColumns>.From<Message>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Message OneOrThrow(MessageCollection c)
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

		public static Message FirstOneWhere(WhereDelegate<MessageColumns> where, Database db = null)
		{
			var results = new MessageCollection(Select<MessageColumns>.From<Message>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static MessageCollection Top(int count, WhereDelegate<MessageColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static MessageCollection Top(int count, WhereDelegate<MessageColumns> where, OrderBy<MessageColumns> orderBy, Database database = null)
        {
            MessageColumns c = new MessageColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Message>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Message>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<MessageColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<MessageCollection>(0);
        }

		public static long Count(WhereDelegate<MessageColumns> where, Database database = null)
		{
			MessageColumns c = new MessageColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Message>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Message>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
