using System;
using StackExchange.Redis;
using CounterCulture.Services;

namespace CounterCulture.Services
{
    public class CacheService : ICacheService
    {
        public CacheService(ConnectionMultiplexer _redis)
        {
            redis = _redis;
            db = redis.GetDatabase();
        }
        
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase db;

        public string Get(string key) {
          return db.StringGet(key);
        }

        public bool Set(string key, string value){
          return db.StringSet(key, value);
        }

        public bool Delete(string key){
          return db.KeyDelete(key);
        }
    }
}
