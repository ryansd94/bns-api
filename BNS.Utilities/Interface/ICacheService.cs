using System;
using static BNS.Utilities.Enums;

namespace BNS.Utilities.Interface
{
    public interface ICacheService
    {
        void AddToCache(string key, object value);
        void RemoveFromCache(string key);
        T GetToCache<T>(string key);
        string GetCacheKey(EControllerKey controllerKey, Guid? objectId = null, Guid? companyId = null);
        string GetCacheNotifyKey(string accountCompanyId);
    }
}
