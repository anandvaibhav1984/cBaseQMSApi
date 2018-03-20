using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public class TestExpressionRepository : RepositoryBase<TestExpressionRepository>, ITestExpressionRepository
    {
        public TestExpressionRepository(IDbFactory dbFactory) : base(dbFactory) { }
        
    }
    public interface ITestExpressionRepository : IRepository<TestExpressionRepository>
    {
        
    }
}
