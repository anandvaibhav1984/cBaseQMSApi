using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository.Repositories
{
  public  class TestRepositry : RepositoryBase<Test>, ITestRepository
    {
        public TestRepositry(IDbFactory dbFactory) : base(dbFactory) { }

        public Test GetTests(string name)
        {
            throw new NotImplementedException();
        }
        public bool ifExistsTestName(string testname,int testmasterid)
        {
            bool result;           
            result=this.DbContext.Tests.Where(x => x.TestName == testname && x.TestMasterID == testmasterid).Count() > 0;
            return !result;
          
        }
    }

    public interface ITestRepository : IRepository<Test>
    {
        Test GetTests(string name);
        bool ifExistsTestName(string testname, int testmasterid);
    }
}
