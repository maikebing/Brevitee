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
    [Brevitee.Data.Table("Feature", "Analytics")]
    public partial class Feature: Dao
    {
        public Feature():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Feature(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		public static implicit operator Feature(DataRow data)
        {
            return new Feature(data);
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

	// property:Value, columnName:Value	
	[Brevitee.Data.Column(Name="Value", ExtractedType="", MaxLength="", AllowNull=false)]
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

	// property:FeatureToCategoryCount, columnName:FeatureToCategoryCount	
	[Brevitee.Data.Column(Name="FeatureToCategoryCount", ExtractedType="", MaxLength="", AllowNull=false)]
	public long? FeatureToCategoryCount
	{
		get
		{
			return GetLongValue("FeatureToCategoryCount");
		}
		set
		{
			SetValue("FeatureToCategoryCount", value);
		}
	}



	// start CategoryId -> CategoryId
	[Brevitee.Data.ForeignKey(
        Table="Feature",
		Name="CategoryId", 
		ExtractedType="", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="Category",
		Suffix="1")]
	public long? CategoryId
	{
		get
		{
			return GetLongValue("CategoryId");
		}
		set
		{
			SetValue("CategoryId", value);
		}
	}

	Category _categoryOfCategoryId;
	public Category CategoryOfCategoryId
	{
		get
		{
			if(_categoryOfCategoryId == null)
			{
				_categoryOfCategoryId = Brevitee.Analytics.Data.Category.OneWhere(f => f.Id == this.CategoryId);
			}
			return _categoryOfCategoryId;
		}
	}
	
				
		


		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new FeatureColumns();
			return (colFilter.Id == IdValue);
		}

		public static FeatureCollection Where(Func<FeatureColumns, QueryFilter<FeatureColumns>> where, OrderBy<FeatureColumns> orderBy = null)
		{
			return new FeatureCollection(new Query<FeatureColumns, Feature>(where, orderBy), true);
		}
		
		public static FeatureCollection Where(WhereDelegate<FeatureColumns> where, Database db = null)
		{
			return new FeatureCollection(new Query<FeatureColumns, Feature>(where, db), true);
		}
		   
		public static FeatureCollection Where(WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy = null, Database db = null)
		{
			return new FeatureCollection(new Query<FeatureColumns, Feature>(where, orderBy, db), true);
		}

		public static FeatureCollection Where(QiQuery where, Database db = null)
		{
			return new FeatureCollection(Select<FeatureColumns>.From<Feature>().Where(where, db));
		}

		public static Feature OneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
		{
			var results = new FeatureCollection(Select<FeatureColumns>.From<Feature>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Feature OneWhere(QiQuery where, Database db = null)
		{
			var results = new FeatureCollection(Select<FeatureColumns>.From<Feature>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Feature OneOrThrow(FeatureCollection c)
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

		public static Feature FirstOneWhere(WhereDelegate<FeatureColumns> where, Database db = null)
		{
			var results = new FeatureCollection(Select<FeatureColumns>.From<Feature>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static FeatureCollection Top(int count, WhereDelegate<FeatureColumns> where, OrderBy<FeatureColumns> orderBy, Database database = null)
        {
            FeatureColumns c = new FeatureColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<Feature>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Feature>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<FeatureColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<FeatureCollection>(0);
        }

		public static long Count(WhereDelegate<FeatureColumns> where, Database database = null)
		{
			FeatureColumns c = new FeatureColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<Feature>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Feature>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
