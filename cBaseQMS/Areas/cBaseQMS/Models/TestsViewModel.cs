
using System;
using System.Collections.Generic;

namespace cBaseQMS.Areas.cBaseQMS.Models
{
    public class TestsViewModel
    {
        public int TestID { get; set; }
        public int TestMasterID { get; set; }
        public string TestName { get; set; }
        public string Descriptions { get; set; }
        public string Operation { get; set; }
        public int Sequence { get; set; }
        public string Expression { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }    
    }
}