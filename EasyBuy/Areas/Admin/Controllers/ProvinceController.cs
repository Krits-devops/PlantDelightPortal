using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class ProvinceController : Controller
    {
        public ActionResult Index()
        {
            mdlProvince objModel = new mdlProvince();
            objModel.lstCountry = new clsCountry().ListAll();
            return View(objModel);
        }

        [HttpGet]
        public ActionResult GetProvinces()
        {
            clsProvince objcls = new clsProvince();
            List<mdlProvince> lstModel = new List<mdlProvince>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlProvince objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsProvince().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            return Json(new clsProvince().GetbyId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlProvince objModel)
        {
            return Json(new clsProvince().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsProvince().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        #region Common Methods
        [HttpPost]
        public ActionResult ProvinceByCountryId(string CountryId)
        {
            clsProvince objcls = new clsProvince();
            List<mdlProvince> lstModel = new List<mdlProvince>();
            lstModel = objcls.GetProvinceByCountryId(CountryId);

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}