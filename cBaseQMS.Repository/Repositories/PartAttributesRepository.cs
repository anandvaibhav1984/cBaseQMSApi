using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public interface IPartAttributesRepository : IRepository<PartAttribute>
    {

    }
    public class PartAttributesRepository : RepositoryBase<PartAttribute>, IPartAttributesRepository
    {
        public PartAttributesRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
