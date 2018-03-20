using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public interface IComponentRepositry : IRepository<Component>
    {

    }

    public class ComponentRepositry : RepositoryBase<Component>, IComponentRepositry
    {
        public ComponentRepositry(IDbFactory dbFactory) : base(dbFactory) { }

    }
}
