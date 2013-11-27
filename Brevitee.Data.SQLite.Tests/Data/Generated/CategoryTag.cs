using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema; 
using Brevitee.Data.Qi;
using Brevitee.ServiceProxy;
using Brevitee.ServiceProxy.Js;

namespace SampleData
{
    [Brevitee.Data.Table("CategoryTag", "Test")]
    public partial class CategoryTag: Dao
    {
        public CategoryTag():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public CategoryTag(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

		}

	// property:Id, columnName:Id	
	[Brevitee.Data.KeyColumn(Name="Id", ExtractedType="BigInt", MaxLength="8")]
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


	// start CategoryId -> CategoryId
	[Brevitee.Data.ForeignKey(
        Table="CategoryTag",
		Name="CategoryId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
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
				_categoryOfCategoryId = SampleData.Category.OneWhere(f => f.Id == this.CategoryId);
			}
			return _categoryOfCategoryId;
		}
	}
	
	// start TagId -> TagId
	[Brevitee.Data.ForeignKey(
        Table="CategoryTag",
		Name="TagId", 
		ExtractedType="BigInt", 
		MaxLength="8",
		AllowNull=false, 
		ReferencedKey="Id",
		ReferencedTable="Tag",
		Suffix="2")]
	public long? TagId
	{
		get
		{
			return GetLongValue("TagId");
		}
		set
		{
			SetValue("TagId", value);
		}
	}

	Tag _tagOfTagId;
	public Tag TagOfTagId
	{
		get
		{
			if(_tagOfTagId == null)
			{
				_tagOfTagId = SampleData.Tag.OneWhere(f => f.Id == this.TagId);
			}
			return _tagOfTagId;
		}
	}
	
				
		
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CategoryTagColumns();
			return (colFilter.Id == IdValue);
		}

		public static CategoryTagCollection Where(Func<CategoryTagColumns, QueryFilter<CategoryTagColumns>> where, OrderBy<CategoryTagColumns> orderBy = null)
		{
			return new CategoryTagCollection(new Query<CategoryTagColumns, CategoryTag>(where, orderBy), true);
		}
		
		public static CategoryTagCollection Where(WhereDelegate<CategoryTagColumns> where, Database db = null)
		{
			return new CategoryTagCollection(new Query<CategoryTagColumns, CategoryTag>(where, db), true);
		}
		   
		public static CategoryTagCollection Where(WhereDelegate<CategoryTagColumns> where, OrderBy<CategoryTagColumns> orderBy = null, Database db = null)
		{
			return new CategoryTagCollection(new Query<CategoryTagColumns, CategoryTag>(where, orderBy, db), true);
		}

		public static CategoryTagCollection Where(QiQuery where, Database db = null)
		{
			return new CategoryTagCollection(Select<CategoryTagColumns>.From<CategoryTag>().Where(where, db));
		}

		public static CategoryTag OneWhere(WhereDelegate<CategoryTagColumns> where, Database db = null)
		{
			var results = new CategoryTagCollection(Select<CategoryTagColumns>.From<CategoryTag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static CategoryTag OneWhere(QiQuery where, Database db = null)
		{
			var results = new CategoryTagCollection(Select<CategoryTagColumns>.From<CategoryTag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static CategoryTag OneOrThrow(CategoryTagCollection c)
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

		public static CategoryTag FirstOneWhere(WhereDelegate<CategoryTagColumns> where, Database db = null)
		{
			var results = new CategoryTagCollection(Select<CategoryTagColumns>.From<CategoryTag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static CategoryTagCollection Top(int count, WhereDelegate<CategoryTagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static CategoryTagCollection Top(int count, WhereDelegate<CategoryTagColumns> where, OrderBy<CategoryTagColumns> orderBy, Database database = null)
        {
            CategoryTagColumns c = new CategoryTagColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? _.Db.For<CategoryTag>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<CategoryTag>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryTagColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<CategoryTagCollection>(0);
        }

		public static long Count(WhereDelegate<CategoryTagColumns> where, Database database = null)
		{
			CategoryTagColumns c = new CategoryTagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? _.Db.For<CategoryTag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<CategoryTag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
