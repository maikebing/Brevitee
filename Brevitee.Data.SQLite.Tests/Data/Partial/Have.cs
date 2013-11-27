using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleData;

namespace SampleData
{
    public partial class Have: IUserItem 
    {
        public void Keep(bool save = true)
        {
            HaveStatusId = HaveStatus.InUse.Id;
            if (save) Save();
        }

        public Give GiveTo(long userId, bool save = true)
        {
            return GaveIt(SampleData.User.OneWhere(c => c.Id == userId), save);
        }

        public Give GiveTo(string sourceId, bool save = true)
        {
            return GaveIt(SampleData.User.OneWhere(c => c.SourceId == sourceId), save);
        }
        
        /// <summary>
        /// States that the haver has given this item to the specified user.
        /// Creates a PendingConfirmation give and sets the HaveStatus to GivenAway
        /// </summary>
        /// <param name="reciever"></param>
        /// <param name="save"></param>
        /// <param name="onGiveSaved"></param>
        /// <returns></returns>
        public Give GaveIt(User reciever, bool save = true, Action<Give> onGiveSaved = null)
        {
            Give give = null;
            if (reciever != null)
            {
                Want want;
                if (reciever.Wants(this.ItemId.GetValueOrDefault(), out want))
                {
                    bool existing;
                    this.HaveStatusId = HaveStatus.GivenAway.Id;
                    give = GetGive(give, want, GiveStatus.PendingConfirmation, out existing);

                    if (save)
                    {
                        give.Save();
                        this.Save();
                        if (onGiveSaved != null && !existing)
                        {
                            onGiveSaved(give);
                        }
                    }
                }
            }

            return give;
        }

        /// <summary>
        /// Creates a give with the Pending status only if the specified reciever want the item.
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="save"></param>
        /// <param name="onGiveSaved"></param>
        /// <returns></returns>
        public Give ProposeGiveTo(User receiver, bool save = true, Action<Give> onGiveSaved = null)
        {
            Give give = null;
            if (receiver != null)
            {
                Want want;
                if (receiver.Wants(this.ItemId.GetValueOrDefault(), out want))
                {
                    bool existing;
                    give = GetGive(give, want, GiveStatus.Pending, out existing);

                    if (save)
                    {
                        give.Save();
                        this.Save();
                        if (onGiveSaved != null && !existing)
                        {
                            onGiveSaved(give);
                        }
                    }
                }
            }

            return give;
        }

        private Give GetGive(Give give, Want want, GiveStatus status, out bool existing)
        {
            existing = true;
            give = SampleData.Give.FirstOneWhere(c => c.HaveId == this.Id && c.WantId == want.Id);
            if (give == null)
            {
                existing = false;
                give = this.GiveCollectionByHaveId.AddNew();
                give.WantId = want.Id;
                give.LastModified = DateTime.UtcNow;
            }

            give.GiveStatusId = status.Id;
            return give;
        }


        /// <summary>
        /// Makes the current item available
        /// </summary>
        /// <param name="save"></param>
        public void Give(bool save = true)
        {
            HaveStatusId = HaveStatus.Available.Id;
            if (save) Save();
        }
        
        public bool IsAvailable
        {
            get
            {
                return this.HaveStatusOfHaveStatusId.Id == HaveStatus.Available.Id;
            }
        }

        public bool IsInUse
        {
            get
            {
                return this.HaveStatusOfHaveStatusId.Id == HaveStatus.InUse.Id;
            }
        }

        public bool IsGivenAway
        {
            get
            {
                return this.HaveStatusOfHaveStatusId.Id == HaveStatus.GivenAway.Id;
            }
        }

        #region IUserItem Members

        public User User
        {
            get
            {
                return UserOfUserId;
            }
        }

        public IItem Item
        {
            get
            {
                return ItemOfItemId;
            }
        }

        #endregion
    }
}
