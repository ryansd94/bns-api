using BNS.Domain.Interface;
using BNS.Utilities.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BNS.Service.Hubs
{
    public class ConnectionMapping<T> : IConnectionMapping<T>
    {
        private readonly ICacheService _cacheService;
        public ConnectionMapping(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public void Add(T key, string connectionId)
        {
            lock (_cacheService)
            {
                var cacheKey = _cacheService.GetCacheNotifyKey(key.ToString());
                var connections = _cacheService.GetToCache<HashSet<string>>(cacheKey);
                if (connections == null)
                {
                    connections = new HashSet<string>();
                }
                lock (connections)
                {
                    connections.Add(connectionId);
                    _cacheService.AddToCache(cacheKey, connections);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            var cacheKey = _cacheService.GetCacheNotifyKey(key.ToString());
            var cacheData = _cacheService.GetToCache<HashSet<string>>(cacheKey);
            if (cacheData != null)
            {
                return (HashSet<string>)cacheData;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_cacheService)
            {
                var cacheKey = _cacheService.GetCacheNotifyKey(key.ToString());
                var connections = _cacheService.GetToCache<HashSet<string>>(cacheKey);
                if (connections == null)
                {
                    return;
                }
                if (connections.Contains(connectionId))
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _cacheService.RemoveFromCache(cacheKey);
                        return;
                    }
                    _cacheService.AddToCache(cacheKey, connections);
                }
            }
        }
    }
}
