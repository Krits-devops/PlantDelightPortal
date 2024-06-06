using PlantDelightPortal.Models;
using System.Collections.Generic;

namespace PlantDelightPortal.Areas.Admin.Models
{
    public class mdlAttributeChoice : mdlgblCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AttributeId { get; set; }
        public string AttributeName { get; set; }
        public List<mdlAttribute> lstAttribute { get; set; }
    }
}