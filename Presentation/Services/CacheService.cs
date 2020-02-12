using System;
using StackExchange.Redis;
using Presentation.Services;

namespace Presentation.Services
{
    public class CacheService : ICacheService
    {
        public CacheService(IConnectionMultiplexer _redis)
        {
            redis = _redis;
            db = redis.GetDatabase();
        }
        
        private readonly IConnectionMultiplexer redis;
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
