using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository.Repositories
{
    public class LocationMasterRepositry : RepositoryBase<LocationMaster>, ILocationMasterRepositry
    {
        public LocationMasterRepositry(IDbFactory dbFactory) : base(dbFactory) { }
       
    }

    public interface ILocationMasterRepositry : IRepository<LocationMaster>
    {
      
    }
}
