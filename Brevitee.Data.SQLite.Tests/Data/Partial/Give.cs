using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleData
{
    public partial class Give: IUserItem
    {
        #region IUserItem Members

        public User User
        {
            get { return HaveOfHaveId.UserOfUserId; }
        }

        public IItem Item
        {
            get { return HaveOfHaveId.ItemOfItemId; }
        }

        #endregion

        public AddressRequest GetAddressRequest(string comment)
        {
            bool ignore;
            return GetAddressRequest(comment, out ignore, true);
        }

        public AddressRequest GetAddressRequest(bool create = true)
        {
            bool ignore;
            return GetAddressRequest(out ignore, create);
        }

        public AddressRequest GetAddressRequest(out bool created, bool create = true)
        {
            return GetAddressRequest(string.Empty, out created, create);
        }

        public AddressRequest GetAddressRequest(string comment, out bool created, bool create = true)
        {
            created = false;
            long requesterId = this.HaveOfHaveId.UserId.GetValueOrDefault();
            long requesteeId = this.WantOfWantId.UserId.GetValueOrDefault();

            AddressRequest request = AddressRequest.OneWhere(c => c.RequesterId == requesterId && c.RequesteeId == requesteeId);
            if (request == null && create)
            {
                request = AddressRequest.Create(requesterId, requesteeId);
                if (!string.IsNullOrWhiteSpace(comment))
                {
                    Task task = new Task(() =>
                    {
                        request.Comment(comment);
                    });
                    
                    task.Start(TaskScheduler.Default);
                }

                created = true;
            }

            return request;
        }

    }
}
