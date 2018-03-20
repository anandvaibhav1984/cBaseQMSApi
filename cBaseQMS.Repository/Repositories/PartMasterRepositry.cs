using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository.Repositories
{
    public class PartMasterRepositry : RepositoryBase<PartMaster>, IPartMasterRepositry
    {
        public PartMasterRepositry(IDbFactory dbFactory) : base(dbFactory) { }

    }

    public interface IPartMasterRepositry : IRepository<PartMaster>
    {

    }
}
