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
    [Brevitee.Data.Table("HtmlElementAttribute", "Analytics")]
    public partial class HtmlElementAttribute: Dao
    {
        public HtmlElementAttribute():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public HtmlElementAttribute(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator HtmlElementAttribute(DataRow data)
        {
            return new HtmlElementAttribute(data);
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
        Table="HtmlElementAttribute",
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
	
	// start AttributeId -> AttributeId
	[Brevitee.Data.ForeignKey(
        Table="HtmlElementAttribute",
		Name="AttributeId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Attribute",
		Suffix="2")]
	public long? AttributeId
	{
		get
		{
			return GetLongValue("AttributeId");
		}
		set
		{
			SetValue("AttributeId", value);
		}
	}

	Attribute _attributeOfAttributeId;
	public Attribute AttributeOfAttributeId
	{
		get
		{
			if(_attributeOfAttributeId == null)
			{
				_attributeOfAttributeId = Brevitee.Analytics.Data.Attribute.OneWhere(f => f.Id == this.AttributeId);
			}
			return _attributeOfAttributeId;
		}
	}
	
				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new HtmlElementAttributeColumns();
			return (colFilter.Id == IdValue);
		}

		public static HtmlElementAttributeCollection Where(Func<HtmlElementAttributeColumns, QueryFilter<HtmlElementAttributeColumns>> where, OrderBy<HtmlElementAttributeColumns> orderBy = null)
		{
			return new HtmlElementAttributeCollection(new Query<HtmlElementAttributeColumns, HtmlElementAttribute>(where, orderBy), true);
		}
		
		public static HtmlElementAttributeCollection Where(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
		{
			return new HtmlElementAttributeCollection(new Query<HtmlElementAttributeColumns, HtmlElementAttribute>(where, db), true);
		}
		   
		public static HtmlElementAttributeCollection Where(WhereDelegate<HtmlElementAttributeColumns> where, OrderBy<HtmlElementAttributeColumns> orderBy = null, Database db = null)
		{
			return new HtmlElementAttributeCollection(new Query<HtmlElementAttributeColumns, HtmlElementAttribute>(where, orderBy, db), true);
		}

		public static HtmlElementAttributeCollection Where(QiQuery where, Database db = null)
		{
			return new HtmlElementAttributeCollection(Select<HtmlElementAttributeColumns>.From<HtmlElementAttribute>().Where(where, db));
		}

		public static HtmlElementAttribute OneWhere(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
		{
			var results = new HtmlElementAttributeCollection(Select<HtmlElementAttributeColumns>.From<HtmlElementAttribute>().Where(where, db));
			return OneOrThrow(results);
		}

		public static HtmlElementAttribute OneWhere(QiQuery where, Database db = null)
		{
			var results = new HtmlElementAttributeCollection(Select<HtmlElementAttributeColumns>.From<HtmlElementAttribute>().Where(where, db));
			return OneOrThrow(results);
		}

		private static HtmlElementAttribute OneOrThrow(HtmlElementAttributeCollection c)
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

		public static HtmlElementAttribute FirstOneWhere(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
		{
			var results = new HtmlElementAttributeCollection(Select<HtmlElementAttributeColumns>.From<HtmlElementAttribute>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static HtmlElementAttributeCollection Top(int count, WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static HtmlElementAttributeCollection Top(int count, WhereDelegate<HtmlElementAttributeColumns> where, OrderBy<HtmlElementAttributeColumns> orderBy, Database database = null)
        {
            HtmlElementAttributeColumns c = new HtmlElementAttributeColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<HtmlElementAttribute>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<HtmlElementAttribute>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HtmlElementAttributeColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<HtmlElementAttributeCollection>(0);
        }

		public static long Count(WhereDelegate<HtmlElementAttributeColumns> where, Database database = null)
		{
			HtmlElementAttributeColumns c = new HtmlElementAttributeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<HtmlElementAttribute>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<HtmlElementAttribute>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
