using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Configuration;

namespace Brevitee.Users.Data
{
    public partial class Account
    {       
        /// <summary>
        /// Creates a new Confirmation with the Created and
        /// Token properties set
        /// </summary>
        /// <returns></returns>
        public static Account Create(User user, string provider, string providerUserId, bool isConfirmed = false)
        {
            DateTime now = DateTime.UtcNow;
            Account result = new Account();
            result.CreationDate = now;
            result.Provider = provider;//DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN");
            result.ProviderUserId = providerUserId;//user.Id.ToString();
            result.Comment = "Account for ({0})::confirmed({1})"._Format(user.UserName, isConfirmed ? "Y" : "N");
            result.ConfirmationDate = isConfirmed ? now : DateTime.MinValue;
            result.IsConfirmed = isConfirmed;
            result.UserId = user.Id;
            result.Token = "AccountConfirmationToken_".RandomLetters(8).Sha256();
            result.IsConfirmed = false;
            result.Save();
            user.AccountsByUserId.Reload();
            return result;
        }

        public bool IsExpired
        {
            get
            {
                if (IsConfirmed.Value)
                {
                    return false;
                }
                else
                {
                    return DateTime.UtcNow.Subtract(CreationDate.Value).Days > 5;
                }
            }
        }

        public void Confirm()
        {
            ConfirmationDate = DateTime.UtcNow;
            IsConfirmed = true;
            Save();
        }

        /// <summary>
        /// Expires the confirmation by setting the Created property to DateTime.MinValue
        /// </summary>
        public void Expire()
        {
            CreationDate = DateTime.MinValue;
        }
    }
}
