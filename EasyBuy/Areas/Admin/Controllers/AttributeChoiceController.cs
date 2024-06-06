using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class AttributeChoiceController : Controller
    {
        public ActionResult Index()
        {
            mdlAttributeChoice objModel = new mdlAttributeChoice();
            objModel.lstAttribute = new clsAttribute().ListAll();
            return View(objModel);
        }

        [HttpGet]
        public ActionResult GetAttributeChoice()
        {
            clsAttributeChoice objcls = new clsAttributeChoice();
            List<mdlAttributeChoice> lstModel = new List<mdlAttributeChoice>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlAttributeChoice objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsAttributeChoice().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            return Json(new clsAttributeChoice().GetbyId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlAttributeChoice objModel)
        {
            return Json(new clsAttributeChoice().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsAttributeChoice().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        #region Common Methods
        [HttpPost]
        public ActionResult AttributeChoiceByAttributeId(string AttributeId)
        {
            clsAttributeChoice objcls = new clsAttributeChoice();
            List<mdlAttributeChoice> lstModel = new List<mdlAttributeChoice>();
            lstModel = objcls.GetAttributeChoiceByAttributeId(AttributeId);

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}