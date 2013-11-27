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
    [Proxy("session")]
    public partial class Session
    {
        
        public object Create(Alsdobo.Models.Session session)
        {
            return Update(session);
        }

        public object Retrieve(long id)
        {
            return Alsdobo.Models.Session.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

        public object Update(Alsdobo.Models.Session session)
        {
            session.Save();
            return session.ToJsonSafe();
        }
        
        public void Delete(Alsdobo.Models.Session session)
        {
            session.Delete();            
        }

        public object[] Search(QiQuery query)
        {
            return new Alsdobo.Models.Qi.Session().Where(query);
        }

        [Exclude]
        public static Type GetModelType()
        {
            return typeof(Alsdobo.Models.Session);          
        }

    }
}