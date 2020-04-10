using System;

namespace Presentation.Services 
{
    public interface ICacheService
    {
        string Get (string key);
        bool Set(string key, string value);
        bool Delete(string key);
    }
}