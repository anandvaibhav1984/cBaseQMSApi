using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Service.Models
{
    public class LocationMasterViewModel
    {
        public int LocationMasterID { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> UpdatedTimestamp { get; set; }
    }
}