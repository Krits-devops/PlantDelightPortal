using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class CityController : Controller
    {
        public ActionResult Index()
        {
            mdlCity objModel = new mdlCity();
            objModel.lstCountry = new clsCountry().ListAll();
            objModel.lstProvince = new clsProvince().ListAll();
            return View(objModel);
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            clsCity objcls = new clsCity();
            List<mdlCity> lstModel = new List<mdlCity>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlCity objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsCity().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            mdlCity objModel = new clsCity().GetbyId(Id);
            List<mdlProvince> lstProvince = new List<mdlProvince>();
            lstProvince = new clsProvince().GetProvinceByCountryId(objModel.CountryId);

            return Json(new { objModel = objModel, lstProvince = lstProvince }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlCity objModel)
        {
            return Json(new clsCity().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsCity().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        #region Common Methods
        [HttpPost]
        public ActionResult CityByProvinceId(string ProvinceId)
        {
            List<mdlCity> lstModel = new List<mdlCity>();
            lstModel = new clsCity().GetCityByProvinceId(ProvinceId);

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}