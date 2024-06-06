using System.Collections.Generic;
using PlantDelightPortal.Areas.Admin.Models;

namespace PlantDelightPortal.Models
{
    public class mdlUserMaster : mdlgblCommon
    {
        #region Table Properties
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string GenderId { get; set; }
        public string SecurityQueId { get; set; }
        public string SecurityQue { get; set; }
        public string SecurityAns { get; set; }
        public bool IsAdmin { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string ProfileImage { get; set; }
        public bool GoogleLogin { get; set; }
        #endregion

        #region List
        public List<mdlGender> lstGender { get; set; }
        public List<mdlSecurityQuestions> lstSecurityQue { get; set; }
        #endregion
    }
}