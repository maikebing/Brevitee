// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.Automation.ContinuousIntegration.Data
{
	// schema = BuildAutomation 
    public static class BuildAutomationContext
    {
		public static Database Db
		{
			get
			{
                return Brevitee.Data.Db.For("BuildAutomation");
			}
		}


	public class BuildJobQueryContext
	{
			public BuildJobCollection Where(WhereDelegate<BuildJobColumns> where, Database db = null)
			{
				return BuildJob.Where(where, db);
			}
		   
			public BuildJobCollection Where(WhereDelegate<BuildJobColumns> where, OrderBy<BuildJobColumns> orderBy = null, Database db = null)
			{
				return BuildJob.Where(where, orderBy, db);
			}

			public BuildJob OneWhere(WhereDelegate<BuildJobColumns> where, Database db = null)
			{
				return BuildJob.OneWhere(where, db);
			}
		
			public BuildJob FirstOneWhere(WhereDelegate<BuildJobColumns> where, Database db = null)
			{
				return BuildJob.FirstOneWhere(where, db);
			}

			public BuildJobCollection Top(int count, WhereDelegate<BuildJobColumns> where, Database db = null)
			{
				return BuildJob.Top(count, where, db);
			}

			public BuildJobCollection Top(int count, WhereDelegate<BuildJobColumns> where, OrderBy<BuildJobColumns> orderBy, Database db = null)
			{
				return BuildJob.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<BuildJobColumns> where, Database db = null)
			{
				return BuildJob.Count(where, db);
			}
	}

	static BuildJobQueryContext _buildJobs;
	static object _buildJobsLock = new object();
	public static BuildJobQueryContext BuildJobs
	{
		get
		{
			return _buildJobsLock.DoubleCheckLock<BuildJobQueryContext>(ref _buildJobs, () => new BuildJobQueryContext());
		}
	}
	public class BuildResultQueryContext
	{
			public BuildResultCollection Where(WhereDelegate<BuildResultColumns> where, Database db = null)
			{
				return BuildResult.Where(where, db);
			}
		   
			public BuildResultCollection Where(WhereDelegate<BuildResultColumns> where, OrderBy<BuildResultColumns> orderBy = null, Database db = null)
			{
				return BuildResult.Where(where, orderBy, db);
			}

			public BuildResult OneWhere(WhereDelegate<BuildResultColumns> where, Database db = null)
			{
				return BuildResult.OneWhere(where, db);
			}
		
			public BuildResult FirstOneWhere(WhereDelegate<BuildResultColumns> where, Database db = null)
			{
				return BuildResult.FirstOneWhere(where, db);
			}

			public BuildResultCollection Top(int count, WhereDelegate<BuildResultColumns> where, Database db = null)
			{
				return BuildResult.Top(count, where, db);
			}

			public BuildResultCollection Top(int count, WhereDelegate<BuildResultColumns> where, OrderBy<BuildResultColumns> orderBy, Database db = null)
			{
				return BuildResult.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<BuildResultColumns> where, Database db = null)
			{
				return BuildResult.Count(where, db);
			}
	}

	static BuildResultQueryContext _buildResults;
	static object _buildResultsLock = new object();
	public static BuildResultQueryContext BuildResults
	{
		get
		{
			return _buildResultsLock.DoubleCheckLock<BuildResultQueryContext>(ref _buildResults, () => new BuildResultQueryContext());
		}
	}    }
}																								
