using PlantDelightPortal.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlProvince : mdlgblCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public List<mdlCountry> lstCountry { get; set; }
    }
}