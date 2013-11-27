using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web;
using Brevitee.OAuth;
using Newtonsoft.Json.Linq;
using Brevitee.Hatagi;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data;
using Brevitee.Incubation;
using System.Web.Mvc;
using Brevitee.Hatagi;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Brevitee.FileExt;
using Brevitee.FileExt.Js;

namespace SampleData
{
    public partial class User
    {
        bool _isSystem;
        bool _isSystemSet;
        public bool IsSystem
        {
            get
            {
                if (!_isSystemSet)
                {
                    return this.Id == System().Id;
                }
                else
                {
                    return _isSystem;
                }
            }
            internal set
            {
                if (!_isSystemSet)
                {
                    _isSystem = value;
                    _isSystemSet = true;
                }
            }
        }

        public virtual bool IsNull
        {
            get
            {
                if (this.Id == null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this user is the currently logged on user.
        /// </summary>
        public bool IsCurrent
        {
            get
            {
                return !this.IsNull && Current.Id == this.Id;
            }
        }

        static User _systemUser;
        /// <summary>
        /// Gets the system user
        /// </summary>
        /// <returns></returns>
        public static User System()
        {
            if (_systemUser == null)
            {
                _systemUser = User.Get("System", "System");
            }

            if (_systemUser == null)
            {
                _systemUser = User.Create("System", "System", "System", "System");
            }

            if (_systemUser != null)
            {
                _systemUser.IsSystem = true;
            }

            return _systemUser;
        }

        /// <summary>
        /// List of all Items that this user has given away (regardless of whether receipt was confirmed)
        /// </summary>
        public ItemCollection Given
        {
            get
            {
                HaveCollection givenAway = Have.Where(c => c.UserId == Id && c.HaveStatusId == HaveStatus.GivenAway.Id);
                if (givenAway.Count > 0)
                {
                    long[] givenAwayItemIds = givenAway.Select<Have, long>(g => g.ItemId.GetValueOrDefault()).ToArray();
                    return Item.Where(c => c.Id.In(givenAwayItemIds));
                }
                else
                {
                    return new ItemCollection();
                }
            }
        }

        public ItemCollection ConfirmedGiven
        {
            get
            {
                ItemCollection result = new ItemCollection();
                HaveCollection givenAway = Have.Where(c => c.UserId == Id && c.HaveStatusId == HaveStatus.GivenAway.Id);
                if (givenAway.Count > 0)
                {
                    long[] givenAwayHaveIds = givenAway.Select<Have, long>(g => g.Id.GetValueOrDefault()).ToArray();
                    GiveCollection confirmedGives = Give.Where(c => c.HaveId.In(givenAwayHaveIds) && c.GiveStatusId == GiveStatus.Confirmed.Id);
                    long[] confirmedGivenAwayHaveIds = confirmedGives.Select(c => c.HaveId.GetValueOrDefault()).ToArray();
                    if (confirmedGivenAwayHaveIds.Length > 0)
                    {
                        HaveCollection haves = Have.Where(c => c.Id.In(confirmedGivenAwayHaveIds));
                        long[] resultIds = haves.Select(c => c.ItemId.GetValueOrDefault()).ToArray();
                        if (resultIds.Length > 0)
                        {
                            result = Item.Where(c => c.Id.In(resultIds));
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Used by the haver to state that they gave the item to the wanter
        /// </summary>
        /// <param name="give"></param>
        /// <returns></returns>
        public Give GaveIt(Give give)
        {
            if (this.Id != give.HaveOfHaveId.UserId)
            {
                throw Args.Exception<InvalidOperationException>("The specified give is not for an item that this user has.  GiveId::{0}, UserId::{1}", give.Id, this.Id);
            }

            return GaveIt(give.HaveOfHaveId, give.WantOfWantId.User);
        }

        public virtual Give GaveIt(Have have, User reciever)
        {
            return have.GaveIt(reciever);
        }

        /// <summary>
        /// Specifies that the user has reived the specified item.  Sets the GiveStatus to Confirmed, 
        /// the WantStatus to Fulfilled and creates a new Have with HaveStatus of InUse.
        /// </summary>
        /// <param name="give"></param>
        /// <returns></returns>
        public virtual Have Recieved(Give give)
        {
            if (this.Id != give.WantOfWantId.UserId)
            {
                throw Args.Exception<InvalidOperationException>("The specified give is not for an item that this user wants. GiveId::{0}, UserId::{1}", give.Id, this.Id);
            }

            give.LastModified = DateTime.UtcNow;
            give.GiveStatusId = GiveStatus.Confirmed.Id;
            give.Save();
            give.WantOfWantId.WantStatusId = WantStatus.Fulfilled.Id;
            give.WantOfWantId.Save();

            Have have = this.HaveCollectionByUserId.AddNew();
            have.LastModified = DateTime.UtcNow;
            have.ItemId = give.HaveOfHaveId.ItemId;
            have.HaveStatusId = HaveStatus.InUse.Id;
            have.Save();

            return have;
        }

        /// <summary>
        /// Gets all items that the current user has
        /// </summary>
        public ItemCollection InPossessionOf
        {
            get
            {
                ItemCollection result = new ItemCollection();
                long[] itemIds = HaveCollectionByUserId.Where(h=> h.IsAvailable || h.IsInUse).Select<Have, long>(h => h.ItemId.GetValueOrDefault()).ToArray();
                if (itemIds.Length > 0)
                {
                    result = Item.Where(c => c.Id.In(itemIds));
                }

                return result;
            }
        }

        public ItemCollection Wanted
        {
            get
            {
                long[] ids = GetWantItemIds();

                if (ids.Length > 0)
                {
                    return Item.Where(c => c.Id.In(ids));
                }
                else
                {
                    return new ItemCollection();
                }
            }
        }

        public ItemCollection WantedItemsNotInWishList
        {
            get
            {
                long[] wishListIds = WishListCollectionByUserId.Select(w => w.Id.GetValueOrDefault()).ToArray();
                List<long> wantIdsInLists = new List<long>();
                if (wishListIds.Length > 0)
                {
                    WantWishListCollection xrefs = WantWishList.Where(c => c.WishListId.In(wishListIds));
                    wantIdsInLists.AddRange(xrefs.Select(x => x.WantId.GetValueOrDefault()));
                }
                List<long> allWantIds = new List<long>(GetWantIds());
                long[] results = allWantIds.Where(id => !wantIdsInLists.Contains(id)).ToArray();
                WantCollection wants = SampleData.Want.Where(c => c.Id.In(results));
                if (results.Length > 0)
                {
                    long[] itemIds = wants.Select(w => w.ItemId.GetValueOrDefault()).ToArray();
                    return Item.Where(c => c.Id.In(itemIds));
                }
                else
                {
                    return new ItemCollection();
                }
            }
        }

        /// <summary>
        /// Gets all fullfilled wants for the current user
        /// </summary>
        public Want[] Gots
        {
            get
            {
                return (from want in this.WantCollectionByUserId
                        where want.WantStatusId == WantStatus.Fulfilled.Id
                        select want).ToArray();
            }
        }

        /// <summary>
        /// Gets all the haves where the status is Available
        /// </summary>
        public HaveCollection WillGive
        {
            get
            {
                long[] haveIds = GetHaveIds();

                if (haveIds.Length > 0)
                {
                    return Have.Where(c => c.Id.In(haveIds) && c.HaveStatusId == HaveStatus.Available.Id);
                }
                else
                {
                    return new HaveCollection();
                }
            }
        }

        /// <summary>
        /// Gets all Gives that are pending for the current user.
        /// </summary>
        public GiveCollection PendingGives
        {
            get
            {
                long[] haveIds = GetHaveIds();

                if (haveIds.Length > 0)
                {
                    GiveCollection pending = Give.Where(c => c.HaveId.In(haveIds) && c.GiveStatusId == GiveStatus.Pending.Id);
                    GiveCollection pendingConfirm = Give.Where(c => c.HaveId.In(haveIds) && c.GiveStatusId == GiveStatus.PendingConfirmation.Id);
                    GiveCollection all = new GiveCollection();
                    all.AddRange(pending);
                    all.AddRange(pendingConfirm);
                    return all;
                }
                else
                {
                    return new GiveCollection();
                }
            }
        }

        /// <summary>
        /// Gets the Gives that are pending confirmation for the current user
        /// </summary>
        public GiveCollection PendingGots
        {
            get
            {
                long[] wantIds = GetWantIds();
                GiveCollection gives = new GiveCollection();
                if (wantIds.Length > 0)
                {
                    gives = Give.Where(c => c.WantId.In(wantIds) && c.GiveStatusId == GiveStatus.PendingConfirmation.Id);
                }

                return gives;
            }
        }

        /// <summary>
        /// If no value is specified, returns a boolean indicating whether the
        /// account has been verified (email link clicked by user).  If a value
        /// is specified sets whether the account is verified to the specified 
        /// value and returns the value as a boolean.
        /// </summary>
        /// <param name="setValue">The value to set or null</param>
        /// <returns>bool</returns>
        public bool EmailIsVerified(bool? setValue = null)
        {
            if (setValue != null)
            {
                SetUserProperty(MethodBase.GetCurrentMethod().Name, setValue.GetValueOrDefault() ? "yes": "no");
                return setValue.GetValueOrDefault();
            }
            else
            {
                string isVerified = GetUserProperty(MethodBase.GetCurrentMethod().Name);
                return isVerified.Equals("yes");
            }
        }

        public string GetUserProperty(string name)
        {
            UserProperty property = UserProperty.OneWhere(c => c.Name == name && c.UserId == this.Id);
            if (property == null)
            {
                return string.Empty;
            }

            return property.Value;
        }

        public void SetUserProperty(string name, string value)
        {
            UserProperty property = UserProperty.OneWhere(c => c.Name == name && c.UserId == this.Id);
            if (property == null)
            {
                property = this.UserPropertyCollectionByUserId.AddNew();
                property.Name = name;
            }

            property.Value = value;
            property.Save();
        }

        public static User FromIdentity(IIdentity identity)
        {
            FacebookIdentity fbId = identity as FacebookIdentity;
            User user = null;
            if (fbId != null)
            {
                user = FromFacebookIdentity(fbId);
            }

            return user;
        }

        public static User FromFacebookIdentity(FacebookIdentity identity)
        {
            return User.OneWhere(c => c.AuthSource == identity.AuthenticationType && c.SourceId == identity.Id);
        }

        public static User Current
        {
            get
            {
                Incubator inc = Providers.GetSessionProvider<Incubator>(new Incubator());
                User current = inc.Get<User>(new NullUser());

                if (HttpContext.Current != null &&
                    HttpContext.Current.User != null &&
                    HttpContext.Current.User.Identity != null &&
                    HttpContext.Current.User.Identity is FacebookIdentity)
                {                    
                    inc.Set<User>(FromFacebookIdentity((FacebookIdentity)HttpContext.Current.User.Identity));
                }

                return inc.Get<User>();
            }
            set
            {
                Providers.GetSessionProvider<Incubator>(new Incubator()).Set<User>(value);
            }
        }

        public static User FromJObject(JObject jsonUser)
        {
            if(jsonUser["id"] == null)
            {
                throw new ArgumentNullException("jsonUser[\"id\"]");
            }

            if(jsonUser["auth_source"] == null)
            {
                throw new ArgumentNullException("jsonUser[\"auth_source\"]");
            }

            jsonUser.CleanQuotes();
            return User.OneWhere(c => c.AuthSource == jsonUser["auth_source"] && c.SourceId == jsonUser["id"]);            
        }

        /// <summary>
        /// Gets the User that represents the specified JObject using the 
        /// id (id->User.SourceId).  If no User exists one will be created
        /// using the name, auth_source and email in the 
        /// specified JObject.
        /// </summary>
        /// <param name="jsonUser"></param>
        /// <returns></returns>
        public static User EnsureUser(JObject jsonUser)
        {
            string name = string.Empty;
            string authSource = string.Empty;
            string sourceId = string.Empty;
            string email = string.Empty;

            if (jsonUser["name"] != null)
                name = jsonUser["name"].ToString().Replace("\"", "");

            if (jsonUser["auth_source"] != null)
                authSource = jsonUser["auth_source"].ToString().Replace("\"", "");

            if (jsonUser["id"] != null)
                sourceId = jsonUser["id"].ToString().Replace("\"", "");

            if (jsonUser["email"] != null)
                email = jsonUser["email"].ToString().Replace("\"", "");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(authSource) || string.IsNullOrEmpty(sourceId))
            {
                name = "The Unknown User";
                authSource = "unknown";
                sourceId = "-404";
            }

            User user = EnsureUser(name, authSource, sourceId, email);
            return user;
        }

        public static User EnsureUser(string name, string authSource, string sourceId, string email)
        {
            User user = Get(sourceId);
            if (user == null)
            {
                user = GetUser(email);
                
                if (user == null)
                {
                    user = Create(name, authSource, sourceId, email);
                }
                else if (user.AuthSource.Equals("TBD") || user.SourceId.Equals("TBD"))
                {
                    SetProperties(name, authSource, sourceId, email, user);
                }
            }
            return user;
        }

        public static User Create(string userName, string authSource, string sourceId, string email)
        {
            User user = new User(); 
            SetProperties(userName, authSource, sourceId, email, user);
            return user;
        }

        private static void SetProperties(string userName, string authSource, string sourceId, string email, User user)
        {
            user.UserName = userName;
            user.AuthSource = authSource;
            user.SourceId = sourceId;
            user.Email = email;
            user.Save();
        }

        /// <summary>
        /// Gets the user with the specified userName by first seeing if a User exists
        /// with the specified userName as their email address, if not a query is made
        /// for the User with the specified userName as their displayName.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User GetUser(string userName)
        {
            User user = GetByEmail(userName);
            if (user == null)
            {
                user = GetByUserName(userName);
            }

            return user;
        }
        
        public static User GetByUserName(string userName)
        {
            return User.OneWhere(c => c.UserName == userName);
        }

        //public static User GetByDisplayName(string displayName)
        //{
        //    return User.OneWhere(c => c.DisplayName == displayName);
        //}


        /// <summary>
        /// Returns an array of User objects representing the email addresses
        /// specified.  If a User is not found for any email address specified the result
        /// for that one will be omitted.
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        public static User[] GetUsers(string[] userNames)
        {
            QuerySet query = new QuerySet();
            foreach (string userName in userNames)
            {
                query.SelectTop<User>(1).Where<UserColumns>(c => c.Email == userName || c.UserName == userName);
            }

            query.Execute(_.Db.For<User>());

            List<User> users = new List<User>();
            for (int i = 0; i < userNames.Length; i++)
            {
                User user = query.Results[i].As<UserCollection>().FirstOrDefault();
                if (user == null)
                {
                    user = GetUser(userNames[i]);
                }

                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users.ToArray();
        }

        public static User Get(string sourceId)
        {
            return Get("Facebook", sourceId);
        }

        public static User Get(string authSource, string sourceId)
        {
            return OneWhere(c => c.AuthSource == authSource && c.SourceId == sourceId);
        }
        
        public static User GetByEmail(string email)
        {
            return OneWhere(c => c.Email == email);
        }
        
        public Invitation Invitation
        {
            get
            {
                return this.InvitationCollectionByInviteeId.FirstOrDefault();
            }
        }

        

        /// <summary>
        /// Returns true if the current user has the specified item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Has(IItem item)
        {
            return Has(item.Id.GetValueOrDefault());
        }

        /// <summary>
        /// Returns true if the current user has the specified item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Has(IItem item, out Have have)
        {
            return Has(item.Id.GetValueOrDefault(), out have);
        }

        /// <summary>
        /// Returns true if the current user has the specified item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Has(long itemId)
        {            
            Have h;
            return Has(itemId, out h);
        }

        /// <summary>
        /// Returns true if the user has the specified item.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool Has(long itemId, out Have have)
        {
            if (itemId <= 0)
            {
                have = null;
                return false;
            }

            var results = (from item in this.HaveCollectionByUserId
                        where item.ItemId == itemId && item.HaveStatusId != HaveStatus.GivenAway.Id && item.HaveStatusId != HaveStatus.Lost.Id
                        select item);
            
            have = results.FirstOrDefault<Have>();

            return results.Count<Have>() > 0;
        }

        /// <summary>
        /// Sets the specified item as an item that the current 
        /// user has.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Have Acquired(long itemId)
        {
            return Acquired(Item.OneWhere(c => c.Id == itemId));
        }

        /// <summary>
        /// Sets the specified item as an item that the current 
        /// user has.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Have Acquired(Item item)
        {
            Have result;
            if (!Has(item, out result))
            {
                result = this.HaveCollectionByUserId.AddNew();
                result.ItemId = item.Id;
                result.HaveStatusId = HaveStatus.Available.Id;
                result.LastModified = DateTime.UtcNow;
                result.Save();
            }

            DontWant(item);

            return result;
        }

        /// <summary>
        /// Deletes all of the user's wants for the specified item.
        /// </summary>
        /// <param name="item"></param>
        public void DontWant(Item item)
        {
            Want toDelete = (from want in WantCollectionByUserId
                             where want.ItemId == item.Id
                             select want).FirstOrDefault();
            
            this.ChildCollections.Clear(); // TODO: review how to make this better.  This is due to the poor implementation of fk relationship lists
            if (toDelete != null)
            {
                toDelete.WantStatusId = WantStatus.DontWant.Id;
            }
        }

        /// <summary>
        /// Add a Want entry for the current user for the specified
        /// item, will not create dupes.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>null if the user already wants the item</returns>
        public Want Want(Item item)
        {
            Want result;
            if (!Wants(item, out result))
            {
                result = SetWant(item.Id.GetValueOrDefault(), result);
            }

            return result;
        }

        /// <summary>
        /// Add a Want entry for the current user for the specified
        /// item, will not create dupes.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Want Want(long itemId)
        {
            Want result;
            if (!Wants(itemId, out result))
            {
                result = SetWant(itemId, result);
            }

            return result;
        }

        private Want SetWant(long itemId, Want result)
        {
            result = WantCollectionByUserId.AddNew();
            result.ItemId = itemId;
            result.WantStatusId = WantStatus.Unfulfilled.Id;
            result.LastModified = DateTime.UtcNow;
            result.Save();
            return result;
        }
        
        public bool Wants(IItem item, out Want want)
        {
            return Wants(item.Id.GetValueOrDefault(), out want);
        }

        public bool Wants(Item item)
        {
            return Wants(item.Id.GetValueOrDefault());
        }

        public bool Wants(Item item, out Want want)
        {
            return Wants(item.Id.GetValueOrDefault(), out want);
        }

        /// <summary>
        /// Returns true if the user wants the specified
        /// item.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool Wants(long itemId)
        {
            Want w;
            return Wants(itemId, out w);
        }

        /// <summary>
        /// Returns true if the use wants the specified item
        /// </summary>
        /// <param name="itemId">The id of the item</param>
        /// <param name="want">The want if the item is wanted</param>
        /// <returns>boolean</returns>
        public bool Wants(long itemId, out Want want)
        {
            if (itemId <= 0)
            {
                want = null;
                return false;
            }

            var wants = (from item in this.WantCollectionByUserId
                            where item.ItemId == itemId && item.WantStatusId != WantStatus.Fulfilled.Id && item.WantStatusId != WantStatus.DontWant.Id
                            select item);

            want = wants.FirstOrDefault<Want>();
            return wants.Count<Want>() > 0;
        }
        
        Address _address;
        public Address Address
        {
            get
            {
                if (UserAddressCollectionByUserId != null)
                {
                    UserAddress xref = UserAddressCollectionByUserId.FirstOrDefault();
                    if (xref != null)
                    {
                        _address = xref.AddressOfAddressId;
                    }
                }

                return _address;
            }
        }

        public bool HasAddressRequest(out AddressRequestCollection addressRequests)
        {
            addressRequests = this.AddressRequestCollectionByRequesteeId;
            return addressRequests.Count > 0;
        }

        /// <summary>
        /// Gets all the Item Ids for the current
        /// users wants where the status is unfilfilled
        /// </summary>
        /// <returns></returns>
        protected internal long[] GetWantItemIds()
        {
            QuerySet query = new QuerySet();
            query.Select<Want>("ItemId").Where<WantColumns>(c => c.UserId == this.Id && c.WantStatusId == WantStatus.Unfulfilled.Id);
            query.Execute(_.Db.For<Want>());

            WantCollection wants = query.Results[0].As<WantCollection>();
            long[] ids = wants.Select<Want, long>(c => c.ItemId.GetValueOrDefault()).ToArray();
            return ids;
        }

        /// <summary>
        /// Gets all the Ids for the current users wants that are unfulfilled
        /// </summary>
        /// <returns></returns>
        protected internal long[] GetWantIds()
        {
            QuerySet query = new QuerySet();
            query.Select<Want>("Id").Where<WantColumns>(c => c.UserId == this.Id && c.WantStatusId == WantStatus.Unfulfilled.Id);
            query.Execute(_.Db.For<Want>());

            WantCollection wants = query.Results[0].As<WantCollection>();
            long[] ids = wants.Select<Want, long>(c => c.Id.GetValueOrDefault()).ToArray();
            return ids;
        }

        protected internal long[] GetHaveIds()
        {
            QuerySet query = new QuerySet();
            query.Select<Have>("Id").Where<HaveColumns>(c => c.UserId == this.Id);
            query.Execute(_.Db.For<Have>());

            HaveCollection haves = query.Results[0].As<HaveCollection>();
            long[] ids = haves.Select<Have, long>(c => c.Id.GetValueOrDefault()).ToArray();
            return ids;
        }

        public void SetPassword(string passwordHash, bool verified = true)
        {
            byte[] byteHash = passwordHash.HexToBytes();
            SampleData.Password passwordData = this.PasswordCollectionByUserId.FirstOrDefault();
            if (passwordData == null)
            {
                passwordData = this.PasswordCollectionByUserId.AddNew();
            }

            passwordData.Modified = DateTime.UtcNow;
            passwordData.Verified = verified;
            passwordData.SHA256 = byteHash;
            passwordData.Save();
        }

        /// <summary>
        /// Gets the user whos verification code matches the specified code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static User GetByVerificationCode(string code)
        {
            User result = null;
            UserProperty property = UserProperty.OneWhere(c => c.Value == code);
            if (property != null)
            {
                result = property.UserOfUserId;
            }

            return result;
        }

        public static User GetByPasswordCode(string code)
        {
            User result = null;
            UserProperty property = UserProperty.OneWhere(c => c.Value == code);
            if (property != null)
            {
                result = property.UserOfUserId;
            }

            return result;
        }
        
        /// <summary>
        /// Ges a unique UserProperty value used to verify the user's email address.
        /// </summary>
        /// <returns></returns>
        public string VerificationCode()
        {
            string propertyName = MethodBase.GetCurrentMethod().Name;
            return GetPropertyCode(propertyName);
        }
        
        public string PasswordCode()
        {
            string propertyName = MethodBase.GetCurrentMethod().Name;
            return GetPropertyCode(propertyName);
        }

        public void DeletePasswordCode()
        {
            UserProperty property = UserProperty.OneWhere(c => c.UserId == this.Id && c.Value == PasswordCode());
            if (property != null)
            {
                property.Delete();
            }
        }

        private string GetPropertyCode(string propertyName)
        {
            UserProperty property = UserProperty.OneWhere(c => c.UserId == this.Id && c.Name == propertyName);
            if (property == null)
            {
                property = SetPropertyCode(propertyName, property);
            }

            return property.Value;
        }

        /// <summary>
        /// Sets the UserProperty value and name
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private UserProperty SetPropertyCode(string propertyName, UserProperty property)
        {
            property = this.UserPropertyCollectionByUserId.AddNew();
            property.Name = propertyName;

            string value = string.Format("{0}__{1}", "c_".RandomString(16), DateTime.UtcNow.ToJulianDate().ToString());
            UserProperty existing = UserProperty.OneWhere(c => c.Name == propertyName && c.Value == value);
            while (existing != null)
            {
                value = string.Format("{0}__{1}", "c_".RandomString(16), DateTime.UtcNow.ToJulianDate().ToString());
                existing = UserProperty.OneWhere(c => c.Name == propertyName && c.Value == value);
            }
            property.Value = value;
            property.Save();
            return property;
        }

    }
}
