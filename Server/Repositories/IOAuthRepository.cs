using System;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Repositories
{
    public interface IOAuthRepository
    {
        bool IsDisconnected { get; }
        bool SaveOAuthClient(OAuthClient client);
        OAuthClient GetOAuthClient(string clientId);
    }
}
