using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  cBaseQMS.Service.Models
{
    public class usp_GetTestMasterByTestIdViewModel
    {
        public int TestMasterID { get; set; }
        public string TestMasterName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}