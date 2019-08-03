using System;
using System.Collections.Generic;

namespace CounterCulture.Constants
{
    public static class ServerUrls
    {
        public static Dictionary<ENV, string> API 
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://192.168.0.8:4000" },
                { ENV.PROD, "https://api.counter-culture.io"}
            };

        public static Dictionary<ENV, string> APP
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://192.168.0.8:8080" },
                { ENV.PROD, "https://counter-culture.io"}
            };

        public static Dictionary<ENV, string> DEV
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://192.168.0.8:9000" },
                { ENV.PROD, "https://geeks.counter-culture.io"}
            };

        public static Dictionary<ENV, string> SECURE 
            => new Dictionary<ENV, string> {
                { ENV.DEV, "http://192.168.0.8:5000" },
                { ENV.PROD, "https://secure.counter-culture.io"}
            };
    }
}