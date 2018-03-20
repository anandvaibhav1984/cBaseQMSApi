using cBaseQMS.Repository;
using cBaseQMS.TestMaster.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQMS.Data;

namespace cBaseQMS.Service.TestMaster
{
    public class TestMasterService:ITestMasterService
    {
        private IRepository<cBaseQMS.Data.TestMaster> testMasterRepository;

        public TestMasterService(IRepository<cBaseQMS.Data.TestMaster> tMasterRepository)
        {
            this.testMasterRepository = tMasterRepository;
        }

        //public List<cBaseQMS.Repository.TestMaster> GetAllTestMaster()
        //{
        //    //return testMasterRepository.GetAll().ToList();
        //    return null;
        //}

        List<Data.TestMaster> ITestMasterService.GetAllTestMaster()
        {
            throw new NotImplementedException();
        }
    }
}
