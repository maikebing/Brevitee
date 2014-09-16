// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Analytics.Metrics
{
	// schema = Metrics 
    public static class MetricsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Metrics";
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
	public class TimerQueryContext
	{
			public TimerCollection Where(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Timer.Where(where, db);
			}
		   
			public TimerCollection Where(WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy = null, Database db = null)
			{
				return Timer.Where(where, orderBy, db);
			}

			public Timer OneWhere(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Timer.OneWhere(where, db);
			}
		
			public Timer FirstOneWhere(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Timer.FirstOneWhere(where, db);
			}

			public TimerCollection Top(int count, WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Timer.Top(count, where, db);
			}

			public TimerCollection Top(int count, WhereDelegate<TimerColumns> where, OrderBy<TimerColumns> orderBy, Database db = null)
			{
				return Timer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TimerColumns> where, Database db = null)
			{
				return Timer.Count(where, db);
			}
	}

	static TimerQueryContext _timers;
	static object _timersLock = new object();
	public static TimerQueryContext Timers
	{
		get
		{
			return _timersLock.DoubleCheckLock<TimerQueryContext>(ref _timers, () => new TimerQueryContext());
		}
	}﻿
	public class MethodTimerQueryContext
	{
			public MethodTimerCollection Where(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return MethodTimer.Where(where, db);
			}
		   
			public MethodTimerCollection Where(WhereDelegate<MethodTimerColumns> where, OrderBy<MethodTimerColumns> orderBy = null, Database db = null)
			{
				return MethodTimer.Where(where, orderBy, db);
			}

			public MethodTimer OneWhere(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return MethodTimer.OneWhere(where, db);
			}
		
			public MethodTimer FirstOneWhere(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return MethodTimer.FirstOneWhere(where, db);
			}

			public MethodTimerCollection Top(int count, WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return MethodTimer.Top(count, where, db);
			}

			public MethodTimerCollection Top(int count, WhereDelegate<MethodTimerColumns> where, OrderBy<MethodTimerColumns> orderBy, Database db = null)
			{
				return MethodTimer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MethodTimerColumns> where, Database db = null)
			{
				return MethodTimer.Count(where, db);
			}
	}

	static MethodTimerQueryContext _methodTimers;
	static object _methodTimersLock = new object();
	public static MethodTimerQueryContext MethodTimers
	{
		get
		{
			return _methodTimersLock.DoubleCheckLock<MethodTimerQueryContext>(ref _methodTimers, () => new MethodTimerQueryContext());
		}
	}﻿
	public class LoadTimerQueryContext
	{
			public LoadTimerCollection Where(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return LoadTimer.Where(where, db);
			}
		   
			public LoadTimerCollection Where(WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy = null, Database db = null)
			{
				return LoadTimer.Where(where, orderBy, db);
			}

			public LoadTimer OneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return LoadTimer.OneWhere(where, db);
			}
		
			public LoadTimer FirstOneWhere(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return LoadTimer.FirstOneWhere(where, db);
			}

			public LoadTimerCollection Top(int count, WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return LoadTimer.Top(count, where, db);
			}

			public LoadTimerCollection Top(int count, WhereDelegate<LoadTimerColumns> where, OrderBy<LoadTimerColumns> orderBy, Database db = null)
			{
				return LoadTimer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoadTimerColumns> where, Database db = null)
			{
				return LoadTimer.Count(where, db);
			}
	}

	static LoadTimerQueryContext _loadTimers;
	static object _loadTimersLock = new object();
	public static LoadTimerQueryContext LoadTimers
	{
		get
		{
			return _loadTimersLock.DoubleCheckLock<LoadTimerQueryContext>(ref _loadTimers, () => new LoadTimerQueryContext());
		}
	}﻿
	public class CustomTimerQueryContext
	{
			public CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return CustomTimer.Where(where, db);
			}
		   
			public CustomTimerCollection Where(WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy = null, Database db = null)
			{
				return CustomTimer.Where(where, orderBy, db);
			}

			public CustomTimer OneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return CustomTimer.OneWhere(where, db);
			}
		
			public CustomTimer FirstOneWhere(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return CustomTimer.FirstOneWhere(where, db);
			}

			public CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return CustomTimer.Top(count, where, db);
			}

			public CustomTimerCollection Top(int count, WhereDelegate<CustomTimerColumns> where, OrderBy<CustomTimerColumns> orderBy, Database db = null)
			{
				return CustomTimer.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CustomTimerColumns> where, Database db = null)
			{
				return CustomTimer.Count(where, db);
			}
	}

	static CustomTimerQueryContext _customTimers;
	static object _customTimersLock = new object();
	public static CustomTimerQueryContext CustomTimers
	{
		get
		{
			return _customTimersLock.DoubleCheckLock<CustomTimerQueryContext>(ref _customTimers, () => new CustomTimerQueryContext());
		}
	}﻿
	public class CounterQueryContext
	{
			public CounterCollection Where(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Counter.Where(where, db);
			}
		   
			public CounterCollection Where(WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy = null, Database db = null)
			{
				return Counter.Where(where, orderBy, db);
			}

			public Counter OneWhere(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Counter.OneWhere(where, db);
			}
		
			public Counter FirstOneWhere(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Counter.FirstOneWhere(where, db);
			}

			public CounterCollection Top(int count, WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Counter.Top(count, where, db);
			}

			public CounterCollection Top(int count, WhereDelegate<CounterColumns> where, OrderBy<CounterColumns> orderBy, Database db = null)
			{
				return Counter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<CounterColumns> where, Database db = null)
			{
				return Counter.Count(where, db);
			}
	}

	static CounterQueryContext _counters;
	static object _countersLock = new object();
	public static CounterQueryContext Counters
	{
		get
		{
			return _countersLock.DoubleCheckLock<CounterQueryContext>(ref _counters, () => new CounterQueryContext());
		}
	}﻿
	public class MethodCounterQueryContext
	{
			public MethodCounterCollection Where(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return MethodCounter.Where(where, db);
			}
		   
			public MethodCounterCollection Where(WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy = null, Database db = null)
			{
				return MethodCounter.Where(where, orderBy, db);
			}

			public MethodCounter OneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return MethodCounter.OneWhere(where, db);
			}
		
			public MethodCounter FirstOneWhere(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return MethodCounter.FirstOneWhere(where, db);
			}

			public MethodCounterCollection Top(int count, WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return MethodCounter.Top(count, where, db);
			}

			public MethodCounterCollection Top(int count, WhereDelegate<MethodCounterColumns> where, OrderBy<MethodCounterColumns> orderBy, Database db = null)
			{
				return MethodCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<MethodCounterColumns> where, Database db = null)
			{
				return MethodCounter.Count(where, db);
			}
	}

	static MethodCounterQueryContext _methodCounters;
	static object _methodCountersLock = new object();
	public static MethodCounterQueryContext MethodCounters
	{
		get
		{
			return _methodCountersLock.DoubleCheckLock<MethodCounterQueryContext>(ref _methodCounters, () => new MethodCounterQueryContext());
		}
	}﻿
	public class LoadCounterQueryContext
	{
			public LoadCounterCollection Where(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return LoadCounter.Where(where, db);
			}
		   
			public LoadCounterCollection Where(WhereDelegate<LoadCounterColumns> where, OrderBy<LoadCounterColumns> orderBy = null, Database db = null)
			{
				return LoadCounter.Where(where, orderBy, db);
			}

			public LoadCounter OneWhere(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return LoadCounter.OneWhere(where, db);
			}
		
			public LoadCounter FirstOneWhere(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return LoadCounter.FirstOneWhere(where, db);
			}

			public LoadCounterCollection Top(int count, WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return LoadCounter.Top(count, where, db);
			}

			public LoadCounterCollection Top(int count, WhereDelegate<LoadCounterColumns> where, OrderBy<LoadCounterColumns> orderBy, Database db = null)
			{
				return LoadCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoadCounterColumns> where, Database db = null)
			{
				return LoadCounter.Count(where, db);
			}
	}

	static LoadCounterQueryContext _loadCounters;
	static object _loadCountersLock = new object();
	public static LoadCounterQueryContext LoadCounters
	{
		get
		{
			return _loadCountersLock.DoubleCheckLock<LoadCounterQueryContext>(ref _loadCounters, () => new LoadCounterQueryContext());
		}
	}﻿
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
	}﻿
	public class ClickCounterQueryContext
	{
			public ClickCounterCollection Where(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return ClickCounter.Where(where, db);
			}
		   
			public ClickCounterCollection Where(WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy = null, Database db = null)
			{
				return ClickCounter.Where(where, orderBy, db);
			}

			public ClickCounter OneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return ClickCounter.OneWhere(where, db);
			}
		
			public ClickCounter FirstOneWhere(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return ClickCounter.FirstOneWhere(where, db);
			}

			public ClickCounterCollection Top(int count, WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return ClickCounter.Top(count, where, db);
			}

			public ClickCounterCollection Top(int count, WhereDelegate<ClickCounterColumns> where, OrderBy<ClickCounterColumns> orderBy, Database db = null)
			{
				return ClickCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<ClickCounterColumns> where, Database db = null)
			{
				return ClickCounter.Count(where, db);
			}
	}

	static ClickCounterQueryContext _clickCounters;
	static object _clickCountersLock = new object();
	public static ClickCounterQueryContext ClickCounters
	{
		get
		{
			return _clickCountersLock.DoubleCheckLock<ClickCounterQueryContext>(ref _clickCounters, () => new ClickCounterQueryContext());
		}
	}﻿
	public class LoginCounterQueryContext
	{
			public LoginCounterCollection Where(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return LoginCounter.Where(where, db);
			}
		   
			public LoginCounterCollection Where(WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy = null, Database db = null)
			{
				return LoginCounter.Where(where, orderBy, db);
			}

			public LoginCounter OneWhere(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return LoginCounter.OneWhere(where, db);
			}
		
			public LoginCounter FirstOneWhere(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return LoginCounter.FirstOneWhere(where, db);
			}

			public LoginCounterCollection Top(int count, WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return LoginCounter.Top(count, where, db);
			}

			public LoginCounterCollection Top(int count, WhereDelegate<LoginCounterColumns> where, OrderBy<LoginCounterColumns> orderBy, Database db = null)
			{
				return LoginCounter.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoginCounterColumns> where, Database db = null)
			{
				return LoginCounter.Count(where, db);
			}
	}

	static LoginCounterQueryContext _loginCounters;
	static object _loginCountersLock = new object();
	public static LoginCounterQueryContext LoginCounters
	{
		get
		{
			return _loginCountersLock.DoubleCheckLock<LoginCounterQueryContext>(ref _loginCounters, () => new LoginCounterQueryContext());
		}
	}    }
}																								
