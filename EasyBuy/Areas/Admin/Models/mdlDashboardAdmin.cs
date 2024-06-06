namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlDashboardAdmin
    {
        public decimal OrderToday { get; set; }
        public decimal OrderThisMonth { get; set; }
        public decimal OrderThisYear { get; set; }

        public decimal SalesToday { get; set; }
        public decimal SalesThisMonth { get; set; }
        public decimal SalesThisYear { get; set; }

        public decimal UserToday { get; set; }
        public decimal UserThisMonth { get; set; }
        public decimal UserThisYear { get; set; }
    }
}