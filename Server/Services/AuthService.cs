using System;
using CounterCulture.Utilities;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services
{
    public class AuthService : IAuthService
    {
      public AuthResponse Authenticate(User user){
            if(user == null) return null;
            return JWTAuthenticator.Authenticate(user, "_secrets.Secret");
      }
    }
}