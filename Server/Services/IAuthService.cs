using System;
using CounterCulture.Utilities;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services 
{
    public interface IAuthService
    {
       AuthResponse Authenticate(User user);
    }
}