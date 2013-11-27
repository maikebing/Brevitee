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
	[Brevitee.Data.Table("ImageCrawler", "Analytics")]
	public partial class ImageCrawler: Dao
	{
		public ImageCrawler():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public ImageCrawler(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator ImageCrawler(DataRow data)
		{
			return new ImageCrawler(data);
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

	// property:RootUrl, columnName:RootUrl	
	[Brevitee.Data.Column(Name="RootUrl", ExtractedType="", MaxLength="", AllowNull=false)]
	public string RootUrl
	{
		get
		{
			return GetStringValue("RootUrl");
		}
		set
		{
			SetValue("RootUrl", value);
		}
	}



				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new ImageCrawlerColumns();
			return (colFilter.Id == IdValue);
		}

		public static ImageCrawlerCollection Where(Func<ImageCrawlerColumns, QueryFilter<ImageCrawlerColumns>> where, OrderBy<ImageCrawlerColumns> orderBy = null)
		{
			return new ImageCrawlerCollection(new Query<ImageCrawlerColumns, ImageCrawler>(where, orderBy), true);
		}
		
		public static ImageCrawlerCollection Where(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
		{
			return new ImageCrawlerCollection(new Query<ImageCrawlerColumns, ImageCrawler>(where, db), true);
		}
		   
		public static ImageCrawlerCollection Where(WhereDelegate<ImageCrawlerColumns> where, OrderBy<ImageCrawlerColumns> orderBy = null, Database db = null)
		{
			return new ImageCrawlerCollection(new Query<ImageCrawlerColumns, ImageCrawler>(where, orderBy, db), true);
		}

		public static ImageCrawlerCollection Where(QiQuery where, Database db = null)
		{
			return new ImageCrawlerCollection(Select<ImageCrawlerColumns>.From<ImageCrawler>().Where(where, db));
		}

		public static ImageCrawler OneWhere(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
		{
			var results = new ImageCrawlerCollection(Select<ImageCrawlerColumns>.From<ImageCrawler>().Where(where, db));
			return OneOrThrow(results);
		}

		public static ImageCrawler OneWhere(QiQuery where, Database db = null)
		{
			var results = new ImageCrawlerCollection(Select<ImageCrawlerColumns>.From<ImageCrawler>().Where(where, db));
			return OneOrThrow(results);
		}

		private static ImageCrawler OneOrThrow(ImageCrawlerCollection c)
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

		public static ImageCrawler FirstOneWhere(WhereDelegate<ImageCrawlerColumns> where, Database db = null)
		{
			var results = new ImageCrawlerCollection(Select<ImageCrawlerColumns>.From<ImageCrawler>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static ImageCrawlerCollection Top(int count, WhereDelegate<ImageCrawlerColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static ImageCrawlerCollection Top(int count, WhereDelegate<ImageCrawlerColumns> where, OrderBy<ImageCrawlerColumns> orderBy, Database database = null)
		{
			ImageCrawlerColumns c = new ImageCrawlerColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database == null ? _.Db.For<ImageCrawler>(): database;
			QuerySet query = GetQuerySet(db); 
			query.Top<ImageCrawler>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ImageCrawlerColumns>(orderBy);
			}

			query.Execute(db);
			return query.Results.As<ImageCrawlerCollection>(0);
		}

		public static long Count(WhereDelegate<ImageCrawlerColumns> where, Database database = null)
		{
			ImageCrawlerColumns c = new ImageCrawlerColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<ImageCrawler>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<ImageCrawler>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
	}
}																								
