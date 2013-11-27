using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleData
{
    public partial class Want: IUserItem
    {
        public WantStatusEnum Status
        {
            get
            {
                return this.WantStatusOfWantStatusId;
            }
        }

        #region IUserItem Members

        public User User
        {
            get { return UserOfUserId; }
        }

        public IItem Item
        {
            get { return ItemOfItemId; }
        }

        #endregion
    }
}
