using PlantDelightPortal.Models;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlProductImage : mdlgblCommon
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ImagePath { get; set; }
    }
}