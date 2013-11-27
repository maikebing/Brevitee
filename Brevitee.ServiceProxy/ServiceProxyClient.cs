using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Brevitee;
using Brevitee.Configuration;
using Brevitee.Incubation;

namespace Brevitee.ServiceProxy
{
    public class ServiceProxyClient : WebClient
    {
        public ServiceProxyClient()
        {
        }

        public ServiceProxyClient(string baseAddress)
            : this()
        {
            this.BaseAddress = baseAddress;
            this.Headers["User-Agent"] = UserAgents.IE10;
            this.UseDefaultCredentials = true;
        }

        public string UserAgent
        {
            get
            {
                return this.Headers["User-Agent"];
            }
            set
            {
                this.Headers["User-Agent"] = value;
            }
        }

        /// <summary>
        /// Convert the specified type into a string or a json string if
        /// it is something other than a string or number (int, decimal, long)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal protected string TranslateParameter(object value)
        {
            if (value == null)
            {
                return "null";
            }

            Type type = value.GetType();
            if (type == typeof(string) ||
                type == typeof(int) ||
                type == typeof(decimal) ||
                type == typeof(long))
            {
                return value.ToString();
            }
            else
            {
                return value.ToJson();
            }
        }

        internal protected string ParametersToQueryString(Dictionary<string, object> parameters)
        {
            StringBuilder result = new StringBuilder();
            bool first = true;
            foreach (string key in parameters.Keys)
            {
                if (!first)
                {
                    result.Append("&");
                }

                result.AppendFormat("{0}={1}", key, TranslateParameter(parameters[key]));
                first = false;
            }

            return result.ToString();
        }

        internal protected string ParametersToQueryString(object[] parameters)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0)
                {
                    result.Append("&");
                }

                result.AppendFormat("{0}={1}", i, TranslateParameter(parameters[i]));
            }

            return result.ToString();
        }

        /// <summary>
        /// The BaseAddress to send requests to 
        /// </summary>
        public string BaseAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Make a GET request to the specified path expecting json
        /// and deserialize it as the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <returns></returns>
        public T GetFromJson<T>(string pathAndQuery)
        {
            string url = GetUrl(pathAndQuery);
            string result = DownloadString(url);
            return result.FromJson<T>();
        }

        /// <summary>
        /// Make a GET request to the specified pathAndQuery expecting xml
        /// and deserialize it as the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public T GetFromXml<T>(string pathAndQuery, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.Default;
            }
            string url = GetUrl(pathAndQuery);
            string result = DownloadString(url);
            return result.FromXml<T>(encoding);
        }

        /// <summary>
        /// Post the specified postData to the specified pathAndQuery expecting
        /// json and deserializing it as the specified generic type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public T PostFromJson<T>(string pathAndQuery, string postData)
        {
            string url = GetUrl(pathAndQuery);
            string result = UploadString(url, postData);
            return result.FromJson<T>();
        }

        /// <summary>
        /// Post the specified postData to the specified pathAndQuery expecting
        /// json and deserializing it as the specified generic typ T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pathAndQuery"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public T PostFromXml<T>(string pathAndQuery, string postData)
        {
            string url = GetUrl(pathAndQuery);
            string result = UploadString(url, postData);
            return result.FromXml<T>();
        }

        private string GetUrl(string pathAndQuery)
        {
            string url = string.Format("{0}{1}", BaseAddress, pathAndQuery);
            return url;
        }

    }
}
