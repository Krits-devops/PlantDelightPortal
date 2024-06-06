using PlantDelightPortal.Areas.Admin.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Models
{
    public class mdlProductDetail
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal StkQty { get; set; }
        public string Description { get; set; }
        public string SubCategoryId { get; set; }
        public decimal Rating { get; set; }
        public bool InCart { get; set; }
        public bool InWishList { get; set; }
        public bool IsRated { get; set; }
        public List<mdlProductImage> lstProductImage { get; set; }
        public List<mdlProductAttribute> lstProductAttribute { get; set; }
        public List<mdlProductRatingReview> lstProductRatingReview { get; set; }
        public List<mdlProduct> lstRelatedProduct { get; set; }
    }
}