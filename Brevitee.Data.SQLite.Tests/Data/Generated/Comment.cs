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
    [Brevitee.Data.Table("Comment", "Test")]
    public partial class Comment: Dao
    {
        public Comment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Comment(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("ActivityComment_CommentId", new ActivityCommentCollection(new Query<ActivityCommentColumns, ActivityComment>((c) => c.CommentId == this.Id), this, "CommentId"));	
            this.ChildCollections.Add("ActivitySystemComment_CommentId", new ActivitySystemCommentCollection(new Query<ActivitySystemCommentColumns, ActivitySystemComment>((c) => c.CommentId == this.Id), this, "CommentId"));	
            this.ChildCollections.Add("AddressRequestComment_CommentId", new AddressRequestCommentCollection(new Query<AddressRequestCommentColumns, AddressRequestComment>((c) => c.CommentId == this.Id), this, "CommentId"));	
            this.ChildCollections.Add("ReviewComment_CommentId", new ReviewCommentCollection(new Query<ReviewCommentColumns, ReviewComment>((c) => c.CommentId == this.Id), this, "CommentId"));	
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

	// property:Text, columnName:Text	
	[Brevitee.Data.Column(Name="Text", DbDataType="NVarChar", MaxLength="4000", AllowNull=false)]
	public string Text
	{
		get
		{
			return GetStringValue("Text");
		}
		set
		{
			SetValue("Text", value);
		}
	}

	// property:Created, columnName:Created	
	[Brevitee.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime Created
	{
		get
		{
			return GetDateTimeValue("Created");
		}
		set
		{
			SetValue("Created", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Brevitee.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="Comment",
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
	
				
	
	public ActivityCommentCollection ActivityCommentCollectionByCommentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ActivityComment_CommentId"))
			{
				SetChildren();
			}

			var c = (ActivityCommentCollection)this.ChildCollections["ActivityComment_CommentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ActivitySystemCommentCollection ActivitySystemCommentCollectionByCommentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ActivitySystemComment_CommentId"))
			{
				SetChildren();
			}

			var c = (ActivitySystemCommentCollection)this.ChildCollections["ActivitySystemComment_CommentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public AddressRequestCommentCollection AddressRequestCommentCollectionByCommentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("AddressRequestComment_CommentId"))
			{
				SetChildren();
			}

			var c = (AddressRequestCommentCollection)this.ChildCollections["AddressRequestComment_CommentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ReviewCommentCollection ReviewCommentCollectionByCommentId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ReviewComment_CommentId"))
			{
				SetChildren();
			}

			var c = (ReviewCommentCollection)this.ChildCollections["ReviewComment_CommentId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CommentColumns();
			return (colFilter.Id == IdValue);
		}

		public static CommentCollection Where(Func<CommentColumns, QueryFilter<CommentColumns>> where, OrderBy<CommentColumns> orderBy = null)
		{
			return new CommentCollection(new Query<CommentColumns, Comment>(where, orderBy), true);
		}
		
		public static CommentCollection Where(WhereDelegate<CommentColumns> where, Database db = null)
		{
			return new CommentCollection(new Query<CommentColumns, Comment>(where, db), true);
		}
		   
		public static CommentCollection Where(WhereDelegate<CommentColumns> where, OrderBy<CommentColumns> orderBy = null, Database db = null)
		{
			return new CommentCollection(new Query<CommentColumns, Comment>(where, orderBy, db), true);
		}

		public static CommentCollection Where(QiQuery where, Database db = null)
		{
			return new CommentCollection(Select<CommentColumns>.From<Comment>().Where(where, db));
		}

		public static Comment OneWhere(WhereDelegate<CommentColumns> where, Database db = null)
		{
			var results = new CommentCollection(Select<CommentColumns>.From<Comment>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Comment OneWhere(QiQuery where, Database db = null)
		{
			var results = new CommentCollection(Select<CommentColumns>.From<Comment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Comment OneOrThrow(CommentCollection c)
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

		public static Comment FirstOneWhere(WhereDelegate<CommentColumns> where, Database db = null)
		{
			var results = new CommentCollection(Select<CommentColumns>.From<Comment>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static CommentCollection Top(int count, WhereDelegate<CommentColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static CommentCollection Top(int count, WhereDelegate<CommentColumns> where, OrderBy<CommentColumns> orderBy, Database database = null)
        {
            CommentColumns c = new CommentColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Comment>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Comment>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CommentColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<CommentCollection>(0);
        }

		public static long Count(WhereDelegate<CommentColumns> where, Database database = null)
		{
			CommentColumns c = new CommentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Comment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Comment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
