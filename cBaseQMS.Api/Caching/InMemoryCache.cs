using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace cBaseQMS.Api.Caching
{
    public class InMemoryCache : ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback,bool redirect) where T : class
        {
            T item=null;
            //T item = !shouldBeKeeped ? MemoryCache.Default.Remove(cacheKey) as T:  MemoryCache.Default.Get(cacheKey) as T;
            if (redirect)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
            else
            {
                item = MemoryCache.Default.Get(cacheKey) as T;
            }
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(1));
            }
            return item;
        }
    }

    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, bool redirect =false) where T : class;
    }
}