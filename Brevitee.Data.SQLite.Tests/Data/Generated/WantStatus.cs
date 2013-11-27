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
    [Brevitee.Data.Table("WantStatus", "Test")]
    public partial class WantStatus: Dao
    {
        public WantStatus():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public WantStatus(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Want_WantStatusId", new WantCollection(new Query<WantColumns, Want>((c) => c.WantStatusId == this.Id), this, "WantStatusId"));	
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="Int", MaxLength="4", AllowNull=false)]
	public int? Value
	{
		get
		{
			return GetIntValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}

	// property:Status, columnName:Status	
	[Brevitee.Data.Column(Name="Status", ExtractedType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Status
	{
		get
		{
			return GetStringValue("Status");
		}
		set
		{
			SetValue("Status", value);
		}
	}


				
	
	public WantCollection WantCollectionByWantStatusId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Want_WantStatusId"))
			{
				SetChildren();
			}

			var c = (WantCollection)this.ChildCollections["Want_WantStatusId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new WantStatusColumns();
			return (colFilter.Id == IdValue);
		}

		public static WantStatusCollection Where(Func<WantStatusColumns, QueryFilter<WantStatusColumns>> where, OrderBy<WantStatusColumns> orderBy = null)
		{
			return new WantStatusCollection(new Query<WantStatusColumns, WantStatus>(where, orderBy), true);
		}
		
		public static WantStatusCollection Where(WhereDelegate<WantStatusColumns> where, Database db = null)
		{
			return new WantStatusCollection(new Query<WantStatusColumns, WantStatus>(where, db), true);
		}
		   
		public static WantStatusCollection Where(WhereDelegate<WantStatusColumns> where, OrderBy<WantStatusColumns> orderBy = null, Database db = null)
		{
			return new WantStatusCollection(new Query<WantStatusColumns, WantStatus>(where, orderBy, db), true);
		}

		public static WantStatusCollection Where(QiQuery where, Database db = null)
		{
			return new WantStatusCollection(Select<WantStatusColumns>.From<WantStatus>().Where(where, db));
		}

		public static WantStatus OneWhere(WhereDelegate<WantStatusColumns> where, Database db = null)
		{
			var results = new WantStatusCollection(Select<WantStatusColumns>.From<WantStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		public static WantStatus OneWhere(QiQuery where, Database db = null)
		{
			var results = new WantStatusCollection(Select<WantStatusColumns>.From<WantStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		private static WantStatus OneOrThrow(WantStatusCollection c)
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

		public static WantStatus FirstOneWhere(WhereDelegate<WantStatusColumns> where, Database db = null)
		{
			var results = new WantStatusCollection(Select<WantStatusColumns>.From<WantStatus>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static WantStatusCollection Top(int count, WhereDelegate<WantStatusColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static WantStatusCollection Top(int count, WhereDelegate<WantStatusColumns> where, OrderBy<WantStatusColumns> orderBy, Database database = null)
        {
            WantStatusColumns c = new WantStatusColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<WantStatus>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<WantStatus>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<WantStatusColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<WantStatusCollection>(0);
        }

		public static long Count(WhereDelegate<WantStatusColumns> where, Database database = null)
		{
			WantStatusColumns c = new WantStatusColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<WantStatus>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<WantStatus>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
