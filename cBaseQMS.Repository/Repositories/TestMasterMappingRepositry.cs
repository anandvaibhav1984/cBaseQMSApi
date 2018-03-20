using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository.Repositories
{
  public  class TestMasterMappingRepositry : RepositoryBase<TestMasterMapping>, ITestMasterMappingRepositry
    {
       

        public TestMasterMappingRepositry(IDbFactory dbFactory) : base(dbFactory) { }

      

        public List<usp_GetLocationAndPartMapping> GetAllLocationAndPartMasterMapping(int ? testmasterid)
        {
            return this.DbContext.usp_GetLocationAndPartMapping(testmasterid).ToList();
        }

        public Test GetTestMasterMapping(int testMasterId)
        {
            throw new NotImplementedException();
        }

        public bool IfExistsPartAndLocationCombination(int locationId, int partId,int testMasterId)
        {
            
            var count= this.DbContext.TestMasterMappings.Where(p => p.LocationMasterID == locationId && p.PartMasterID == partId && p.TestMasterID==testMasterId).Count();
            return (count == 0);
          
        }
    }

    public interface ITestMasterMappingRepositry : IRepository<TestMasterMapping>
    {
        Test GetTestMasterMapping(int testMasterId );
        List<usp_GetLocationAndPartMapping> GetAllLocationAndPartMasterMapping(int ? testmasterid);
        bool IfExistsPartAndLocationCombination(int locationId, int partId, int testMasterId);
    }
}
