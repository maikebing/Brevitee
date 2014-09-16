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
		public static string ConnectionName
		{
			get
			{
				return "Analytics";
			}
		}

		public static Database Db
		{
			get
			{
				return Brevitee.Data.Db.For(ConnectionName);
			}
		}

﻿
	public class CrawlerQueryContext
	{
			public CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Crawler.Where(where, db);
			}
		   
			public CrawlerCollection Where(WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy = null, Database db = null)
			{
				return Crawler.Where(where, orderBy, db);
			}

			public Crawler OneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Crawler.OneWhere(where, db);
			}
		
			public Crawler FirstOneWhere(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Crawler.FirstOneWhere(where, db);
			}

			public CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Crawler.Top(count, where, db);
			}

			public CrawlerCollection Top(int count, WhereDelegate<CrawlerColumns> where, OrderBy<CrawlerColumns> orderBy, Database db = null)
			{
				return Crawler.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CrawlerColumns> where, Database db = null)
			{
				return Crawler.Count(where, db);
			}
	}

	static CrawlerQueryContext _crawlers;
	static object _crawlersLock = new object();
	public static CrawlerQueryContext Crawlers
	{
		get
		{
			return _crawlersLock.DoubleCheckLock<CrawlerQueryContext>(ref _crawlers, () => new CrawlerQueryContext());
		}
	}﻿
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
	}﻿
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
	}﻿
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
	}﻿
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
	}﻿
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
	}﻿
	public class FragmentQueryContext
	{
			public FragmentCollection Where(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Fragment.Where(where, db);
			}
		   
			public FragmentCollection Where(WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy = null, Database db = null)
			{
				return Fragment.Where(where, orderBy, db);
			}

			public Fragment OneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Fragment.OneWhere(where, db);
			}
		
			public Fragment FirstOneWhere(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Fragment.FirstOneWhere(where, db);
			}

			public FragmentCollection Top(int count, WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Fragment.Top(count, where, db);
			}

			public FragmentCollection Top(int count, WhereDelegate<FragmentColumns> where, OrderBy<FragmentColumns> orderBy, Database db = null)
			{
				return Fragment.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<FragmentColumns> where, Database db = null)
			{
				return Fragment.Count(where, db);
			}
	}

	static FragmentQueryContext _fragments;
	static object _fragmentsLock = new object();
	public static FragmentQueryContext Fragments
	{
		get
		{
			return _fragmentsLock.DoubleCheckLock<FragmentQueryContext>(ref _fragments, () => new FragmentQueryContext());
		}
	}﻿
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
	}﻿
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
	}﻿
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
	}﻿
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
	}﻿
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
	}    }
}																								
