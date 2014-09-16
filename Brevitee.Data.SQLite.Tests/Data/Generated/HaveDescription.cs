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
    [Brevitee.Data.Table("HaveDescription", "Test")]
    public partial class HaveDescription: Dao
    {
        public HaveDescription():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public HaveDescription(DataRow data): base(data)
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

	// property:LastModified, columnName:LastModified	
	[Brevitee.Data.Column(Name="LastModified", DbDataType="DateTime", MaxLength="8", AllowNull=false)]
	public DateTime LastModified
	{
		get
		{
			return GetDateTimeValue("LastModified");
		}
		set
		{
			SetValue("LastModified", value);
		}
	}


	// start HaveId -> HaveId
	[Brevitee.Data.ForeignKey(
        Table="HaveDescription",
		Name="HaveId", 
		DbDataType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Have",
		Suffix="1")]
	public long? HaveId
	{
		get
		{
			return GetLongValue("HaveId");
		}
		set
		{
			SetValue("HaveId", value);
		}
	}

	Have _haveOfHaveId;
	public Have HaveOfHaveId
	{
		get
		{
			if(_haveOfHaveId == null)
			{
				_haveOfHaveId = SampleData.Have.OneWhere(f => f.Id == this.HaveId);
			}
			return _haveOfHaveId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new HaveDescriptionColumns();
			return (colFilter.Id == IdValue);
		}

		public static HaveDescriptionCollection Where(Func<HaveDescriptionColumns, QueryFilter<HaveDescriptionColumns>> where, OrderBy<HaveDescriptionColumns> orderBy = null)
		{
			return new HaveDescriptionCollection(new Query<HaveDescriptionColumns, HaveDescription>(where, orderBy), true);
		}
		
		public static HaveDescriptionCollection Where(WhereDelegate<HaveDescriptionColumns> where, Database db = null)
		{
			return new HaveDescriptionCollection(new Query<HaveDescriptionColumns, HaveDescription>(where, db), true);
		}
		   
		public static HaveDescriptionCollection Where(WhereDelegate<HaveDescriptionColumns> where, OrderBy<HaveDescriptionColumns> orderBy = null, Database db = null)
		{
			return new HaveDescriptionCollection(new Query<HaveDescriptionColumns, HaveDescription>(where, orderBy, db), true);
		}

		public static HaveDescriptionCollection Where(QiQuery where, Database db = null)
		{
			return new HaveDescriptionCollection(Select<HaveDescriptionColumns>.From<HaveDescription>().Where(where, db));
		}

		public static HaveDescription OneWhere(WhereDelegate<HaveDescriptionColumns> where, Database db = null)
		{
			var results = new HaveDescriptionCollection(Select<HaveDescriptionColumns>.From<HaveDescription>().Where(where, db));
			return OneOrThrow(results);
		}

		public static HaveDescription OneWhere(QiQuery where, Database db = null)
		{
			var results = new HaveDescriptionCollection(Select<HaveDescriptionColumns>.From<HaveDescription>().Where(where, db));
			return OneOrThrow(results);
		}

		private static HaveDescription OneOrThrow(HaveDescriptionCollection c)
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

		public static HaveDescription FirstOneWhere(WhereDelegate<HaveDescriptionColumns> where, Database db = null)
		{
			var results = new HaveDescriptionCollection(Select<HaveDescriptionColumns>.From<HaveDescription>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static HaveDescriptionCollection Top(int count, WhereDelegate<HaveDescriptionColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static HaveDescriptionCollection Top(int count, WhereDelegate<HaveDescriptionColumns> where, OrderBy<HaveDescriptionColumns> orderBy, Database database = null)
        {
            HaveDescriptionColumns c = new HaveDescriptionColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<HaveDescription>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<HaveDescription>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HaveDescriptionColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<HaveDescriptionCollection>(0);
        }

		public static long Count(WhereDelegate<HaveDescriptionColumns> where, Database database = null)
		{
			HaveDescriptionColumns c = new HaveDescriptionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<HaveDescription>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<HaveDescription>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
