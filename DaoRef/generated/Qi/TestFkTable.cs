using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using System.Web.Mvc;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef.Qi
{
    public class TestFkTableController : DaoController
    {
        public ActionResult Save(Brevitee.DaoRef.TestFkTable[] values)
        {
            try
            {
                TestFkTableCollection saver = new TestFkTableCollection();
                saver.AddRange(values);
                saver.Save();
                return Json(new { Success = true, Message = "", Dao = "" });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult Create(Brevitee.DaoRef.TestFkTable dao)
        {
            return Update(dao);
        }

        public ActionResult Retrieve(long id)
        {
            try
            {
                object value = Brevitee.DaoRef.TestFkTable.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
                return Json(new { Success = true, Message = "", Dao = value });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult Update(Brevitee.DaoRef.TestFkTable dao)
        {
            try
            {
                dao.Save();
                return Json(new { Success = true, Message = "", Dao = dao.ToJsonSafe() });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult Delete(long id)
        {
            try
            {
                string msg = "";
                Brevitee.DaoRef.TestFkTable dao = Brevitee.DaoRef.TestFkTable.OneWhere(c => c.KeyColumn == id);
                if (dao != null)
                {
                    dao.Delete();
                }
                else
                {
                    msg = string.Format("The specified id ({0}) was not found in the table (TestFkTable)", id);
                }
                return Json(new { Success = true, Message = msg, Dao = "" });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult OneWhere(QiQuery query)
        {
            try
            {
                object value = Brevitee.DaoRef.TestFkTable.OneWhere(query).ToJsonSafe();
                return Json(new { Success = true, Message = "", Dao = value });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult Where(QiQuery query)
        {
            try
            {
                object[] value = Brevitee.DaoRef.TestFkTable.Where(query).ToJsonSafe();
                return Json(new { Success = true, Message = "", Dao = value });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        private ActionResult GetErrorResult(Exception ex)
        {
            return Json(new { Success = false, Message = ex.Message });
        }
    }
}