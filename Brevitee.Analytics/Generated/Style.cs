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
    [Brevitee.Data.Table("Style", "Analytics")]
    public partial class Style: Dao
    {
        public Style():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Style(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Style(DataRow data)
        {
            return new Style(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("HtmlElementStyle_StyleId", new HtmlElementStyleCollection(new Query<HtmlElementStyleColumns, HtmlElementStyle>((c) => c.StyleId == this.Id), this, "StyleId"));							
            this.ChildCollections.Add("Style_HtmlElementStyle_HtmlElement",  new XrefDaoCollection<HtmlElementStyle, HtmlElement>(this, false));
				
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

	// property:Name, columnName:Name	
	[Brevitee.Data.Column(Name="Name", ExtractedType="", MaxLength="", AllowNull=false)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=true)]
	public string Value
	{
		get
		{
			return GetStringValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



				
	[Exclude]	
	public HtmlElementStyleCollection HtmlElementStylesByStyleId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HtmlElementStyle_StyleId"))
			{
				SetChildren();
			}

			var c = (HtmlElementStyleCollection)this.ChildCollections["HtmlElementStyle_StyleId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<HtmlElementStyle, HtmlElement> HtmlElements
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Style_HtmlElementStyle_HtmlElement"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HtmlElementStyle, HtmlElement>)this.ChildCollections["Style_HtmlElementStyle_HtmlElement"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new StyleColumns();
			return (colFilter.Id == IdValue);
		}

		public static StyleCollection Where(Func<StyleColumns, QueryFilter<StyleColumns>> where, OrderBy<StyleColumns> orderBy = null)
		{
			return new StyleCollection(new Query<StyleColumns, Style>(where, orderBy), true);
		}
		
		public static StyleCollection Where(WhereDelegate<StyleColumns> where, Database db = null)
		{
			return new StyleCollection(new Query<StyleColumns, Style>(where, db), true);
		}
		   
		public static StyleCollection Where(WhereDelegate<StyleColumns> where, OrderBy<StyleColumns> orderBy = null, Database db = null)
		{
			return new StyleCollection(new Query<StyleColumns, Style>(where, orderBy, db), true);
		}

		public static StyleCollection Where(QiQuery where, Database db = null)
		{
			return new StyleCollection(Select<StyleColumns>.From<Style>().Where(where, db));
		}

		public static Style OneWhere(WhereDelegate<StyleColumns> where, Database db = null)
		{
			var results = new StyleCollection(Select<StyleColumns>.From<Style>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Style OneWhere(QiQuery where, Database db = null)
		{
			var results = new StyleCollection(Select<StyleColumns>.From<Style>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Style OneOrThrow(StyleCollection c)
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

		public static Style FirstOneWhere(WhereDelegate<StyleColumns> where, Database db = null)
		{
			var results = new StyleCollection(Select<StyleColumns>.From<Style>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static StyleCollection Top(int count, WhereDelegate<StyleColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static StyleCollection Top(int count, WhereDelegate<StyleColumns> where, OrderBy<StyleColumns> orderBy, Database database = null)
        {
            StyleColumns c = new StyleColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Style>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Style>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<StyleColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<StyleCollection>(0);
        }

		public static long Count(WhereDelegate<StyleColumns> where, Database database = null)
		{
			StyleColumns c = new StyleColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Style>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Style>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
