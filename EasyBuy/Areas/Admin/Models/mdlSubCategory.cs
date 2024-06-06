using PlantDelightPortal.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlSubCategory : mdlgblCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<mdlCategory> lstCategory { get; set; }
    }
}