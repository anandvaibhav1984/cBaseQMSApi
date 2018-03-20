using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  cBaseQMS.Service.Models
{
    public class PartMasterViewModel
    {
        public int PartMasterID { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> UpdatedTimestamp { get; set; }
    }
}