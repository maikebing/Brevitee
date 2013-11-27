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
    [Brevitee.Data.Table("PinboardPosting", "Test")]
    public partial class PinboardPosting: Dao
    {
        public PinboardPosting():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public PinboardPosting(DataRow data): base(data)
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

	// property:Content, columnName:Content	
	[Brevitee.Data.Column(Name="Content", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Content
	{
		get
		{
			return GetStringValue("Content");
		}
		set
		{
			SetValue("Content", value);
		}
	}

	// property:Dimensions, columnName:Dimensions	
	[Brevitee.Data.Column(Name="Dimensions", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Dimensions
	{
		get
		{
			return GetStringValue("Dimensions");
		}
		set
		{
			SetValue("Dimensions", value);
		}
	}

	// property:Coordinates, columnName:Coordinates	
	[Brevitee.Data.Column(Name="Coordinates", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Coordinates
	{
		get
		{
			return GetStringValue("Coordinates");
		}
		set
		{
			SetValue("Coordinates", value);
		}
	}


	// start UserId -> UserId
	[Brevitee.Data.ForeignKey(
        Table="PinboardPosting",
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
			var colFilter = new PinboardPostingColumns();
			return (colFilter.Id == IdValue);
		}

		public static PinboardPostingCollection Where(Func<PinboardPostingColumns, QueryFilter<PinboardPostingColumns>> where, OrderBy<PinboardPostingColumns> orderBy = null)
		{
			return new PinboardPostingCollection(new Query<PinboardPostingColumns, PinboardPosting>(where, orderBy), true);
		}
		
		public static PinboardPostingCollection Where(WhereDelegate<PinboardPostingColumns> where, Database db = null)
		{
			return new PinboardPostingCollection(new Query<PinboardPostingColumns, PinboardPosting>(where, db), true);
		}
		   
		public static PinboardPostingCollection Where(WhereDelegate<PinboardPostingColumns> where, OrderBy<PinboardPostingColumns> orderBy = null, Database db = null)
		{
			return new PinboardPostingCollection(new Query<PinboardPostingColumns, PinboardPosting>(where, orderBy, db), true);
		}

		public static PinboardPostingCollection Where(QiQuery where, Database db = null)
		{
			return new PinboardPostingCollection(Select<PinboardPostingColumns>.From<PinboardPosting>().Where(where, db));
		}

		public static PinboardPosting OneWhere(WhereDelegate<PinboardPostingColumns> where, Database db = null)
		{
			var results = new PinboardPostingCollection(Select<PinboardPostingColumns>.From<PinboardPosting>().Where(where, db));
			return OneOrThrow(results);
		}

		public static PinboardPosting OneWhere(QiQuery where, Database db = null)
		{
			var results = new PinboardPostingCollection(Select<PinboardPostingColumns>.From<PinboardPosting>().Where(where, db));
			return OneOrThrow(results);
		}

		private static PinboardPosting OneOrThrow(PinboardPostingCollection c)
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

		public static PinboardPosting FirstOneWhere(WhereDelegate<PinboardPostingColumns> where, Database db = null)
		{
			var results = new PinboardPostingCollection(Select<PinboardPostingColumns>.From<PinboardPosting>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static PinboardPostingCollection Top(int count, WhereDelegate<PinboardPostingColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static PinboardPostingCollection Top(int count, WhereDelegate<PinboardPostingColumns> where, OrderBy<PinboardPostingColumns> orderBy, Database database = null)
        {
            PinboardPostingColumns c = new PinboardPostingColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<PinboardPosting>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<PinboardPosting>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<PinboardPostingColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<PinboardPostingCollection>(0);
        }

		public static long Count(WhereDelegate<PinboardPostingColumns> where, Database database = null)
		{
			PinboardPostingColumns c = new PinboardPostingColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<PinboardPosting>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<PinboardPosting>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
