using System;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Services {
    public interface IOAuthService
    {
        AuthResponse Authenticate(OAuthClient client);
        OAuthClient GetClient(string client_id);
        OAuthClient RegisterClient(OAuthClient client);
    }
}