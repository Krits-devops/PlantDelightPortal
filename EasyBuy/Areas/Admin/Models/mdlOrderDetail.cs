using PlantDelightPortal.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlOrderDetail
    {
        public string OrderNo { get; set; }
        public decimal ItemCount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalAmt { get; set; }
        public List<mdlProductDetail> lstProductDetail { get; set; }
    }
}