// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Classification
{
	// schema = Classification 
    public static class ClassificationContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Classification";
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
	}﻿
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
	}    }
}																								
