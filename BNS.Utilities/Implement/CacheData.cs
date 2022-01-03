using BNS.Utilities.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
namespace BNS.Utilities.Implement
{
    public class CacheData: ICacheData
    {
        private readonly IMemoryCache _cache;

        public CacheData(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public void AddToCache(string key, object value)
        {
            _cache.Set<object>(key, value);
        }


        public void RemoveFromCache(string key)
        {
            _cache.Remove(key);
        }


        public object GetToCache(string key)
        {
            return _cache.Get<object>(key);
        }

    }
}
