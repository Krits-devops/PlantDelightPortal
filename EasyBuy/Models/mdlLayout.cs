using System.Collections.Generic;
using PlantDelightPortal.Areas.Admin.Models;

namespace PlantDelightPortal.Models
{
    public class mdlLayout : mdlgblCommon
    {
        public string GenderId { get; set; }
        public string SecurityQueId { get; set; }
        public List<mdlGender> lstGender { get; set; }
        public List<mdlSecurityQuestions> lstSecurityQue { get; set; }
    }
}