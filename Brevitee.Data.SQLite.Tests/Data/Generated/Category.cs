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
    [Brevitee.Data.Table("Category", "Test")]
    public partial class Category: Dao
    {
        public Category():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Category(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("CategoryTag_CategoryId", new CategoryTagCollection(new Query<CategoryTagColumns, CategoryTag>((c) => c.CategoryId == this.Id), this, "CategoryId"));	
		}

	// property:Id, columnName:Id	
	[Brevitee.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="8")]
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
	[Brevitee.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
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


				
	
	public CategoryTagCollection CategoryTagCollectionByCategoryId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("CategoryTag_CategoryId"))
			{
				SetChildren();
			}

			var c = (CategoryTagCollection)this.ChildCollections["CategoryTag_CategoryId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new CategoryColumns();
			return (colFilter.Id == IdValue);
		}

		public static CategoryCollection Where(Func<CategoryColumns, QueryFilter<CategoryColumns>> where, OrderBy<CategoryColumns> orderBy = null)
		{
			return new CategoryCollection(new Query<CategoryColumns, Category>(where, orderBy), true);
		}
		
		public static CategoryCollection Where(WhereDelegate<CategoryColumns> where, Database db = null)
		{
			return new CategoryCollection(new Query<CategoryColumns, Category>(where, db), true);
		}
		   
		public static CategoryCollection Where(WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy = null, Database db = null)
		{
			return new CategoryCollection(new Query<CategoryColumns, Category>(where, orderBy, db), true);
		}

		public static CategoryCollection Where(QiQuery where, Database db = null)
		{
			return new CategoryCollection(Select<CategoryColumns>.From<Category>().Where(where, db));
		}

		public static Category OneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
		{
			var results = new CategoryCollection(Select<CategoryColumns>.From<Category>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Category OneWhere(QiQuery where, Database db = null)
		{
			var results = new CategoryCollection(Select<CategoryColumns>.From<Category>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Category OneOrThrow(CategoryCollection c)
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

		public static Category FirstOneWhere(WhereDelegate<CategoryColumns> where, Database db = null)
		{
			var results = new CategoryCollection(Select<CategoryColumns>.From<Category>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static CategoryCollection Top(int count, WhereDelegate<CategoryColumns> where, OrderBy<CategoryColumns> orderBy, Database database = null)
        {
            CategoryColumns c = new CategoryColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Category>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Category>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<CategoryColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<CategoryCollection>(0);
        }

		public static long Count(WhereDelegate<CategoryColumns> where, Database database = null)
		{
			CategoryColumns c = new CategoryColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Category>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Category>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
