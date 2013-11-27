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
    [Brevitee.Data.Table("User", "Test")]
    public partial class User: Dao
    {
        public User():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public User(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Activity_UserId", new ActivityCollection(new Query<ActivityColumns, Activity>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("AddressRequest_RequesterId", new AddressRequestCollection(new Query<AddressRequestColumns, AddressRequest>((c) => c.RequesterId == this.Id), this, "RequesterId"));	
            this.ChildCollections.Add("AddressRequest_RequesteeId", new AddressRequestCollection(new Query<AddressRequestColumns, AddressRequest>((c) => c.RequesteeId == this.Id), this, "RequesteeId"));	
            this.ChildCollections.Add("Comment_UserId", new CommentCollection(new Query<CommentColumns, Comment>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Have_UserId", new HaveCollection(new Query<HaveColumns, Have>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Invitation_InviterId", new InvitationCollection(new Query<InvitationColumns, Invitation>((c) => c.InviterId == this.Id), this, "InviterId"));	
            this.ChildCollections.Add("Invitation_InviteeId", new InvitationCollection(new Query<InvitationColumns, Invitation>((c) => c.InviteeId == this.Id), this, "InviteeId"));	
            this.ChildCollections.Add("ItemReview_UserId", new ItemReviewCollection(new Query<ItemReviewColumns, ItemReview>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("LastLogin_UserId", new LastLoginCollection(new Query<LastLoginColumns, LastLogin>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Message_FromUserId", new MessageCollection(new Query<MessageColumns, Message>((c) => c.FromUserId == this.Id), this, "FromUserId"));	
            this.ChildCollections.Add("Need_UserId", new NeedCollection(new Query<NeedColumns, Need>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Password_UserId", new PasswordCollection(new Query<PasswordColumns, Password>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("PinboardPosting_UserId", new PinboardPostingCollection(new Query<PinboardPostingColumns, PinboardPosting>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("ReviewComment_UserId", new ReviewCommentCollection(new Query<ReviewCommentColumns, ReviewComment>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("RockPaperScissors_UserIdOne", new RockPaperScissorsCollection(new Query<RockPaperScissorsColumns, RockPaperScissors>((c) => c.UserIdOne == this.Id), this, "UserIdOne"));	
            this.ChildCollections.Add("RockPaperScissors_UserIdTwo", new RockPaperScissorsCollection(new Query<RockPaperScissorsColumns, RockPaperScissors>((c) => c.UserIdTwo == this.Id), this, "UserIdTwo"));	
            this.ChildCollections.Add("UserAddress_UserId", new UserAddressCollection(new Query<UserAddressColumns, UserAddress>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("UserData_UserId", new UserDataCollection(new Query<UserDataColumns, UserData>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("UserItemReview_ReviewerId", new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>((c) => c.ReviewerId == this.Id), this, "ReviewerId"));	
            this.ChildCollections.Add("UserItemReview_RevieweeId", new UserItemReviewCollection(new Query<UserItemReviewColumns, UserItemReview>((c) => c.RevieweeId == this.Id), this, "RevieweeId"));	
            this.ChildCollections.Add("UserProperty_UserId", new UserPropertyCollection(new Query<UserPropertyColumns, UserProperty>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("UserReview_ReviewerId", new UserReviewCollection(new Query<UserReviewColumns, UserReview>((c) => c.ReviewerId == this.Id), this, "ReviewerId"));	
            this.ChildCollections.Add("UserReview_RevieweeId", new UserReviewCollection(new Query<UserReviewColumns, UserReview>((c) => c.RevieweeId == this.Id), this, "RevieweeId"));	
            this.ChildCollections.Add("UserRole_UserId", new UserRoleCollection(new Query<UserRoleColumns, UserRole>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("UserSetting_UserId", new UserSettingCollection(new Query<UserSettingColumns, UserSetting>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("UserTag_UserId", new UserTagCollection(new Query<UserTagColumns, UserTag>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("Want_UserId", new WantCollection(new Query<WantColumns, Want>((c) => c.UserId == this.Id), this, "UserId"));	
            this.ChildCollections.Add("WishList_UserId", new WishListCollection(new Query<WishListColumns, WishList>((c) => c.UserId == this.Id), this, "UserId"));	
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

	// property:FirstName, columnName:FirstName	
	[Brevitee.Data.Column(Name="FirstName", ExtractedType="VarChar", MaxLength="30", AllowNull=true)]
	public string FirstName
	{
		get
		{
			return GetStringValue("FirstName");
		}
		set
		{
			SetValue("FirstName", value);
		}
	}

	// property:LastName, columnName:LastName	
	[Brevitee.Data.Column(Name="LastName", ExtractedType="VarChar", MaxLength="50", AllowNull=true)]
	public string LastName
	{
		get
		{
			return GetStringValue("LastName");
		}
		set
		{
			SetValue("LastName", value);
		}
	}

	// property:UserName, columnName:UserName	
	[Brevitee.Data.Column(Name="UserName", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string UserName
	{
		get
		{
			return GetStringValue("UserName");
		}
		set
		{
			SetValue("UserName", value);
		}
	}

	// property:Email, columnName:Email	
	[Brevitee.Data.Column(Name="Email", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Email
	{
		get
		{
			return GetStringValue("Email");
		}
		set
		{
			SetValue("Email", value);
		}
	}

	// property:AuthSource, columnName:AuthSource	
	[Brevitee.Data.Column(Name="AuthSource", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string AuthSource
	{
		get
		{
			return GetStringValue("AuthSource");
		}
		set
		{
			SetValue("AuthSource", value);
		}
	}

	// property:SourceId, columnName:SourceId	
	[Brevitee.Data.Column(Name="SourceId", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string SourceId
	{
		get
		{
			return GetStringValue("SourceId");
		}
		set
		{
			SetValue("SourceId", value);
		}
	}


				
	
	public ActivityCollection ActivityCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Activity_UserId"))
			{
				SetChildren();
			}

			var c = (ActivityCollection)this.ChildCollections["Activity_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public AddressRequestCollection AddressRequestCollectionByRequesterId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("AddressRequest_RequesterId"))
			{
				SetChildren();
			}

			var c = (AddressRequestCollection)this.ChildCollections["AddressRequest_RequesterId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public AddressRequestCollection AddressRequestCollectionByRequesteeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("AddressRequest_RequesteeId"))
			{
				SetChildren();
			}

			var c = (AddressRequestCollection)this.ChildCollections["AddressRequest_RequesteeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public CommentCollection CommentCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Comment_UserId"))
			{
				SetChildren();
			}

			var c = (CommentCollection)this.ChildCollections["Comment_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public HaveCollection HaveCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Have_UserId"))
			{
				SetChildren();
			}

			var c = (HaveCollection)this.ChildCollections["Have_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public InvitationCollection InvitationCollectionByInviterId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Invitation_InviterId"))
			{
				SetChildren();
			}

			var c = (InvitationCollection)this.ChildCollections["Invitation_InviterId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public InvitationCollection InvitationCollectionByInviteeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Invitation_InviteeId"))
			{
				SetChildren();
			}

			var c = (InvitationCollection)this.ChildCollections["Invitation_InviteeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ItemReviewCollection ItemReviewCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemReview_UserId"))
			{
				SetChildren();
			}

			var c = (ItemReviewCollection)this.ChildCollections["ItemReview_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public LastLoginCollection LastLoginCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("LastLogin_UserId"))
			{
				SetChildren();
			}

			var c = (LastLoginCollection)this.ChildCollections["LastLogin_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public MessageCollection MessageCollectionByFromUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Message_FromUserId"))
			{
				SetChildren();
			}

			var c = (MessageCollection)this.ChildCollections["Message_FromUserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public NeedCollection NeedCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Need_UserId"))
			{
				SetChildren();
			}

			var c = (NeedCollection)this.ChildCollections["Need_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public PasswordCollection PasswordCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Password_UserId"))
			{
				SetChildren();
			}

			var c = (PasswordCollection)this.ChildCollections["Password_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public PinboardPostingCollection PinboardPostingCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("PinboardPosting_UserId"))
			{
				SetChildren();
			}

			var c = (PinboardPostingCollection)this.ChildCollections["PinboardPosting_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ReviewCommentCollection ReviewCommentCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ReviewComment_UserId"))
			{
				SetChildren();
			}

			var c = (ReviewCommentCollection)this.ChildCollections["ReviewComment_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public RockPaperScissorsCollection RockPaperScissorsCollectionByUserIdOne
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RockPaperScissors_UserIdOne"))
			{
				SetChildren();
			}

			var c = (RockPaperScissorsCollection)this.ChildCollections["RockPaperScissors_UserIdOne"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public RockPaperScissorsCollection RockPaperScissorsCollectionByUserIdTwo
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("RockPaperScissors_UserIdTwo"))
			{
				SetChildren();
			}

			var c = (RockPaperScissorsCollection)this.ChildCollections["RockPaperScissors_UserIdTwo"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserAddressCollection UserAddressCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserAddress_UserId"))
			{
				SetChildren();
			}

			var c = (UserAddressCollection)this.ChildCollections["UserAddress_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserDataCollection UserDataCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserData_UserId"))
			{
				SetChildren();
			}

			var c = (UserDataCollection)this.ChildCollections["UserData_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserItemReviewCollection UserItemReviewCollectionByReviewerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserItemReview_ReviewerId"))
			{
				SetChildren();
			}

			var c = (UserItemReviewCollection)this.ChildCollections["UserItemReview_ReviewerId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserItemReviewCollection UserItemReviewCollectionByRevieweeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserItemReview_RevieweeId"))
			{
				SetChildren();
			}

			var c = (UserItemReviewCollection)this.ChildCollections["UserItemReview_RevieweeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserPropertyCollection UserPropertyCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserProperty_UserId"))
			{
				SetChildren();
			}

			var c = (UserPropertyCollection)this.ChildCollections["UserProperty_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserReviewCollection UserReviewCollectionByReviewerId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserReview_ReviewerId"))
			{
				SetChildren();
			}

			var c = (UserReviewCollection)this.ChildCollections["UserReview_ReviewerId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserReviewCollection UserReviewCollectionByRevieweeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserReview_RevieweeId"))
			{
				SetChildren();
			}

			var c = (UserReviewCollection)this.ChildCollections["UserReview_RevieweeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserRoleCollection UserRoleCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserRole_UserId"))
			{
				SetChildren();
			}

			var c = (UserRoleCollection)this.ChildCollections["UserRole_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserSettingCollection UserSettingCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserSetting_UserId"))
			{
				SetChildren();
			}

			var c = (UserSettingCollection)this.ChildCollections["UserSetting_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserTagCollection UserTagCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserTag_UserId"))
			{
				SetChildren();
			}

			var c = (UserTagCollection)this.ChildCollections["UserTag_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public WantCollection WantCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Want_UserId"))
			{
				SetChildren();
			}

			var c = (WantCollection)this.ChildCollections["Want_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public WishListCollection WishListCollectionByUserId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("WishList_UserId"))
			{
				SetChildren();
			}

			var c = (WishListCollection)this.ChildCollections["WishList_UserId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UserColumns();
			return (colFilter.Id == IdValue);
		}

		public static UserCollection Where(Func<UserColumns, QueryFilter<UserColumns>> where, OrderBy<UserColumns> orderBy = null)
		{
			return new UserCollection(new Query<UserColumns, User>(where, orderBy), true);
		}
		
		public static UserCollection Where(WhereDelegate<UserColumns> where, Database db = null)
		{
			return new UserCollection(new Query<UserColumns, User>(where, db), true);
		}
		   
		public static UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
		{
			return new UserCollection(new Query<UserColumns, User>(where, orderBy, db), true);
		}

		public static UserCollection Where(QiQuery where, Database db = null)
		{
			return new UserCollection(Select<UserColumns>.From<User>().Where(where, db));
		}

		public static User OneWhere(WhereDelegate<UserColumns> where, Database db = null)
		{
			var results = new UserCollection(Select<UserColumns>.From<User>().Where(where, db));
			return OneOrThrow(results);
		}

		public static User OneWhere(QiQuery where, Database db = null)
		{
			var results = new UserCollection(Select<UserColumns>.From<User>().Where(where, db));
			return OneOrThrow(results);
		}

		private static User OneOrThrow(UserCollection c)
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

		public static User FirstOneWhere(WhereDelegate<UserColumns> where, Database db = null)
		{
			var results = new UserCollection(Select<UserColumns>.From<User>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UserCollection Top(int count, WhereDelegate<UserColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database database = null)
        {
            UserColumns c = new UserColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<User>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<User>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UserColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<UserCollection>(0);
        }

		public static long Count(WhereDelegate<UserColumns> where, Database database = null)
		{
			UserColumns c = new UserColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<User>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<User>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
