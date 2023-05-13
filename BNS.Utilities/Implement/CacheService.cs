﻿using BNS.Utilities.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace BNS.Utilities.Implement
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void AddToCache(string key, object value)
        {
            _distributedCache.SetString(key, JsonConvert.SerializeObject(value));
        }

        public void RemoveFromCache(string key)
        {
            _distributedCache.Remove(key);
        }

        public T GetToCache<T>(string key)
        {
            var cacheData = _distributedCache.GetString(key);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<T>(cacheData);
            }
            return default(T);
        }

        public string GetCacheKey(Enums.EControllerKey controllerKey, Guid? objectId, Guid? companyId)
        {
            string key = controllerKey.ToString();
            if (objectId != null)
            {
                key = string.Format("{0}-{1}", key, objectId.Value.ToString());
            }
            if (companyId != null)
            {
                key = string.Format("{0}-{1}", key, companyId.Value.ToString());
            }
            return key;
        }

        public string GetCacheNotifyKey(string accountCompanyId)
        {
            return accountCompanyId;
        }
    }
}
