using System;
using System.Collections.Generic;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Repositories
{
    public interface IOAuthRepository
    {
        bool IsDisconnected { get; }
        bool Save(OAuthClient client);
        OAuthClient Get(string client_id);
        OAuthClient Find(string client_id, string secret);
    }
}
