using System;
using CounterCulture.Utilities;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services
{
    public class AuthService : IAuthService
    {

      public AuthService(AppSecrets _appSecrets){
        appSecrets = _appSecrets;
      }

      private AppSecrets appSecrets { get; set; }

      public AuthResponse Authenticate(User user){
        if(user == null) return null;
        return JWTAuthenticator.Authenticate(user, appSecrets.Secret);
      }
      
    }
}