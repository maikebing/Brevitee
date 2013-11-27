using System;
using System.Collections.Generic;
using System.Text;
using Brevitee;
using System.Web.Mvc;
using Brevitee.Data;
using Brevitee.Data.Qi;

namespace Brevitee.BattleStickers.Qi
{
    public class PlayerOneEquipmentController : DaoController
    {	
		public ActionResult Save(Brevitee.BattleStickers.PlayerOneEquipment[] values)
		{
			try
			{
				PlayerOneEquipmentCollection saver = new PlayerOneEquipmentCollection();
				saver.AddRange(values);
				saver.Save();
				return Json(new { Success = true, Message = "", Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult Create(Brevitee.BattleStickers.PlayerOneEquipment dao)
		{
			return Update(dao);
		}

		public ActionResult Retrieve(long id)
        {
			try
			{
				object value = Brevitee.BattleStickers.PlayerOneEquipment.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
        }

		public ActionResult Update(Brevitee.BattleStickers.PlayerOneEquipment dao)
        {
			try
			{
				dao.Save();
				return Json(new { Success = true, Message = "", Dao = dao.ToJsonSafe() });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}            
        }

		public ActionResult Delete(long id)
		{
			try
			{
				string msg = "";
				Brevitee.BattleStickers.PlayerOneEquipment dao = Brevitee.BattleStickers.PlayerOneEquipment.OneWhere(c => c.KeyColumn == id);				
				if(dao != null)
				{
					dao.Delete();	
				}
				else
				{
					msg = string.Format("The specified id ({0}) was not found in the table (PlayerOneEquipment)", id);
				}
				return Json(new { Success = true, Message = msg, Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult OneWhere(QiQuery query)
		{
			try
			{
				query.table = Dao.TableName(typeof(PlayerOneEquipment));
				object value = Brevitee.BattleStickers.PlayerOneEquipment.OneWhere(query).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}	 			
		}

		public ActionResult Where(QiQuery query)
		{
			try
			{
				query.table = Dao.TableName(typeof(PlayerOneEquipment));
				object[] value = Brevitee.BattleStickers.PlayerOneEquipment.Where(query).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
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