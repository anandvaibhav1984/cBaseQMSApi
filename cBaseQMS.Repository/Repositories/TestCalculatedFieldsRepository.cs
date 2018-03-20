using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public interface ITestCalculatedFieldsRepository : IRepository<TestCalculatedField>
    {

    }
    public class TestCalculatedFieldsRepository : RepositoryBase<TestCalculatedField>, ITestCalculatedFieldsRepository
    {
        public TestCalculatedFieldsRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
