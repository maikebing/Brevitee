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
	[Brevitee.Data.Table("Url", "Analytics")]
	public partial class Url: Dao
	{
		public Url():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public Url(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Url(DataRow data)
		{
			return new Url(data);
		}

		private void SetChildren()
		{

			this.ChildCollections.Add("HtmlElement_UrlId", new HtmlElementCollection(new Query<HtmlElementColumns, HtmlElement>((c) => c.UrlId == this.Id), this, "UrlId"));	
			this.ChildCollections.Add("Image_UrlId", new ImageCollection(new Query<ImageColumns, Image>((c) => c.UrlId == this.Id), this, "UrlId"));	
			this.ChildCollections.Add("UrlTag_UrlId", new UrlTagCollection(new Query<UrlTagColumns, UrlTag>((c) => c.UrlId == this.Id), this, "UrlId"));				
			this.ChildCollections.Add("Url_UrlTag_Tag",  new XrefDaoCollection<UrlTag, Tag>(this, false));
							
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



	// start ProtocolId -> ProtocolId
	[Brevitee.Data.ForeignKey(
		Table="Url",
		Name="ProtocolId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Protocol",
		Suffix="1")]
	public long? ProtocolId
	{
		get
		{
			return GetLongValue("ProtocolId");
		}
		set
		{
			SetValue("ProtocolId", value);
		}
	}

	Protocol _protocolOfProtocolId;
	public Protocol ProtocolOfProtocolId
	{
		get
		{
			if(_protocolOfProtocolId == null)
			{
				_protocolOfProtocolId = Brevitee.Analytics.Data.Protocol.OneWhere(f => f.Id == this.ProtocolId);
			}
			return _protocolOfProtocolId;
		}
	}
	
	// start DomainId -> DomainId
	[Brevitee.Data.ForeignKey(
		Table="Url",
		Name="DomainId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Domain",
		Suffix="2")]
	public long? DomainId
	{
		get
		{
			return GetLongValue("DomainId");
		}
		set
		{
			SetValue("DomainId", value);
		}
	}

	Domain _domainOfDomainId;
	public Domain DomainOfDomainId
	{
		get
		{
			if(_domainOfDomainId == null)
			{
				_domainOfDomainId = Brevitee.Analytics.Data.Domain.OneWhere(f => f.Id == this.DomainId);
			}
			return _domainOfDomainId;
		}
	}
	
	// start PortId -> PortId
	[Brevitee.Data.ForeignKey(
		Table="Url",
		Name="PortId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Port",
		Suffix="3")]
	public long? PortId
	{
		get
		{
			return GetLongValue("PortId");
		}
		set
		{
			SetValue("PortId", value);
		}
	}

	Port _portOfPortId;
	public Port PortOfPortId
	{
		get
		{
			if(_portOfPortId == null)
			{
				_portOfPortId = Brevitee.Analytics.Data.Port.OneWhere(f => f.Id == this.PortId);
			}
			return _portOfPortId;
		}
	}
	
	// start PathId -> PathId
	[Brevitee.Data.ForeignKey(
		Table="Url",
		Name="PathId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Path",
		Suffix="4")]
	public long? PathId
	{
		get
		{
			return GetLongValue("PathId");
		}
		set
		{
			SetValue("PathId", value);
		}
	}

	Path _pathOfPathId;
	public Path PathOfPathId
	{
		get
		{
			if(_pathOfPathId == null)
			{
				_pathOfPathId = Brevitee.Analytics.Data.Path.OneWhere(f => f.Id == this.PathId);
			}
			return _pathOfPathId;
		}
	}
	
	// start QueryStringId -> QueryStringId
	[Brevitee.Data.ForeignKey(
		Table="Url",
		Name="QueryStringId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="QueryString",
		Suffix="5")]
	public long? QueryStringId
	{
		get
		{
			return GetLongValue("QueryStringId");
		}
		set
		{
			SetValue("QueryStringId", value);
		}
	}

	QueryString _queryStringOfQueryStringId;
	public QueryString QueryStringOfQueryStringId
	{
		get
		{
			if(_queryStringOfQueryStringId == null)
			{
				_queryStringOfQueryStringId = Brevitee.Analytics.Data.QueryString.OneWhere(f => f.Id == this.QueryStringId);
			}
			return _queryStringOfQueryStringId;
		}
	}
	
				
	[Exclude]	
	public HtmlElementCollection HtmlElementsByUrlId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("HtmlElement_UrlId"))
			{
				SetChildren();
			}

			var c = (HtmlElementCollection)this.ChildCollections["HtmlElement_UrlId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public ImageCollection ImagesByUrlId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("Image_UrlId"))
			{
				SetChildren();
			}

			var c = (ImageCollection)this.ChildCollections["Image_UrlId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		[Exclude]	
	public UrlTagCollection UrlTagsByUrlId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UrlTag_UrlId"))
			{
				SetChildren();
			}

			var c = (UrlTagCollection)this.ChildCollections["UrlTag_UrlId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		// Xref       
		public XrefDaoCollection<UrlTag, Tag> Tags
		{
			get
			{
				if(!this.ChildCollections.ContainsKey("Url_UrlTag_Tag"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<UrlTag, Tag>)this.ChildCollections["Url_UrlTag_Tag"];
				if(!xref.Loaded)
				{
					xref.Load();
				}

				return xref;
			}
		}

		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new UrlColumns();
			return (colFilter.Id == IdValue);
		}

		public static UrlCollection Where(Func<UrlColumns, QueryFilter<UrlColumns>> where, OrderBy<UrlColumns> orderBy = null)
		{
			return new UrlCollection(new Query<UrlColumns, Url>(where, orderBy), true);
		}
		
		public static UrlCollection Where(WhereDelegate<UrlColumns> where, Database db = null)
		{
			return new UrlCollection(new Query<UrlColumns, Url>(where, db), true);
		}
		   
		public static UrlCollection Where(WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy = null, Database db = null)
		{
			return new UrlCollection(new Query<UrlColumns, Url>(where, orderBy, db), true);
		}

		public static UrlCollection Where(QiQuery where, Database db = null)
		{
			return new UrlCollection(Select<UrlColumns>.From<Url>().Where(where, db));
		}

		public static Url OneWhere(WhereDelegate<UrlColumns> where, Database db = null)
		{
			var results = new UrlCollection(Select<UrlColumns>.From<Url>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Url OneWhere(QiQuery where, Database db = null)
		{
			var results = new UrlCollection(Select<UrlColumns>.From<Url>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Url OneOrThrow(UrlCollection c)
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

		public static Url FirstOneWhere(WhereDelegate<UrlColumns> where, Database db = null)
		{
			var results = new UrlCollection(Select<UrlColumns>.From<Url>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static UrlCollection Top(int count, WhereDelegate<UrlColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static UrlCollection Top(int count, WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy, Database database = null)
		{
			UrlColumns c = new UrlColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<Url>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<Url>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<UrlColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<UrlCollection>(0);
		}

		public static long Count(WhereDelegate<UrlColumns> where, Database database = null)
		{
			UrlColumns c = new UrlColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Url>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Url>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
