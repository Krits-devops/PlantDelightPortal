using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlrptOrder
    {
        public string Flag { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderNo { get; set; }
        public string StatusId { get; set; }
        public string UpdateStatusId { get; set; }
        public List<mdlOrderStatus> lstOrderStatus { get; set; }
    }

    public class mdlrptOrderDetail
    {
        public string Id { get; set; }
        public string SrNo { get; set; }
        public string OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string SName { get; set; }
        public string OrderAmount { get; set; }
        public string OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
    }
}