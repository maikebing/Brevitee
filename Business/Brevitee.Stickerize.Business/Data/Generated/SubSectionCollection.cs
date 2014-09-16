using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace Brevitee.Stickerize.Business.Data
{
    public class SubSectionCollection: DaoCollection<SubSectionColumns, SubSection>
    { 
		public SubSectionCollection(){}
		public SubSectionCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public SubSectionCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public SubSectionCollection(Query<SubSectionColumns, SubSection> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public SubSectionCollection(Database db, Query<SubSectionColumns, SubSection> q, bool load) : base(db, q, load) { }
		public SubSectionCollection(Query<SubSectionColumns, SubSection> q, bool load) : base(q, load) { }
    }
}