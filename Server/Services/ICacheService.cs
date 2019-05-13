using System;

namespace CounterCulture.Services 
{
    public interface ICacheService
    {
        string Get (string key);
        bool Set(string key, string value);
    }
}