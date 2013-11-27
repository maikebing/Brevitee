using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.Encryption;

namespace Brevitee.Users.Data
{
    [Proxy("session")]
    public partial class Session
    {
        public static Session Get(string userName, bool isActive = true)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Get(user, isActive);
        }

        /// <summary>
        /// Get a Session instance for the specified userName, it will 
        /// be created if it doesn't exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public static Session Get(User user, bool isActive = true)
        {
            Session session = user.SessionsByUserId.FirstOrDefault();
            DateTime now = DateTime.UtcNow;

            if (session == null)
            {
                session = user.SessionsByUserId.AddNew();
                session.Identifier = "sid_".RandomLetters(16).Sha256();
                session.CreationDate = now;
            }

            if (isActive)
            {
                session.Touch(false); // save:false because save is called below
            }
            else
            {
                session.IsActive = false;
            }
            
            session.Save();            

            return session;
        }

        /// <summary>
        /// Updates the LastActivity property and sets IsActive to true
        /// </summary>
        /// <param name="save"></param>
        public void Touch(bool save = true)
        {
            DateTime now = DateTime.UtcNow;
            LastActivity = now;
            IsActive = true;

            if (save)
            {
                Save();
            }
        }


    }
}
