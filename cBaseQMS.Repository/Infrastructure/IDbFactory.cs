using System;

namespace cBaseQms.Repository.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CBaseDevEntities Init();
    }
}