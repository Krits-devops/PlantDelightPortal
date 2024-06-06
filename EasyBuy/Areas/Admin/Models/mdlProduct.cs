using PlantDelightPortal.Models;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlProduct : mdlgblCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public decimal Price { get; set; }
        public decimal StkQty { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string JsonProductAttributeList { get; set; }
        public List<mdlCategory> lstCategory { get; set; }
        public List<mdlSubCategory> lstSubCategory { get; set; }
        public string ImagePath { get; set; }
        public decimal Rating { get; set; }
        public bool InCart { get; set; }
        public bool InWishList { get; set; }
        public List<mdlProductImage> lstProductImage { get; set; }
        public List<mdlProductAttribute> lstProductAttribute { get; set; }
    }
}