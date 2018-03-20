using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public interface ITestInputFieldsRepositry : IRepository<InputField>
    {

    }
    public class TestInputFieldsRepositry : RepositoryBase<InputField>, ITestInputFieldsRepositry
    {
        public TestInputFieldsRepositry(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
