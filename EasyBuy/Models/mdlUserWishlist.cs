﻿namespace PlantDelightPortal.Models
{
    public class mdlUserWishlist
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
        public string ImagePath { get; set; }
        public decimal Rating { get; set; }
        public bool InCart { get; set; }
        public bool InWishList { get; set; }
    }
}