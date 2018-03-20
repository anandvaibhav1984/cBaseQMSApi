using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cBaseQMS.Areas.cBaseQMS.Models
{
    public class TestMasterMappingViewModel
    {
        public int TestMasterMapID { get; set; }      
        public int TestMasterID { get; set; }      
        public int PartMasterID { get; set; }       
        public int LocationMasterID { get; set; }
        public string operation { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
      
    }
}