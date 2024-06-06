using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class AttributeController : Controller
    {
        public ActionResult Index()
        {
            mdlAttribute objModel = new mdlAttribute();
            objModel.lstCategory = new clsCategory().ListAll();
            objModel.lstSubCategory = new clsSubCategory().ListAll();
            return View(objModel);
        }

        [HttpGet]
        public ActionResult GetAttribute()
        {
            clsAttribute objcls = new clsAttribute();
            List<mdlAttribute> lstModel = new List<mdlAttribute>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlAttribute objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsAttribute().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            mdlAttribute objModel = new clsAttribute().GetbyId(Id);
            List<mdlSubCategory> lstSubCategory = new List<mdlSubCategory>();
            lstSubCategory = new clsSubCategory().GetSubCategoryByCategoryId(objModel.CategoryId);

            return Json(new { objModel = objModel, lstSubCategory = lstSubCategory }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlAttribute objModel)
        {
            return Json(new clsAttribute().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsAttribute().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        #region Common Methods
        [HttpPost]
        public ActionResult AttributeBySubCategoryId(string SubCategoryId)
        {
            clsAttribute objcls = new clsAttribute();
            List<mdlAttribute> lstModel = new List<mdlAttribute>();
            lstModel = objcls.GetAttributeBySubCategoryId(SubCategoryId);

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}