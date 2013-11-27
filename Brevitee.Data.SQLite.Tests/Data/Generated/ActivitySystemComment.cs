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
    [Brevitee.Data.Table("ActivitySystemComment", "Test")]
    public partial class ActivitySystemComment: Dao
    {
        public ActivitySystemComment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ActivitySystemComment(DataRow data): base(data)
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


	// start ActivityId -> ActivityId
	[Brevitee.Data.ForeignKey(
        Table="ActivitySystemComment",
		Name="ActivityId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Activity",
		Suffix="1")]
	public long? ActivityId
	{
		get
		{
			return GetLongValue("ActivityId");
		}
		set
		{
			SetValue("ActivityId", value);
		}
	}

	Activity _activityOfActivityId;
	public Activity ActivityOfActivityId
	{
		get
		{
			if(_activityOfActivityId == null)
			{
				_activityOfActivityId = SampleData.Activity.OneWhere(f => f.Id == this.ActivityId);
			}
			return _activityOfActivityId;
		}
	}
	
	// start CommentId -> CommentId
	[Brevitee.Data.ForeignKey(
        Table="ActivitySystemComment",
		Name="CommentId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Comment",
		Suffix="2")]
	public long? CommentId
	{
		get
		{
			return GetLongValue("CommentId");
		}
		set
		{
			SetValue("CommentId", value);
		}
	}

	Comment _commentOfCommentId;
	public Comment CommentOfCommentId
	{
		get
		{
			if(_commentOfCommentId == null)
			{
				_commentOfCommentId = SampleData.Comment.OneWhere(f => f.Id == this.CommentId);
			}
			return _commentOfCommentId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ActivitySystemCommentColumns();
			return (colFilter.Id == IdValue);
		}

		public static ActivitySystemCommentCollection Where(Func<ActivitySystemCommentColumns, QueryFilter<ActivitySystemCommentColumns>> where, OrderBy<ActivitySystemCommentColumns> orderBy = null)
		{
			return new ActivitySystemCommentCollection(new Query<ActivitySystemCommentColumns, ActivitySystemComment>(where, orderBy), true);
		}
		
		public static ActivitySystemCommentCollection Where(WhereDelegate<ActivitySystemCommentColumns> where, Database db = null)
		{
			return new ActivitySystemCommentCollection(new Query<ActivitySystemCommentColumns, ActivitySystemComment>(where, db), true);
		}
		   
		public static ActivitySystemCommentCollection Where(WhereDelegate<ActivitySystemCommentColumns> where, OrderBy<ActivitySystemCommentColumns> orderBy = null, Database db = null)
		{
			return new ActivitySystemCommentCollection(new Query<ActivitySystemCommentColumns, ActivitySystemComment>(where, orderBy, db), true);
		}

		public static ActivitySystemCommentCollection Where(QiQuery where, Database db = null)
		{
			return new ActivitySystemCommentCollection(Select<ActivitySystemCommentColumns>.From<ActivitySystemComment>().Where(where, db));
		}

		public static ActivitySystemComment OneWhere(WhereDelegate<ActivitySystemCommentColumns> where, Database db = null)
		{
			var results = new ActivitySystemCommentCollection(Select<ActivitySystemCommentColumns>.From<ActivitySystemComment>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ActivitySystemComment OneWhere(QiQuery where, Database db = null)
		{
			var results = new ActivitySystemCommentCollection(Select<ActivitySystemCommentColumns>.From<ActivitySystemComment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ActivitySystemComment OneOrThrow(ActivitySystemCommentCollection c)
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

		public static ActivitySystemComment FirstOneWhere(WhereDelegate<ActivitySystemCommentColumns> where, Database db = null)
		{
			var results = new ActivitySystemCommentCollection(Select<ActivitySystemCommentColumns>.From<ActivitySystemComment>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ActivitySystemCommentCollection Top(int count, WhereDelegate<ActivitySystemCommentColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ActivitySystemCommentCollection Top(int count, WhereDelegate<ActivitySystemCommentColumns> where, OrderBy<ActivitySystemCommentColumns> orderBy, Database database = null)
        {
            ActivitySystemCommentColumns c = new ActivitySystemCommentColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<ActivitySystemComment>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ActivitySystemComment>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ActivitySystemCommentColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ActivitySystemCommentCollection>(0);
        }

		public static long Count(WhereDelegate<ActivitySystemCommentColumns> where, Database database = null)
		{
			ActivitySystemCommentColumns c = new ActivitySystemCommentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ActivitySystemComment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ActivitySystemComment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
