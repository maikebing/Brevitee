// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Stickerize.Business.Data
{
	// schema = Stickerize 
    public static class StickerizeContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Stickerize";
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
	public class LoginTimeQueryContext
	{
			public LoginTimeCollection Where(WhereDelegate<LoginTimeColumns> where, Database db = null)
			{
				return LoginTime.Where(where, db);
			}
		   
			public LoginTimeCollection Where(WhereDelegate<LoginTimeColumns> where, OrderBy<LoginTimeColumns> orderBy = null, Database db = null)
			{
				return LoginTime.Where(where, orderBy, db);
			}

			public LoginTime OneWhere(WhereDelegate<LoginTimeColumns> where, Database db = null)
			{
				return LoginTime.OneWhere(where, db);
			}
		
			public LoginTime FirstOneWhere(WhereDelegate<LoginTimeColumns> where, Database db = null)
			{
				return LoginTime.FirstOneWhere(where, db);
			}

			public LoginTimeCollection Top(int count, WhereDelegate<LoginTimeColumns> where, Database db = null)
			{
				return LoginTime.Top(count, where, db);
			}

			public LoginTimeCollection Top(int count, WhereDelegate<LoginTimeColumns> where, OrderBy<LoginTimeColumns> orderBy, Database db = null)
			{
				return LoginTime.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoginTimeColumns> where, Database db = null)
			{
				return LoginTime.Count(where, db);
			}
	}

	static LoginTimeQueryContext _loginTimes;
	static object _loginTimesLock = new object();
	public static LoginTimeQueryContext LoginTimes
	{
		get
		{
			return _loginTimesLock.DoubleCheckLock<LoginTimeQueryContext>(ref _loginTimes, () => new LoginTimeQueryContext());
		}
	}﻿
	public class StickerizerQueryContext
	{
			public StickerizerCollection Where(WhereDelegate<StickerizerColumns> where, Database db = null)
			{
				return Stickerizer.Where(where, db);
			}
		   
			public StickerizerCollection Where(WhereDelegate<StickerizerColumns> where, OrderBy<StickerizerColumns> orderBy = null, Database db = null)
			{
				return Stickerizer.Where(where, orderBy, db);
			}

			public Stickerizer OneWhere(WhereDelegate<StickerizerColumns> where, Database db = null)
			{
				return Stickerizer.OneWhere(where, db);
			}
		
			public Stickerizer FirstOneWhere(WhereDelegate<StickerizerColumns> where, Database db = null)
			{
				return Stickerizer.FirstOneWhere(where, db);
			}

			public StickerizerCollection Top(int count, WhereDelegate<StickerizerColumns> where, Database db = null)
			{
				return Stickerizer.Top(count, where, db);
			}

			public StickerizerCollection Top(int count, WhereDelegate<StickerizerColumns> where, OrderBy<StickerizerColumns> orderBy, Database db = null)
			{
				return Stickerizer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizerColumns> where, Database db = null)
			{
				return Stickerizer.Count(where, db);
			}
	}

	static StickerizerQueryContext _stickerizers;
	static object _stickerizersLock = new object();
	public static StickerizerQueryContext Stickerizers
	{
		get
		{
			return _stickerizersLock.DoubleCheckLock<StickerizerQueryContext>(ref _stickerizers, () => new StickerizerQueryContext());
		}
	}﻿
	public class StickerizeeQueryContext
	{
			public StickerizeeCollection Where(WhereDelegate<StickerizeeColumns> where, Database db = null)
			{
				return Stickerizee.Where(where, db);
			}
		   
			public StickerizeeCollection Where(WhereDelegate<StickerizeeColumns> where, OrderBy<StickerizeeColumns> orderBy = null, Database db = null)
			{
				return Stickerizee.Where(where, orderBy, db);
			}

			public Stickerizee OneWhere(WhereDelegate<StickerizeeColumns> where, Database db = null)
			{
				return Stickerizee.OneWhere(where, db);
			}
		
			public Stickerizee FirstOneWhere(WhereDelegate<StickerizeeColumns> where, Database db = null)
			{
				return Stickerizee.FirstOneWhere(where, db);
			}

			public StickerizeeCollection Top(int count, WhereDelegate<StickerizeeColumns> where, Database db = null)
			{
				return Stickerizee.Top(count, where, db);
			}

			public StickerizeeCollection Top(int count, WhereDelegate<StickerizeeColumns> where, OrderBy<StickerizeeColumns> orderBy, Database db = null)
			{
				return Stickerizee.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizeeColumns> where, Database db = null)
			{
				return Stickerizee.Count(where, db);
			}
	}

	static StickerizeeQueryContext _stickerizees;
	static object _stickerizeesLock = new object();
	public static StickerizeeQueryContext Stickerizees
	{
		get
		{
			return _stickerizeesLock.DoubleCheckLock<StickerizeeQueryContext>(ref _stickerizees, () => new StickerizeeQueryContext());
		}
	}﻿
	public class StickerizableQueryContext
	{
			public StickerizableCollection Where(WhereDelegate<StickerizableColumns> where, Database db = null)
			{
				return Stickerizable.Where(where, db);
			}
		   
			public StickerizableCollection Where(WhereDelegate<StickerizableColumns> where, OrderBy<StickerizableColumns> orderBy = null, Database db = null)
			{
				return Stickerizable.Where(where, orderBy, db);
			}

			public Stickerizable OneWhere(WhereDelegate<StickerizableColumns> where, Database db = null)
			{
				return Stickerizable.OneWhere(where, db);
			}
		
			public Stickerizable FirstOneWhere(WhereDelegate<StickerizableColumns> where, Database db = null)
			{
				return Stickerizable.FirstOneWhere(where, db);
			}

			public StickerizableCollection Top(int count, WhereDelegate<StickerizableColumns> where, Database db = null)
			{
				return Stickerizable.Top(count, where, db);
			}

			public StickerizableCollection Top(int count, WhereDelegate<StickerizableColumns> where, OrderBy<StickerizableColumns> orderBy, Database db = null)
			{
				return Stickerizable.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizableColumns> where, Database db = null)
			{
				return Stickerizable.Count(where, db);
			}
	}

	static StickerizableQueryContext _stickerizables;
	static object _stickerizablesLock = new object();
	public static StickerizableQueryContext Stickerizables
	{
		get
		{
			return _stickerizablesLock.DoubleCheckLock<StickerizableQueryContext>(ref _stickerizables, () => new StickerizableQueryContext());
		}
	}﻿
	public class StickerizableListQueryContext
	{
			public StickerizableListCollection Where(WhereDelegate<StickerizableListColumns> where, Database db = null)
			{
				return StickerizableList.Where(where, db);
			}
		   
			public StickerizableListCollection Where(WhereDelegate<StickerizableListColumns> where, OrderBy<StickerizableListColumns> orderBy = null, Database db = null)
			{
				return StickerizableList.Where(where, orderBy, db);
			}

			public StickerizableList OneWhere(WhereDelegate<StickerizableListColumns> where, Database db = null)
			{
				return StickerizableList.OneWhere(where, db);
			}
		
			public StickerizableList FirstOneWhere(WhereDelegate<StickerizableListColumns> where, Database db = null)
			{
				return StickerizableList.FirstOneWhere(where, db);
			}

			public StickerizableListCollection Top(int count, WhereDelegate<StickerizableListColumns> where, Database db = null)
			{
				return StickerizableList.Top(count, where, db);
			}

			public StickerizableListCollection Top(int count, WhereDelegate<StickerizableListColumns> where, OrderBy<StickerizableListColumns> orderBy, Database db = null)
			{
				return StickerizableList.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizableListColumns> where, Database db = null)
			{
				return StickerizableList.Count(where, db);
			}
	}

	static StickerizableListQueryContext _stickerizableLists;
	static object _stickerizableListsLock = new object();
	public static StickerizableListQueryContext StickerizableLists
	{
		get
		{
			return _stickerizableListsLock.DoubleCheckLock<StickerizableListQueryContext>(ref _stickerizableLists, () => new StickerizableListQueryContext());
		}
	}﻿
	public class StickerQueryContext
	{
			public StickerCollection Where(WhereDelegate<StickerColumns> where, Database db = null)
			{
				return Sticker.Where(where, db);
			}
		   
			public StickerCollection Where(WhereDelegate<StickerColumns> where, OrderBy<StickerColumns> orderBy = null, Database db = null)
			{
				return Sticker.Where(where, orderBy, db);
			}

			public Sticker OneWhere(WhereDelegate<StickerColumns> where, Database db = null)
			{
				return Sticker.OneWhere(where, db);
			}
		
			public Sticker FirstOneWhere(WhereDelegate<StickerColumns> where, Database db = null)
			{
				return Sticker.FirstOneWhere(where, db);
			}

			public StickerCollection Top(int count, WhereDelegate<StickerColumns> where, Database db = null)
			{
				return Sticker.Top(count, where, db);
			}

			public StickerCollection Top(int count, WhereDelegate<StickerColumns> where, OrderBy<StickerColumns> orderBy, Database db = null)
			{
				return Sticker.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerColumns> where, Database db = null)
			{
				return Sticker.Count(where, db);
			}
	}

	static StickerQueryContext _stickers;
	static object _stickersLock = new object();
	public static StickerQueryContext Stickers
	{
		get
		{
			return _stickersLock.DoubleCheckLock<StickerQueryContext>(ref _stickers, () => new StickerQueryContext());
		}
	}﻿
	public class StickerizationQueryContext
	{
			public StickerizationCollection Where(WhereDelegate<StickerizationColumns> where, Database db = null)
			{
				return Stickerization.Where(where, db);
			}
		   
			public StickerizationCollection Where(WhereDelegate<StickerizationColumns> where, OrderBy<StickerizationColumns> orderBy = null, Database db = null)
			{
				return Stickerization.Where(where, orderBy, db);
			}

			public Stickerization OneWhere(WhereDelegate<StickerizationColumns> where, Database db = null)
			{
				return Stickerization.OneWhere(where, db);
			}
		
			public Stickerization FirstOneWhere(WhereDelegate<StickerizationColumns> where, Database db = null)
			{
				return Stickerization.FirstOneWhere(where, db);
			}

			public StickerizationCollection Top(int count, WhereDelegate<StickerizationColumns> where, Database db = null)
			{
				return Stickerization.Top(count, where, db);
			}

			public StickerizationCollection Top(int count, WhereDelegate<StickerizationColumns> where, OrderBy<StickerizationColumns> orderBy, Database db = null)
			{
				return Stickerization.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizationColumns> where, Database db = null)
			{
				return Stickerization.Count(where, db);
			}
	}

	static StickerizationQueryContext _stickerizations;
	static object _stickerizationsLock = new object();
	public static StickerizationQueryContext Stickerizations
	{
		get
		{
			return _stickerizationsLock.DoubleCheckLock<StickerizationQueryContext>(ref _stickerizations, () => new StickerizationQueryContext());
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
	public class SubSectionQueryContext
	{
			public SubSectionCollection Where(WhereDelegate<SubSectionColumns> where, Database db = null)
			{
				return SubSection.Where(where, db);
			}
		   
			public SubSectionCollection Where(WhereDelegate<SubSectionColumns> where, OrderBy<SubSectionColumns> orderBy = null, Database db = null)
			{
				return SubSection.Where(where, orderBy, db);
			}

			public SubSection OneWhere(WhereDelegate<SubSectionColumns> where, Database db = null)
			{
				return SubSection.OneWhere(where, db);
			}
		
			public SubSection FirstOneWhere(WhereDelegate<SubSectionColumns> where, Database db = null)
			{
				return SubSection.FirstOneWhere(where, db);
			}

			public SubSectionCollection Top(int count, WhereDelegate<SubSectionColumns> where, Database db = null)
			{
				return SubSection.Top(count, where, db);
			}

			public SubSectionCollection Top(int count, WhereDelegate<SubSectionColumns> where, OrderBy<SubSectionColumns> orderBy, Database db = null)
			{
				return SubSection.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SubSectionColumns> where, Database db = null)
			{
				return SubSection.Count(where, db);
			}
	}

	static SubSectionQueryContext _subSections;
	static object _subSectionsLock = new object();
	public static SubSectionQueryContext SubSections
	{
		get
		{
			return _subSectionsLock.DoubleCheckLock<SubSectionQueryContext>(ref _subSections, () => new SubSectionQueryContext());
		}
	}﻿
	public class StickerizerStickerizeeQueryContext
	{
			public StickerizerStickerizeeCollection Where(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
			{
				return StickerizerStickerizee.Where(where, db);
			}
		   
			public StickerizerStickerizeeCollection Where(WhereDelegate<StickerizerStickerizeeColumns> where, OrderBy<StickerizerStickerizeeColumns> orderBy = null, Database db = null)
			{
				return StickerizerStickerizee.Where(where, orderBy, db);
			}

			public StickerizerStickerizee OneWhere(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
			{
				return StickerizerStickerizee.OneWhere(where, db);
			}
		
			public StickerizerStickerizee FirstOneWhere(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
			{
				return StickerizerStickerizee.FirstOneWhere(where, db);
			}

			public StickerizerStickerizeeCollection Top(int count, WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
			{
				return StickerizerStickerizee.Top(count, where, db);
			}

			public StickerizerStickerizeeCollection Top(int count, WhereDelegate<StickerizerStickerizeeColumns> where, OrderBy<StickerizerStickerizeeColumns> orderBy, Database db = null)
			{
				return StickerizerStickerizee.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizerStickerizeeColumns> where, Database db = null)
			{
				return StickerizerStickerizee.Count(where, db);
			}
	}

	static StickerizerStickerizeeQueryContext _stickerizerStickerizees;
	static object _stickerizerStickerizeesLock = new object();
	public static StickerizerStickerizeeQueryContext StickerizerStickerizees
	{
		get
		{
			return _stickerizerStickerizeesLock.DoubleCheckLock<StickerizerStickerizeeQueryContext>(ref _stickerizerStickerizees, () => new StickerizerStickerizeeQueryContext());
		}
	}﻿
	public class StickerizableListStickerizableQueryContext
	{
			public StickerizableListStickerizableCollection Where(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
			{
				return StickerizableListStickerizable.Where(where, db);
			}
		   
			public StickerizableListStickerizableCollection Where(WhereDelegate<StickerizableListStickerizableColumns> where, OrderBy<StickerizableListStickerizableColumns> orderBy = null, Database db = null)
			{
				return StickerizableListStickerizable.Where(where, orderBy, db);
			}

			public StickerizableListStickerizable OneWhere(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
			{
				return StickerizableListStickerizable.OneWhere(where, db);
			}
		
			public StickerizableListStickerizable FirstOneWhere(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
			{
				return StickerizableListStickerizable.FirstOneWhere(where, db);
			}

			public StickerizableListStickerizableCollection Top(int count, WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
			{
				return StickerizableListStickerizable.Top(count, where, db);
			}

			public StickerizableListStickerizableCollection Top(int count, WhereDelegate<StickerizableListStickerizableColumns> where, OrderBy<StickerizableListStickerizableColumns> orderBy, Database db = null)
			{
				return StickerizableListStickerizable.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StickerizableListStickerizableColumns> where, Database db = null)
			{
				return StickerizableListStickerizable.Count(where, db);
			}
	}

	static StickerizableListStickerizableQueryContext _stickerizableListStickerizables;
	static object _stickerizableListStickerizablesLock = new object();
	public static StickerizableListStickerizableQueryContext StickerizableListStickerizables
	{
		get
		{
			return _stickerizableListStickerizablesLock.DoubleCheckLock<StickerizableListStickerizableQueryContext>(ref _stickerizableListStickerizables, () => new StickerizableListStickerizableQueryContext());
		}
	}﻿
	public class SubSectionStickerizableQueryContext
	{
			public SubSectionStickerizableCollection Where(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
			{
				return SubSectionStickerizable.Where(where, db);
			}
		   
			public SubSectionStickerizableCollection Where(WhereDelegate<SubSectionStickerizableColumns> where, OrderBy<SubSectionStickerizableColumns> orderBy = null, Database db = null)
			{
				return SubSectionStickerizable.Where(where, orderBy, db);
			}

			public SubSectionStickerizable OneWhere(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
			{
				return SubSectionStickerizable.OneWhere(where, db);
			}
		
			public SubSectionStickerizable FirstOneWhere(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
			{
				return SubSectionStickerizable.FirstOneWhere(where, db);
			}

			public SubSectionStickerizableCollection Top(int count, WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
			{
				return SubSectionStickerizable.Top(count, where, db);
			}

			public SubSectionStickerizableCollection Top(int count, WhereDelegate<SubSectionStickerizableColumns> where, OrderBy<SubSectionStickerizableColumns> orderBy, Database db = null)
			{
				return SubSectionStickerizable.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SubSectionStickerizableColumns> where, Database db = null)
			{
				return SubSectionStickerizable.Count(where, db);
			}
	}

	static SubSectionStickerizableQueryContext _subSectionStickerizables;
	static object _subSectionStickerizablesLock = new object();
	public static SubSectionStickerizableQueryContext SubSectionStickerizables
	{
		get
		{
			return _subSectionStickerizablesLock.DoubleCheckLock<SubSectionStickerizableQueryContext>(ref _subSectionStickerizables, () => new SubSectionStickerizableQueryContext());
		}
	}    }
}																								
