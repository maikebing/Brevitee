using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Brevitee.Distributed
{
    public class ComputeNode: IRepositoryProvider, IHasInfo
    {
        [Info("The host name of the compute node.")]
        public string HostName { get; set; }

        public IPAddress IPAddress
        {
            get
            {
                IPAddress result = null;
                if (IPV4Address != null)
                {
                    result = IPV4Address;
                }

                if (result == null && IPV6Address != null)
                {
                    result = IPV6Address;
                }

                return result;
            }         
        }

        [Info("The ipv4 address of the compute node")]
        public IPAddress IPV4Address { get; set; }

        [Info("The ipv6 address of the compute node")]
        public IPAddress IPV6Address { get; set; }

        public static ComputeNode FromCurrentHost()
        {
            ComputeNode result = new ComputeNode();
            string hostName = Dns.GetHostName();
            result.HostName = hostName;

            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    result.IPV4Address = address;
                }
                else if (address.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    result.IPV6Address = address;
                }
            }

            return result;
        }

        public IRepositoryProvider RepositoryProvider
        {
            get;
            set;
        }

        #region IRepositoryProvider Members

        public virtual void Create(object value)
        {
            EnsureProvider();
            RepositoryProvider.Create(value);
        }

        private void EnsureProvider()
        {
            Expect.IsNotNull(RepositoryProvider, "The RepositoryProvider must be specified");
        }

        public virtual T Retrieve<T>(object key)
        {
            EnsureProvider();
            return RepositoryProvider.Retrieve<T>(key);
        }

        public virtual void Update(object value)
        {
            EnsureProvider();
            RepositoryProvider.Update(value);
        }

        public virtual IEnumerable<T> Search<T>(object query)
        {
            EnsureProvider();
            return RepositoryProvider.Search<T>(query);
        }

        public virtual void Delete(object value)
        {
            EnsureProvider();
            RepositoryProvider.Delete(value);
        }
        #endregion

        #region IHasInfo Members

        public object GetInfo()
        {
            return InfoAttribute.GetInfo(this);
        }

        public Dictionary<string, string> GetInfoDictionary()
        {
            return InfoAttribute.GetInfoDictionary(this);
        }

        #endregion

        public override string ToString()
        {
            string host = this.HostName ?? "null";
            string ipv4 = this.IPV4Address == null ? "null" : this.IPV4Address.ToString();
            string ipv6 = this.IPV6Address == null ? "null" : this.IPV6Address.ToString();
            return "HostName:{0},IPV4:{1},IPV6:{2}"._Format(host, ipv4, ipv6);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ComputeNode))
            {
                return obj.GetHashCode().Equals(this.GetHashCode());
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
