// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Data
{
	// schema = Analytics
	// connection Name = Analytics
    [Brevitee.Data.Table("HtmlElementStyle", "Analytics")]
    public partial class HtmlElementStyle: Dao
    {
        public HtmlElementStyle():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public HtmlElementStyle(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator HtmlElementStyle(DataRow data)
        {
            return new HtmlElementStyle(data);
        }

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Exclude]
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="", MaxLength="")]
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



	// start HtmlElementId -> HtmlElementId
	[Brevitee.Data.ForeignKey(
        Table="HtmlElementStyle",
		Name="HtmlElementId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="HtmlElement",
		Suffix="1")]
	public long? HtmlElementId
	{
		get
		{
			return GetLongValue("HtmlElementId");
		}
		set
		{
			SetValue("HtmlElementId", value);
		}
	}

	HtmlElement _htmlElementOfHtmlElementId;
	public HtmlElement HtmlElementOfHtmlElementId
	{
		get
		{
			if(_htmlElementOfHtmlElementId == null)
			{
				_htmlElementOfHtmlElementId = Brevitee.Analytics.Data.HtmlElement.OneWhere(f => f.Id == this.HtmlElementId);
			}
			return _htmlElementOfHtmlElementId;
		}
	}
	
	// start StyleId -> StyleId
	[Brevitee.Data.ForeignKey(
        Table="HtmlElementStyle",
		Name="StyleId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Style",
		Suffix="2")]
	public long? StyleId
	{
		get
		{
			return GetLongValue("StyleId");
		}
		set
		{
			SetValue("StyleId", value);
		}
	}

	Style _styleOfStyleId;
	public Style StyleOfStyleId
	{
		get
		{
			if(_styleOfStyleId == null)
			{
				_styleOfStyleId = Brevitee.Analytics.Data.Style.OneWhere(f => f.Id == this.StyleId);
			}
			return _styleOfStyleId;
		}
	}
	
				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new HtmlElementStyleColumns();
			return (colFilter.Id == IdValue);
		}

		public static HtmlElementStyleCollection Where(Func<HtmlElementStyleColumns, QueryFilter<HtmlElementStyleColumns>> where, OrderBy<HtmlElementStyleColumns> orderBy = null)
		{
			return new HtmlElementStyleCollection(new Query<HtmlElementStyleColumns, HtmlElementStyle>(where, orderBy), true);
		}
		
		public static HtmlElementStyleCollection Where(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
		{
			return new HtmlElementStyleCollection(new Query<HtmlElementStyleColumns, HtmlElementStyle>(where, db), true);
		}
		   
		public static HtmlElementStyleCollection Where(WhereDelegate<HtmlElementStyleColumns> where, OrderBy<HtmlElementStyleColumns> orderBy = null, Database db = null)
		{
			return new HtmlElementStyleCollection(new Query<HtmlElementStyleColumns, HtmlElementStyle>(where, orderBy, db), true);
		}

		public static HtmlElementStyleCollection Where(QiQuery where, Database db = null)
		{
			return new HtmlElementStyleCollection(Select<HtmlElementStyleColumns>.From<HtmlElementStyle>().Where(where, db));
		}

		public static HtmlElementStyle OneWhere(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
		{
			var results = new HtmlElementStyleCollection(Select<HtmlElementStyleColumns>.From<HtmlElementStyle>().Where(where, db));
			return OneOrThrow(results);
		}

		public static HtmlElementStyle OneWhere(QiQuery where, Database db = null)
		{
			var results = new HtmlElementStyleCollection(Select<HtmlElementStyleColumns>.From<HtmlElementStyle>().Where(where, db));
			return OneOrThrow(results);
		}

		private static HtmlElementStyle OneOrThrow(HtmlElementStyleCollection c)
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

		public static HtmlElementStyle FirstOneWhere(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
		{
			var results = new HtmlElementStyleCollection(Select<HtmlElementStyleColumns>.From<HtmlElementStyle>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static HtmlElementStyleCollection Top(int count, WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static HtmlElementStyleCollection Top(int count, WhereDelegate<HtmlElementStyleColumns> where, OrderBy<HtmlElementStyleColumns> orderBy, Database database = null)
        {
            HtmlElementStyleColumns c = new HtmlElementStyleColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<HtmlElementStyle>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<HtmlElementStyle>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HtmlElementStyleColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<HtmlElementStyleCollection>(0);
        }

		public static long Count(WhereDelegate<HtmlElementStyleColumns> where, Database database = null)
		{
			HtmlElementStyleColumns c = new HtmlElementStyleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<HtmlElementStyle>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<HtmlElementStyle>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
