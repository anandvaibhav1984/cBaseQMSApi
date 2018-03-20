using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public interface IVWLocationAttributesRepository : IRepository<vWLocationAttribute>
    {

    }
    public class VWLocationAttributesRepository : RepositoryBase<vWLocationAttribute>, IVWLocationAttributesRepository
    {
        public VWLocationAttributesRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
