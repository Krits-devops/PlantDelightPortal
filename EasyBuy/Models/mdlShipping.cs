using PlantDelightPortal.Areas.Admin.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Models
{
    public class mdlShipping : mdlgblCommon
    {
        #region Table Properties
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }

        public string CountryId { get; set; }
        public List<mdlCountry> lstCountry { get; set; }
        public string ProvinceId { get; set; }
        public List<mdlProvince> lstProvince { get; set; }
        public string CityId { get; set; }
        public List<mdlCity> lstCity { get; set; }

        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        #endregion
    }
}