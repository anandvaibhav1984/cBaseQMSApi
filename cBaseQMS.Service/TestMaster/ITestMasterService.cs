using cBaseQMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.TestMaster.Service
{
    public interface ITestMasterService
    {
        List<cBaseQMS.Data.TestMaster> GetAllTestMaster();
        
    }
}
