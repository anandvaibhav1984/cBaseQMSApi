using cBaseQMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQMS.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(long id);
        //IQueryable<T> GetQueryable(long id);
        IQueryable<T> GetQueryable();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
