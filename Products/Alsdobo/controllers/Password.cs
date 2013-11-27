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
    [Proxy("password")]
    public partial class Password
    {
        
        public object Create(Alsdobo.Models.Password password)
        {
            return Update(password);
        }

        public object Retrieve(long id)
        {
            return Alsdobo.Models.Password.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

        public object Update(Alsdobo.Models.Password password)
        {
            password.Save();
            return password.ToJsonSafe();
        }
        
        public void Delete(Alsdobo.Models.Password password)
        {
            password.Delete();            
        }

        public object[] Search(QiQuery query)
        {
            return new Alsdobo.Models.Qi.Password().Where(query);
        }

        [Exclude]
        public static Type GetModelType()
        {
            return typeof(Alsdobo.Models.Password);          
        }

    }
}