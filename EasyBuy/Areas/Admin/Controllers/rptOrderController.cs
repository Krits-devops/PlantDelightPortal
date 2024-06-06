using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class rptOrderController : Controller
    {
        public ActionResult rptOrder(string Flag = "")
        {
            mdlrptOrder objModel = new mdlrptOrder();

            if (!string.IsNullOrEmpty(Flag))
            {
                objModel.Flag = Flag;

                if (objModel.Flag == "Today")
                {
                    objModel.FromDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                    objModel.ToDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
                }
                else if (objModel.Flag == "Monthly")
                {
                    DateTime now = DateTime.Now;
                    DateTime startDate = new DateTime(now.Year, now.Month, 1);
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                    objModel.FromDate = startDate.ToString("dd/MM/yyyy").Replace("-", "/");
                    objModel.ToDate = endDate.ToString("dd/MM/yyyy").Replace("-", "/");
                }
                else if (objModel.Flag == "Yearly")
                {
                    int year = DateTime.Now.Year;
                    DateTime startDate = new DateTime(year, 1, 1);
                    DateTime endDate = new DateTime(year, 12, 31);

                    objModel.FromDate = startDate.ToString("dd/MM/yyyy").Replace("-", "/");
                    objModel.ToDate = endDate.ToString("dd/MM/yyyy").Replace("-", "/");
                }
            }
            else
            {
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, 1);

                objModel.FromDate = startDate.ToString("dd/MM/yyyy").Replace("-", "/");
                objModel.ToDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            }

            List<mdlOrderStatus> lstOrderStatus = new List<mdlOrderStatus>();
            objModel.lstOrderStatus = new clsOrderStatus().ListAll();

            return View(objModel);
        }

        [HttpPost]
        public ActionResult rptOrderData(mdlrptOrder objModel)
        {
            List<mdlrptOrderDetail> lstrptOrderDetail = new List<mdlrptOrderDetail>();
            lstrptOrderDetail = new clsrptOrder().GetReportData(objModel);

            return Json(lstrptOrderDetail, JsonRequestBehavior.AllowGet);
        }
    }
}