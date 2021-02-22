using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ICacheService
    {
        public string GetCacheValue(string key);
        public Task<string> GetCacheValueAsync(string key);
        public void SetCacheValue(KeyValuePair<string, string> KeyValue, TimeSpan time);
        public Task SetCacheValueAsync(KeyValuePair<string, string> KeyValue, TimeSpan time);
    }
}