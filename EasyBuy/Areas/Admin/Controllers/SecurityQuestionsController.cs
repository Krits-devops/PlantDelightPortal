using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class SecurityQuestionsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSecurityQuestions()
        {
            clsSecurityQuestions objcls = new clsSecurityQuestions();
            List<mdlSecurityQuestions> lstModel = new List<mdlSecurityQuestions>();
            lstModel = objcls.ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(mdlSecurityQuestions objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsSecurityQuestions().Add(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult getbyId(string Id)
        {
            return Json(new clsSecurityQuestions().GetbyId(Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(mdlSecurityQuestions objModel)
        {
            return Json(new clsSecurityQuestions().Update(objModel), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsSecurityQuestions().Delete(Id), JsonRequestBehavior.AllowGet);
        }
    }
}