using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class OrderStatusController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetOrderStatus()
        {
            clsOrderStatus objcls = new clsOrderStatus();
            List<mdlOrderStatus> lstModel = new List<mdlOrderStatus>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlOrderStatus objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsOrderStatus().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            return Json(new clsOrderStatus().GetbyId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlOrderStatus objModel)
        {
            return Json(new clsOrderStatus().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsOrderStatus().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateOrderStatus(string OrderId, string OrderStatusId)
        {
            return Json(new clsOrderStatus().UpdateOrderStatus(OrderId, OrderStatusId), JsonRequestBehavior.AllowGet);
        }
    }
}