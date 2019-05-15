using System;
using CounterCulture.Helpers;

namespace CounterCulture.Services 
{
    public interface IAuthService
    {
       AuthResponse Authenticate(User user);
    }
}