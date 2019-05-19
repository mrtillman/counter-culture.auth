using System;

namespace CounterCulture.Models {
    public class AppSecrets {
        public string MySQLConnectionString { get; set; }
        public string Secret { get; set; }
        public string RedisConnectionString { get; set; }
    }
}