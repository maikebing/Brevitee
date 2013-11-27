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
    [Brevitee.Data.Table("HaveStatus", "Test")]
    public partial class HaveStatus: Dao
    {
        public HaveStatus():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public HaveStatus(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("Have_HaveStatusId", new HaveCollection(new Query<HaveColumns, Have>((c) => c.HaveStatusId == this.Id), this, "HaveStatusId"));	
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


				
	
	public HaveCollection HaveCollectionByHaveStatusId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Have_HaveStatusId"))
			{
				SetChildren();
			}

			var c = (HaveCollection)this.ChildCollections["Have_HaveStatusId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new HaveStatusColumns();
			return (colFilter.Id == IdValue);
		}

		public static HaveStatusCollection Where(Func<HaveStatusColumns, QueryFilter<HaveStatusColumns>> where, OrderBy<HaveStatusColumns> orderBy = null)
		{
			return new HaveStatusCollection(new Query<HaveStatusColumns, HaveStatus>(where, orderBy), true);
		}
		
		public static HaveStatusCollection Where(WhereDelegate<HaveStatusColumns> where, Database db = null)
		{
			return new HaveStatusCollection(new Query<HaveStatusColumns, HaveStatus>(where, db), true);
		}
		   
		public static HaveStatusCollection Where(WhereDelegate<HaveStatusColumns> where, OrderBy<HaveStatusColumns> orderBy = null, Database db = null)
		{
			return new HaveStatusCollection(new Query<HaveStatusColumns, HaveStatus>(where, orderBy, db), true);
		}

		public static HaveStatusCollection Where(QiQuery where, Database db = null)
		{
			return new HaveStatusCollection(Select<HaveStatusColumns>.From<HaveStatus>().Where(where, db));
		}

		public static HaveStatus OneWhere(WhereDelegate<HaveStatusColumns> where, Database db = null)
		{
			var results = new HaveStatusCollection(Select<HaveStatusColumns>.From<HaveStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		public static HaveStatus OneWhere(QiQuery where, Database db = null)
		{
			var results = new HaveStatusCollection(Select<HaveStatusColumns>.From<HaveStatus>().Where(where, db));
			return OneOrThrow(results);
		}

		private static HaveStatus OneOrThrow(HaveStatusCollection c)
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

		public static HaveStatus FirstOneWhere(WhereDelegate<HaveStatusColumns> where, Database db = null)
		{
			var results = new HaveStatusCollection(Select<HaveStatusColumns>.From<HaveStatus>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static HaveStatusCollection Top(int count, WhereDelegate<HaveStatusColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static HaveStatusCollection Top(int count, WhereDelegate<HaveStatusColumns> where, OrderBy<HaveStatusColumns> orderBy, Database database = null)
        {
            HaveStatusColumns c = new HaveStatusColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<HaveStatus>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<HaveStatus>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HaveStatusColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<HaveStatusCollection>(0);
        }

		public static long Count(WhereDelegate<HaveStatusColumns> where, Database database = null)
		{
			HaveStatusColumns c = new HaveStatusColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<HaveStatus>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<HaveStatus>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
