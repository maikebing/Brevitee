using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Encryption;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public partial class LockOut
    {
        public static LockOut Last(string userName)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Last(user);
        }

        public static LockOut Last(User user)
        {
            return LockOut.Top(1, c => c.UserId == user.Id && c.Unlocked == false, Order.By<LockOutColumns>(c => c.DateTime, SortOrder.Descending)).FirstOrDefault();
        }
    }
}
