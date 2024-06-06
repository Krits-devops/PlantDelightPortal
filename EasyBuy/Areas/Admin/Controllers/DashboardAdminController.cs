using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using System.Web.Mvc;

namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class DashboardAdminController : Controller
    {
        public ActionResult Index()
        {
            mdlDashboardAdmin objModel = new mdlDashboardAdmin();

            objModel = new clsDashboardAdmin().GetDashboardData();

            return View(objModel);
        }
    }
}