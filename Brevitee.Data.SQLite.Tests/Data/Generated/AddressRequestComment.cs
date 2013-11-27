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
    [Brevitee.Data.Table("AddressRequestComment", "Test")]
    public partial class AddressRequestComment: Dao
    {
        public AddressRequestComment():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public AddressRequestComment(DataRow data): base(data)
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


	// start AddressRequestId -> AddressRequestId
	[Brevitee.Data.ForeignKey(
        Table="AddressRequestComment",
		Name="AddressRequestId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="AddressRequest",
		Suffix="1")]
	public long? AddressRequestId
	{
		get
		{
			return GetLongValue("AddressRequestId");
		}
		set
		{
			SetValue("AddressRequestId", value);
		}
	}

	AddressRequest _addressRequestOfAddressRequestId;
	public AddressRequest AddressRequestOfAddressRequestId
	{
		get
		{
			if(_addressRequestOfAddressRequestId == null)
			{
				_addressRequestOfAddressRequestId = SampleData.AddressRequest.OneWhere(f => f.Id == this.AddressRequestId);
			}
			return _addressRequestOfAddressRequestId;
		}
	}
	
	// start CommentId -> CommentId
	[Brevitee.Data.ForeignKey(
        Table="AddressRequestComment",
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
			var colFilter = new AddressRequestCommentColumns();
			return (colFilter.Id == IdValue);
		}

		public static AddressRequestCommentCollection Where(Func<AddressRequestCommentColumns, QueryFilter<AddressRequestCommentColumns>> where, OrderBy<AddressRequestCommentColumns> orderBy = null)
		{
			return new AddressRequestCommentCollection(new Query<AddressRequestCommentColumns, AddressRequestComment>(where, orderBy), true);
		}
		
		public static AddressRequestCommentCollection Where(WhereDelegate<AddressRequestCommentColumns> where, Database db = null)
		{
			return new AddressRequestCommentCollection(new Query<AddressRequestCommentColumns, AddressRequestComment>(where, db), true);
		}
		   
		public static AddressRequestCommentCollection Where(WhereDelegate<AddressRequestCommentColumns> where, OrderBy<AddressRequestCommentColumns> orderBy = null, Database db = null)
		{
			return new AddressRequestCommentCollection(new Query<AddressRequestCommentColumns, AddressRequestComment>(where, orderBy, db), true);
		}

		public static AddressRequestCommentCollection Where(QiQuery where, Database db = null)
		{
			return new AddressRequestCommentCollection(Select<AddressRequestCommentColumns>.From<AddressRequestComment>().Where(where, db));
		}

		public static AddressRequestComment OneWhere(WhereDelegate<AddressRequestCommentColumns> where, Database db = null)
		{
			var results = new AddressRequestCommentCollection(Select<AddressRequestCommentColumns>.From<AddressRequestComment>().Where(where, db));
			return OneOrThrow(results);
		}

		public static AddressRequestComment OneWhere(QiQuery where, Database db = null)
		{
			var results = new AddressRequestCommentCollection(Select<AddressRequestCommentColumns>.From<AddressRequestComment>().Where(where, db));
			return OneOrThrow(results);
		}

		private static AddressRequestComment OneOrThrow(AddressRequestCommentCollection c)
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

		public static AddressRequestComment FirstOneWhere(WhereDelegate<AddressRequestCommentColumns> where, Database db = null)
		{
			var results = new AddressRequestCommentCollection(Select<AddressRequestCommentColumns>.From<AddressRequestComment>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static AddressRequestCommentCollection Top(int count, WhereDelegate<AddressRequestCommentColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static AddressRequestCommentCollection Top(int count, WhereDelegate<AddressRequestCommentColumns> where, OrderBy<AddressRequestCommentColumns> orderBy, Database database = null)
        {
            AddressRequestCommentColumns c = new AddressRequestCommentColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<AddressRequestComment>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<AddressRequestComment>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AddressRequestCommentColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<AddressRequestCommentCollection>(0);
        }

		public static long Count(WhereDelegate<AddressRequestCommentColumns> where, Database database = null)
		{
			AddressRequestCommentColumns c = new AddressRequestCommentColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<AddressRequestComment>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<AddressRequestComment>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
