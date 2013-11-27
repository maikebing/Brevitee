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
    [Proxy("role")]
    public partial class Role
    {
        
        public object Create(Alsdobo.Models.Role role)
        {
            return Update(role);
        }

        public object Retrieve(long id)
        {
            return Alsdobo.Models.Role.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

        public object Update(Alsdobo.Models.Role role)
        {
            role.Save();
            return role.ToJsonSafe();
        }
        
        public void Delete(Alsdobo.Models.Role role)
        {
            role.Delete();            
        }

        public object[] Search(QiQuery query)
        {
            return new Alsdobo.Models.Qi.Role().Where(query);
        }

        [Exclude]
        public static Type GetModelType()
        {
            return typeof(Alsdobo.Models.Role);          
        }

    }
}