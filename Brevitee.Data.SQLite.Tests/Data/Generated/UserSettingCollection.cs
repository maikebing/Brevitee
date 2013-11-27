using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Brevitee.Data;

namespace SampleData
{
    public class UserSettingCollection: DaoCollection<UserSettingColumns, UserSetting>
    { 
		public UserSettingCollection(){}
		public UserSettingCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public UserSettingCollection(Query<UserSettingColumns, UserSetting> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public UserSettingCollection(Query<UserSettingColumns, UserSetting> q, bool load) : base(q, load) { }
    }
}