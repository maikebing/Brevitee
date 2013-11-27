using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Brevitee.Data
{
    public class DaoController: Controller
    {
        public ActionResult Default()
        {
            return JavaScript("alert('dao.default: You probably didn't intend for this');");
        }

        public ActionResult Proxies(bool min = false)
        {
            return new DaoProxyResult(min);
        }
    }
}
