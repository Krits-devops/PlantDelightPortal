using PlantDelightPortal.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlCity : mdlgblCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public List<mdlCountry> lstCountry { get; set; }
        public List<mdlProvince> lstProvince { get; set; }
    }
}