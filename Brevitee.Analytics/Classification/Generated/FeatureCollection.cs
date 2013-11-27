using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Classification
{
    public class FeatureCollection: DaoCollection<FeatureColumns, Feature>
    { 
		public FeatureCollection(){}
		public FeatureCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public FeatureCollection(Query<FeatureColumns, Feature> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public FeatureCollection(Query<FeatureColumns, Feature> q, bool load) : base(q, load) { }
    }
}