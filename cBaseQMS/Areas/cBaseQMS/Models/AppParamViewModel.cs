using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Areas.cBaseQMS.Models
{
    public class AppParamViewModel
    {
        public int AppParamID { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}