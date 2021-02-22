using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess;
using StackExchange.Redis;

namespace DataService.CacheServices
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _Database;
        
        public CacheService(IConnectionMultiplexer redis)
        {
            _Database = redis.GetDatabase();
        }
        
        public string GetCacheValue(string key)
        {
            return _Database.StringGet(key);
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _Database.StringGetAsync(key);
        }

        public void SetCacheValue(KeyValuePair<string, string> KeyValue, TimeSpan time)
        { 
            _Database.StringSet(KeyValue.Key, KeyValue.Value, time);
        }

        public async Task SetCacheValueAsync(KeyValuePair<string, string> KeyValue, TimeSpan time)
        {
            await _Database.StringSetAsync(KeyValue.Key, KeyValue.Value, time);
        }
    }
}