using PlantDelightPortal.Areas.Admin.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Models
{
    public class mdlProductRatingReview
    {
        #region Table Properties
        public string Id { get; set; }
        public string ProductId { get; set; }
        public decimal Rating { get; set; }
        public string Review { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        #endregion

        #region Other
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public decimal StkQty { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal ProductRating { get; set; }
        public List<mdlProductAttribute> lstProductAttribute { get; set; }
        #endregion
    }
}