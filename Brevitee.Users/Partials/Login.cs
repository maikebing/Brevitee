using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Encryption;
using Brevitee.Data;

namespace Brevitee.Users.Data
{
    public partial class Login
    {
        public static Login Last(string userName)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Last(user);
        }

        public static Login Last(User user)
        {
            return Login.Top(1, c => c.UserId == user.Id, Order.By<LoginColumns>(c => c.DateTime, SortOrder.Descending)).FirstOrDefault();
            //DateTime lastLogin = login == null ? DateTime.MinValue : login.DateTime.Value;
        }
    }
}
