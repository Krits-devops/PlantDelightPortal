using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class GenderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetGender()
        {
            clsGender objcls = new clsGender();
            List<mdlGender> lstModel = new List<mdlGender>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlGender objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsGender().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            return Json(new clsGender().GetbyId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlGender objModel)
        {
            return Json(new clsGender().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsGender().Delete(Id), JsonRequestBehavior.AllowGet);
        }
    }
}