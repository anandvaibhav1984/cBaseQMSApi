using cBaseQms.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Repository.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        CBaseDevEntities dbContext;

        public CBaseDevEntities Init()
        {
            return dbContext ?? (dbContext = new CBaseDevEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
