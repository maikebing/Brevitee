using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Analytics.Data
{
    public class ImageRatingCollection: DaoCollection<ImageRatingColumns, ImageRating>
    { 
		public ImageRatingCollection(){}
		public ImageRatingCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public ImageRatingCollection(Query<ImageRatingColumns, ImageRating> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public ImageRatingCollection(Query<ImageRatingColumns, ImageRating> q, bool load) : base(q, load) { }
    }
}