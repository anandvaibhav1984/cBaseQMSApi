using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Areas.cBaseQMS.Models
{
    public class GetLocationAndPartMappingViewModel
    {
        public int TestMasterMapID { get; set; }
        public string LocationName { get; set; }
        public string PartNumber { get; set; }
        public int TestMasterID { get; set; }
    }
}