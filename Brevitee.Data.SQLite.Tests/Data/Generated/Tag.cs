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
    [Brevitee.Data.Table("Tag", "Test")]
    public partial class Tag: Dao
    {
        public Tag():base()
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

        public Tag(DataRow data): base(data)
		{
			this.KeyColumnName = "Id";
			this.SetChildren();
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("CategoryTag_TagId", new CategoryTagCollection(new Query<CategoryTagColumns, CategoryTag>((c) => c.TagId == this.Id), this, "TagId"));	
            this.ChildCollections.Add("ItemTag_TagId", new ItemTagCollection(new Query<ItemTagColumns, ItemTag>((c) => c.TagId == this.Id), this, "TagId"));	
            this.ChildCollections.Add("UserTag_TagId", new UserTagCollection(new Query<UserTagColumns, UserTag>((c) => c.TagId == this.Id), this, "TagId"));	
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


				
	
	public CategoryTagCollection CategoryTagCollectionByTagId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("CategoryTag_TagId"))
			{
				SetChildren();
			}

			var c = (CategoryTagCollection)this.ChildCollections["CategoryTag_TagId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public ItemTagCollection ItemTagCollectionByTagId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("ItemTag_TagId"))
			{
				SetChildren();
			}

			var c = (ItemTagCollection)this.ChildCollections["ItemTag_TagId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
		
	public UserTagCollection UserTagCollectionByTagId
	{
		get
		{
			if(!this.ChildCollections.ContainsKey("UserTag_TagId"))
			{
				SetChildren();
			}

			var c = (UserTagCollection)this.ChildCollections["UserTag_TagId"];
			if(!c.Loaded)
			{
				c.Load();
			}
			return c;
		}
	}
			
		public override IQueryFilter GetUniqueFilter()
		{
			var colFilter = new TagColumns();
			return (colFilter.Id == IdValue);
		}

		public static TagCollection Where(Func<TagColumns, QueryFilter<TagColumns>> where, OrderBy<TagColumns> orderBy = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, orderBy), true);
		}
		
		public static TagCollection Where(WhereDelegate<TagColumns> where, Database db = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, db), true);
		}
		   
		public static TagCollection Where(WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy = null, Database db = null)
		{
			return new TagCollection(new Query<TagColumns, Tag>(where, orderBy, db), true);
		}

		public static TagCollection Where(QiQuery where, Database db = null)
		{
			return new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
		}

		public static Tag OneWhere(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
			return OneOrThrow(results);
		}

		public static Tag OneWhere(QiQuery where, Database db = null)
		{
			var results = new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
			return OneOrThrow(results);
		}

		private static Tag OneOrThrow(TagCollection c)
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

		public static Tag FirstOneWhere(WhereDelegate<TagColumns> where, Database db = null)
		{
			var results = new TagCollection(Select<TagColumns>.From<Tag>().Where(where, db));
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, Database db = null)
		{
			return Top(count, where, null, db);
		}

		public static TagCollection Top(int count, WhereDelegate<TagColumns> where, OrderBy<TagColumns> orderBy, Database database = null)
        {
            TagColumns c = new TagColumns();
            IQueryFilter filter = where(c);         
            
			Database db = database == null ? Db.For<Tag>(): database;
			QuerySet query = GetQuerySet(db); 
            query.Top<Tag>(count);
            query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<TagColumns>(orderBy);
			}

            query.Execute(db);
            return query.Results.As<TagCollection>(0);
        }

		public static long Count(WhereDelegate<TagColumns> where, Database database = null)
		{
			TagColumns c = new TagColumns();
			IQueryFilter filter = where(c) ;

			Database db = database == null ? Db.For<Tag>(): database;
			QuerySet query = GetQuerySet(db);	 
			query.Count<Tag>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
    }
}																								
