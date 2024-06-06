using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class SubCategoryController : Controller
    {
        public ActionResult Index()
        {
            mdlSubCategory objModel = new mdlSubCategory();
            objModel.lstCategory = new clsCategory().ListAll();
            return View(objModel);
        }

        [HttpGet]
        public ActionResult GetSubCategory()
        {
            clsSubCategory objcls = new clsSubCategory();
            List<mdlSubCategory> lstModel = new List<mdlSubCategory>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlSubCategory objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsSubCategory().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            return Json(new clsSubCategory().GetbyId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlSubCategory objModel)
        {
            return Json(new clsSubCategory().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsSubCategory().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        #region Common Methods
        [HttpPost]
        public ActionResult SubCategoryByCategoryId(string CategoryId)
        {
            clsSubCategory objcls = new clsSubCategory();
            List<mdlSubCategory> lstModel = new List<mdlSubCategory>();
            lstModel = objcls.GetSubCategoryByCategoryId(CategoryId);

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}