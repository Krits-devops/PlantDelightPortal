using System.Collections.Generic;

namespace PlantDelightPortal.Models
{
    public class mdlUserOrderDetail
    {
        public string OrderNo { get; set; }
        public decimal ItemCount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalAmt { get; set; }
        public List<mdlProductDetail> lstProductDetail { get; set; }
        public mdlShipping objmdlShipping { get; set; }
    }
}