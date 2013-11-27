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
    [Brevitee.Data.Table("Attribute", "Analytics")]
    public partial class Attribute: Dao
    {
        public Attribute():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Attribute(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Attribute(DataRow data)
        {
            return new Attribute(data);
        }

		private void SetChildren()
		{

            this.ChildCollections.Add("HtmlElementAttribute_AttributeId", new HtmlElementAttributeCollection(new Query<HtmlElementAttributeColumns, HtmlElementAttribute>((c) => c.AttributeId == this.Id), this, "AttributeId"));							
            this.ChildCollections.Add("Attribute_HtmlElementAttribute_HtmlElement",  new XrefDaoCollection<HtmlElementAttribute, HtmlElement>(this, false));
				
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
	public HtmlElementAttributeCollection HtmlElementAttributesByAttributeId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HtmlElementAttribute_AttributeId"))
			{
				SetChildren();
			}

			var c = (HtmlElementAttributeCollection)this.ChildCollections["HtmlElementAttribute_AttributeId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<HtmlElementAttribute, HtmlElement> HtmlElements
        {
            get
            {
				if(!this.ChildCollections.ContainsKey("Attribute_HtmlElementAttribute_HtmlElement"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HtmlElementAttribute, HtmlElement>)this.ChildCollections["Attribute_HtmlElementAttribute_HtmlElement"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
            }
        }
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new AttributeColumns();
			return (colFilter.Id == IdValue);
		}

		public static AttributeCollection Where(Func<AttributeColumns, QueryFilter<AttributeColumns>> where, OrderBy<AttributeColumns> orderBy = null)
		{
			return new AttributeCollection(new Query<AttributeColumns, Attribute>(where, orderBy), true);
		}
		
		public static AttributeCollection Where(WhereDelegate<AttributeColumns> where, Database db = null)
		{
			return new AttributeCollection(new Query<AttributeColumns, Attribute>(where, db), true);
		}
		   
		public static AttributeCollection Where(WhereDelegate<AttributeColumns> where, OrderBy<AttributeColumns> orderBy = null, Database db = null)
		{
			return new AttributeCollection(new Query<AttributeColumns, Attribute>(where, orderBy, db), true);
		}

		public static AttributeCollection Where(QiQuery where, Database db = null)
		{
			return new AttributeCollection(Select<AttributeColumns>.From<Attribute>().Where(where, db));
		}

		public static Attribute OneWhere(WhereDelegate<AttributeColumns> where, Database db = null)
		{
			var results = new AttributeCollection(Select<AttributeColumns>.From<Attribute>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Attribute OneWhere(QiQuery where, Database db = null)
		{
			var results = new AttributeCollection(Select<AttributeColumns>.From<Attribute>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Attribute OneOrThrow(AttributeCollection c)
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

		public static Attribute FirstOneWhere(WhereDelegate<AttributeColumns> where, Database db = null)
		{
			var results = new AttributeCollection(Select<AttributeColumns>.From<Attribute>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static AttributeCollection Top(int count, WhereDelegate<AttributeColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static AttributeCollection Top(int count, WhereDelegate<AttributeColumns> where, OrderBy<AttributeColumns> orderBy, Database database = null)
        {
            AttributeColumns c = new AttributeColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Attribute>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Attribute>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<AttributeColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<AttributeCollection>(0);
        }

		public static long Count(WhereDelegate<AttributeColumns> where, Database database = null)
		{
			AttributeColumns c = new AttributeColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Attribute>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Attribute>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
