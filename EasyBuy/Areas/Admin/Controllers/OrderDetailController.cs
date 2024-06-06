using PlantDelightPortal.Classes;
using PlantDelightPortal.Models;
using System;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        public ActionResult Index(string Id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                RedirectToAction("Index", "Home");

            mdlUserOrderDetail objModel = new mdlUserOrderDetail();

            //try
            //{
            //    if (!string.IsNullOrEmpty(Id))
            //        objModel = new clsOrder().GetUserOrderDetail(Id);
            //}
            //catch (Exception ex)
            //{

            //}
            return View(objModel);
        }
    }
}