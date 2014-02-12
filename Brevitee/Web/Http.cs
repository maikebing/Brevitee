using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

namespace Brevitee.Web
{
    public static class Http
    {
        public static string GetString(string url, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.DownloadString(url);
        }
        
        public static string PostString(string url, string postData, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.UploadString(url, postData);
        }

        public static byte[] GetData(string url, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.DownloadData(url);
        }

        public static byte[] PostData(string url, byte[] postData, Dictionary<string, string> headers = null)
        {
            WebClient client = GetClient();
            SetHeaders(headers, client);
            return client.UploadData(url, postData);
        }

        private static WebClient GetClient(string agent = null)
        {
            if (string.IsNullOrEmpty(agent))
            {
                agent = UserAgents.FF10;
            }
            WebClient client = new WebClient();
            client.Headers["User-Agent"] = agent;
            client.UseDefaultCredentials = true;

            return client;
        }

        private static void SetHeaders(Dictionary<string, string> headers, WebClient client)
        {
            if (headers != null)
            {
                headers.Keys.Each(key =>
                {
                    client.Headers[key] = headers[key];
                });
            }
        }
    }
}
