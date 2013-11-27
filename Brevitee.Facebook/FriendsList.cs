using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net;
using Brevitee.Hatagi.Data;

namespace Brevitee.Facebook
{
    public class FriendsList
    {
        public FriendsList(JObject jObject)
        {
            Initialize(jObject);
        }

        public FriendsList(string json)
            : this(JObject.Parse(json))
        {
        }

        public FriendsList(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User must be specified");
            }

            using (WebClient client = new WebClient())
            {
                string url = GetFriendsUrl(user);
                JObject jObject = JObject.Parse(client.DownloadString(url));
                Initialize(jObject);
            }
        }

        private static string GetFriendsUrl(User user)
        {
            string url = string.Format(FacebookHelper.FriendsUrlFormat, user.SourceId);
            url = FacebookHelper.AppendAccessToken(url);
            return url;
        }

        private void Initialize(JObject jObject)
        {
            this.JObject = jObject;
            JArray friends = (JArray)jObject["data"];
            this.Data = new Friend[friends.Count];
            for (int i = 0; i < Data.Length; i++)
            {
                this.Data[i] = new Friend((JObject)friends[i]);
            }

            this.Paging = new Paging((JObject)jObject["paging"]);
        }


        internal JObject JObject { get; set; }

        public Friend[] Data { get; set; }

        public Paging Paging { get; set; }

        public bool Next(out FriendsList nextFriendsList)
        {
            if (Paging != null && !string.IsNullOrEmpty(Paging.Next))
            {
                using (WebClient client = new WebClient())
                {
                    string url = Paging.Next;
                    JObject jObject = JObject.Parse(client.DownloadString(url));
                    nextFriendsList = new FriendsList(jObject);
                    return true;
                }
            }
            else
            {
                nextFriendsList = this;
                return false;
            }
        }
    }
}
