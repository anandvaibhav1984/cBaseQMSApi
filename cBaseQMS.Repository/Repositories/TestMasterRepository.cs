using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cBaseQms.Repository.Repositories
{
    public class TestMasterRepository : RepositoryBase<TestMaster>, ITestMasterRepository
    {
        public TestMasterRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public TestMaster GetTestMasterByName(string name)
        {
            var testMaster = this.DbContext.TestMasters.Where(x => x.TestMasterName == name).FirstOrDefault();
            return testMaster;
        }

        public bool GetCountTestMasterByName(string name)
        {
            var testMaster = this.DbContext.TestMasters.Where(x => x.TestMasterName == name).ToList();
            return (testMaster.Count == 0);
        }

        public override void Update(TestMaster entity)
        {
            entity.CreatedOn = DateTime.Now;
            base.Update(entity);
        }

        public List<usp_GetTestMasterByTestId> getGetTestMasterByTestId(int? testid)
        {
            return this.DbContext.usp_GetTestMasterByTestId(testid).ToList();
        }
    }

    public interface ITestMasterRepository : IRepository<TestMaster>
    {
        TestMaster GetTestMasterByName(string name);

        bool GetCountTestMasterByName(string name);

        List<usp_GetTestMasterByTestId> getGetTestMasterByTestId(int? testid);
    }
}