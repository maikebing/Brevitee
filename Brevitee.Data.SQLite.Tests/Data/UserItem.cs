using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace SampleData
{
    /// <summary>
    /// Represents a relationship between a user and an Item.
    /// This object supports the underlying framework.
    /// </summary>
    public class UserItem: IUserItem
    {
        public UserItem(IPrincipal principal, IItem item)
            : this(User.FromIdentity(principal.Identity), item)
        {
        }

        public UserItem(User user, IItem item)
        {
            if (user == null)
            {
                user = new NullUser();
            }

            this.User = user;
            this.Item = item;
        }

        public User User { get; set; }
        public IItem Item { get; set; }
    }
}
