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
    [Brevitee.Data.Table("HtmlElement", "Analytics")]
    public partial class HtmlElement: Dao
    {
        public HtmlElement():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public HtmlElement(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator HtmlElement(DataRow data)
        {
            return new HtmlElement(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("HtmlElementStyle_HtmlElementId", new HtmlElementStyleCollection(new Query<HtmlElementStyleColumns, HtmlElementStyle>((c) => c.HtmlElementId == this.Id), this, "HtmlElementId"));	
            this.ChildCollections.Add("HtmlElementAttribute_HtmlElementId", new HtmlElementAttributeCollection(new Query<HtmlElementAttributeColumns, HtmlElementAttribute>((c) => c.HtmlElementId == this.Id), this, "HtmlElementId"));				
            this.ChildCollections.Add("HtmlElement_HtmlElementStyle_Style",  new XrefDaoCollection<HtmlElementStyle, Style>(this, false));
				
            this.ChildCollections.Add("HtmlElement_HtmlElementAttribute_Attribute",  new XrefDaoCollection<HtmlElementAttribute, Attribute>(this, false));
							
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

	// property:DomId, columnName:DomId	
	[Brevitee.Data.Column(Name="DomId", ExtractedType="", MaxLength="", AllowNull=false)]
	public string DomId
	{
		get
		{
			return GetStringValue("DomId");
		}
		set
		{
			SetValue("DomId", value);
		}
	}

	// property:TagName, columnName:TagName	
	[Brevitee.Data.Column(Name="TagName", ExtractedType="", MaxLength="", AllowNull=false)]
	public string TagName
	{
		get
		{
			return GetStringValue("TagName");
		}
		set
		{
			SetValue("TagName", value);
		}
	}



	// start UrlId -> UrlId
	[Brevitee.Data.ForeignKey(
        Table="HtmlElement",
		Name="UrlId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Url",
		Suffix="1")]
	public long? UrlId
	{
		get
		{
			return GetLongValue("UrlId");
		}
		set
		{
			SetValue("UrlId", value);
		}
	}

	Url _urlOfUrlId;
	public Url UrlOfUrlId
	{
		get
		{
			if(_urlOfUrlId == null)
			{
				_urlOfUrlId = Brevitee.Analytics.Data.Url.OneWhere(f => f.Id == this.UrlId);
			}
			return _urlOfUrlId;
		}
	}
	
	// start ContentId -> ContentId
	[Brevitee.Data.ForeignKey(
        Table="HtmlElement",
		Name="ContentId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Content",
		Suffix="2")]
	public long? ContentId
	{
		get
		{
			return GetLongValue("ContentId");
		}
		set
		{
			SetValue("ContentId", value);
		}
	}

	Content _contentOfContentId;
	public Content ContentOfContentId
	{
		get
		{
			if(_contentOfContentId == null)
			{
				_contentOfContentId = Brevitee.Analytics.Data.Content.OneWhere(f => f.Id == this.ContentId);
			}
			return _contentOfContentId;
		}
	}
	
				
	[Exclude]	
	public HtmlElementStyleCollection HtmlElementStylesByHtmlElementId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HtmlElementStyle_HtmlElementId"))
			{
				SetChildren();
			}

			var c = (HtmlElementStyleCollection)this.ChildCollections["HtmlElementStyle_HtmlElementId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public HtmlElementAttributeCollection HtmlElementAttributesByHtmlElementId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HtmlElementAttribute_HtmlElementId"))
			{
				SetChildren();
			}

			var c = (HtmlElementAttributeCollection)this.ChildCollections["HtmlElementAttribute_HtmlElementId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		// Xref       
        public XrefDaoCollection<HtmlElementStyle, Style> Styles
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("HtmlElement_HtmlElementStyle_Style"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HtmlElementStyle, Style>)this.ChildCollections["HtmlElement_HtmlElementStyle_Style"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }		// Xref       
        public XrefDaoCollection<HtmlElementAttribute, Attribute> Attributes
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("HtmlElement_HtmlElementAttribute_Attribute"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HtmlElementAttribute, Attribute>)this.ChildCollections["HtmlElement_HtmlElementAttribute_Attribute"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }

		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new HtmlElementColumns();
			return (colFilter.Id == IdValue);
		}

		public static HtmlElementCollection Where(Func<HtmlElementColumns, QueryFilter<HtmlElementColumns>> where, OrderBy<HtmlElementColumns> orderBy = null)
		{
			return new HtmlElementCollection(new Query<HtmlElementColumns, HtmlElement>(where, orderBy), true);
		}
		
		public static HtmlElementCollection Where(WhereDelegate<HtmlElementColumns> where, Database db = null)
		{
			return new HtmlElementCollection(new Query<HtmlElementColumns, HtmlElement>(where, db), true);
		}
		   
		public static HtmlElementCollection Where(WhereDelegate<HtmlElementColumns> where, OrderBy<HtmlElementColumns> orderBy = null, Database db = null)
		{
			return new HtmlElementCollection(new Query<HtmlElementColumns, HtmlElement>(where, orderBy, db), true);
		}

		public static HtmlElementCollection Where(QiQuery where, Database db = null)
		{
			return new HtmlElementCollection(Select<HtmlElementColumns>.From<HtmlElement>().Where(where, db));
		}

		public static HtmlElement OneWhere(WhereDelegate<HtmlElementColumns> where, Database db = null)
		{
			var results = new HtmlElementCollection(Select<HtmlElementColumns>.From<HtmlElement>().Where(where, db));
			return OneOrThrow(results);
		}

		public static HtmlElement OneWhere(QiQuery where, Database db = null)
		{
			var results = new HtmlElementCollection(Select<HtmlElementColumns>.From<HtmlElement>().Where(where, db));
			return OneOrThrow(results);
		}

		private static HtmlElement OneOrThrow(HtmlElementCollection c)
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

		public static HtmlElement FirstOneWhere(WhereDelegate<HtmlElementColumns> where, Database db = null)
		{
			var results = new HtmlElementCollection(Select<HtmlElementColumns>.From<HtmlElement>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static HtmlElementCollection Top(int count, WhereDelegate<HtmlElementColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static HtmlElementCollection Top(int count, WhereDelegate<HtmlElementColumns> where, OrderBy<HtmlElementColumns> orderBy, Database database = null)
        {
            HtmlElementColumns c = new HtmlElementColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<HtmlElement>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<HtmlElement>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HtmlElementColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<HtmlElementCollection>(0);
        }

		public static long Count(WhereDelegate<HtmlElementColumns> where, Database database = null)
		{
			HtmlElementColumns c = new HtmlElementColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<HtmlElement>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<HtmlElement>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
