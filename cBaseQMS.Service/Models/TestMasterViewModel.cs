
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  cBaseQMS.Service.Models
{
    
    public class TestMasterViewModel
    {
        public int TestMasterID { get; set; }
        public string TestMasterName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string operation { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
       

    }

   }