using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brevitee.UserAccounts;

namespace Brevitee.Shop
{
    [Proxy]
    public class Shopper
    {  
        /// <summary>
        /// Get a Shopper instance representing the current user
        /// </summary>
        /// <returns></returns>
        protected internal static Shopper GetCurrent()
        {
            throw new NotImplementedException();
        }

        public void AddToCart(string source, string sourceId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCart()
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(string source, string sourceId)
        {
            throw new NotImplementedException();
        }

    }
}
