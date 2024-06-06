using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlrptSales
    {
        public string Flag { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string StatusId { get; set; }
        public List<mdlOrderStatus> lstOrderStatus { get; set; }
    }

    public class mdlrptSalesDetail
    {
        public string Id { get; set; }
        public string SrNo { get; set; }
        public string OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string ItemName { get; set; }
        public string Amount { get; set; }
        public string OrderStatus { get; set; }
    }
}