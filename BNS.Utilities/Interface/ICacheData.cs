using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Utilities.Interface
{
    public interface ICacheData
    {
        void AddToCache(string key, object value);
        void RemoveFromCache(string key);
        object GetToCache(string key);
    }
}
