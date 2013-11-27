using System;
using System.Data;
using System.Data.Common;
using Brevitee;
using Brevitee.Data;
using Brevitee.Data.Schema; 
using Brevitee.Data.Qi;
using Alsdobo.Models;

namespace Alsdobo.Controllers
{
    [Proxy("user")]
    public partial class User
    {
        
        public object Create(Alsdobo.Models.User user)
        {
            return Update(user);
        }

        public object Retrieve(long id)
        {
            return Alsdobo.Models.User.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

        public object Update(Alsdobo.Models.User user)
        {
            user.Save();
            return user.ToJsonSafe();
        }
        
        public void Delete(Alsdobo.Models.User user)
        {
            user.Delete();            
        }

        public object[] Search(QiQuery query)
        {
            return new Alsdobo.Models.Qi.User().Where(query);
        }

        [Exclude]
        public static Type GetModelType()
        {
            return typeof(Alsdobo.Models.User);          
        }

    }
}