using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageCollection: DaoCollection<ImageColumns, Image>
    { 
		public ImageCollection(){}
		public ImageCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ImageCollection(Query<ImageColumns, Image> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ImageCollection(Query<ImageColumns, Image> q, bool load) : base(q, load) { }
    }
}