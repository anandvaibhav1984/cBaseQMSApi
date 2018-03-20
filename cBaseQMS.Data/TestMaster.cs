using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.Data
{
    public class TestMaster: BaseEntity
    {
        public int TestMasterID { get; set; }
        public string TestMasterName { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Tests> Tests { get; set; }
    }
}
