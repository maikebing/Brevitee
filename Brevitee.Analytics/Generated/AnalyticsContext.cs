// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Data
{
	// schema = Analytics 
    public static class AnalyticsContext
    {
		public static Database Db
		{
			get
			{
				return _.Db.For("Analytics");
			}
		}


	public class WriteHistoryQueryContext
	{
			public WriteHistoryCollection Where(WhereDelegate<WriteHistoryColumns> where, Database db = null)
			{
				return WriteHistory.Where(where, db);
			}
		   
			public WriteHistoryCollection Where(WhereDelegate<WriteHistoryColumns> where, OrderBy<WriteHistoryColumns> orderBy = null, Database db = null)
			{
				return WriteHistory.Where(where, orderBy, db);
			}

			public WriteHistory OneWhere(WhereDelegate<WriteHistoryColumns> where, Database db = null)
			{
				return WriteHistory.OneWhere(where, db);
			}
		
			public WriteHistory FirstOneWhere(WhereDelegate<WriteHistoryColumns> where, Database db = null)
			{
				return WriteHistory.FirstOneWhere(where, db);
			}

			public WriteHistoryCollection Top(int count, WhereDelegate<WriteHistoryColumns> where, Database db = null)
			{
				return WriteHistory.Top(count, where, db);
			}

			public WriteHistoryCollection Top(int count, WhereDelegate<WriteHistoryColumns> where, OrderBy<WriteHistoryColumns> orderBy, Database db = null)
			{
				return WriteHistory.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<WriteHistoryColumns> where, Database db = null)
			{
				return WriteHistory.Count(where, db);
			}
	}

	static WriteHistoryQueryContext _writeHistories;
	static object _writeHistoriesLock = new object();
	public static WriteHistoryQueryContext WriteHistories
	{
		get
		{
			return _writeHistoriesLock.DoubleCheckLock<WriteHistoryQueryContext>(ref _writeHistories, () => new WriteHistoryQueryContext());
		}
	}
	public class UserIdentifierQueryContext
	{
			public UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return UserIdentifier.Where(where, db);
			}
		   
			public UserIdentifierCollection Where(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy = null, Database db = null)
			{
				return UserIdentifier.Where(where, orderBy, db);
			}

			public UserIdentifier OneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return UserIdentifier.OneWhere(where, db);
			}
		
			public UserIdentifier FirstOneWhere(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return UserIdentifier.FirstOneWhere(where, db);
			}

			public UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return UserIdentifier.Top(count, where, db);
			}

			public UserIdentifierCollection Top(int count, WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy, Database db = null)
			{
				return UserIdentifier.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserIdentifierColumns> where, Database db = null)
			{
				return UserIdentifier.Count(where, db);
			}
	}

	static UserIdentifierQueryContext _userIdentifiers;
	static object _userIdentifiersLock = new object();
	public static UserIdentifierQueryContext UserIdentifiers
	{
		get
		{
			return _userIdentifiersLock.DoubleCheckLock<UserIdentifierQueryContext>(ref _userIdentifiers, () => new UserIdentifierQueryContext());
		}
	}
	public class ImageRatingQueryContext
	{
			public ImageRatingCollection Where(WhereDelegate<ImageRatingColumns> where, Database db = null)
			{
				return ImageRating.Where(where, db);
			}
		   
			public ImageRatingCollection Where(WhereDelegate<ImageRatingColumns> where, OrderBy<ImageRatingColumns> orderBy = null, Database db = null)
			{
				return ImageRating.Where(where, orderBy, db);
			}

			public ImageRating OneWhere(WhereDelegate<ImageRatingColumns> where, Database db = null)
			{
				return ImageRating.OneWhere(where, db);
			}
		
			public ImageRating FirstOneWhere(WhereDelegate<ImageRatingColumns> where, Database db = null)
			{
				return ImageRating.FirstOneWhere(where, db);
			}

			public ImageRatingCollection Top(int count, WhereDelegate<ImageRatingColumns> where, Database db = null)
			{
				return ImageRating.Top(count, where, db);
			}

			public ImageRatingCollection Top(int count, WhereDelegate<ImageRatingColumns> where, OrderBy<ImageRatingColumns> orderBy, Database db = null)
			{
				return ImageRating.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ImageRatingColumns> where, Database db = null)
			{
				return ImageRating.Count(where, db);
			}
	}

	static ImageRatingQueryContext _imageRatings;
	static object _imageRatingsLock = new object();
	public static ImageRatingQueryContext ImageRatings
	{
		get
		{
			return _imageRatingsLock.DoubleCheckLock<ImageRatingQueryContext>(ref _imageRatings, () => new ImageRatingQueryContext());
		}
	}
	public class ImageCrawlerQueryContext
	{
			public ImageCrawlerCollection Where(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
			{
				return ImageCrawler.Where(where, db);
			}
		   
			public ImageCrawlerCollection Where(WhereDelegate<ImageCrawlerColumns> where, OrderBy<ImageCrawlerColumns> orderBy = null, Database db = null)
			{
				return ImageCrawler.Where(where, orderBy, db);
			}

			public ImageCrawler OneWhere(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
			{
				return ImageCrawler.OneWhere(where, db);
			}
		
			public ImageCrawler FirstOneWhere(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
			{
				return ImageCrawler.FirstOneWhere(where, db);
			}

			public ImageCrawlerCollection Top(int count, WhereDelegate<ImageCrawlerColumns> where, Database db = null)
			{
				return ImageCrawler.Top(count, where, db);
			}

			public ImageCrawlerCollection Top(int count, WhereDelegate<ImageCrawlerColumns> where, OrderBy<ImageCrawlerColumns> orderBy, Database db = null)
			{
				return ImageCrawler.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
			{
				return ImageCrawler.Count(where, db);
			}
	}

	static ImageCrawlerQueryContext _imageCrawlers;
	static object _imageCrawlersLock = new object();
	public static ImageCrawlerQueryContext ImageCrawlers
	{
		get
		{
			return _imageCrawlersLock.DoubleCheckLock<ImageCrawlerQueryContext>(ref _imageCrawlers, () => new ImageCrawlerQueryContext());
		}
	}
	public class HtmlElementQueryContext
	{
			public HtmlElementCollection Where(WhereDelegate<HtmlElementColumns> where, Database db = null)
			{
				return HtmlElement.Where(where, db);
			}
		   
			public HtmlElementCollection Where(WhereDelegate<HtmlElementColumns> where, OrderBy<HtmlElementColumns> orderBy = null, Database db = null)
			{
				return HtmlElement.Where(where, orderBy, db);
			}

			public HtmlElement OneWhere(WhereDelegate<HtmlElementColumns> where, Database db = null)
			{
				return HtmlElement.OneWhere(where, db);
			}
		
			public HtmlElement FirstOneWhere(WhereDelegate<HtmlElementColumns> where, Database db = null)
			{
				return HtmlElement.FirstOneWhere(where, db);
			}

			public HtmlElementCollection Top(int count, WhereDelegate<HtmlElementColumns> where, Database db = null)
			{
				return HtmlElement.Top(count, where, db);
			}

			public HtmlElementCollection Top(int count, WhereDelegate<HtmlElementColumns> where, OrderBy<HtmlElementColumns> orderBy, Database db = null)
			{
				return HtmlElement.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HtmlElementColumns> where, Database db = null)
			{
				return HtmlElement.Count(where, db);
			}
	}

	static HtmlElementQueryContext _htmlElements;
	static object _htmlElementsLock = new object();
	public static HtmlElementQueryContext HtmlElements
	{
		get
		{
			return _htmlElementsLock.DoubleCheckLock<HtmlElementQueryContext>(ref _htmlElements, () => new HtmlElementQueryContext());
		}
	}
	public class ContentQueryContext
	{
			public ContentCollection Where(WhereDelegate<ContentColumns> where, Database db = null)
			{
				return Content.Where(where, db);
			}
		   
			public ContentCollection Where(WhereDelegate<ContentColumns> where, OrderBy<ContentColumns> orderBy = null, Database db = null)
			{
				return Content.Where(where, orderBy, db);
			}

			public Content OneWhere(WhereDelegate<ContentColumns> where, Database db = null)
			{
				return Content.OneWhere(where, db);
			}
		
			public Content FirstOneWhere(WhereDelegate<ContentColumns> where, Database db = null)
			{
				return Content.FirstOneWhere(where, db);
			}

			public ContentCollection Top(int count, WhereDelegate<ContentColumns> where, Database db = null)
			{
				return Content.Top(count, where, db);
			}

			public ContentCollection Top(int count, WhereDelegate<ContentColumns> where, OrderBy<ContentColumns> orderBy, Database db = null)
			{
				return Content.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ContentColumns> where, Database db = null)
			{
				return Content.Count(where, db);
			}
	}

	static ContentQueryContext _contents;
	static object _contentsLock = new object();
	public static ContentQueryContext Contents
	{
		get
		{
			return _contentsLock.DoubleCheckLock<ContentQueryContext>(ref _contents, () => new ContentQueryContext());
		}
	}
	public class StyleQueryContext
	{
			public StyleCollection Where(WhereDelegate<StyleColumns> where, Database db = null)
			{
				return Style.Where(where, db);
			}
		   
			public StyleCollection Where(WhereDelegate<StyleColumns> where, OrderBy<StyleColumns> orderBy = null, Database db = null)
			{
				return Style.Where(where, orderBy, db);
			}

			public Style OneWhere(WhereDelegate<StyleColumns> where, Database db = null)
			{
				return Style.OneWhere(where, db);
			}
		
			public Style FirstOneWhere(WhereDelegate<StyleColumns> where, Database db = null)
			{
				return Style.FirstOneWhere(where, db);
			}

			public StyleCollection Top(int count, WhereDelegate<StyleColumns> where, Database db = null)
			{
				return Style.Top(count, where, db);
			}

			public StyleCollection Top(int count, WhereDelegate<StyleColumns> where, OrderBy<StyleColumns> orderBy, Database db = null)
			{
				return Style.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StyleColumns> where, Database db = null)
			{
				return Style.Count(where, db);
			}
	}

	static StyleQueryContext _styles;
	static object _stylesLock = new object();
	public static StyleQueryContext Styles
	{
		get
		{
			return _stylesLock.DoubleCheckLock<StyleQueryContext>(ref _styles, () => new StyleQueryContext());
		}
	}
	public class AttributeQueryContext
	{
			public AttributeCollection Where(WhereDelegate<AttributeColumns> where, Database db = null)
			{
				return Attribute.Where(where, db);
			}
		   
			public AttributeCollection Where(WhereDelegate<AttributeColumns> where, OrderBy<AttributeColumns> orderBy = null, Database db = null)
			{
				return Attribute.Where(where, orderBy, db);
			}

			public Attribute OneWhere(WhereDelegate<AttributeColumns> where, Database db = null)
			{
				return Attribute.OneWhere(where, db);
			}
		
			public Attribute FirstOneWhere(WhereDelegate<AttributeColumns> where, Database db = null)
			{
				return Attribute.FirstOneWhere(where, db);
			}

			public AttributeCollection Top(int count, WhereDelegate<AttributeColumns> where, Database db = null)
			{
				return Attribute.Top(count, where, db);
			}

			public AttributeCollection Top(int count, WhereDelegate<AttributeColumns> where, OrderBy<AttributeColumns> orderBy, Database db = null)
			{
				return Attribute.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AttributeColumns> where, Database db = null)
			{
				return Attribute.Count(where, db);
			}
	}

	static AttributeQueryContext _attributes;
	static object _attributesLock = new object();
	public static AttributeQueryContext Attributes
	{
		get
		{
			return _attributesLock.DoubleCheckLock<AttributeQueryContext>(ref _attributes, () => new AttributeQueryContext());
		}
	}
	public class CategoryQueryContext
	{
			public CategoryCollection Where(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Category.Where(where, db);
			}
		   
			public CategoryCollection Where(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database db = null)
			{
				return Category.Where(where, orderBy, db);
			}

			public Category OneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Category.OneWhere(where, db);
			}
		
			public Category FirstOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Category.FirstOneWhere(where, db);
			}

			public CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Category.Top(count, where, db);
			}

			public CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database db = null)
			{
				return Category.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CategoryColumns> where, Database db = null)
			{
				return Category.Count(where, db);
			}
	}

	static CategoryQueryContext _categories;
	static object _categoriesLock = new object();
	public static CategoryQueryContext Categories
	{
		get
		{
			return _categoriesLock.DoubleCheckLock<CategoryQueryContext>(ref _categories, () => new CategoryQueryContext());
		}
	}
	public class FeatureQueryContext
	{
			public FeatureCollection Where(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Feature.Where(where, db);
			}
		   
			public FeatureCollection Where(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy = null, Database db = null)
			{
				return Feature.Where(where, orderBy, db);
			}

			public Feature OneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Feature.OneWhere(where, db);
			}
		
			public Feature FirstOneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Feature.FirstOneWhere(where, db);
			}

			public FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Feature.Top(count, where, db);
			}

			public FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy, Database db = null)
			{
				return Feature.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<FeatureColumns> where, Database db = null)
			{
				return Feature.Count(where, db);
			}
	}

	static FeatureQueryContext _features;
	static object _featuresLock = new object();
	public static FeatureQueryContext Features
	{
		get
		{
			return _featuresLock.DoubleCheckLock<FeatureQueryContext>(ref _features, () => new FeatureQueryContext());
		}
	}
	public class ProtocolQueryContext
	{
			public ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Protocol.Where(where, db);
			}
		   
			public ProtocolCollection Where(WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy = null, Database db = null)
			{
				return Protocol.Where(where, orderBy, db);
			}

			public Protocol OneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Protocol.OneWhere(where, db);
			}
		
			public Protocol FirstOneWhere(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Protocol.FirstOneWhere(where, db);
			}

			public ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Protocol.Top(count, where, db);
			}

			public ProtocolCollection Top(int count, WhereDelegate<ProtocolColumns> where, OrderBy<ProtocolColumns> orderBy, Database db = null)
			{
				return Protocol.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ProtocolColumns> where, Database db = null)
			{
				return Protocol.Count(where, db);
			}
	}

	static ProtocolQueryContext _protocols;
	static object _protocolsLock = new object();
	public static ProtocolQueryContext Protocols
	{
		get
		{
			return _protocolsLock.DoubleCheckLock<ProtocolQueryContext>(ref _protocols, () => new ProtocolQueryContext());
		}
	}
	public class DomainQueryContext
	{
			public DomainCollection Where(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Domain.Where(where, db);
			}
		   
			public DomainCollection Where(WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy = null, Database db = null)
			{
				return Domain.Where(where, orderBy, db);
			}

			public Domain OneWhere(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Domain.OneWhere(where, db);
			}
		
			public Domain FirstOneWhere(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Domain.FirstOneWhere(where, db);
			}

			public DomainCollection Top(int count, WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Domain.Top(count, where, db);
			}

			public DomainCollection Top(int count, WhereDelegate<DomainColumns> where, OrderBy<DomainColumns> orderBy, Database db = null)
			{
				return Domain.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<DomainColumns> where, Database db = null)
			{
				return Domain.Count(where, db);
			}
	}

	static DomainQueryContext _domains;
	static object _domainsLock = new object();
	public static DomainQueryContext Domains
	{
		get
		{
			return _domainsLock.DoubleCheckLock<DomainQueryContext>(ref _domains, () => new DomainQueryContext());
		}
	}
	public class PortQueryContext
	{
			public PortCollection Where(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Port.Where(where, db);
			}
		   
			public PortCollection Where(WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy = null, Database db = null)
			{
				return Port.Where(where, orderBy, db);
			}

			public Port OneWhere(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Port.OneWhere(where, db);
			}
		
			public Port FirstOneWhere(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Port.FirstOneWhere(where, db);
			}

			public PortCollection Top(int count, WhereDelegate<PortColumns> where, Database db = null)
			{
				return Port.Top(count, where, db);
			}

			public PortCollection Top(int count, WhereDelegate<PortColumns> where, OrderBy<PortColumns> orderBy, Database db = null)
			{
				return Port.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PortColumns> where, Database db = null)
			{
				return Port.Count(where, db);
			}
	}

	static PortQueryContext _ports;
	static object _portsLock = new object();
	public static PortQueryContext Ports
	{
		get
		{
			return _portsLock.DoubleCheckLock<PortQueryContext>(ref _ports, () => new PortQueryContext());
		}
	}
	public class PathQueryContext
	{
			public PathCollection Where(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Path.Where(where, db);
			}
		   
			public PathCollection Where(WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy = null, Database db = null)
			{
				return Path.Where(where, orderBy, db);
			}

			public Path OneWhere(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Path.OneWhere(where, db);
			}
		
			public Path FirstOneWhere(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Path.FirstOneWhere(where, db);
			}

			public PathCollection Top(int count, WhereDelegate<PathColumns> where, Database db = null)
			{
				return Path.Top(count, where, db);
			}

			public PathCollection Top(int count, WhereDelegate<PathColumns> where, OrderBy<PathColumns> orderBy, Database db = null)
			{
				return Path.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PathColumns> where, Database db = null)
			{
				return Path.Count(where, db);
			}
	}

	static PathQueryContext _paths;
	static object _pathsLock = new object();
	public static PathQueryContext Paths
	{
		get
		{
			return _pathsLock.DoubleCheckLock<PathQueryContext>(ref _paths, () => new PathQueryContext());
		}
	}
	public class QueryStringQueryContext
	{
			public QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return QueryString.Where(where, db);
			}
		   
			public QueryStringCollection Where(WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy = null, Database db = null)
			{
				return QueryString.Where(where, orderBy, db);
			}

			public QueryString OneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return QueryString.OneWhere(where, db);
			}
		
			public QueryString FirstOneWhere(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return QueryString.FirstOneWhere(where, db);
			}

			public QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return QueryString.Top(count, where, db);
			}

			public QueryStringCollection Top(int count, WhereDelegate<QueryStringColumns> where, OrderBy<QueryStringColumns> orderBy, Database db = null)
			{
				return QueryString.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<QueryStringColumns> where, Database db = null)
			{
				return QueryString.Count(where, db);
			}
	}

	static QueryStringQueryContext _queryStrings;
	static object _queryStringsLock = new object();
	public static QueryStringQueryContext QueryStrings
	{
		get
		{
			return _queryStringsLock.DoubleCheckLock<QueryStringQueryContext>(ref _queryStrings, () => new QueryStringQueryContext());
		}
	}
	public class UrlQueryContext
	{
			public UrlCollection Where(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Url.Where(where, db);
			}
		   
			public UrlCollection Where(WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy = null, Database db = null)
			{
				return Url.Where(where, orderBy, db);
			}

			public Url OneWhere(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Url.OneWhere(where, db);
			}
		
			public Url FirstOneWhere(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Url.FirstOneWhere(where, db);
			}

			public UrlCollection Top(int count, WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Url.Top(count, where, db);
			}

			public UrlCollection Top(int count, WhereDelegate<UrlColumns> where, OrderBy<UrlColumns> orderBy, Database db = null)
			{
				return Url.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UrlColumns> where, Database db = null)
			{
				return Url.Count(where, db);
			}
	}

	static UrlQueryContext _urls;
	static object _urlsLock = new object();
	public static UrlQueryContext Urls
	{
		get
		{
			return _urlsLock.DoubleCheckLock<UrlQueryContext>(ref _urls, () => new UrlQueryContext());
		}
	}
	public class TagQueryContext
	{
			public TagCollection Where(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Tag.Where(where, db);
			}
		   
			public TagCollection Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null)
			{
				return Tag.Where(where, orderBy, db);
			}

			public Tag OneWhere(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Tag.OneWhere(where, db);
			}
		
			public Tag FirstOneWhere(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Tag.FirstOneWhere(where, db);
			}

			public TagCollection Top(int count, WhereDelegate<TagColumns> where, Database db = null)
			{
				return Tag.Top(count, where, db);
			}

			public TagCollection Top(int count, WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database db = null)
			{
				return Tag.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TagColumns> where, Database db = null)
			{
				return Tag.Count(where, db);
			}
	}

	static TagQueryContext _tags;
	static object _tagsLock = new object();
	public static TagQueryContext Tags
	{
		get
		{
			return _tagsLock.DoubleCheckLock<TagQueryContext>(ref _tags, () => new TagQueryContext());
		}
	}
	public class ImageQueryContext
	{
			public ImageCollection Where(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Image.Where(where, db);
			}
		   
			public ImageCollection Where(WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy = null, Database db = null)
			{
				return Image.Where(where, orderBy, db);
			}

			public Image OneWhere(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Image.OneWhere(where, db);
			}
		
			public Image FirstOneWhere(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Image.FirstOneWhere(where, db);
			}

			public ImageCollection Top(int count, WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Image.Top(count, where, db);
			}

			public ImageCollection Top(int count, WhereDelegate<ImageColumns> where, OrderBy<ImageColumns> orderBy, Database db = null)
			{
				return Image.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ImageColumns> where, Database db = null)
			{
				return Image.Count(where, db);
			}
	}

	static ImageQueryContext _images;
	static object _imagesLock = new object();
	public static ImageQueryContext Images
	{
		get
		{
			return _imagesLock.DoubleCheckLock<ImageQueryContext>(ref _images, () => new ImageQueryContext());
		}
	}
	public class UrlTagQueryContext
	{
			public UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return UrlTag.Where(where, db);
			}
		   
			public UrlTagCollection Where(WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy = null, Database db = null)
			{
				return UrlTag.Where(where, orderBy, db);
			}

			public UrlTag OneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return UrlTag.OneWhere(where, db);
			}
		
			public UrlTag FirstOneWhere(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return UrlTag.FirstOneWhere(where, db);
			}

			public UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return UrlTag.Top(count, where, db);
			}

			public UrlTagCollection Top(int count, WhereDelegate<UrlTagColumns> where, OrderBy<UrlTagColumns> orderBy, Database db = null)
			{
				return UrlTag.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UrlTagColumns> where, Database db = null)
			{
				return UrlTag.Count(where, db);
			}
	}

	static UrlTagQueryContext _urlTags;
	static object _urlTagsLock = new object();
	public static UrlTagQueryContext UrlTags
	{
		get
		{
			return _urlTagsLock.DoubleCheckLock<UrlTagQueryContext>(ref _urlTags, () => new UrlTagQueryContext());
		}
	}
	public class ImageTagQueryContext
	{
			public ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return ImageTag.Where(where, db);
			}
		   
			public ImageTagCollection Where(WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy = null, Database db = null)
			{
				return ImageTag.Where(where, orderBy, db);
			}

			public ImageTag OneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return ImageTag.OneWhere(where, db);
			}
		
			public ImageTag FirstOneWhere(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return ImageTag.FirstOneWhere(where, db);
			}

			public ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return ImageTag.Top(count, where, db);
			}

			public ImageTagCollection Top(int count, WhereDelegate<ImageTagColumns> where, OrderBy<ImageTagColumns> orderBy, Database db = null)
			{
				return ImageTag.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ImageTagColumns> where, Database db = null)
			{
				return ImageTag.Count(where, db);
			}
	}

	static ImageTagQueryContext _imageTags;
	static object _imageTagsLock = new object();
	public static ImageTagQueryContext ImageTags
	{
		get
		{
			return _imageTagsLock.DoubleCheckLock<ImageTagQueryContext>(ref _imageTags, () => new ImageTagQueryContext());
		}
	}
	public class HtmlElementStyleQueryContext
	{
			public HtmlElementStyleCollection Where(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
			{
				return HtmlElementStyle.Where(where, db);
			}
		   
			public HtmlElementStyleCollection Where(WhereDelegate<HtmlElementStyleColumns> where, OrderBy<HtmlElementStyleColumns> orderBy = null, Database db = null)
			{
				return HtmlElementStyle.Where(where, orderBy, db);
			}

			public HtmlElementStyle OneWhere(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
			{
				return HtmlElementStyle.OneWhere(where, db);
			}
		
			public HtmlElementStyle FirstOneWhere(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
			{
				return HtmlElementStyle.FirstOneWhere(where, db);
			}

			public HtmlElementStyleCollection Top(int count, WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
			{
				return HtmlElementStyle.Top(count, where, db);
			}

			public HtmlElementStyleCollection Top(int count, WhereDelegate<HtmlElementStyleColumns> where, OrderBy<HtmlElementStyleColumns> orderBy, Database db = null)
			{
				return HtmlElementStyle.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HtmlElementStyleColumns> where, Database db = null)
			{
				return HtmlElementStyle.Count(where, db);
			}
	}

	static HtmlElementStyleQueryContext _htmlElementStyles;
	static object _htmlElementStylesLock = new object();
	public static HtmlElementStyleQueryContext HtmlElementStyles
	{
		get
		{
			return _htmlElementStylesLock.DoubleCheckLock<HtmlElementStyleQueryContext>(ref _htmlElementStyles, () => new HtmlElementStyleQueryContext());
		}
	}
	public class HtmlElementAttributeQueryContext
	{
			public HtmlElementAttributeCollection Where(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
			{
				return HtmlElementAttribute.Where(where, db);
			}
		   
			public HtmlElementAttributeCollection Where(WhereDelegate<HtmlElementAttributeColumns> where, OrderBy<HtmlElementAttributeColumns> orderBy = null, Database db = null)
			{
				return HtmlElementAttribute.Where(where, orderBy, db);
			}

			public HtmlElementAttribute OneWhere(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
			{
				return HtmlElementAttribute.OneWhere(where, db);
			}
		
			public HtmlElementAttribute FirstOneWhere(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
			{
				return HtmlElementAttribute.FirstOneWhere(where, db);
			}

			public HtmlElementAttributeCollection Top(int count, WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
			{
				return HtmlElementAttribute.Top(count, where, db);
			}

			public HtmlElementAttributeCollection Top(int count, WhereDelegate<HtmlElementAttributeColumns> where, OrderBy<HtmlElementAttributeColumns> orderBy, Database db = null)
			{
				return HtmlElementAttribute.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<HtmlElementAttributeColumns> where, Database db = null)
			{
				return HtmlElementAttribute.Count(where, db);
			}
	}

	static HtmlElementAttributeQueryContext _htmlElementAttributes;
	static object _htmlElementAttributesLock = new object();
	public static HtmlElementAttributeQueryContext HtmlElementAttributes
	{
		get
		{
			return _htmlElementAttributesLock.DoubleCheckLock<HtmlElementAttributeQueryContext>(ref _htmlElementAttributes, () => new HtmlElementAttributeQueryContext());
		}
	}    }
}																								
