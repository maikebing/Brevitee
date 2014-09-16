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
    [Brevitee.Data.Table("ActivityComment", "Test")]
    public partial class ActivityComment: Dao
    {
        public ActivityComment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public ActivityComment(DataRow data): base(data)
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


	// start ActivityId -> ActivityId
	[Brevitee.Data.ForeignKey(
        Table="ActivityComment",
		Name="ActivityId", 
		DbDataType="BigInt", 
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
        Table="ActivityComment",
		Name="CommentId", 
		DbDataType="BigInt", 
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
			var colFilter = new ActivityCommentColumns();
			return (colFilter.Id == IdValue);
		}

		public static ActivityCommentCollection Where(Func<ActivityCommentColumns, QueryFilter<ActivityCommentColumns>> where, OrderBy<ActivityCommentColumns> orderBy = null)
		{
			return new ActivityCommentCollection(new Query<ActivityCommentColumns, ActivityComment>(where, orderBy), true);
		}
		
		public static ActivityCommentCollection Where(WhereDelegate<ActivityCommentColumns> where, Database db = null)
		{
			return new ActivityCommentCollection(new Query<ActivityCommentColumns, ActivityComment>(where, db), true);
		}
		   
		public static ActivityCommentCollection Where(WhereDelegate<ActivityCommentColumns> where, OrderBy<ActivityCommentColumns> orderBy = null, Database db = null)
		{
			return new ActivityCommentCollection(new Query<ActivityCommentColumns, ActivityComment>(where, orderBy, db), true);
		}

		public static ActivityCommentCollection Where(QiQuery where, Database db = null)
		{
			return new ActivityCommentCollection(Select<ActivityCommentColumns>.From<ActivityComment>().Where(where, db));
		}

		public static ActivityComment OneWhere(WhereDelegate<ActivityCommentColumns> where, Database db = null)
		{
			var results = new ActivityCommentCollection(Select<ActivityCommentColumns>.From<ActivityComment>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ActivityComment OneWhere(QiQuery where, Database db = null)
		{
			var results = new ActivityCommentCollection(Select<ActivityCommentColumns>.From<ActivityComment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ActivityComment OneOrThrow(ActivityCommentCollection c)
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

		public static ActivityComment FirstOneWhere(WhereDelegate<ActivityCommentColumns> where, Database db = null)
		{
			var results = new ActivityCommentCollection(Select<ActivityCommentColumns>.From<ActivityComment>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ActivityCommentCollection Top(int count, WhereDelegate<ActivityCommentColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ActivityCommentCollection Top(int count, WhereDelegate<ActivityCommentColumns> where, OrderBy<ActivityCommentColumns> orderBy, Database database = null)
        {
            ActivityCommentColumns c = new ActivityCommentColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<ActivityComment>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<ActivityComment>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ActivityCommentColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<ActivityCommentCollection>(0);
        }

		public static long Count(WhereDelegate<ActivityCommentColumns> where, Database database = null)
		{
			ActivityCommentColumns c = new ActivityCommentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<ActivityComment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ActivityComment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
