using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageTagCollection: DaoCollection<ImageTagColumns, ImageTag>
    { 
		public ImageTagCollection(){}
		public ImageTagCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ImageTagCollection(Query<ImageTagColumns, ImageTag> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ImageTagCollection(Query<ImageTagColumns, ImageTag> q, bool load) : base(q, load) { }
    }
}