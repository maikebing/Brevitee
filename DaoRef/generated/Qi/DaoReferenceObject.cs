using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using System.Web.Mvc;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.DaoRef.Qi
{
    public class DaoReferenceObjectController : DaoController
    {
        public ActionResult Save(Brevitee.DaoRef.DaoReferenceObject[] values)
        {
            try
            {
                DaoReferenceObjectCollection saver = new DaoReferenceObjectCollection();
                saver.AddRange(values);
                saver.Save();
                return Json(new { Success = true, Message = "", Dao = "" });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult Create(Brevitee.DaoRef.DaoReferenceObject dao)
        {
            return Update(dao);
        }

        public ActionResult Retrieve(long id)
        {
            try
            {
                object value = Brevitee.DaoRef.DaoReferenceObject.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
                return Json(new { Success = true, Message = "", Dao = value });
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        public ActionResult Update(Brevitee.DaoRef.DaoReferenceObject dao)
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
                Brevitee.DaoRef.DaoReferenceObject dao = Brevitee.DaoRef.DaoReferenceObject.OneWhere(c => c.KeyColumn == id);
                if (dao != null)
                {
                    dao.Delete();
                }
                else
                {
                    msg = string.Format("The specified id ({0}) was not found in the table (DaoReferenceObject)", id);
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
                object value = Brevitee.DaoRef.DaoReferenceObject.OneWhere(query).ToJsonSafe();
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
                object[] value = Brevitee.DaoRef.DaoReferenceObject.Where(query).ToJsonSafe();
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