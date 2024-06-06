using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlrptUser
    {
        public string Flag { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class mdlrptUserDetail
    {
        public string SrNo { get; set; }
        public string ProfileImage { get; set; }
        public string RegDate { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string GoogleLogin { get; set; }
    }
}