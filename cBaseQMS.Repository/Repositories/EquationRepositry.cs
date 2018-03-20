using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public interface IEquationRepositry : IRepository<Equation>
    {

    }
    public class EquationRepositry : RepositoryBase<Equation>, IEquationRepositry
    {
        public EquationRepositry(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
