using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brevitee.Distributed
{
    /// <summary>
    /// Implementers of this interface should 
    /// have properties addorned with the InfoAttribute
    /// attribute.  Each property will be included 
    /// as properties on the object returned by 
    /// GetInfo as well as the Dictionary returned
    /// by GetInfoDictionary
    /// </summary>
    public interface IHasInfo
    {
        object GetInfo();
        Dictionary<string, string> GetInfoDictionary();
    }
}
