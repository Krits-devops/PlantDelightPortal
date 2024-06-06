using PlantDelightPortal.Classes;
using PlantDelightPortal.Models;
using System.Web.Mvc;

namespace PlantDelightPortal.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckDataExists(string TableName = "", string ColumnName = "", string ColumnValue = "", string WhereCondition = "")
        {
            int ResultCount = 0;
            ResultCount = new clsCommon().CheckDataExists(TableName, ColumnName, ColumnValue, WhereCondition);

            return Json(new { cnt = ResultCount });
        }
    }
}